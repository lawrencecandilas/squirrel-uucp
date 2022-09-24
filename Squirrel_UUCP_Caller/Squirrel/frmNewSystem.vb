Imports System.ComponentModel.Design
Imports System.Threading

Public Class frmNewSystem
    'Needed to get config file paths.  Passed to us by New()
    Public conf As Object = Nothing
    'Current Builder in the panel.  The Builder is responsible for writing the
    'config files if the user clicks Add.  Therefore the Builder will need 
    'access to some info in Conf.
    Public Builder As Object = Nothing
    Public NeedRefresh As Boolean = False
    Public Sub New(in_conf As Object)
        DebugOut("frmNewSystem.New(in_conf)")
        ' This call is required by the designer.
        InitializeComponent()

        'Needs a handle to current configuration.
        conf = in_conf
    End Sub
    Public Function NewInviteDetailLine(in_type As String, in_description As String) As ListViewItem
        Dim lsv As New ListViewItem
        lsv.SubItems.Insert(0, New ListViewItem.ListViewSubItem With {.Text = in_type})
        lsv.SubItems.Insert(1, New ListViewItem.ListViewSubItem With {.Text = in_description})
        Return lsv
    End Function
    Public Sub PanelChange(in_text As String)
        btnInviteFile.Enabled = False
        lbxBuilders.Enabled = False
        Dim Panel As New frmNewSystem_NowEditing
        Panel1.Controls.Clear()
        Panel1.Controls.Add(Panel)
    End Sub

    Private Sub frmNewSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DebugOut("frmNewSystem.Load(...)")
        btnBuildSystem.Enabled = False
        btnCancel.Enabled = True

        lbxBuilders.Items.Add("[Select]")
        For Each Supported In AvailableTransports
            If AvailableBuilders.Contains(LCase(Supported.Key)) Then '
                lbxBuilders.Items.Add(Supported.Key)
            End If
        Next

        lbxBuilders.SelectedIndex = 0
        lbxBuilders_SelectedIndexChanged(lbxBuilders, EventArgs.Empty)
        btnBuildSystem.Enabled = False
    End Sub
    Private Sub lbxBuilders_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbxBuilders.SelectedIndexChanged
        'When the user selects a system type from the list, we dynamically 
        'load its builder.
        '
        'The builder gets a handle back to the listbox, so it can disable the
        'listbox once editing starts.
        '
        If lbxBuilders.SelectedIndex > 0 Then
            btnBuildSystem.Enabled = True
            Dim TransportName As String = lbxBuilders.SelectedItem.ToString
            pnlNewSystemBuilderObject.Controls.Clear()
            Builder = Activator.CreateInstance(AvailableBuilders(Trim(LCase(TransportName))),
                                               Me, conf.Winpathof_etcuucp, conf.Ports, conf.Systems)
        Else
            btnBuildSystem.Enabled = False
            pnlNewSystemBuilderObject.Controls.Clear()
            'ucoNewSystem_NoneSelected is a "blank" builder to just enable
            'something to be displayed for the default [Select] listbox item.
            Builder = New ucoBUILDER_NoneSelected
        End If
        pnlNewSystemBuilderObject.Controls.Add(Builder)
    End Sub

    Private Sub btnBuildSystem_Click(sender As Object, e As EventArgs) Handles btnBuildSystem.Click
        If Builder Is Nothing Then
            DebugOut("btnBuildSystem_Click called while Builder was Nothing")
            Exit Sub
        End If
        If lbxBuilders.SelectedIndex = 0 Then
            DebugOut("frmNewSystem.btnBuildSystem_Click called while lbxBuilders.SelectedIndex was 0 ")
            Exit Sub
        End If

        Dim NewSystem As New Hashtable

        'Ask builder to spit out a hashtable, bail if that can't be done for
        'some weird reason
        NewSystem = Builder.MakeFromUI
        If NewSystem("failed") Then
            MsgBox(NewSystem("whyfailed"), MsgBoxStyle.Critical, "Can't create New system because...")
            Exit Sub
        End If

        'Call builder's validator on the hashtable, bail if not valid
        Dim NewSystemProblems As String
        NewSystemProblems = Builder.CheckIfValid(NewSystem, 1)
        If NewSystemProblems <> "" Then
            MsgBox(NewSystemProblems, MsgBoxStyle.Critical, "Can't create new system because...")
            Exit Sub
        End If

        If Builder.EtchInFiles(NewSystem) Then
            NeedRefresh = True
            Me.Close()
        End If
    End Sub
    Private Sub frmNewSystem_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing

        'NeedRefresh will be set to true if a new system was successfully
        'added.  In that case, the btnBuildSystem likely called us and we can
        'just close.
        If NeedRefresh Then
            Builder.Dispose
            Exit Sub
        End If

        'Otherwise we do a little song and dance to make sure the user really
        'wants to exit, but only if they modified something.
        If Builder IsNot Nothing Then
            If Builder.Modified Then
                Dim a As MsgBoxResult = MsgBox("Discard new system information entered so far?", MsgBoxStyle.YesNo, "Backing out")
                If a = MsgBoxResult.Yes Then
                    Builder.Dispose
                Else
                    e.Cancel = True
                    Exit Sub
                End If
            Else
                Builder.Dispose
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Public Function MakeFromInvite(in_filename As String) As Hashtable
        DebugOut("frmNewSystem.MakeFromInvite(in_filename=" & Chr(34) & in_filename & Chr(34) & ")")
        'Opens specified file and reads in options, processing 'private-key' and
        ''end-private-key' appropriately.
        '
        'Returns a hashtable of options as keys set to their values, with
        'additional keys 'failed' and 'whyfailed' - which will be set if
        'something did go wrong.
        '
        Dim Failed As Boolean = False
        Dim WhyFailed As String = ""

        Dim NVPs As New Hashtable

        Dim SplitLine(2) As String

        Dim Mode As Integer = 0
        Dim LinesRead As Integer = 0
        Dim KeyText As String = ""

        Try
            DebugOut(" opening file...")
            Dim Reader0 As System.IO.StreamReader
            Reader0 = System.IO.File.OpenText(in_filename)

            Do
                If LinesRead > 100 Then
                    Failed = True : WhyFailed = "invite file has more than 100 lines, bailing out"
                    Exit Do
                End If
                If Reader0.EndOfStream Then Exit Do
                Dim FileLine As String = Trim(Reader0.ReadLine)
                DebugOut(" read line " & Chr(34) & FileLine & Chr(34))
                LinesRead += 1

                'skip blank lines
                If FileLine = "" Then Continue Do
                'skip unix-commented lines
                If Mid(FileLine, 1, 1) = "#" Then Continue Do

                SplitOnFirstSpace(FileLine, SplitLine)
                If FileLine = "private-key" Then
                    If KeyText <> "" Then
                        Failed = True : WhyFailed = "two private-key's in this file??? weird, rejecting invite file"
                        Exit Do
                    End If
                    If Mode = 1 Then
                        Failed = True : WhyFailed = "saw private-key unexpectedly - rejecting invite file"
                        Exit Do
                    End If
                    DebugOut(" private-key ...")
                    Mode = 1
                    Continue Do
                End If

                If FileLine = "end-private-key" Then
                    If Mode = 0 Then
                        Failed = True : WhyFailed = "saw end-private-key unexpectedly - rejecting invite file"
                        Exit Do
                    Else
                        DebugOut(" ... end-private-key")
                        NVPs("private-key-text") = KeyText
                        Mode = 0
                    End If
                    Continue Do
                End If

                If Mode = 0 Then
                    If NVPs.ContainsKey(LCase(SplitLine(1))) Then
                        Failed = True : WhyFailed = Chr(34) & SplitLine(1) & Chr(34) &
                                                    "appears twice in invite file - rejecting invite file"
                        Exit Do
                    End If
                    NVPs(LCase(SplitLine(1))) = SplitLine(2)
                End If

                If Mode = 1 Then
                    KeyText &= FileLine & vbLf
                End If
            Loop
            Reader0.Dispose()
        Catch
            Failed = True : WhyFailed = "error while reading invite file"
        End Try

        If Mode = 1 Then
            Failed = True : WhyFailed = "never encounted end-private-key - rejecting invite file"
        End If

        If Failed Then
            DebugOut(" " & WhyFailed)
            NVPs.Clear()
            NVPs("failed") = "true"
            NVPs("whyfailed") = WhyFailed
        Else
            NVPs("failed") = "false"
            NVPs("whyfailed") = "no problems reading file"
        End If

        MakeFromInvite = NVPs
    End Function

    Private Sub btnInviteFile_Click(sender As Object, e As EventArgs) Handles btnInviteFile.Click
        DebugOut("frmNewSystem.btnInviteFile_Click(...)")
        If Builder Is Nothing Then
            DebugOut("frmNewSystem.btnBuildSystem_Click called while Builder was Nothing")
            Exit Sub
        End If

        'Ask user for an invite file.
        If ofdInviteFile.ShowDialog = DialogResult.OK Then
            Dim NewSystem As New Hashtable
            'Ask inviter to spit out a hashtable, bail if that can't be done.
            NewSystem = MakeFromInvite(ofdInviteFile.FileName)
            If NewSystem("falied") Then
                MsgBox(NewSystem("whyfailed"), MsgBoxStyle.Critical, "Unable to process this invite file")
                Exit Sub
            End If

            'Need to load appropriate builder
            If Not NewSystem.Contains("transport-type") Then
                SquirrelComms.Item(22).SystemError("")
                Exit Sub
            End If

            If AvailableBuilders.Contains(Trim(LCase(NewSystem("transport-type")))) Then
                Builder = Activator.CreateInstance(AvailableBuilders(Trim(LCase(NewSystem("transport-type")))),
                                                   Me, conf.Winpathof_etcuucp, conf.Ports, conf.Systems)
            Else
                SquirrelComms.Item(23).SystemError("The transport type in the invite file was " & Chr(34) & NewSystem("transport-type") & Chr(34))
                Exit Sub
            End If

            'Call builder's validator on the hashtable, bail if not valid
            Dim NewSystemProblems As String
            NewSystemProblems = Builder.CheckIfValid(NewSystem)
            If NewSystemProblems <> "" Then
                MsgBox(NewSystemProblems, MsgBoxStyle.Critical, "Unable to process this invite file")
                Exit Sub
            End If

            'Confirm things that the invite file imposes on the UUCP system

            Dim Warner As New frmInviteWarner

            Dim n As String = NewSystem("system-name")
            Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                           ">>>",
                                           "This invitation is from " & n
                                           ))
            Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                           ">>>",
                                           n & " will take calls from you using " & NewSystem("transport-type")
                                           ))

            'expected-caller-nodename
            'TODO: Verify incoming nodename is valid and gag if it's not.
            If NewSystem.Contains("expected-caller-nodename") Then
                If NewSystem("expected-caller-nodename") <> conf.uuname Then
                    Warner.WantsToChangeNodename()
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      ">>>",
                                                      n & " wants your nodename to be " & NewSystem("expected-caller-nodename")
                                                      ))
                End If
            End If

            Warner.InviteDetails.Items.Add(NewInviteDetailLine("", ""))

            Dim SystemsToKnowPile As List(Of String) = New List(Of String)

            If NewSystem.Contains("forward-to-me") Then
                Warner.HasForwardingOptions()
                Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      ">>>",
                                                      n & " will carry/hold files for other systems"
                                                      ))
                Dim Preview As String = OptionListPreview(NewSystem("forward-to-me"))
                If Preview = "" Then
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "... but did not provide a list of those systems"
                                                      ))
                Else
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "... specifically: " & Preview
                                                      ))
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "(Above systems will be added as known systems)"
                                                      ))
                End If
                Warner.InviteDetails.Items.Add(NewInviteDetailLine("", ""))
            End If

            If NewSystem.Contains("please-forward-from-me") Then
                Warner.HasForwardingOptions()
                Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      ">>>",
                                                      n & " requests you carry/hold files for other systems"
                                                      ))
                Dim Preview As String = OptionListPreview(NewSystem("please-forward-from-me"))
                If Preview = "" Then
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      ">>>",
                                                      "... but did not provide a list of those systems"
                                                      ))
                Else
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "... specifically: " & Preview
                                                      ))
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "(Above systems will be added as known systems)"
                                                      ))
                    ThrowOnThePile(SystemsToKnowPile, NewSystem("please-forward-from-me"))
                End If
                Warner.InviteDetails.Items.Add(NewInviteDetailLine("", ""))
            End If

            Dim KeyFileName As String = RandStr() & ".key.txt"
            Dim KeyFileDest As String = Slashes(conf.Winpathof_etcuucp & "\" & KeyFileName)

            If NewSystem.ContainsKey("private-key-text") Then
                Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                  ">>>",
                                                  n & " provided you a private SSH key"
                                                  ))
                Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                  "",
                                                  "... which will be copied here: " & Chr(34) & KeyFileDest & Chr(34)
                                                  ))
            Else
                If NewSystem.ContainsKey("private-key-file") Then
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "This invite file expects an existing key file "
                                                      ))
                    Warner.InviteDetails.Items.Add(NewInviteDetailLine(
                                                      "",
                                                      "... and that key file is: " & Chr(34) & NewSystem("private-key-file") & Chr(34)
                                                      ))
                End If
            End If

            Warner.ShowDialog()
            If Not Warner.Accept Then
                Warner.Dispose()
                Exit Sub
            End If
            If Warner.IgnoreForwardingOpts Then
                If NewSystem.Contains("please-forward-from-me") Then NewSystem.Remove("please-forward-from-me")
                If NewSystem.Contains("forward-to-me") Then NewSystem.Remove("forward-to-me")
            End If
            If Warner.IgnoreNodename Then
                If NewSystem.Contains("expected-caller-nodename") Then NewSystem.Remove("expected-caller-nodename")
            End If
            Warner.Dispose()

            'Convert in-line key to file.
            'If an in-line key was defined, the hashtable maker would have
            'extracted the whole shebang into this item.
            If NewSystem.ContainsKey("private-key-text") Then
                Try
                    System.IO.File.WriteAllText(KeyFileDest,
                    NewSystem("private-key-text")
                    )
                    NewSystem.Add("private-key-file", KeyFileDest)
                Catch
                    SquirrelComms.Item(21).SystemError("")
                    Exit Sub
                End Try
            Else
                'But, if there is no in-line key, then 'private-key' needs to
                'be a defined option, and point to a file that exists.
                If Not NewSystem.ContainsKey("private-key-file") Then
                        Exit Sub
                    Else
                        If Not Validation_DoesFileExist(Slashes(NewSystem("private-key-file"))) Then
                            Exit Sub
                        End If
                    End If
                End If

                lbxBuilders.Enabled = False
                btnInviteFile.Enabled = False

            conf.AddKnownSystems = SystemsToKnowPile

            'Validation
            Dim ValidationResult As String = Builder.CheckIfValid(NewSystem)
            If ValidationResult <> "" Then
                MsgBox(ValidationResult, MsgBoxStyle.Critical, "Can't create New system because...")
                Exit Sub
            End If

            'All looks good, tell builder to commit it to files.
            'If builder is successful, we'll refresh the UI with the newly
            'updated config.
            If Builder.EtchInFiles(NewSystem, conf) Then
                    NeedRefresh = True
                    Me.Close()
                End If

            End If
    End Sub

End Class