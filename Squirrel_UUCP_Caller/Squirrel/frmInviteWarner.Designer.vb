<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInviteWarner
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
        Me.InviteDetails = New System.Windows.Forms.ListView()
        Me.Header = New System.Windows.Forms.ColumnHeader()
        Me.Description = New System.Windows.Forms.ColumnHeader()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.chkIgnoreNodename = New System.Windows.Forms.CheckBox()
        Me.chkIgnoreForwardingOpts = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'InviteDetails
        '
        Me.InviteDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InviteDetails.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Header, Me.Description})
        Me.InviteDetails.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.InviteDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.InviteDetails.HideSelection = True
        Me.InviteDetails.Location = New System.Drawing.Point(12, 65)
        Me.InviteDetails.MultiSelect = False
        Me.InviteDetails.Name = "InviteDetails"
        Me.InviteDetails.Size = New System.Drawing.Size(615, 227)
        Me.InviteDetails.TabIndex = 0
        Me.InviteDetails.UseCompatibleStateImageBehavior = False
        Me.InviteDetails.View = System.Windows.Forms.View.Details
        '
        'Header
        '
        Me.Header.Width = 48
        '
        'Description
        '
        Me.Description.Width = 1024
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(12, 12)
        Me.TextBox1.MinimumSize = New System.Drawing.Size(614, 47)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(615, 47)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "You are about to accept an invite from a remote UUCP system." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Accepting this invi" &
    "te will make the following configuration changes." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Review the below and make sur" &
    "e you understand all changes."
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(426, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(201, 24)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAccept
        '
        Me.btnAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAccept.Location = New System.Drawing.Point(12, 320)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(201, 24)
        Me.btnAccept.TabIndex = 7
        Me.btnAccept.Text = "Accept"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'chkIgnoreNodename
        '
        Me.chkIgnoreNodename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIgnoreNodename.AutoSize = True
        Me.chkIgnoreNodename.Enabled = False
        Me.chkIgnoreNodename.Location = New System.Drawing.Point(12, 295)
        Me.chkIgnoreNodename.Name = "chkIgnoreNodename"
        Me.chkIgnoreNodename.Size = New System.Drawing.Size(157, 19)
        Me.chkIgnoreNodename.TabIndex = 8
        Me.chkIgnoreNodename.Text = "Don't change nodename"
        Me.chkIgnoreNodename.UseVisualStyleBackColor = True
        '
        'chkIgnoreForwardingOpts
        '
        Me.chkIgnoreForwardingOpts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIgnoreForwardingOpts.AutoSize = True
        Me.chkIgnoreForwardingOpts.Enabled = False
        Me.chkIgnoreForwardingOpts.Location = New System.Drawing.Point(175, 295)
        Me.chkIgnoreForwardingOpts.Name = "chkIgnoreForwardingOpts"
        Me.chkIgnoreForwardingOpts.Size = New System.Drawing.Size(201, 19)
        Me.chkIgnoreForwardingOpts.TabIndex = 9
        Me.chkIgnoreForwardingOpts.Text = "Don't change forwarding options"
        Me.chkIgnoreForwardingOpts.UseVisualStyleBackColor = True
        '
        'frmInviteWarner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 356)
        Me.Controls.Add(Me.chkIgnoreForwardingOpts)
        Me.Controls.Add(Me.chkIgnoreNodename)
        Me.Controls.Add(Me.btnAccept)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.InviteDetails)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(654, 395)
        Me.Name = "frmInviteWarner"
        Me.Text = "Accept Invite?"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents InviteDetails As ListView
    Friend WithEvents Header As ColumnHeader
    Friend WithEvents Description As ColumnHeader
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnAccept As Button
    Friend WithEvents chkIgnoreNodename As CheckBox
    Friend WithEvents chkIgnoreForwardingOpts As CheckBox
End Class
