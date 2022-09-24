Public Class ucoDISPLAY_NoSystemsNotice
    'Form1 will instantiate this if the configuration has no systems, just as
    'a friendly communication to the user.
    '
    'Form1 will not try to call SetFields() on this, so it's not needed.

    Private Sub ucoDISPLAY_NoSystemsNotice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub

End Class
