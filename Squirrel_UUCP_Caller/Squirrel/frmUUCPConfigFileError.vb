Public Class frmUUCPConfigFileError
    'This form is used to report UUCP configuration file errors to the user.
    '
    'Currently it is called from Form1 when Valid from its instance of Config
    'is False.
    Private Sub frmUUCPConfigFileError_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class