Public Class frmUUCPNodenameChange
    Public NewNameSet As Boolean = False

    Private Sub frmUUCPNodenameChange_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
    End Sub

    Private Sub btnSetNewUUCPNodename_Click(sender As Object, e As EventArgs) Handles btnSetNewUUCPNodename.Click
        Dim tempstr1 As String = tbxNewUUCPNodename.Text

        'NOTE: tempstr1 passed by reference, might be modified upon return.
        If IsNodenameValid(tempstr1) Then
            tbxNewUUCPNodename.Text = tempstr1
            NewNameSet = True
            Me.Close()
        Else
            SquirrelComms.Item(14).UserError()
            NewNameSet = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        NewNameSet = False
        Me.Close()
    End Sub
End Class