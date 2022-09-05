Public Class ucoSSHTransport
    Public Sub SetFields(in_systemName As String,
                         in_portName As String,
                         in_SSHServer As String,
                         in_SSHKeyPath As String,
                         in_SSHLoginName As String,
                         in_uucicoUserName As String)
        DebugOut("ucoSSHTransport.SetFields(" & Chr(34) & in_systemName & Chr(34) &
                                            "," & Chr(34) & in_portName & Chr(34) &
                                            "," & Chr(34) & in_SSHServer & Chr(34) &
                                            "," & Chr(34) & in_SSHKeyPath & Chr(34) &
                                            "," & Chr(34) & in_SSHLoginName & Chr(34) &
                                            "," & Chr(34) & in_uucicoUserName & Chr(34) &
                                            ")")
        tbxSystemName.Text = in_systemName
        tbxPortName.Text = in_portName
        tbxSSHServer.Text = in_SSHServer
        tbxSSHKeyPath.Text = in_SSHKeyPath
        tbxSSHLoginName.Text = in_SSHLoginName
        tbxuucicoUsername.Text = in_uucicoUserName
    End Sub

    Private Sub ucoSSHTransport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub

End Class
