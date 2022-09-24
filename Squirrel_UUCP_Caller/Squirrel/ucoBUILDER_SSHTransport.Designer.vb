<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucoBUILDER_SSHTransport
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbxNewSystemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbxNewSSHLoginName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbxNewSSHKeyPath = New System.Windows.Forms.TextBox()
        Me.tbxNewSSHServer = New System.Windows.Forms.TextBox()
        Me.btnSetDefault = New System.Windows.Forms.Button()
        Me.ofdSSHTransportKey = New System.Windows.Forms.OpenFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbxuucicoCallUsername = New System.Windows.Forms.TextBox()
        Me.tbxuucicoCallPassword = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Location = New System.Drawing.Point(13, 11)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(177, 15)
        Me.LabelTitle.TabIndex = 15
        Me.LabelTitle.Text = "New System - Reachable By SSH"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "System Name"
        '
        'tbxNewSystemName
        '
        Me.tbxNewSystemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxNewSystemName.Location = New System.Drawing.Point(115, 35)
        Me.tbxNewSystemName.Name = "tbxNewSystemName"
        Me.tbxNewSystemName.Size = New System.Drawing.Size(373, 23)
        Me.tbxNewSystemName.TabIndex = 17
        Me.tbxNewSystemName.Tag = "system-name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "SSH Server"
        '
        'tbxNewSSHLoginName
        '
        Me.tbxNewSSHLoginName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxNewSSHLoginName.Location = New System.Drawing.Point(115, 143)
        Me.tbxNewSSHLoginName.Name = "tbxNewSSHLoginName"
        Me.tbxNewSSHLoginName.Size = New System.Drawing.Size(373, 23)
        Me.tbxNewSSHLoginName.TabIndex = 22
        Me.tbxNewSSHLoginName.Tag = "ssh-username"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "SSH Login Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 15)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "SSH Key Path"
        '
        'tbxNewSSHKeyPath
        '
        Me.tbxNewSSHKeyPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxNewSSHKeyPath.Location = New System.Drawing.Point(115, 93)
        Me.tbxNewSSHKeyPath.Name = "tbxNewSSHKeyPath"
        Me.tbxNewSSHKeyPath.ReadOnly = True
        Me.tbxNewSSHKeyPath.Size = New System.Drawing.Size(256, 23)
        Me.tbxNewSSHKeyPath.TabIndex = 19
        Me.tbxNewSSHKeyPath.Tag = "INTERNAL-SSHKeyPath"
        '
        'tbxNewSSHServer
        '
        Me.tbxNewSSHServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxNewSSHServer.Location = New System.Drawing.Point(115, 64)
        Me.tbxNewSSHServer.Name = "tbxNewSSHServer"
        Me.tbxNewSSHServer.Size = New System.Drawing.Size(373, 23)
        Me.tbxNewSSHServer.TabIndex = 18
        Me.tbxNewSSHServer.Tag = "ssh-server"
        '
        'btnSetDefault
        '
        Me.btnSetDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetDefault.Location = New System.Drawing.Point(377, 92)
        Me.btnSetDefault.Name = "btnSetDefault"
        Me.btnSetDefault.Size = New System.Drawing.Size(111, 25)
        Me.btnSetDefault.TabIndex = 24
        Me.btnSetDefault.Text = "Import Keyfile"
        Me.btnSetDefault.UseVisualStyleBackColor = True
        '
        'ofdSSHTransportKey
        '
        Me.ofdSSHTransportKey.Title = "Select SSH private key file to import"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(466, 15)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "SSH keyfiles must reside in UUCP configuration folder.  Use [Import Keyfile] to a" &
    "dd one."
        '
        'tbxuucicoCallUsername
        '
        Me.tbxuucicoCallUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxuucicoCallUsername.Location = New System.Drawing.Point(115, 172)
        Me.tbxuucicoCallUsername.Name = "tbxuucicoCallUsername"
        Me.tbxuucicoCallUsername.Size = New System.Drawing.Size(373, 23)
        Me.tbxuucicoCallUsername.TabIndex = 26
        Me.tbxuucicoCallUsername.Tag = "uucico-call-username"
        '
        'tbxuucicoCallPassword
        '
        Me.tbxuucicoCallPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxuucicoCallPassword.Location = New System.Drawing.Point(115, 201)
        Me.tbxuucicoCallPassword.Name = "tbxuucicoCallPassword"
        Me.tbxuucicoCallPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxuucicoCallPassword.Size = New System.Drawing.Size(373, 23)
        Me.tbxuucicoCallPassword.TabIndex = 27
        Me.tbxuucicoCallPassword.Tag = "uucico-call-password"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 15)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "uucico username"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 204)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 15)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "uucico password"
        '
        'ucoBUILDER_SSHTransport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbxuucicoCallPassword)
        Me.Controls.Add(Me.tbxuucicoCallUsername)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSetDefault)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbxNewSSHLoginName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbxNewSSHKeyPath)
        Me.Controls.Add(Me.tbxNewSSHServer)
        Me.Controls.Add(Me.tbxNewSystemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelTitle)
        Me.Name = "ucoBUILDER_SSHTransport"
        Me.Size = New System.Drawing.Size(506, 240)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tbxNewSystemName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents tbxNewSSHLoginName As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbxNewSSHKeyPath As TextBox
    Friend WithEvents tbxNewSSHServer As TextBox
    Friend WithEvents btnSetDefault As Button
    Friend WithEvents ofdSSHTransportKey As OpenFileDialog
    Friend WithEvents Label2 As Label
    Friend WithEvents tbxuucicoCallUsername As TextBox
    Friend WithEvents tbxuucicoCallPassword As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
