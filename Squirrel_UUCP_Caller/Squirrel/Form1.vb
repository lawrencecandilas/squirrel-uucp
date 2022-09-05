Imports System.IO
Imports System.Reflection.Metadata
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Public Class Form1
    Public Conf As Object

    '-------------------------------------------------------------------------
    'Form1 Load - Initialization
    '-------------------------------------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        DebugOut("Form1_Load(...)")
        'Everything starts here.
        '
        'ConfigClasses.Config reads in UUCP config files during creation.
        '
        'Any problem with the Cygwin environment or UUCP configuration will
        'be detected upon instantiation, and reported through Conf.valid.

        Conf = New ConfigClasses.Config
        UISetupForConfValid(Conf.valid)
        If Not Conf.Valid Then
            Dim ConfigErrorReporter As New Form3
            For Each Thing In Conf.Problems
                ConfigErrorReporter.ListBox1.Items.Add(Thing.Body)
            Next
            ConfigErrorReporter.ShowDialog()
            ConfigErrorReporter.Dispose()
        End If
    End Sub
    Private Sub Form1_Close(sender As Object, e As EventArgs) Handles Me.FormClosing
        DebugOut("Form1_Close(...)")
        CurrentSettings.SaveSettings
    End Sub

    Private Sub UISetupForConfValid(in_validflag As Boolean)
        'Called by Form1_Load, and a few other things that modify the UUCP
        'config and then want to refresh the UI based on the updated config
        'state.
        '
        'This:
        '- enables or disables most UI elements according to whether the 
        'incoming boolean is true or false, which should be the Conf.Valid
        'flag.    
        '- also updates lsvConfigReport and a few items on the Cygwin tab
        '- is responsible for populating lsvConfigReport based on each item
        'in the CygwinChecks collection.

        lsvConfigReport.Items.Clear()
        For Each Thing In CygwinChecks
            Dim lsv As New ListViewItem
            If Thing.Pass.ToString Then
                lsv.SubItems.Insert(0, New ListViewItem.ListViewSubItem With {.Text = "OK"})
            Else
                lsv.SubItems.Insert(0, New ListViewItem.ListViewSubItem With {.Text = "Error"})
            End If
            lsv.SubItems.Insert(1, New ListViewItem.ListViewSubItem With {.Text = Thing.Desc})
            lsvConfigReport.Items.Insert(0, lsv)
        Next
        If Not Conf.SetRootFromConfig Then
            Dim lsv As New ListViewItem
            lsv.SubItems.Insert(0, New ListViewItem.ListViewSubItem With {.Text = "Active"})
            lsv.SubItems.Insert(1, New ListViewItem.ListViewSubItem With {.Text = "PORTABLE MODE"})
            lsvConfigReport.Items.Insert(0, lsv)
        End If

        tbxCygwinRoot.Text = Slashes(Conf.Winpathof_cygwinroot)
        lblUUCPNodeName.Text = Conf.uuname

        RefreshSystemsList()

        Dim Found As Boolean = False

        If lsvSystemsList.Items.Count > 0 Then
            If CurrentSettings.DEFAULTSYS <> "" Then
                For Each ListedSystem In lsvSystemsList.Items
                    If LCase(ListedSystem.Text) = LCase(CurrentSettings.DEFAULTSYS) Then
                        '
                        'What we are trying to achieve here:
                        '* If the DEFAULTSYS (from Settings) is found in the
                        'current list of systems (from UUCP config files),
                        'then we want to make it the currently selected system
                        'automatically.  
                        '* This UI is dynamic and changing the selected system
                        'triggers a few things.  So we need the normal VB
                        'event stuff happening.  For example, if we change a
                        'ListView's Selected property, it should trigger a 
                        'SelectedIndexChanged event and cause the sub handling
                        'that event to be called.
                        '* Well, apparently that doesn't happen for a ListView
                        'unless something references it's handle.
                        'https://social.msdn.microsoft.com/Forums/en-US/a1ee0914-bd59-430e-89a2-71af7a4715f0/programatic-set-of-listview-selecteditems-does-not-increment-count?forum=csharplanguage#:~:text=The%20reason%20the%20SelectItems%20does%20not%20reflect%20the,ListView.SelectedItems.%20The%20Remarks%20section%20touches%20on%20this%20issue.
                        Dim WTF As New Object
                        WTF = lsvSystemsList.Handle

                        lsvSystemsList.Select()

                        ListedSystem.Selected = True

                        'tbxSelectedSystemName.Text = ListedSystem.Text
                        lsvFilesToSend_SelectedIndexChanged(Me, EventArgs.Empty)
                        lsvSystemsList_SelectedIndexChanged(Me, EventArgs.Empty)
                        Found = True
                        Exit For
                    End If
                Next
            End If
        End If

        If Not Found Then
            CurrentSettings.DEFAULTSYS = ""
            CurrentSettings.SaveSettings()
            tbxSelectedSystemName.Text = "[No system selected]"
        End If

        UpdateBtnSetDefault()

        FileSystemWatcher1.EnableRaisingEvents = False
        If in_validflag Then
            lsvFilesToSend.Enabled = True
            lsvReceivedFiles.Enabled = True
            btnAddSystem.Enabled = True
            btnAddFile.Enabled = True
            tbxRecipient.Enabled = True
            tbxCallLog.Enabled = True
            tbxReceiveFolder.Enabled = True
            btnOpenExplorer.Enabled = True
            lblNodeName.Text = Conf.uuname
            If lsvSystemsList.SelectedItems.Count = 0 Then
                SquirrelComms.Item(6).SetTbxCallInfo(tbxCallInfo)
            End If
            If lsvSystemsList.SelectedItems.Count = 1 Then
                SquirrelComms.Item(7).SetTbxCallInfo(tbxCallInfo)
            End If

            RefreshFilesToSend()
            RefreshReceivedFiles()
            FileSystemWatcher1.Path = Conf.Winpathof_uucppublic
            FileSystemWatcher1.EnableRaisingEvents = True
        Else
            lsvFilesToSend.Items.Clear()
            lsvFilesToSend.Enabled = False
            lsvReceivedFiles.Items.Clear()
            lsvReceivedFiles.Enabled = False
            btnAddSystem.Enabled = False
            btnRemoveSystem.Enabled = False
            btnSetDefault.Enabled = False
            btnViewRawConfig.Enabled = False
            btnAddFile.Enabled = False
            btnRemoveFile.Enabled = False
            tbxRecipient.Enabled = False
            btnCall.Enabled = False
            SquirrelComms.Item(32).SetTbxCallInfo(tbxCallInfo)
            tbxCallLog.Clear()
            tbxCallLog.Enabled = False
            tbxReceiveFolder.Clear()
            tbxReceiveFolder.Enabled = False
            btnOpenExplorer.Enabled = False
            lblNodeName.Text = "[Config Problem - Double Click For Details]"
        End If

    End Sub

    '--- Form level ----------------------------------------------------------
    '--- UI stuff ------------------------------------------------------------
    Private Sub TabControl1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Resize
        'When the main form is resized (which will cause TabControl1 to be
        'resized), there are some columns in the various listviews that we
        'want to anchor to the right side.  THe following functions will do
        'that.
        lsvConfigReportResizer(lsvConfigReport, TabControl1.Width)
        lsvFilesToSendResizer(lsvFilesToSend, TabControl1.Width)
        lsvReceivedFilesResizer(lsvReceivedFiles, TabControl1.Width)
    End Sub

    '-------------------------------------------------------------------------
    'Cygwin Tab
    '-------------------------------------------------------------------------    

    '--- UI stuff ------------------------------------------------------------
    Private Sub TabPage1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpgCygwin.Enter
        lsvConfigReportResizer(lsvConfigReport, TabControl1.Width)
    End Sub
    Private Sub lsvConfigReportResizer(in_lsv As Object, in_width As Integer)
        'Called when window is resized.
        'Makes the 2nd column right edge hug the edge of the window.
        '
        If in_lsv.Columns.Count = 2 Then
            in_lsv.Columns.Item(1).Width = in_width - 75
        End If
    End Sub

    '--- Real stuff ----------------------------------------------------------
    Private Sub btnRecheckConfig_Click(sender As Object, e As EventArgs) Handles btnRecheckConfig.Click
        DebugOut("btnRecheckConfig_Click(...)")
        'Recheck environment, refresh UI, and tell the user.
        '
        'UISetupForCOnfValid() will update lsvConfigReport, which tells the
        'user the status of the environment.
        '
        CygwinEnvironmentCheck(Conf)
        UISetupForConfValid(Conf.Valid)
        SquirrelComms.Item(24).UserNotice()
    End Sub

    Private Sub btnOpenConfigFolder_Click(sender As Object, e As EventArgs) Handles btnOpenConfigFolder.Click
        DebugOut("btnOpenConfigFolder_Click(...)")
        DebugOut(" warning user about modifying files while this is running")
        'Open explorer.exe on the UUCP config folder, if the user accepts the
        'disclaimer.
        '
        Dim Result As MsgBoxResult
        Result = MsgBox(
            "Opening or modifying UUCP configuration files while the application is running may cause the application to refresh or close." & vbCrLf &
            "Proceed?",
            MsgBoxStyle.YesNo,
            "Open UUCP Configuration Folder")
        If Result = MsgBoxResult.Yes Then
            DebugOut(" user said OK to proceed")
            Process.Start("explorer.exe", Slashes(Conf.Winpathof_etcuucp))
        Else
            DebugOut(" user backed out")
        End If
    End Sub

    Private Sub btnOpenCygwinShell_Click(sender As Object, e As EventArgs) Handles btnOpenCygwinShell.Click
        DebugOut("btnOpenCygwinShell_Click(...)")
        'Pop open the Cygwin shell in a console window.
        '
        Process.Start(Slashes(Conf.Winpathof_cygwinroot & "\Cygwin.bat"))
    End Sub
    Private Sub btnChangeNodename_Click(sender As Object, e As EventArgs) Handles btnChangeNodename.Click
        DebugOut("btnChangeNodename_Click(...)")
        DebugOut(" asking user for a new nodename")
        Dim Dialog0 As New UUCPNodenameChange
        UUCPNodenameChange.ShowDialog()
        'If the user didn't cooperate ...
        If Not UUCPNodenameChange.NewNameSet Then
            '... then that's a problem.
            DebugOut(" user backed out")
            Exit Sub
        Else
            '...otherwise make the file,
            DebugOut(" user provided a new nodename, making new config file")
            'more specifically, try to make the file...
            If Not NewUUCPConfigFile(Conf.Winpathof_etcuucp & "\config",
                                     UUCPNodenameChange.tbxNewUUCPNodename.Text) Then
                'if something goes wrong while making the file. :O
                'TODO
            End If
        End If
        'and we don't need that form for that dialog anymore.
        UUCPNodenameChange.Dispose()

        Conf.Refresh()
        UISetupForConfValid(Conf.valid)
    End Sub
    Private Sub btnChangeCygwinRoot_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DebugOut("btnChangeCygwinRoot(...)")
        DebugOut(" asking user for new path")
        'This is the "Change..." button for the Cygwin root.
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            DebugOut(" new path is " & Chr(34) & FolderBrowserDialog1.SelectedPath & Chr(34))
            CurrentSettings.CYGROOT = FolderBrowserDialog1.SelectedPath
            CurrentSettings.SaveSettings()
            DebugOut(" telling user a restart is needed")
            SquirrelComms.Item(31).UserNotice()
        Else
            DebugOut(" user backed out")
        End If
    End Sub


    '-------------------------------------------------------------------------
    'Systems Tab
    '-------------------------------------------------------------------------    

    '--- UI stuff ------------------------------------------------------------
    Private Sub lsvSystemsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsvSystemsList.SelectedIndexChanged
        'When the listview containing our list of systems experiences a
        'selected item change, a lot happens - because we update the panel
        'that shows the summary of the system's configuration.
        '
        'Other UI elements also have to be updated to reflect the currently
        'selected system.
        '
        'Reset "View Raw Config" button
        btnViewRawConfig.Text = "View Raw Config"

        'Update Set Default button
        UpdateBtnSetDefault()

        'Clear the "show system" panel.
        pnlShowSystem.Controls.Clear()

        If lsvSystemsList.SelectedItems.Count = 0 Then
            chkForwardFrom.Checked = False
            chkForwardFrom.Enabled = False
            chkForwardTo.Checked = False
            chkForwardTo.Enabled = False
            btnCall.Enabled = False
            btnRemoveSystem.Enabled = False
            SquirrelComms.Item(6).SetTbxCallInfo(tbxCallInfo)
            tbxSelectedSystemName.Text = "[No system selected]"
            Exit Sub
        End If

        'Is something other than a single system selected?
        'If so, leave blank.
        If lsvSystemsList.SelectedItems.Count <> 1 Then
            chkForwardFrom.Checked = False
            chkForwardFrom.Enabled = False
            chkForwardTo.Checked = False
            chkForwardTo.Enabled = False
            btnCall.Enabled = False
            SquirrelComms.Item(8).SetTbxCallInfo(tbxCallInfo)
            tbxSelectedSystemName.Text = "[Multiple systems selected]"
            Exit Sub
        End If

        'Get index of selected system.
        Dim selectedSystemName As String =
            lsvSystemsList.SelectedItems(0).Text

        If Conf.systems.Contains(selectedSystemName) Then
            pnlShowSystemUpdate(selectedSystemName)
            chkForwardFrom.Checked = Conf.systems.Item(selectedSystemName).ForwardFrom
            chkForwardFrom.Enabled = True
            chkForwardTo.Checked = Conf.systems.Item(selectedSystemName).ForwardTo
            chkForwardTo.Enabled = True
            btnRemoveSystem.Enabled = True
            btnCall.Enabled = True
            SquirrelComms.Item(7).SetTbxCallInfo(tbxCallInfo)
            tbxSelectedSystemName.Text = selectedSystemName
        End If

    End Sub
    Public Sub RefreshSystemsList()
        'Reads UUCP port and sys files and populates lsvSystemsList
        'accordingly.
        '(Clears list before population, of course.)
        '
        'Also:
        '- Updates some related UI elements based on the systems list content.
        '
        lsvSystemsList.Enabled = False
        lsvSystemsList.Items.Clear()
        For Each UUCPSystem In Conf.Systems
            lsvSystemsList.Items.Add(UUCPSystem.Name)
        Next
        If lsvSystemsList.Items.Count = 0 Then
            'Related UI elements
            '-----------------------------------------------------------------
            chkForwardFrom.Checked = False
            chkForwardFrom.Enabled = False
            chkForwardTo.Enabled = False
            chkForwardTo.Enabled = False
            btnRemoveSystem.Enabled = False
            btnCall.Enabled = False
            tbxCallInfo.Text = SquirrelComms.Item(15).Body
            Dim panel As New ucoNoSystemsNotice
            pnlShowSystem.Controls.Clear()
            pnlShowSystem.Controls.Add(panel)
            '-----------------------------------------------------------------
        Else
            lsvSystemsList.Enabled = True
        End If
    End Sub
    Private Sub pnlShowSystemUpdate(in_selectedSystemName As String)
        'This will load the correct "viewer" user control object for the
        'selected system's transport type.  The user control object loaded
        'will provide a summary view of the system's configuration.
        '
        'If the transport type is unknown then we simply load the user control
        'object that displays the raw configuration.
        '
        Select Case Conf.systems.Item(in_selectedSystemName).Port.TransportObject.TypeName
            Case "KnownSystem"
                Dim panel As New ucoKnownSystem
                panel.SetFields(in_selectedSystemName)
                pnlShowSystem.Controls.Clear()
                pnlShowSystem.Controls.Add(panel)
                PopulateTbxRecipient(Conf.Systems.Item(in_selectedSystemName).Name)
                btnViewRawConfig.Enabled = True
            Case "SSHTransport"
                Dim selectedSSHServer As String =
                 Conf.Systems.Item(in_selectedSystemName).Port.TransportObject.SSHServer
                Dim panel As New ucoSSHTransport
                panel.SetFields(in_selectedSystemName,
                        Conf.Systems.Item(in_selectedSystemName).Port.Name,
                        Conf.Systems.Item(in_selectedSystemName).Port.TransportObject.SSHServer,
                        Conf.Systems.Item(in_selectedSystemName).Port.TransportObject.SSHKeyPath,
                        Conf.Systems.Item(in_selectedSystemName).Port.TransportObject.SSHLoginName,
                        Conf.Systems.Item(in_selectedSystemName).uucicoUsername
                        )
                pnlShowSystem.Controls.Clear()
                pnlShowSystem.Controls.Add(panel)
                PopulateTbxRecipient(Conf.Systems.Item(in_selectedSystemName).Name)
                btnViewRawConfig.Enabled = True
            Case Else
                'Unknown system - load user control object that shows raw config.
                pnlShowSystemUpdate_Generic(in_selectedSystemName)
                'No need to enable "View Raw Config" button if that's what we
                'are already displaying.
                btnViewRawConfig.Enabled = False
        End Select
    End Sub

    Private Sub pnlShowSystemUpdate_Generic(in_selectedSystemName As String)
        'Code that loads the user control object that displays the raw
        'configuration text is factored out into its own function so we can
        'reuse it when the "View Raw Config" button is pressed.
        '
        Dim panel As New ucoOtherTransport
        panel.tbxConfigText.AppendText("==== PORT ====" & vbCrLf)
        For Each x As String In Conf.systems.Item(in_selectedSystemName).Port.port_conflines
            panel.tbxConfigText.AppendText(x & vbCrLf)
        Next
        panel.tbxConfigText.AppendText(vbCrLf)
        panel.tbxConfigText.AppendText("==== SYSTEM ====" & vbCrLf)
        For Each x As String In Conf.systems.Item(in_selectedSystemName).sys_conflines
            panel.tbxConfigText.AppendText(x & vbCrLf)
        Next
        pnlShowSystem.Controls.Clear()
        pnlShowSystem.Controls.Add(panel)
    End Sub
    Private Sub UpdateBtnSetDefault()
        'Update "Set Default" button's appearance and state.
        '
        'The text of the button change to "Remove Default" if the currently
        'selected system is the default system, and otherwise it says "Set
        'Default."
        '
        If lsvSystemsList.SelectedItems.Count <> 1 Then
            btnSetDefault.Enabled = False
            btnSetDefault.Text = "Set Default"
            Exit Sub
        End If
        btnSetDefault.Enabled = True
        If CurrentSettings.DEFAULTSYS = lsvSystemsList.SelectedItems.Item(0).Text Then
            btnSetDefault.Text = "Remove Default"
        Else
            btnSetDefault.Text = "Set Default"
        End If
    End Sub
    Public Sub PopulateTbxRecipient(in_systemName As String)
        'Initialize "Recipient" textbox under Systems tab with a proper UUCP
        'destination.  User can edit to send to specific user or hop to other
        'system.
        '
        tbxRecipient.Text = in_systemName & "!"
    End Sub
    Public Function ValidateTbxRecipient(in_recipient) As Integer
        'Validator for "Recipient" textbox.
        'Returns:
        ' 0 = OK.
        ' 1 = System specified without recipient.
        ' 2 = Not OK
        If Trim(in_recipient) = "" Then ValidateTbxRecipient = 2 : Exit Function
        ValidateTbxRecipient = 0
    End Function
    Private Sub btnViewRawConfig_Click(sender As Object, e As EventArgs) Handles btnViewRawConfig.Click
        'Do nothing if something other than a single system is selected.
        If lsvSystemsList.SelectedItems.Count <> 1 Then Exit Sub

        'Get index of selected system.
        Dim selectedSystemName As String =
            lsvSystemsList.SelectedItems(0).Text

        'Use pnlShowSystemUpdate_Generic to render raw text of config file.
        If Conf.systems.Contains(selectedSystemName) Then
            pnlShowSystem.Controls.Clear()
            If btnViewRawConfig.Text = "View Raw Config" Then
                pnlShowSystemUpdate_Generic(selectedSystemName)
                btnViewRawConfig.Text = "View Summary"
            Else
                pnlShowSystemUpdate(selectedSystemName)
                btnViewRawConfig.Text = "View Raw Config"
            End If
        End If
    End Sub

    '--- Real stuff ----------------------------------------------------------

    Private Sub chkForwardTo_CheckedChanged(sender As Object, e As EventArgs) Handles chkForwardTo.CheckedChanged
        DebugOut("chkForwardTo_CheckedChanged(...)")
        'Do nothing if something other than a single system is selected.
        If lsvSystemsList.SelectedItems.Count <> 1 Then Exit Sub

        'Get index of selected system.
        Dim selectedSystemName As String =
            lsvSystemsList.SelectedItems(0).Text

        Conf.ModifyForwardTo(chkForwardTo.Checked, selectedSystemName)
    End Sub
    Private Sub chkForwardFrom_CheckedChanged(sender As Object, e As EventArgs) Handles chkForwardFrom.CheckedChanged
        DebugOut("chkForwardFrom_CheckedChanged(...)")
        'Do nothing if something other than a single system is selected.
        If lsvSystemsList.SelectedItems.Count <> 1 Then Exit Sub

        'Get index of selected system.
        Dim selectedSystemName As String =
            lsvSystemsList.SelectedItems(0).Text

        Conf.ModifyForwardFrom(chkForwardFrom.Checked, selectedSystemName)
    End Sub
    Private Sub btnAddSystem_Click(sender As Object, e As EventArgs) Handles btnAddSystem.Click
        DebugOut("btnAddSystem_Click(...)")
        'frmNewSystem needs to be passed a pointer to Conf.
        '
        'Reason: I decided to make the user control objects that frmNewSystem
        'might instantiate responsible for updating config files.
        '
        'So the config file paths in Conf needs to trickle over to them
        '
        DebugOut(" presenting new system dialog to user ...")
        Dim Dialog0 As New frmNewSystem(Conf)
        Dialog0.pnlNewSystemBuilderObject.Controls.Clear()
        'Let frmNewSystem do this.
        'Dialog0.pnlNewSystemBuilderObject.Controls.Add(New ucoNewSystem_NoneSelected)
        Dialog0.ShowDialog()
        DebugOut(" new system dialog closed")
        If Dialog0.NeedRefresh Then
            DebugOut(" refreshing UI")
            Conf.Refresh()
            UISetupForConfValid(Conf.valid)
        End If
        Dialog0.Dispose()
    End Sub

    Private Sub btnRemoveSystem_Click(sender As Object, e As EventArgs) Handles btnRemoveSystem.Click
        DebugOut("btnRemoveSystem_Click(...)")
        'Remove selected system from sys, Port, and call files.
        'Then refresh the UI to reflect the change.
        ' 
        'Of course this is only actually done if a system is actually
        'selected.
        'We also account for the possibily that multiple systems were
        'selected - we'll do nothing but an exit in that case 
        '
        If lsvSystemsList.SelectedIndices.Count <> 1 Then
            'we don't handle this yet
            DebugOut(" btnRemoveSystemClick called while there wasn't exactly one selected system")
            Exit Sub
        End If

        Dim SplitLine(2) As String
        Dim portname As String = ""

        Dim FileLine As String = ""
        Dim Buffer As String = ""
        Dim Buffer0 As String = ""
        Dim DoNotWant As Boolean = False

        'We delete stuff from file by reading the entire file into a buffer,
        'skipping over what we want to delete, then writing a new file
        '(backing old file)

        'Remove system out of sys.
        'Note port of system while we are at it
        Dim SSHKeyPath As String = ""

        DebugOut(" preparing to rewrite sys file, looking for associated port ...")
        Dim Reader0 As System.IO.StreamReader
        Reader0 = System.IO.File.OpenText(Slashes(Conf.Winpathof_etcuucp & "\sys"))

        Do Until Reader0.EndOfStream
            FileLine = Reader0.ReadLine
            SplitOnFirstSpace(FileLine, SplitLine)

            If LCase(SplitLine(1)) = "system" Then
                If LCase(SplitLine(2)) = lsvSystemsList.SelectedItems.Item(0).Text Then
                    DoNotWant = True
                Else
                    DoNotWant = False
                End If
            End If

            If DoNotWant Then
                If LCase(SplitLine(1)) = "port" Then
                    DebugOut(" port is " & Chr(34) & portname & Chr(34))
                    portname = SplitLine(2)
                End If
            End If

            If Not DoNotWant Then
                Buffer0 &= FileLine & vbLf
            End If
        Loop
        Reader0.Dispose()

        'Because we want to ask the user if it's OK to delete the key file 
        'before doing any destructive action, AND we won't know the key file
        'name until we look in the Port file, we don't do any actual
        'configuration file rewriting yet.

        'Remove port out of Port file.
        DebugOut(" preparing to rewrite port file, looking for any key file references ...")
        Dim Reader1 As System.IO.StreamReader
        Reader1 = System.IO.File.OpenText(Slashes(Conf.Winpathof_etcuucp & "\Port"))

        Do Until Reader1.EndOfStream
            FileLine = Reader1.ReadLine
            SplitOnFirstSpace(FileLine, SplitLine)

            If LCase(SplitLine(1)) = "port" Then
                If LCase(SplitLine(2)) = portname Then
                    DoNotWant = True
                Else
                    DoNotWant = False
                End If
            End If

            'If we found a line beginning with 'command', and we are within
            'the part of the file we don't want (flag is set when we encounter
            'the name of the port we want to remove), then we'll search for
            'key file references.
            If LCase(SplitLine(1)) = "command" And DoNotWant = False Then
                '-------------------------------------------------------------
                DebugOut(" found a 'command' configuration line, looking for reference to a key file ...")
                Dim cstart As Integer = 1
                Dim cend As Integer = Len(SplitLine(2))
                Dim ctoken As String = ""
                Dim cnextflag As Integer = 0
                Dim endflag As Boolean = False
                Do
                    cend = InStr(cstart, SplitLine(2), " ") - 1
                    If cend = -1 Then
                        cend = Len(SplitLine(2))
                        endflag = True
                    End If
                    ctoken = Mid(SplitLine(2), cstart, (cend - cstart) + 1)
                    cstart = cend + 2
                    DebugOut(">>> token '" & ctoken & "'")
                    Select Case ctoken
                        Case "-i" : cnextflag = 100 : Continue Do
                    End Select
                    Select Case cnextflag
                        Case 100 '-i option for SSH key path
                            cnextflag = 0
                            If SSHKeyPath <> "" Then
                                DebugOut(" looks like there are multiple '-i' options for some weird reason")
                                DebugOut(" not going to offer to delete any file for the user")
                                SSHKeyPath = ""
                                Exit Do
                            End If
                            SSHKeyPath = ctoken
                            DebugOut(" found reference to key file " & Chr(34) & SSHKeyPath & Chr(34))
                    End Select
                    cnextflag = 0
                Loop Until endflag
                '-------------------------------------------------------------
            End If

            If Not DoNotWant Then
                Buffer &= FileLine & vbLf
            End If
        Loop
        Reader1.Dispose()

        'If we found a reference to a key file, offer to delete it for the
        'user.
        If SSHKeyPath <> "" Then
            Dim Result As MsgBoxResult = MsgBox(
                                                "The system you want removed references the following key file:" & vbCrLf & vbCrLf &
                                                Chr(34) & Cyg2WinPath(SSHKeyPath, Conf.Winpathof_cygwinroot) & Chr(34) & vbCrLf & vbCrLf &
                                                "YES -- Remove key file and system" & vbCrLf &
                                                "NO -- Keep key file, remove system" & vbCrLf &
                                                "Cancel -- Don't remove keyfile or system",
                                                MsgBoxStyle.YesNoCancel,
                                                "Delete key file?")
            If Result = MsgBoxResult.Cancel Then Exit Sub
            If Result = MsgBoxResult.Yes Then
                Try
                    System.IO.File.Delete(Cyg2WinPath(SSHKeyPath, Conf.Winpathof_cygwinroot))
                    DebugOut(" deleted " & Chr(34) & Cyg2WinPath(SSHKeyPath, Conf.Winpathof_cygwinroot) & Chr(34))
                Catch
                    SquirrelComms.Item(26).SystemError(Cyg2WinPath(SSHKeyPath, Conf.Winpathof_cygwinroot))
                End Try
            End If
        End If

        'Now we'll rewrite our sys file minus the system we want to delete.
        DebugOut(" going to rewrite sys file now ...")
        System.IO.File.Move(Slashes(Conf.Winpathof_etcuucp & "\sys"), Slashes(Conf.Winpathof_etcuucp & "\sys.bak"), True)
        DebugOut(" old sys file backed up")
        System.IO.File.Delete(Slashes(Conf.Winpathof_etcuucp & "\sys"))
        System.IO.File.WriteAllText(Slashes(Conf.Winpathof_etcuucp & "\sys"), Buffer0)

        DebugOut(" going to rewrite port file now")
        'Same with the Port file - rewrite entire file, omitting port
        'referenced by the system user wants to remove.
        System.IO.File.Move(Slashes(Conf.Winpathof_etcuucp & "\Port"), Slashes(Conf.Winpathof_etcuucp & "\Port.bak"), True)
        DebugOut(" old Port file backed up")
        System.IO.File.Delete(Slashes(Conf.Winpathof_etcuucp & "\Port"))
        System.IO.File.WriteAllText(Slashes(Conf.Winpathof_etcuucp & "\Port"), Buffer)

        Buffer = ""

        'Remove system line out of call file
        DebugOut(" preparing to rewrite call file ...")
        Dim Reader2 As System.IO.StreamReader
        Reader2 = System.IO.File.OpenText(Slashes(Conf.Winpathof_etcuucp & "\call"))

        Do Until Reader2.EndOfStream
            FileLine = Reader2.ReadLine
            SplitOnFirstSpace(FileLine, SplitLine)

            If LCase(SplitLine(1)) = lsvSystemsList.SelectedItems.Item(0).Text Then
                Continue Do
            End If

            Buffer &= FileLine & vbLf
        Loop
        Reader2.Dispose()

        DebugOut(" going to rewrite call file now ...")
        System.IO.File.Move(Slashes(Conf.Winpathof_etcuucp & "\call"), Slashes(Conf.Winpathof_etcuucp & "\call.bak"), True)
        DebugOut(" old call file backed up ...")
        System.IO.File.Delete(Slashes(Conf.Winpathof_etcuucp & "\call"))
        System.IO.File.WriteAllText(Slashes(Conf.Winpathof_etcuucp & "\call"), Buffer)

        DebugOut(" system is now removed")

        Conf.Refresh()
        UISetupForConfValid(Conf.valid)
    End Sub
    Private Sub btnSetDefault_Click(sender As Object, e As EventArgs) Handles btnSetDefault.Click
        DebugOut("btnSetDefault_Click(...)")
        'Make currently selected system the default system.
        'Unless it's already the default system - then make nothing the
        'default
        '
        'Of course this is only actually done if a system is actually
        'selected.
        'We also account for the possibily that multiple systems were
        'selected - we'll do nothing an exit in that case too.
        If lsvSystemsList.SelectedItems.Count <> 1 Then
            DebugOut(" btnSetDefault_Click called while there wasn't exactly one selected system")
            'we don't handle this yet
            Exit Sub
        End If

        'Default system is saved in CurrentSettings.DEFAULT 
        If CurrentSettings.DEFAULTSYS = lsvSystemsList.SelectedItems.Item(0).Text Then
            CurrentSettings.DEFAULTSYS = ""
            DebugOut(" default system cleared")
        Else
            CurrentSettings.DEFAULTSYS = lsvSystemsList.SelectedItems.Item(0).Text
            DebugOut(" default system is now " & Chr(34) & lsvSystemsList.SelectedItems.Item(0).Text & Chr(34))
        End If
        UpdateBtnSetDefault()
    End Sub

    '-------------------------------------------------------------------------
    'Outbox Tab
    '-------------------------------------------------------------------------    

    '--- UI stuff ------------------------------------------------------------

    Private Sub TabPage2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpgOutbox.Enter
        lsvFilesToSendResizer(lsvFilesToSend, TabControl1.Width)
    End Sub
    Private Sub lsvFilesToSendResizer(in_lsv As Object, in_width As Integer)
        If in_lsv.Columns.Count = 5 Then
            Dim First3 As Integer =
                in_lsv.Columns.Item(0).Width +
                in_lsv.Columns.Item(1).Width +
                in_lsv.Columns.Item(2).Width
            Dim Second As Integer = in_width - First3
            If Second < 85 Then Exit Sub
            in_lsv.Columns.Item(4).Width = 75
            in_lsv.Columns.Item(3).Width = Second - 75
        End If
    End Sub
    Private Sub lsvFilesToSend_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lsvFilesToSend.SelectedIndexChanged
        'When the number of selected items is changed in lsvFilesToSend listview,
        'we want the button to update it's text in response.
        '
        UpdateBtnRemoveFiles()
    End Sub
    Private Sub lsvReceivedFilesResizer(in_lsv As Object, in_width As Integer)
        If in_lsv.Columns.Count <> 0 Then
            in_lsv.Columns.Item(0).Width = in_width
        End If
    End Sub
    Public Sub RefreshFilesToSend()
        'Runs uustat and populates lsvFilesToSend accordingly.
        '(Clears list before population accordingly.)
        '
        'Also:
        '- Updates the Remove Files button ( UpdateBtnRemoveFiles() )
        '
        lsvFilesToSend.Enabled = False
        lsvFilesToSend.Items.Clear()
        Dim lsvFilesToSendPopulator = New FilesToSendPopulator(Conf, lsvFilesToSend)
        lsvFilesToSend.SelectedIndices.Clear()
        UpdateBtnRemoveFiles()
        lsvFilesToSend.Enabled = True
        lsvFilesToSendResizer(lsvFilesToSend, TabControl1.Width)
        UpdateBtnRemoveFiles()
    End Sub
    Public Sub RefreshFilesToSend_NoClear()
        'Runs uustat and sets file send list accordingly.
        'Doesn't clear list.  Used when adding a new file.
        lsvFilesToSend.Enabled = False
        'lsvFilesToSend.Items.Clear()
        Dim lsvFilesToSendPopulator = New FilesToSendPopulator(Conf, lsvFilesToSend)
        'lsvFilesToSend.SelectedIndices.Clear()
        UpdateBtnRemoveFiles()
        lsvFilesToSend.Enabled = True
        lsvFilesToSendResizer(lsvFilesToSend, TabControl1.Width)
    End Sub
    Private Class FilesToSendPopulator
        Private ReadOnly conf As Object
        Private valid As Boolean = False
        Private ReadOnly listviewToPopulate As Object
        Private ReadOnly listViewItems As New Collection
        Public Sub New(in_conf As Object, in_listview As Object)
            conf = in_conf
            listviewToPopulate = in_listview
            listviewToPopulate.Enabled = False
            CommandConnector(conf.Winpathof_cygwinroot & "\bin",
                             conf.Winpathof_uustat,
                             "--config " & Win2CygPath(conf.Winpathof_etcuucp) & "\config --all",
                             Me)
        End Sub
        Public Sub CommandIsDone()

            listviewToPopulate.Invoke(Sub()
                                          listviewToPopulate.Enabled = True
                                      End Sub)
            valid = True
        End Sub
        Public Sub EatStdoutLine(in_outputLine As String)
            Dim uustatItems(5) As String
            uustatSplit(in_outputLine, uustatItems, conf.Winpathof_cygwinroot)

            listviewToPopulate.Invoke(Sub()
                                          For Each t In listviewToPopulate.Items
                                              If t.Text = uustatItems(1) Then Exit Sub
                                          Next

                                          Dim lvi As New ListViewItem
                                          lvi.Text = uustatItems(1)
                                          For i = 2 To 5
                                              lvi.SubItems.Add(uustatItems(i))
                                          Next

                                          listviewToPopulate.Items.Insert(0, lvi)
                                      End Sub)

        End Sub
        Public Sub EatStderrLine(in_outputLine As String)

        End Sub
    End Class
    Private Sub UpdateBtnRemoveFiles()
        'Button text changes according to number of files that will be deleted.        
        If lsvFilesToSend.SelectedIndices.Count = 1 Then
            btnRemoveFile.Text = "Remove File"
            btnRemoveFile.Enabled = True
            Exit Sub
        ElseIf lsvFilesToSend.SelectedIndices.Count = 0 Then
            btnRemoveFile.Text = "Remove Files"
            btnRemoveFile.Enabled = False
            Exit Sub
        ElseIf lsvFilesToSend.SelectedIndices.Count = lsvFilesToSend.Items.Count Then
            btnRemoveFile.Text = "Remove All Files"
            btnRemoveFile.Enabled = True
            Exit Sub
        Else
            btnRemoveFile.Text = "Remove " & lsvFilesToSend.SelectedIndices.Count & " Files"
            btnRemoveFile.Enabled = True
        End If
    End Sub

    '--- Real stuff ----------------------------------------------------------
    Private Sub btnAddFile_Click(sender As Object, e As EventArgs) Handles btnAddFile.Click

        'Can't add file if no system selected.
        '
        If lsvSystemsList.SelectedItems.Count = 0 Then
            SquirrelComms.Item(0).UserError()
            Exit Sub
        End If

        Dim i As Integer = InStr(tbxRecipient.Text, "!")

        'If recipient doesn't have a ! in it, then that's a problem.
        '
        If i = -1 Then
            SquirrelComms.Item(1).UserError()
            Exit Sub
        End If

        'The text to the left of the ! in a UUCP bang path is the system you
        'are contacting.  If the user changed it to something else, slap
        'their hand.  
        '
        'This might be considered bad UI but it's an educational opportunity,
        'after all our user may eventually graduate to real UUCP tools.
        '
        Dim s1 As String = lsvSystemsList.SelectedItems(0).Text
        Dim s2 As String = tbxRecipient.Text
        If Microsoft.VisualBasic.Strings.Left(s1, i) <> Microsoft.VisualBasic.Strings.Left(s2, i - 1) Then
            SquirrelComms.Item(2).UserError()
            tbxRecipient.Text = s1 & "!"
            Exit Sub
        End If

        'I mean we do need a recipient specified in the first place, too.
        '
        If tbxRecipient.Text = "" Then
            SquirrelComms.Item(4).UserError()
            Exit Sub
        End If

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            'Let uuto (actually uucp with --uuto switch) command in Cygwin do the
            'real work.
            '
            Dim cmdresult As New Collection
            Dim cmdparams As String =
                    "--config " & Win2CygPath(Conf.Winpathof_etcuucp) & "\config " &
                    "--uuto --jobid --nocopy --noexpand --nouucico " &
                    "'" & Win2CygPath(OpenFileDialog1.FileName) & "' " &
                    "'" & Trim(tbxRecipient.Text) & "'"

            cmdresult = ExecuteCommand(Conf.Winpathof_uuto, cmdparams)

            If cmdresult.Item(1) = 0 Then
                RefreshFilesToSend_NoClear()
            Else
                SquirrelComms.Item(3).CommandError(Conf.Winpathof_uuto & " " & cmdparams, cmdresult.Item(3))
            End If

        End If

    End Sub
    Private Sub btnRemoveFile_Click(sender As Object, e As EventArgs) Handles btnRemoveFile.Click
        'The Remove Files button should be DISABLED by UpdateBtnRemoveFiles()
        'when the number of selected items in lsvFilesToSend is 0, but just in 
        'case, we'll check here.
        '
        If lsvFilesToSend.SelectedIndices.Count = 0 Then Return

        'Ask uustat in Cygwin to delete the UUCP job ID.
        'UUCP job IDs are stored in SubItems(0) in lsvFilesToSend list view.
        '
        Dim ErrorFlag As Boolean = False
        For Each lsv As ListViewItem In lsvFilesToSend.SelectedItems
            Dim cmdresult As New Collection
            cmdresult = ExecuteCommand(
                Conf.Winpathof_uustat,
                "--config /etc/uucp/config --kill '" & lsv.SubItems(0).Text)
            If cmdresult.Item(1) = 0 Then
                lsvFilesToSend.Items.Remove(lsv)
            Else
                ErrorFlag = True
            End If

            'If uustat failed, refresh the file list, because it might not be
            'in sync anymore.
            ' 
            If ErrorFlag = True Then
                RefreshFilesToSend()
            End If
        Next
    End Sub


    '-------------------------------------------------------------------------
    'Call Tab
    '-------------------------------------------------------------------------    

    '--- UI stuff ------------------------------------------------------------

    '--- Real stuff ----------------------------------------------------------
    Private Sub btnSaveLog_Click(sender As Object, e As EventArgs) Handles btnSaveLog.Click
        'Let user save a copy of the last call log

        'If the box is empty a call probably hasn't happened yet.  We should
        'have the button disabled, but just in case we don't, let's check and
        'just exit in that case.
        If tbxCallLog.Text = "" Then Exit Sub

        Dim LogName As String
        LogName = "Squirrel Call Log" & Now.ToString(" yyyyddmmHHmmss") & ".txt"

        If FolderBrowserDialog2.ShowDialog = DialogResult.OK Then
            Try
                System.IO.File.WriteAllText(
                    FolderBrowserDialog2.SelectedPath & "\" & LogName,
                    tbxCallLog.Text)
            Catch ex As Exception
                SquirrelComms.Item(25).SystemError("")
            End Try
        End If
    End Sub
    Private Sub btnCall_Click(sender As Object, e As EventArgs) Handles btnCall.Click
        If lsvSystemsList.SelectedItems.Count <> 1 Then
            Exit Sub
        End If

        btnSaveLog.Enabled = False
        btnCall.Enabled = False
        btnCall.Text = "Process Active"

        SquirrelComms.Item(9).SetTbxCallInfo(tbxCallInfo)
        tbxCallLog.Clear()
        tbxCallLog.AppendText("Call process started: " & DateAndTime.Now & vbCrLf)

        Dim Result As Integer = ExecuteCommandInWindow(Conf.Winpathof_bash,
                               "-c -l " &
                               Chr(34) &
                               Conf.UNIXpathof_uucico_front & " " &
                               lsvSystemsList.SelectedItems(0).Text &
                               Chr(34))

        If Result = 0 Then
            tbxCallLog.AppendText("No error code returned" & vbCrLf)
            SquirrelComms.Item(10).SetTbxCallInfo(tbxCallInfo)
        Else
            tbxCallLog.AppendText("Process returned error code " & Result & vbCrLf)
            SquirrelComms.Item(11).SetTbxCallInfo(tbxCallInfo)
        End If

        tbxCallLog.AppendText("Call process ended:   " & DateAndTime.Now & vbCrLf & vbCrLf)

        Threading.Thread.Sleep(250)
        Dim Tries As Integer = 4

        Dim ex As IOException = Nothing
        Dim reader As System.IO.StreamReader
        Do Until Tries = 0
            Tries = Tries - 1
            Try
                reader = System.IO.File.OpenText(Conf.Winpathof_uulog)
            Catch ex
            End Try
            If IsNothing(ex) Then Exit Do
            DebugOut(" log file in use, waiting 1 second ...")
            Threading.Thread.Sleep(1000)
        Loop

        If Tries <> 0 Then
            Do Until reader.EndOfStream
                Dim fileLine As String = Trim(reader.ReadLine)
                tbxCallLog.AppendText(fileLine & vbCrLf)
            Loop
            reader.Dispose()
        Else
            tbxCallLog.AppendText("Unable to retrieve information from the previous call." &
                                  vbCrLf &
                                  "Sorry.")
        End If

        RefreshFilesToSend()
        RefreshReceivedFiles()

        btnCall.Enabled = True
        btnSaveLog.Enabled = True
        btnCall.Text = "Call"
    End Sub

    '-------------------------------------------------------------------------
    'Inbox Tab
    '-------------------------------------------------------------------------    

    '--- UI stuff ------------------------------------------------------------
    Private Sub TabPage5_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpgInbox.Enter
        lsvReceivedFilesResizer(lsvReceivedFiles, TabControl1.Width)
    End Sub
    Public Sub RefreshReceivedFiles()
        'Looks at files in the UUCP public folder and populates
        'lsvReceivedFiles accordingly.
        '(Clears list before population, of course.)
        '
        'Also:
        '- Calls lsvReceivedFilesResizer to make the window look right.
        '
        lsvReceivedFiles.Items.Clear()
        For Each f As String In
            Microsoft.VisualBasic.FileIO.FileSystem.GetFiles(Conf.WinPathof_uucppublic, FileIO.SearchOption.SearchAllSubDirectories)
            Dim lvi As New ListViewItem
            lvi.Text = f
            lsvReceivedFiles.Items.Insert(0, lvi)
        Next
        lsvReceivedFilesResizer(lsvReceivedFiles, TabControl1.Width)
    End Sub

    'FileSystemWatcher1 watches the incoming UUCP public folder.
    'Changes will enable the timer - handler on the timer will update the
    'file display.
    Public Sub FileSystemWatcher1_Created(ByVal sender As Object, ByVal e As EventArgs) Handles FileSystemWatcher1.Created
        Timer1.Enabled = True
    End Sub
    Public Sub FileSystemWatcher1_Deleted(ByVal sender As Object, ByVal e As EventArgs) Handles FileSystemWatcher1.Deleted
        Timer1.Enabled = True
    End Sub
    Public Sub FileSystemWatcher1_Renamed(ByVal sender As Object, ByVal e As EventArgs) Handles FileSystemWatcher1.Renamed
        Timer1.Enabled = True
    End Sub

    '--- Real stuff ----------------------------------------------------------
    Private Sub btnOpenExplorer_Click(sender As Object, e As EventArgs) Handles btnOpenExplorer.Click
        Process.Start("explorer.exe", Conf.Winpathof_uucppublic)
    End Sub

    Private Sub lblNodeName_DoubleClick(sender As Object, e As EventArgs) Handles lblNodeName.DoubleClick
        'If there are one or more problems with the UUCP configuration files,
        'and the user double-clicks here (which will say "[Configuration
        'Error]"), we'll pop up the configuration error report dialog.
        If Not Conf.Valid Then
            Dim ConfigErrorReporter As New Form3
            For Each Thing In Conf.Problems
                ConfigErrorReporter.ListBox1.Items.Add(Thing.Body)
            Next
            ConfigErrorReporter.ShowDialog()
            ConfigErrorReporter.Dispose()
        End If
    End Sub
End Class