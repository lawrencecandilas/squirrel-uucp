Public Class ucoKnownSystem
    Public Sub SetFields(in_systemName As String)
        DebugOut("ucoKnownSystem.SetFields(" & Chr(34) & in_systemName & Chr(34) & ")")
        tbxSystemName.Text = in_systemName
    End Sub
    Private Sub ucoKnownSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
End Class
