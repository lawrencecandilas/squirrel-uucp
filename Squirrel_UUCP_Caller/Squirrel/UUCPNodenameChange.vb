Public Class UUCPNodenameChange
    Public NewNameSet As Boolean = False

    Private Sub UUCPNodenameChange_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
    End Sub

    Private Function IsNodenameValid(ByRef in_Nodename As String)
        IsNodenameValid = False

        'Blank?  Invalid, bounce immediately.
        If in_Nodename = "" Then Exit Function

        Dim tempchr As String = UCase(Mid(in_Nodename, 1, 1))
        'First character not a letter?  Bounce immediately.
        If AscW(tempchr) < 65 Or AscW(tempchr) > 90 Then Exit Function

        'Loop and look for very illegal characters (outside of printable ASCII
        'range.  Convert sorta illegal ones (most punctuation) to underscores.
        'Don't feel like arguing with Windows UUCP users about nodenames too
        'much.
        Dim tempstr As String = ""
        Dim lastchr As String = ""
        Dim c As Integer = 1
        For c = 1 To Len(tbxNewUUCPNodename.Text)
            lastchr = tempchr
            tempchr = LCase(Mid(tbxNewUUCPNodename.Text, c, 1))
            'unprintable ASCII is bounced as invalid
            If AscW(tempchr) < 32 Or AscW(tempchr) > 127 Then
                Exit Function
            End If
            'convert these chars to underscores
            If InStr(" /\@#$%^&*()![]{};;',.<>?|=+-" + Chr(34), tempchr) <> 0 Then
                tempchr = "_"
            End If
            'max 1 underscore in a row.
            If lastchr = "_" And tempchr = "_" Then Continue For
            tempstr &= tempchr
        Next

        'Modify passed variable by reference with underscored version.
        in_Nodename = tempstr
        'We're good.
        IsNodenameValid = True
    End Function
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