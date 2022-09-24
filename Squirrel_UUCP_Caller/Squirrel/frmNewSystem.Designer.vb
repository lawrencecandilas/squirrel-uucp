<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewSystem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbxBuilders = New System.Windows.Forms.ListBox()
        Me.pnlNewSystemBuilderObject = New System.Windows.Forms.Panel()
        Me.btnBuildSystem = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnInviteFile = New System.Windows.Forms.Button()
        Me.ofdInviteFile = New System.Windows.Forms.OpenFileDialog()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbxBuilders
        '
        Me.lbxBuilders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxBuilders.FormattingEnabled = True
        Me.lbxBuilders.ItemHeight = 15
        Me.lbxBuilders.Location = New System.Drawing.Point(44, 24)
        Me.lbxBuilders.Name = "lbxBuilders"
        Me.lbxBuilders.ScrollAlwaysVisible = True
        Me.lbxBuilders.Size = New System.Drawing.Size(304, 49)
        Me.lbxBuilders.TabIndex = 1
        '
        'pnlNewSystemBuilderObject
        '
        Me.pnlNewSystemBuilderObject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNewSystemBuilderObject.Location = New System.Drawing.Point(12, 98)
        Me.pnlNewSystemBuilderObject.Name = "pnlNewSystemBuilderObject"
        Me.pnlNewSystemBuilderObject.Size = New System.Drawing.Size(560, 242)
        Me.pnlNewSystemBuilderObject.TabIndex = 3
        '
        'btnBuildSystem
        '
        Me.btnBuildSystem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBuildSystem.Enabled = False
        Me.btnBuildSystem.Location = New System.Drawing.Point(12, 346)
        Me.btnBuildSystem.Name = "btnBuildSystem"
        Me.btnBuildSystem.Size = New System.Drawing.Size(111, 24)
        Me.btnBuildSystem.TabIndex = 4
        Me.btnBuildSystem.Text = "Add"
        Me.btnBuildSystem.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(461, 346)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(111, 24)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnInviteFile
        '
        Me.btnInviteFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInviteFile.Location = New System.Drawing.Point(400, 23)
        Me.btnInviteFile.Name = "btnInviteFile"
        Me.btnInviteFile.Size = New System.Drawing.Size(111, 49)
        Me.btnInviteFile.TabIndex = 6
        Me.btnInviteFile.Text = "Load Invite File"
        Me.btnInviteFile.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnInviteFile)
        Me.Panel1.Controls.Add(Me.lbxBuilders)
        Me.Panel1.Location = New System.Drawing.Point(12, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(560, 88)
        Me.Panel1.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(358, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "- OR -"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(400, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "... use an Invite file"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Select a transport type and enter details manually ..."
        '
        'frmNewSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 382)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnBuildSystem)
        Me.Controls.Add(Me.pnlNewSystemBuilderObject)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "frmNewSystem"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "New System"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbxBuilders As ListBox
    Friend WithEvents pnlNewSystemBuilderObject As Panel
    Friend WithEvents btnBuildSystem As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnInviteFile As Button
    Friend WithEvents ofdInviteFile As OpenFileDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
End Class
