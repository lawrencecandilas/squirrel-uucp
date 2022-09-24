Public Class ucoDISPLAY_KnownSystem
    Public Sub SetFields(in_params As Hashtable)
        DebugOut("ucoDISPLAY_KnownSystem.SetFields(in_params)")
        DebugOut(" begin hashtable in_params")
        For Each param In in_params
            DebugOut("  " & param.key & "=" & Chr(34) & param.value & Chr(34&))
        Next
        DebugOut(" end hashtable in_params")
        'NOTE: External code expects this function to exist for all
        'ucoDISPLAY_* User Control objects.
        '

        'This will go through the in_params hashtable and if any controls have
        'a matching tag, the control will be implanted with the value.
        Dim OfThisOne As Control
        For Each OfThisOne In Me.Controls
            If IsNothing(OfThisOne.Tag) Then Continue For
            If in_params.ContainsKey(OfThisOne.Tag) Then
                Implant(OfThisOne, in_params(OfThisOne.Tag))
            End If
        Next
    End Sub

    Private Sub ucoDISPLAY_KnownSystem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dock = DockStyle.Fill
    End Sub
End Class
