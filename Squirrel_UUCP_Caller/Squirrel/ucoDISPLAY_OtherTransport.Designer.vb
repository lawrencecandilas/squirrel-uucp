<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucoDISPLAY_OtherTransport
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tbxConfigText = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'tbxConfigText
        '
        Me.tbxConfigText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxConfigText.Location = New System.Drawing.Point(3, 3)
        Me.tbxConfigText.Multiline = True
        Me.tbxConfigText.Name = "tbxConfigText"
        Me.tbxConfigText.ReadOnly = True
        Me.tbxConfigText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbxConfigText.Size = New System.Drawing.Size(376, 188)
        Me.tbxConfigText.TabIndex = 0
        '
        'ucoOtherTransport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tbxConfigText)
        Me.Name = "ucoOtherTransport"
        Me.Size = New System.Drawing.Size(382, 194)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbxConfigText As TextBox
End Class
