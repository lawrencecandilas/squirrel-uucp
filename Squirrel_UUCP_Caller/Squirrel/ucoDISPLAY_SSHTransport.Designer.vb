<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucoDISPLAY_SSHTransport
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
        Me.tbxPortName = New System.Windows.Forms.TextBox()
        Me.tbxSSHServer = New System.Windows.Forms.TextBox()
        Me.tbxSSHKeyPath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbxSystemName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbxSSHLoginName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.tbxuucicoUsername = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbxPortName
        '
        Me.tbxPortName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxPortName.Location = New System.Drawing.Point(115, 64)
        Me.tbxPortName.Name = "tbxPortName"
        Me.tbxPortName.ReadOnly = True
        Me.tbxPortName.Size = New System.Drawing.Size(373, 23)
        Me.tbxPortName.TabIndex = 2
        Me.tbxPortName.Tag = "INTERNAL-PortName"
        '
        'tbxSSHServer
        '
        Me.tbxSSHServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxSSHServer.Location = New System.Drawing.Point(115, 93)
        Me.tbxSSHServer.Name = "tbxSSHServer"
        Me.tbxSSHServer.ReadOnly = True
        Me.tbxSSHServer.Size = New System.Drawing.Size(373, 23)
        Me.tbxSSHServer.TabIndex = 3
        Me.tbxSSHServer.Tag = "ssh-server"
        '
        'tbxSSHKeyPath
        '
        Me.tbxSSHKeyPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxSSHKeyPath.Location = New System.Drawing.Point(115, 122)
        Me.tbxSSHKeyPath.Name = "tbxSSHKeyPath"
        Me.tbxSSHKeyPath.ReadOnly = True
        Me.tbxSSHKeyPath.Size = New System.Drawing.Size(373, 23)
        Me.tbxSSHKeyPath.TabIndex = 4
        Me.tbxSSHKeyPath.Tag = "INTERNAL-SSHKeyPath"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Port Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 125)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "SSH Key Path"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "SSH Login Name"
        '
        'tbxSystemName
        '
        Me.tbxSystemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxSystemName.Location = New System.Drawing.Point(115, 35)
        Me.tbxSystemName.Name = "tbxSystemName"
        Me.tbxSystemName.ReadOnly = True
        Me.tbxSystemName.Size = New System.Drawing.Size(373, 23)
        Me.tbxSystemName.TabIndex = 10
        Me.tbxSystemName.Tag = "system-name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 15)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "System Name"
        '
        'tbxSSHLoginName
        '
        Me.tbxSSHLoginName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxSSHLoginName.Location = New System.Drawing.Point(115, 152)
        Me.tbxSSHLoginName.Name = "tbxSSHLoginName"
        Me.tbxSSHLoginName.ReadOnly = True
        Me.tbxSSHLoginName.Size = New System.Drawing.Size(373, 23)
        Me.tbxSSHLoginName.TabIndex = 12
        Me.tbxSSHLoginName.Tag = "ssh-username"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "SSH Server"
        '
        'LabelTitle
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Location = New System.Drawing.Point(13, 11)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(80, 15)
        Me.LabelTitle.TabIndex = 14
        Me.LabelTitle.Text = "SSH Transport"
        '
        'tbxuucicoUsername
        '
        Me.tbxuucicoUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxuucicoUsername.Location = New System.Drawing.Point(115, 181)
        Me.tbxuucicoUsername.Name = "tbxuucicoUsername"
        Me.tbxuucicoUsername.ReadOnly = True
        Me.tbxuucicoUsername.Size = New System.Drawing.Size(373, 23)
        Me.tbxuucicoUsername.TabIndex = 15
        Me.tbxuucicoUsername.Tag = "uucico-call-username"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 15)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "uucico username"
        '
        'ucoDISPLAY_SSHTransport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbxuucicoUsername)
        Me.Controls.Add(Me.LabelTitle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbxSSHLoginName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbxSystemName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbxSSHKeyPath)
        Me.Controls.Add(Me.tbxSSHServer)
        Me.Controls.Add(Me.tbxPortName)
        Me.DoubleBuffered = True
        Me.Name = "ucoDISPLAY_SSHTransport"
        Me.Size = New System.Drawing.Size(506, 219)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbxPortName As TextBox
    Friend WithEvents tbxSSHServer As TextBox
    Friend WithEvents tbxSSHKeyPath As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents tbxSystemName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbxSSHLoginName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents tbxuucicoUsername As TextBox
    Friend WithEvents Label6 As Label
End Class
