Public Class frmNewSystem_NowEditing
    'Code in frmNewSystem will replace Panel1 with an instance of this when
    'the user begins editing.
    '
    'Reason:
    'This prevents the user from selecting a new system while in the middle of
    'editing - the user can close the dialog - getting asked if sure - or say
    'OK to the edit.
    '
    Private Sub frmNewSystem_NowEditing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
End Class
