Public Class frmInviteWarner
    Public Accept As Boolean = False
    Public IgnoreNodename As Boolean = False
    Public IgnoreForwardingOpts As Boolean = False

    Public Sub WantsToChangeNodename()
        chkIgnoreNodename.Enabled = True
        chkIgnoreNodename.Checked = False
    End Sub

    Public Sub HasForwardingOptions()
        chkIgnoreForwardingOpts.Enabled = True
        chkIgnoreForwardingOpts.Checked = False
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Accept = False
        IgnoreNodename = False
        IgnoreForwardingOpts = False
        Me.Close()
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Accept = True
        IgnoreNodename = chkIgnoreNodename.Checked
        IgnoreForwardingOpts = chkIgnoreForwardingOpts.Checked
        Me.Close()
    End Sub
End Class