Public Class ucoDISPLAY_OtherTransport
    'Form1 will instantiate this if it can't discern the type of transport,
    'or if "View Raw Config" is selected.
    '
    'Form1 will not try to call SetFields() on this, so it's not needed.

    Private Sub ucoDISPLAY_OtherTransport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
End Class
