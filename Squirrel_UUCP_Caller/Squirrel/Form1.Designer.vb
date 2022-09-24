<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim ch10 As System.Windows.Forms.ColumnHeader
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpgHome = New System.Windows.Forms.TabPage()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.tpgSystems = New System.Windows.Forms.TabPage()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkForwardTo = New System.Windows.Forms.CheckBox()
        Me.chkForwardFrom = New System.Windows.Forms.CheckBox()
        Me.pnlShowSystem = New System.Windows.Forms.Panel()
        Me.btnRemoveSystem = New System.Windows.Forms.Button()
        Me.btnAddSystem = New System.Windows.Forms.Button()
        Me.btnViewRawConfig = New System.Windows.Forms.Button()
        Me.lsvSystemsList = New System.Windows.Forms.ListView()
        Me.btnSetDefault = New System.Windows.Forms.Button()
        Me.tpgOutbox = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbxRecipient = New System.Windows.Forms.TextBox()
        Me.btnAddFile = New System.Windows.Forms.Button()
        Me.btnRemoveFile = New System.Windows.Forms.Button()
        Me.lsvFilesToSend = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader()
        Me.tpgCall = New System.Windows.Forms.TabPage()
        Me.tbxCallLog = New System.Windows.Forms.TextBox()
        Me.btnSaveLog = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tbxCallInfo = New System.Windows.Forms.TextBox()
        Me.btnCall = New System.Windows.Forms.Button()
        Me.tpgInbox = New System.Windows.Forms.TabPage()
        Me.tbxReceiveFolder = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lsvReceivedFiles = New System.Windows.Forms.ListView()
        Me.btnOpenExplorer = New System.Windows.Forms.Button()
        Me.tpgCygwin = New System.Windows.Forms.TabPage()
        Me.lsvConfigReport = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader()
        Me.btnOpenCygwinShell = New System.Windows.Forms.Button()
        Me.btnRecheckConfig = New System.Windows.Forms.Button()
        Me.btnOpenConfigFolder = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnChangeNodename = New System.Windows.Forms.Button()
        Me.lblUUCPNodeName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.tbxCygwinRoot = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblSelectedSystemName = New System.Windows.Forms.Label()
        Me.lblNFiles = New System.Windows.Forms.Label()
        Me.lblFilesSize = New System.Windows.Forms.Label()
        Me.lblNRecipients = New System.Windows.Forms.Label()
        Me.lblNodeName = New System.Windows.Forms.TextBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.FolderBrowserDialog2 = New System.Windows.Forms.FolderBrowserDialog()
        Me.tbxSelectedSystemName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        ch10 = New System.Windows.Forms.ColumnHeader()
        Me.TabControl1.SuspendLayout()
        Me.tpgHome.SuspendLayout()
        Me.tpgSystems.SuspendLayout()
        Me.tpgOutbox.SuspendLayout()
        Me.tpgCall.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tpgInbox.SuspendLayout()
        Me.tpgCygwin.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ch10
        '
        ch10.Text = "File Path/Name"
        ch10.Width = 1024
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpgHome)
        Me.TabControl1.Controls.Add(Me.tpgSystems)
        Me.TabControl1.Controls.Add(Me.tpgOutbox)
        Me.TabControl1.Controls.Add(Me.tpgCall)
        Me.TabControl1.Controls.Add(Me.tpgInbox)
        Me.TabControl1.Controls.Add(Me.tpgCygwin)
        Me.TabControl1.Location = New System.Drawing.Point(12, 9)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(630, 342)
        Me.TabControl1.TabIndex = 0
        '
        'tpgHome
        '
        Me.tpgHome.Controls.Add(Me.RichTextBox1)
        Me.tpgHome.Controls.Add(Me.TextBox2)
        Me.tpgHome.Location = New System.Drawing.Point(4, 24)
        Me.tpgHome.Name = "tpgHome"
        Me.tpgHome.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgHome.Size = New System.Drawing.Size(622, 314)
        Me.tpgHome.TabIndex = 5
        Me.tpgHome.Text = "Home"
        Me.tpgHome.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.RichTextBox1.Location = New System.Drawing.Point(240, 6)
        Me.RichTextBox1.MaxLength = 16384
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(377, 302)
        Me.RichTextBox1.TabIndex = 2
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.TextBox2.Location = New System.Drawing.Point(6, 6)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(228, 302)
        Me.TextBox2.TabIndex = 1
        Me.TextBox2.Text = "Squirrel UUCP Caller" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Version 202209r2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tpgSystems
        '
        Me.tpgSystems.Controls.Add(Me.btnViewRawConfig)
        Me.tpgSystems.Controls.Add(Me.Label7)
        Me.tpgSystems.Controls.Add(Me.chkForwardTo)
        Me.tpgSystems.Controls.Add(Me.chkForwardFrom)
        Me.tpgSystems.Controls.Add(Me.pnlShowSystem)
        Me.tpgSystems.Controls.Add(Me.btnRemoveSystem)
        Me.tpgSystems.Controls.Add(Me.btnAddSystem)
        Me.tpgSystems.Controls.Add(Me.lsvSystemsList)
        Me.tpgSystems.Controls.Add(Me.btnSetDefault)
        Me.tpgSystems.Location = New System.Drawing.Point(4, 24)
        Me.tpgSystems.Name = "tpgSystems"
        Me.tpgSystems.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgSystems.Size = New System.Drawing.Size(622, 314)
        Me.tpgSystems.TabIndex = 0
        Me.tpgSystems.Text = "Systems"
        Me.tpgSystems.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(357, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 15)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Forward :"
        '
        'chkForwardTo
        '
        Me.chkForwardTo.AutoSize = True
        Me.chkForwardTo.Enabled = False
        Me.chkForwardTo.Location = New System.Drawing.Point(469, 9)
        Me.chkForwardTo.Name = "chkForwardTo"
        Me.chkForwardTo.Size = New System.Drawing.Size(38, 19)
        Me.chkForwardTo.TabIndex = 6
        Me.chkForwardTo.Text = "To"
        Me.chkForwardTo.UseVisualStyleBackColor = True
        '
        'chkForwardFrom
        '
        Me.chkForwardFrom.AutoSize = True
        Me.chkForwardFrom.Enabled = False
        Me.chkForwardFrom.Location = New System.Drawing.Point(413, 9)
        Me.chkForwardFrom.Name = "chkForwardFrom"
        Me.chkForwardFrom.Size = New System.Drawing.Size(54, 19)
        Me.chkForwardFrom.TabIndex = 5
        Me.chkForwardFrom.Text = "From"
        Me.chkForwardFrom.UseVisualStyleBackColor = True
        '
        'pnlShowSystem
        '
        Me.pnlShowSystem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlShowSystem.BackColor = System.Drawing.Color.Transparent
        Me.pnlShowSystem.Location = New System.Drawing.Point(240, 36)
        Me.pnlShowSystem.Name = "pnlShowSystem"
        Me.pnlShowSystem.Size = New System.Drawing.Size(376, 272)
        Me.pnlShowSystem.TabIndex = 0
        '
        'btnRemoveSystem
        '
        Me.btnRemoveSystem.Enabled = False
        Me.btnRemoveSystem.Location = New System.Drawing.Point(123, 6)
        Me.btnRemoveSystem.Name = "btnRemoveSystem"
        Me.btnRemoveSystem.Size = New System.Drawing.Size(111, 25)
        Me.btnRemoveSystem.TabIndex = 4
        Me.btnRemoveSystem.Text = "Remove System"
        Me.btnRemoveSystem.UseVisualStyleBackColor = True
        '
        'btnAddSystem
        '
        Me.btnAddSystem.Location = New System.Drawing.Point(6, 6)
        Me.btnAddSystem.Name = "btnAddSystem"
        Me.btnAddSystem.Size = New System.Drawing.Size(111, 25)
        Me.btnAddSystem.TabIndex = 3
        Me.btnAddSystem.Text = "Add System"
        Me.btnAddSystem.UseVisualStyleBackColor = True
        '
        'btnViewRawConfig
        '
        Me.btnViewRawConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewRawConfig.Location = New System.Drawing.Point(505, 5)
        Me.btnViewRawConfig.Name = "btnViewRawConfig"
        Me.btnViewRawConfig.Size = New System.Drawing.Size(111, 25)
        Me.btnViewRawConfig.TabIndex = 2
        Me.btnViewRawConfig.Text = "View Raw Config"
        Me.btnViewRawConfig.UseVisualStyleBackColor = True
        '
        'lsvSystemsList
        '
        Me.lsvSystemsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lsvSystemsList.Location = New System.Drawing.Point(6, 36)
        Me.lsvSystemsList.MultiSelect = False
        Me.lsvSystemsList.Name = "lsvSystemsList"
        Me.lsvSystemsList.Size = New System.Drawing.Size(228, 272)
        Me.lsvSystemsList.TabIndex = 0
        Me.lsvSystemsList.UseCompatibleStateImageBehavior = False
        Me.lsvSystemsList.View = System.Windows.Forms.View.List
        '
        'btnSetDefault
        '
        Me.btnSetDefault.Enabled = False
        Me.btnSetDefault.Location = New System.Drawing.Point(240, 6)
        Me.btnSetDefault.Name = "btnSetDefault"
        Me.btnSetDefault.Size = New System.Drawing.Size(111, 25)
        Me.btnSetDefault.TabIndex = 1
        Me.btnSetDefault.Text = "Set Default"
        Me.btnSetDefault.UseVisualStyleBackColor = True
        '
        'tpgOutbox
        '
        Me.tpgOutbox.Controls.Add(Me.Label2)
        Me.tpgOutbox.Controls.Add(Me.tbxRecipient)
        Me.tpgOutbox.Controls.Add(Me.btnAddFile)
        Me.tpgOutbox.Controls.Add(Me.btnRemoveFile)
        Me.tpgOutbox.Controls.Add(Me.lsvFilesToSend)
        Me.tpgOutbox.Location = New System.Drawing.Point(4, 24)
        Me.tpgOutbox.Name = "tpgOutbox"
        Me.tpgOutbox.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgOutbox.Size = New System.Drawing.Size(622, 314)
        Me.tpgOutbox.TabIndex = 1
        Me.tpgOutbox.Text = "Outbox"
        Me.tpgOutbox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Recipient"
        '
        'tbxRecipient
        '
        Me.tbxRecipient.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxRecipient.Location = New System.Drawing.Point(67, 6)
        Me.tbxRecipient.MaxLength = 256
        Me.tbxRecipient.Name = "tbxRecipient"
        Me.tbxRecipient.Size = New System.Drawing.Size(311, 23)
        Me.tbxRecipient.TabIndex = 4
        '
        'btnAddFile
        '
        Me.btnAddFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddFile.Location = New System.Drawing.Point(385, 5)
        Me.btnAddFile.Name = "btnAddFile"
        Me.btnAddFile.Size = New System.Drawing.Size(114, 25)
        Me.btnAddFile.TabIndex = 1
        Me.btnAddFile.Text = "Add File"
        Me.btnAddFile.UseVisualStyleBackColor = True
        '
        'btnRemoveFile
        '
        Me.btnRemoveFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveFile.Enabled = False
        Me.btnRemoveFile.Location = New System.Drawing.Point(505, 5)
        Me.btnRemoveFile.Name = "btnRemoveFile"
        Me.btnRemoveFile.Size = New System.Drawing.Size(111, 25)
        Me.btnRemoveFile.TabIndex = 3
        Me.btnRemoveFile.Text = "Remove File"
        Me.btnRemoveFile.UseVisualStyleBackColor = True
        '
        'lsvFilesToSend
        '
        Me.lsvFilesToSend.AllowColumnReorder = True
        Me.lsvFilesToSend.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsvFilesToSend.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lsvFilesToSend.FullRowSelect = True
        Me.lsvFilesToSend.GridLines = True
        Me.lsvFilesToSend.Location = New System.Drawing.Point(6, 36)
        Me.lsvFilesToSend.Name = "lsvFilesToSend"
        Me.lsvFilesToSend.Size = New System.Drawing.Size(616, 273)
        Me.lsvFilesToSend.TabIndex = 0
        Me.lsvFilesToSend.UseCompatibleStateImageBehavior = False
        Me.lsvFilesToSend.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "UUCP Job ID"
        Me.ColumnHeader1.Width = 200
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Destination System"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Recipient"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "File Path/Name"
        Me.ColumnHeader4.Width = 250
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "File Size"
        Me.ColumnHeader5.Width = 75
        '
        'tpgCall
        '
        Me.tpgCall.Controls.Add(Me.tbxCallLog)
        Me.tpgCall.Controls.Add(Me.btnSaveLog)
        Me.tpgCall.Controls.Add(Me.Label1)
        Me.tpgCall.Controls.Add(Me.Panel1)
        Me.tpgCall.Location = New System.Drawing.Point(4, 24)
        Me.tpgCall.Name = "tpgCall"
        Me.tpgCall.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgCall.Size = New System.Drawing.Size(622, 314)
        Me.tpgCall.TabIndex = 2
        Me.tpgCall.Text = "Call"
        Me.tpgCall.UseVisualStyleBackColor = True
        '
        'tbxCallLog
        '
        Me.tbxCallLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxCallLog.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.tbxCallLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxCallLog.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.tbxCallLog.Location = New System.Drawing.Point(274, 36)
        Me.tbxCallLog.Multiline = True
        Me.tbxCallLog.Name = "tbxCallLog"
        Me.tbxCallLog.ReadOnly = True
        Me.tbxCallLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbxCallLog.Size = New System.Drawing.Size(342, 272)
        Me.tbxCallLog.TabIndex = 5
        '
        'btnSaveLog
        '
        Me.btnSaveLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveLog.Enabled = False
        Me.btnSaveLog.Location = New System.Drawing.Point(505, 5)
        Me.btnSaveLog.Name = "btnSaveLog"
        Me.btnSaveLog.Size = New System.Drawing.Size(114, 25)
        Me.btnSaveLog.TabIndex = 7
        Me.btnSaveLog.Text = "Save Log"
        Me.btnSaveLog.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(272, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Previous Call Log"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.tbxCallInfo)
        Me.Panel1.Controls.Add(Me.btnCall)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(262, 301)
        Me.Panel1.TabIndex = 4
        '
        'tbxCallInfo
        '
        Me.tbxCallInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxCallInfo.BackColor = System.Drawing.SystemColors.Window
        Me.tbxCallInfo.Location = New System.Drawing.Point(16, 17)
        Me.tbxCallInfo.Multiline = True
        Me.tbxCallInfo.Name = "tbxCallInfo"
        Me.tbxCallInfo.ReadOnly = True
        Me.tbxCallInfo.Size = New System.Drawing.Size(230, 215)
        Me.tbxCallInfo.TabIndex = 4
        Me.tbxCallInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCall
        '
        Me.btnCall.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCall.Enabled = False
        Me.btnCall.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnCall.Location = New System.Drawing.Point(16, 238)
        Me.btnCall.Name = "btnCall"
        Me.btnCall.Size = New System.Drawing.Size(230, 47)
        Me.btnCall.TabIndex = 3
        Me.btnCall.Text = "Call"
        Me.btnCall.UseVisualStyleBackColor = True
        '
        'tpgInbox
        '
        Me.tpgInbox.Controls.Add(Me.tbxReceiveFolder)
        Me.tpgInbox.Controls.Add(Me.Label3)
        Me.tpgInbox.Controls.Add(Me.lsvReceivedFiles)
        Me.tpgInbox.Controls.Add(Me.btnOpenExplorer)
        Me.tpgInbox.Location = New System.Drawing.Point(4, 24)
        Me.tpgInbox.Name = "tpgInbox"
        Me.tpgInbox.Size = New System.Drawing.Size(622, 314)
        Me.tpgInbox.TabIndex = 4
        Me.tpgInbox.Text = "Inbox"
        Me.tpgInbox.UseVisualStyleBackColor = True
        '
        'tbxReceiveFolder
        '
        Me.tbxReceiveFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxReceiveFolder.Location = New System.Drawing.Point(156, 6)
        Me.tbxReceiveFolder.Name = "tbxReceiveFolder"
        Me.tbxReceiveFolder.ReadOnly = True
        Me.tbxReceiveFolder.Size = New System.Drawing.Size(338, 23)
        Me.tbxReceiveFolder.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 15)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Local Received Files Folder"
        '
        'lsvReceivedFiles
        '
        Me.lsvReceivedFiles.AllowColumnReorder = True
        Me.lsvReceivedFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsvReceivedFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ch10})
        Me.lsvReceivedFiles.FullRowSelect = True
        Me.lsvReceivedFiles.GridLines = True
        Me.lsvReceivedFiles.Location = New System.Drawing.Point(3, 36)
        Me.lsvReceivedFiles.Name = "lsvReceivedFiles"
        Me.lsvReceivedFiles.Size = New System.Drawing.Size(616, 265)
        Me.lsvReceivedFiles.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lsvReceivedFiles.TabIndex = 9
        Me.lsvReceivedFiles.UseCompatibleStateImageBehavior = False
        Me.lsvReceivedFiles.View = System.Windows.Forms.View.Details
        '
        'btnOpenExplorer
        '
        Me.btnOpenExplorer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpenExplorer.Location = New System.Drawing.Point(505, 5)
        Me.btnOpenExplorer.Name = "btnOpenExplorer"
        Me.btnOpenExplorer.Size = New System.Drawing.Size(114, 25)
        Me.btnOpenExplorer.TabIndex = 8
        Me.btnOpenExplorer.Text = "Open Explorer"
        Me.btnOpenExplorer.UseVisualStyleBackColor = True
        '
        'tpgCygwin
        '
        Me.tpgCygwin.Controls.Add(Me.lsvConfigReport)
        Me.tpgCygwin.Controls.Add(Me.btnOpenCygwinShell)
        Me.tpgCygwin.Controls.Add(Me.btnRecheckConfig)
        Me.tpgCygwin.Controls.Add(Me.btnOpenConfigFolder)
        Me.tpgCygwin.Controls.Add(Me.GroupBox1)
        Me.tpgCygwin.Location = New System.Drawing.Point(4, 24)
        Me.tpgCygwin.Name = "tpgCygwin"
        Me.tpgCygwin.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgCygwin.Size = New System.Drawing.Size(622, 314)
        Me.tpgCygwin.TabIndex = 3
        Me.tpgCygwin.Text = "Cygwin"
        Me.tpgCygwin.UseVisualStyleBackColor = True
        '
        'lsvConfigReport
        '
        Me.lsvConfigReport.AllowDrop = True
        Me.lsvConfigReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsvConfigReport.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lsvConfigReport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvConfigReport.Location = New System.Drawing.Point(6, 144)
        Me.lsvConfigReport.MultiSelect = False
        Me.lsvConfigReport.Name = "lsvConfigReport"
        Me.lsvConfigReport.ShowGroups = False
        Me.lsvConfigReport.Size = New System.Drawing.Size(610, 158)
        Me.lsvConfigReport.TabIndex = 28
        Me.lsvConfigReport.UseCompatibleStateImageBehavior = False
        Me.lsvConfigReport.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Status"
        Me.ColumnHeader6.Width = 64
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Configuration Item"
        Me.ColumnHeader7.Width = 512
        '
        'btnOpenCygwinShell
        '
        Me.btnOpenCygwinShell.Location = New System.Drawing.Point(318, 6)
        Me.btnOpenCygwinShell.Name = "btnOpenCygwinShell"
        Me.btnOpenCygwinShell.Size = New System.Drawing.Size(150, 24)
        Me.btnOpenCygwinShell.TabIndex = 27
        Me.btnOpenCygwinShell.Text = "Open Cygwin Shell"
        Me.btnOpenCygwinShell.UseVisualStyleBackColor = True
        '
        'btnRecheckConfig
        '
        Me.btnRecheckConfig.Location = New System.Drawing.Point(6, 6)
        Me.btnRecheckConfig.Name = "btnRecheckConfig"
        Me.btnRecheckConfig.Size = New System.Drawing.Size(150, 24)
        Me.btnRecheckConfig.TabIndex = 26
        Me.btnRecheckConfig.Text = "Recheck Config"
        Me.btnRecheckConfig.UseVisualStyleBackColor = True
        '
        'btnOpenConfigFolder
        '
        Me.btnOpenConfigFolder.Location = New System.Drawing.Point(162, 6)
        Me.btnOpenConfigFolder.Name = "btnOpenConfigFolder"
        Me.btnOpenConfigFolder.Size = New System.Drawing.Size(150, 24)
        Me.btnOpenConfigFolder.TabIndex = 25
        Me.btnOpenConfigFolder.Text = "Open Config Folder"
        Me.btnOpenConfigFolder.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnChangeNodename)
        Me.GroupBox1.Controls.Add(Me.lblUUCPNodeName)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.tbxCygwinRoot)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(680, 102)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cygwin Configuration"
        '
        'btnChangeNodename
        '
        Me.btnChangeNodename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeNodename.Location = New System.Drawing.Point(499, 58)
        Me.btnChangeNodename.Name = "btnChangeNodename"
        Me.btnChangeNodename.Size = New System.Drawing.Size(111, 25)
        Me.btnChangeNodename.TabIndex = 20
        Me.btnChangeNodename.Text = "Change"
        Me.btnChangeNodename.UseVisualStyleBackColor = True
        '
        'lblUUCPNodeName
        '
        Me.lblUUCPNodeName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUUCPNodeName.Location = New System.Drawing.Point(186, 59)
        Me.lblUUCPNodeName.Name = "lblUUCPNodeName"
        Me.lblUUCPNodeName.ReadOnly = True
        Me.lblUUCPNodeName.Size = New System.Drawing.Size(307, 23)
        Me.lblUUCPNodeName.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.Label8.Location = New System.Drawing.Point(6, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 15)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "UUCP Node Name"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(499, 28)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(111, 25)
        Me.Button2.TabIndex = 15
        Me.Button2.Text = "Change"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tbxCygwinRoot
        '
        Me.tbxCygwinRoot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxCygwinRoot.Location = New System.Drawing.Point(186, 29)
        Me.tbxCygwinRoot.Name = "tbxCygwinRoot"
        Me.tbxCygwinRoot.ReadOnly = True
        Me.tbxCygwinRoot.Size = New System.Drawing.Size(307, 23)
        Me.tbxCygwinRoot.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.Label5.Location = New System.Drawing.Point(6, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Cygwin Root Location"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Title = "Add which file to Outbox?"
        '
        'lblSelectedSystemName
        '
        Me.lblSelectedSystemName.AutoSize = True
        Me.lblSelectedSystemName.Location = New System.Drawing.Point(64, 9)
        Me.lblSelectedSystemName.Name = "lblSelectedSystemName"
        Me.lblSelectedSystemName.Size = New System.Drawing.Size(0, 15)
        Me.lblSelectedSystemName.TabIndex = 2
        '
        'lblNFiles
        '
        Me.lblNFiles.AutoSize = True
        Me.lblNFiles.Location = New System.Drawing.Point(329, 9)
        Me.lblNFiles.Name = "lblNFiles"
        Me.lblNFiles.Size = New System.Drawing.Size(0, 15)
        Me.lblNFiles.TabIndex = 4
        '
        'lblFilesSize
        '
        Me.lblFilesSize.AutoSize = True
        Me.lblFilesSize.Location = New System.Drawing.Point(419, 9)
        Me.lblFilesSize.Name = "lblFilesSize"
        Me.lblFilesSize.Size = New System.Drawing.Size(0, 15)
        Me.lblFilesSize.TabIndex = 6
        '
        'lblNRecipients
        '
        Me.lblNRecipients.AutoSize = True
        Me.lblNRecipients.Location = New System.Drawing.Point(570, 9)
        Me.lblNRecipients.Name = "lblNRecipients"
        Me.lblNRecipients.Size = New System.Drawing.Size(0, 15)
        Me.lblNRecipients.TabIndex = 8
        '
        'lblNodeName
        '
        Me.lblNodeName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNodeName.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lblNodeName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblNodeName.ForeColor = System.Drawing.SystemColors.Window
        Me.lblNodeName.Location = New System.Drawing.Point(88, 353)
        Me.lblNodeName.MaxLength = 256
        Me.lblNodeName.Name = "lblNodeName"
        Me.lblNodeName.ReadOnly = True
        Me.lblNodeName.Size = New System.Drawing.Size(238, 16)
        Me.lblNodeName.TabIndex = 10
        Me.lblNodeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.IncludeSubdirectories = True
        Me.FileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.FileName
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'Timer1
        '
        Me.Timer1.Interval = 3000
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Where is your Cygwin root folder?"
        '
        'FolderBrowserDialog2
        '
        Me.FolderBrowserDialog2.Description = "Save call log file where?"
        '
        'tbxSelectedSystemName
        '
        Me.tbxSelectedSystemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxSelectedSystemName.BackColor = System.Drawing.SystemColors.ControlDark
        Me.tbxSelectedSystemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxSelectedSystemName.ForeColor = System.Drawing.SystemColors.Window
        Me.tbxSelectedSystemName.Location = New System.Drawing.Point(384, 353)
        Me.tbxSelectedSystemName.MaxLength = 256
        Me.tbxSelectedSystemName.Name = "tbxSelectedSystemName"
        Me.tbxSelectedSystemName.ReadOnly = True
        Me.tbxSelectedSystemName.Size = New System.Drawing.Size(254, 16)
        Me.tbxSelectedSystemName.TabIndex = 11
        Me.tbxSelectedSystemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 353)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Nodename"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(334, 353)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 15)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Calling"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 381)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbxSelectedSystemName)
        Me.Controls.Add(Me.lblNodeName)
        Me.Controls.Add(Me.lblNRecipients)
        Me.Controls.Add(Me.lblFilesSize)
        Me.Controls.Add(Me.lblNFiles)
        Me.Controls.Add(Me.lblSelectedSystemName)
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(670, 420)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Squirrel UUCP Caller"
        Me.TabControl1.ResumeLayout(False)
        Me.tpgHome.ResumeLayout(False)
        Me.tpgHome.PerformLayout()
        Me.tpgSystems.ResumeLayout(False)
        Me.tpgSystems.PerformLayout()
        Me.tpgOutbox.ResumeLayout(False)
        Me.tpgOutbox.PerformLayout()
        Me.tpgCall.ResumeLayout(False)
        Me.tpgCall.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tpgInbox.ResumeLayout(False)
        Me.tpgInbox.PerformLayout()
        Me.tpgCygwin.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tpgOutbox As TabPage
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnAddFile As Button
    Friend WithEvents btnRemoveFile As Button
    Friend WithEvents lsvFilesToSend As ListView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents tpgCall As TabPage
    Friend WithEvents btnCall As Button
    Friend WithEvents lblSelectedSystemName As Label
    Friend WithEvents lblNFiles As Label
    Friend WithEvents lblFilesSize As Label
    Friend WithEvents lblNRecipients As Label
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents tbxRecipient As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tpgSystems As TabPage
    Friend WithEvents btnSetDefault As Button
    Friend WithEvents pnlShowSystem As Panel
    Friend WithEvents lsvSystemsList As ListView
    Friend WithEvents btnViewRawConfig As Button
    Friend WithEvents lblNodeName As TextBox
    Friend WithEvents tpgCygwin As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents tbxCallInfo As TextBox
    Friend WithEvents btnSaveLog As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tbxCallLog As TextBox
    Friend WithEvents tpgInbox As TabPage
    Friend WithEvents btnOpenExplorer As Button
    Friend WithEvents lsvReceivedFiles As ListView
    Friend WithEvents ch10 As ColumnHeader
    Friend WithEvents Label3 As Label
    Friend WithEvents tbxReceiveFolder As TextBox
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnRecheckConfig As Button
    Friend WithEvents btnOpenConfigFolder As Button
    Friend WithEvents btnOpenCygwinShell As Button
    Friend WithEvents btnRemoveSystem As Button
    Friend WithEvents btnAddSystem As Button
    Friend WithEvents lsvConfigReport As ListView
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnChangeNodename As Button
    Friend WithEvents lblUUCPNodeName As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents tbxCygwinRoot As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents tpgHome As TabPage
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents FolderBrowserDialog2 As FolderBrowserDialog
    Friend WithEvents tbxSelectedSystemName As TextBox
    Friend WithEvents chkForwardTo As CheckBox
    Friend WithEvents chkForwardFrom As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
