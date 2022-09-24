Public Class ucoBUILDER_SSHTransport
    'Required Public Variables
    Public Modified As Boolean = False
    Public valid As Boolean = False

    'These are required and set by New()
    Public Caller As Object = Nothing
    Public Winpathof_etcuucp As String = ""
    Public Ports As Collection
    Public Systems As Collection
    Private Sub ucoBUILDER_SSHTransport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
    Public Sub New(in_caller As Object, in_Winpathof_etcuucp As String, in_ports As Object, in_systems As Object)
        ' This call is required by the designer.
        InitializeComponent()

        Caller = in_caller
        Winpathof_etcuucp = in_Winpathof_etcuucp
        Ports = in_ports
        Systems = in_systems
    End Sub

    Public Function CheckIfValid(ByRef in_NewSystemHashtable As Hashtable, Optional vmode As Integer = 0) As String
        'If returned string is null, there are no problems.
        CheckIfValid = ""
        'Check against control Tags and if the incoming hashtable didn't 
        'include any attributes, include them as blank.
        '
        Dim OfThisOne As Control
        For Each OfThisOne In Me.Controls
            If IsNothing(OfThisOne.Tag) Then Continue For
            If OfThisOne.Tag = "" Then Continue For
            If Not in_NewSystemHashtable.Contains(OfThisOne.Tag) Then
                in_NewSystemHashtable(OfThisOne.Tag) = ""
            End If
        Next

        'Validate each attribute.
        For Each Thing In in_NewSystemHashtable
            CheckIfValid &= ValidateAs(Thing.Value, Thing.Key, vmode)
        Next
    End Function
    Public Function MakeFromUI() As Hashtable
        DebugOut("ucoNewSystem_SSHTransport.MakeFromUI()")
        'Pull bits from UI and stuff them in a hashtable.
        '
        'Unlike reading from an invite file, this should always succeed.
        Dim NVPs As New Hashtable
        NVPs("transport-type") = "SSHTransport"

        Dim OfThisOne As Control
        For Each OfThisOne In Me.Controls
            If IsNothing(OfThisOne.Tag) Then Continue For
            NVPs(OfThisOne.Tag) = Extract(OfThisOne)
        Next

        NVPs("failed") = False
        NVPs("whyfailed") = ""
        MakeFromUI = NVPs
    End Function

    Public Function EtchInFiles(in_NewSystemHashtable As Hashtable, in_conf As Object) As Boolean
        DebugOut("ucoNewSystem_SSHTransport.EtchInFiles(in_NewSystemHashtable, in_conf)")
        'EtchInFiles assumes it's dealing with a completed and validated
        'hashtable describing a new system!
        '
        'That's responsibily of calling routine, which is expected to be
        'frmNewSystem.
        '
        EtchInFiles = False

        Dim i As Integer = 0

        'Port name
        Dim portname As String = "SSH-" & RandStr()
        Dim NewSSHKeyPath = "/etc/uucp/" & My.Computer.FileSystem.GetName(in_NewSystemHashtable("INTERNAL-SSHKeyPath"))
        Dim NewSSHUserName = in_NewSystemHashtable("ssh-username")
        Dim NewSSHSystemName = in_NewSystemHashtable("system-name")
        Dim NewuucicoCallUsername = in_NewSystemHashtable("uucico-call-username")
        Dim NewuucicoCallPassword = in_NewSystemHashtable("uucico-call-password")

        Dim PortServerSplit(2) As String
        ExtractPort(in_NewSystemHashtable("ssh-server"), PortServerSplit, 22)
        Dim NewSSHServer = PortServerSplit(1)
        Dim NewSSHPort = PortServerSplit(2)

        'At this point we need to examine the current configuration and
        'determine if we are going to...
        'A. Append port/system to existing Port/sys conf files
        'B. Overwrite existing system in sys file, add new port to Port file
        'C. Do nothing because it already exists.

        'Check existing systems in config
        Dim ExistingSystemFound As Boolean = False
        For Each ExistingSystem In Systems
            If (ExistingSystem.Name = NewSSHSystemName) Then
                ExistingSystemFound = True
                Exit For
            End If
        Next
        If ExistingSystemFound Then
            SquirrelComms.Item(16).UserError()
            Exit Function
        End If

        'Add new SSH transport based system to our config file.
        '
        'Append new port definition to Port file.
        Try
            My.Computer.FileSystem.WriteAllText(
                Slashes(Winpathof_etcuucp & "\Port"),
                    vbLf &
                    "port " & portname &
                    vbLf &
                    "type pipe" &
                    vbLf &
                    "command /bin/ssh -a -x -q" &
                        " -o StrictHostKeyChecking=no " &
                        " -p " &
                        NewSSHPort &
                        " -i " &
                        NewSSHKeyPath &
                        " -l " &
                        NewSSHUserName &
                        " " &
                        NewSSHServer &
                    vbLf &
                    "reliable true" &
                    vbLf &
                    "protocol etyig" &
                    vbLf,
                    vbTrue) 'true here means append, false or absent means overwrite
            DebugOut(" Success: successfully appended new port " &
                     Chr(34) & portname & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & "\Port" & Chr(34)
                     )
        Catch
            SquirrelComms.Item(19).SystemError("")
            DebugOut(" ERROR: FAILED while appending new port " &
                     Chr(34) & portname & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & "\Port" & Chr(34)
                     )
            EtchInFiles = False
            Exit Function
        End Try

        'Append to new system to sys file
        Try
            Dim SysDefaultOptions As New Hashtable
            Dim SysFileBuffer As String = ""
            SysDefaultOptions = in_conf.PrepareToModifySys(SysFileBuffer)

            If in_NewSystemHashtable.Contains("please-forward-from-me") Then
                in_conf.ModifyForward(SysDefaultOptions, "forward-from", True, NewSSHSystemName)
            End If
            If in_NewSystemHashtable.Contains("forward-to-me") Then
                in_conf.ModifyForward(SysDefaultOptions, "forward-to", True, NewSSHSystemName)
            End If

            SysFileBuffer &=
                vbLf &
                    "system " & NewSSHSystemName &
                    vbLf &
                    "call-login *" &
                    vbLf &
                    "call-password *" &
                    vbLf &
                    "time any" &
                    vbLf &
                    "chat " & Chr(34) & Chr(34) & " \d\d\r\c ogin: \d\L word: \P" &
                    vbLf &
                    "chat-timeout 30" &
                    vbLf &
                    "protocol i" &
                    vbLf &
                    "port " & portname &
                    vbLf
            in_conf.CommitModifiedSys(SysFileBuffer, SysDefaultOptions)

            DebugOut(" Success: successfully appended new system " &
                Chr(34) & NewSSHSystemName & Chr(34) &
                " to " &
                Chr(34) & Winpathof_etcuucp & "\sys" & Chr(34)
                )
        Catch
            SquirrelComms.Item(19).SystemError("")
            DebugOut(" ERROR: FAILED while appending new system " &
                     Chr(34) & NewSSHSystemName & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & "\sys" & Chr(34)
                     )
            EtchInFiles = False
            Exit Function
        End Try

        'Append username/password to call file
        Try
            My.Computer.FileSystem.WriteAllText(
                    Slashes(Winpathof_etcuucp & "\call"),
                    NewSSHSystemName &
                    vbTab &
                    NewuucicoCallUserName &
                    vbTab &
                    NewuucicoCallPassword &
                    vbLf,
                vbTrue) 'true here means append, false or absent means overwrite
        Catch
            SquirrelComms.Item(20).SystemError("")
            DebugOut(" ERROR: FAILED while appending new username " &
                     Chr(34) & NewuucicoCallUserName & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & "\call" & Chr(34)
                     )
            EtchInFiles = False
            Exit Function
        End Try

        EtchInFiles = True
    End Function

    Private Sub tbxNewSystemName_TextChanged(sender As Object, e As EventArgs) Handles tbxNewSystemName.TextChanged
        Modified = True
        Caller.PanelChange("")
    End Sub

    Private Sub tbxNewSSHServer_TextChanged(sender As Object, e As EventArgs) Handles tbxNewSSHServer.TextChanged
        Modified = True
        Caller.PanelChange("")
    End Sub

    Private Sub tbxNewSSHKeyPath_TextChanged(sender As Object, e As EventArgs) Handles tbxNewSSHKeyPath.TextChanged
        Modified = True
        Caller.PanelChange("")
    End Sub

    Private Sub tbxNewSSHLoginName_TextChanged(sender As Object, e As EventArgs) Handles tbxNewSSHLoginName.TextChanged
        Modified = True
        Caller.PanelChange("")
    End Sub

    Private Sub btnSetDefault_Click(sender As Object, e As EventArgs) Handles btnSetDefault.Click
        If ofdSSHTransportKey.ShowDialog() = DialogResult.OK Then

            If Not System.IO.File.Exists(ofdSSHTransportKey.FileName) Then
                SquirrelComms.Item(17).UserError()
                Exit Sub
            End If

            DebugOut("copying " &
                     Chr(34) & ofdSSHTransportKey.FileName & Chr(34) &
                     " to " &
                     Chr(34) & Winpathof_etcuucp & Chr(34))
            Try
                FileCopy(
                ofdSSHTransportKey.FileName,
                Slashes(Winpathof_etcuucp & "\" & System.IO.Path.GetFileName(ofdSSHTransportKey.FileName))
                )
                tbxNewSSHKeyPath.Text =
                Slashes(Winpathof_etcuucp & "\" & System.IO.Path.GetFileName(ofdSSHTransportKey.FileName))
            Catch
                SquirrelComms.Item(18).SystemError("")
                DebugOut(" copy failed!")
            End Try

        End If

    End Sub
End Class
