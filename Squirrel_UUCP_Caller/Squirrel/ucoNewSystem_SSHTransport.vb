Public Class ucoNewSystem_SSHTransport
    Public Modified As Boolean = False
    Public valid As Boolean = False

    Public Caller As Object = Nothing

    Public Winpathof_etcuucp As String = ""

    Public Ports As Collection
    Public Systems As Collection
    Private Sub ucoNewSystem_SSHTransport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
    Public Function CheckIfValid(in_NewSystemHashtable As Hashtable) As String
        'If returned string is null, there are no problems.
        CheckIfValid = ""

        'TODO: add uucico-call-username and uucico-call-password

        ''system-name' must appear and not be null
        If Not in_NewSystemHashtable.ContainsKey("system-name") Then
            CheckIfValid &= "- 'system-name' required option not defined" & vbCrLf
        Else
            If in_NewSystemHashtable("system-name") = "" Then
                CheckIfValid &= "- 'system-name' required option defined but blank" & vbCrLf
            End If
        End If
        'TODO: shouldn't be more than 32 characters
        'TODO: should only contain letter, number, and dash/underscores
        'TODO: probably shouldn't begin with a letter

        ''ssh-server' must appear and not be null
        If Not in_NewSystemHashtable.ContainsKey("ssh-server") Then
            CheckIfValid &= "- 'ssh-server' required option not found" & vbCrLf
        Else
            If in_NewSystemHashtable("ssh-server") = "" Then
                CheckIfValid &= "- 'ssh-server' required option defined but blank" & vbCrLf
            End If
        End If
        'TODO: needs to be a valid IPv4/IPv6 or domain name.

        ''ssh-username' must appear and not be null
        If Not in_NewSystemHashtable.ContainsKey("ssh-username") Then
            CheckIfValid &= "- 'ssh-username' required option not found" & vbCrLf
        Else
            If in_NewSystemHashtable("ssh-username") = "" Then
                CheckIfValid &= "- 'ssh-username' required option defined but blank" & vbCrLf
            End If
        End If
        'TODO: needs to be a valid unix username

        'either 'private-key-file' must appear and point to an existing file
        'OR
        ''private-key' must appear and not be null (contains SSH key text)
        '
        If in_NewSystemHashtable.ContainsKey("private-key-file") Then
            If Not System.IO.File.Exists(in_NewSystemHashtable("private-key-file")) Then
                CheckIfValid &= " - private key file " &
                            Chr(34) & in_NewSystemHashtable("private-key-file") & Chr(34) &
                            " doesn't exist or inaccessible." & vbCrLf
            End If
        ElseIf Not in_NewSystemHashtable.ContainsKey("private-key-text") Then
            CheckIfValid &= " - didn't see a 'private-key-file' option or a 'private-key'/'end-private-key' block." & vbCrLf
        End If
        'TODO: keytext needs to be in OpenSSH private key format

    End Function
    Public Function MakeFromUI() As Hashtable
        DebugOut("ucoNewSystem_SSHTransport.MakeFromUI()")
        'Pull bits from UI and stuff them in a hashtable.
        '
        'Unlike reading from an invite file, this should always succeed.
        Dim NVPs As New Hashtable
        NVPs("transport-type") = "SSHTransport"
        NVPs("system-name") = Trim(tbxNewSystemName.Text)
        NVPs("ssh-username") = Trim(tbxNewSSHLoginName.Text)
        NVPs("ssh-server") = Trim(tbxNewSSHServer.Text)
        NVPs("private-key-file") = Trim(tbxNewSSHKeyPath.Text)
        NVPs("uucico-call-username") = Trim(tbxuucicoCallUsername.Text)
        NVPs("uucico-call-password") = Trim(tbxuucicoCallPassword.Text)

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
        'Form2.
        '
        EtchInFiles = False

        Dim i As Integer = 0

        'Port name
        Dim portname As String = "SSH-" & RandStr()
        Dim NewSSHKeyPath = "/etc/uucp/" & My.Computer.FileSystem.GetName(in_NewSystemHashtable("private-key-file"))
        Dim NewSSHUserName = in_NewSystemHashtable("ssh-username")
        Dim NewSSHSystemName = in_NewSystemHashtable("system-name")
        Dim NewuucicoCallUserName = in_NewSystemHashtable("uucico-call-username")
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

    Public Sub New(in_caller As Object, in_Winpathof_etcuucp As String, in_ports As Object, in_systems As Object)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Caller = in_caller
        Winpathof_etcuucp = in_Winpathof_etcuucp
        Ports = in_ports
        Systems = in_systems
    End Sub

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
