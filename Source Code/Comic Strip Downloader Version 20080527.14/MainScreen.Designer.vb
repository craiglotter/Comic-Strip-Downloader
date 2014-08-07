<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Screen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Screen))
        Me.SavePath = New System.Windows.Forms.TextBox
        Me.DownloadPath = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.DownloadImage = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label
        Me.downloadcycle = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Silent_CheckBox = New System.Windows.Forms.CheckBox
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImportDownloadListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExportDownloadListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ProxyUsernamePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.GenerateWebPageCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AutoUpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowMainScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinimiseMaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label5 = New System.Windows.Forms.Label
        Me.filesdownloaded = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Button6 = New System.Windows.Forms.Button
        Me.DownloadPrefix = New System.Windows.Forms.ListBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.filesdownloadedthiscycle = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog
        Me.DownloadComic = New System.Windows.Forms.ListBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.LastActionLabel = New System.Windows.Forms.Label
        Me.NumberDownloadEntries = New System.Windows.Forms.Label
        Me.CurrentlySelected = New System.Windows.Forms.Label
        Me.SendMail_CheckBox = New System.Windows.Forms.CheckBox
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SavePath
        '
        Me.SavePath.BackColor = System.Drawing.SystemColors.Window
        Me.SavePath.Location = New System.Drawing.Point(25, 209)
        Me.SavePath.Name = "SavePath"
        Me.SavePath.ReadOnly = True
        Me.SavePath.Size = New System.Drawing.Size(554, 20)
        Me.SavePath.TabIndex = 0
        '
        'DownloadPath
        '
        Me.DownloadPath.FormattingEnabled = True
        Me.DownloadPath.Location = New System.Drawing.Point(26, 57)
        Me.DownloadPath.Name = "DownloadPath"
        Me.DownloadPath.Size = New System.Drawing.Size(332, 82)
        Me.DownloadPath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 193)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Folder to Save Comic Strips to:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "URL to Download from:"
        '
        'DownloadImage
        '
        Me.DownloadImage.FormattingEnabled = True
        Me.DownloadImage.Location = New System.Drawing.Point(481, 57)
        Me.DownloadImage.Name = "DownloadImage"
        Me.DownloadImage.Size = New System.Drawing.Size(98, 82)
        Me.DownloadImage.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(478, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Image Identifier:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(25, 148)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(107, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Add Download"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.GripMargin = New System.Windows.Forms.Padding(0)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripProgressBar1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 314)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(690, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        Me.ToolStripProgressBar1.Visible = False
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(0, 17)
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(251, 148)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(107, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Remove Download"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(585, 206)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Browse"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select the path to save the images to:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Next Download Cycle:"
        '
        'downloadcycle
        '
        Me.downloadcycle.AutoSize = True
        Me.downloadcycle.ForeColor = System.Drawing.Color.Green
        Me.downloadcycle.Location = New System.Drawing.Point(161, 292)
        Me.downloadcycle.Name = "downloadcycle"
        Me.downloadcycle.Size = New System.Drawing.Size(19, 13)
        Me.downloadcycle.TabIndex = 11
        Me.downloadcycle.Text = "10"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(364, 245)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(118, 24)
        Me.Button4.TabIndex = 12
        Me.Button4.Text = "Cancel Download"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Location = New System.Drawing.Point(240, 245)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(118, 24)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Force Download"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "csd"
        Me.SaveFileDialog1.Filter = "Comic Strip Downloader Config Files|*.csd|All files|*.*"
        Me.SaveFileDialog1.Title = "Save your Download List to the following location:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "csd"
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Comic Strip Downloader Config Files|*.csd|All files|*.*"
        Me.OpenFileDialog1.Title = "Load a Download List from the following location:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(543, 245)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(62, 13)
        Me.LinkLabel1.TabIndex = 16
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Activity Log"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(611, 245)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(50, 13)
        Me.LinkLabel2.TabIndex = 17
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Error Log"
        '
        'Silent_CheckBox
        '
        Me.Silent_CheckBox.AutoSize = True
        Me.Silent_CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Silent_CheckBox.Location = New System.Drawing.Point(626, 12)
        Me.Silent_CheckBox.Name = "Silent_CheckBox"
        Me.Silent_CheckBox.Size = New System.Drawing.Size(52, 17)
        Me.Silent_CheckBox.TabIndex = 24
        Me.Silent_CheckBox.Text = "Silent"
        Me.ToolTip1.SetToolTip(Me.Silent_CheckBox, "Toggle for silent downloading")
        Me.Silent_CheckBox.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(343, 41)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(15, 15)
        Me.Button7.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.Button7, "Sort on URL")
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(460, 40)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(15, 15)
        Me.Button8.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.Button8, "Sort on Comic Name")
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(564, 40)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(15, 15)
        Me.Button9.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.Button9, "Sort on Image Identifier")
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(646, 39)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(15, 15)
        Me.Button10.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.Button10, "Sort on Save Prefix")
        Me.Button10.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.HelpToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(690, 24)
        Me.MenuStrip1.TabIndex = 18
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportDownloadListToolStripMenuItem, Me.ExportDownloadListToolStripMenuItem, Me.ToolStripSeparator4, Me.ProxyUsernamePasswordToolStripMenuItem, Me.ToolStripSeparator3, Me.ToolStripMenuItem2, Me.GenerateWebPageCodeToolStripMenuItem, Me.ToolStripSeparator2, Me.ToolStripMenuItem1, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'ImportDownloadListToolStripMenuItem
        '
        Me.ImportDownloadListToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ImportDownloadListToolStripMenuItem.Name = "ImportDownloadListToolStripMenuItem"
        Me.ImportDownloadListToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ImportDownloadListToolStripMenuItem.Text = "Import Download List"
        Me.ImportDownloadListToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ExportDownloadListToolStripMenuItem
        '
        Me.ExportDownloadListToolStripMenuItem.Name = "ExportDownloadListToolStripMenuItem"
        Me.ExportDownloadListToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ExportDownloadListToolStripMenuItem.Text = "Export Download List"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(211, 6)
        '
        'ProxyUsernamePasswordToolStripMenuItem
        '
        Me.ProxyUsernamePasswordToolStripMenuItem.Name = "ProxyUsernamePasswordToolStripMenuItem"
        Me.ProxyUsernamePasswordToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ProxyUsernamePasswordToolStripMenuItem.Text = "Proxy Username/Password"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(211, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(214, 22)
        Me.ToolStripMenuItem2.Text = "Generate RSS Code"
        '
        'GenerateWebPageCodeToolStripMenuItem
        '
        Me.GenerateWebPageCodeToolStripMenuItem.Name = "GenerateWebPageCodeToolStripMenuItem"
        Me.GenerateWebPageCodeToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.GenerateWebPageCodeToolStripMenuItem.Text = "Generate Web Page Code"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(211, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(214, 22)
        Me.ToolStripMenuItem1.Text = "Minimise Application"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(211, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem1, Me.AutoUpdateToolStripMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(143, 22)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'AutoUpdateToolStripMenuItem
        '
        Me.AutoUpdateToolStripMenuItem.Name = "AutoUpdateToolStripMenuItem"
        Me.AutoUpdateToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.AutoUpdateToolStripMenuItem.Text = "AutoUpdate"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Comic Strip Downloader"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowMainScreenToolStripMenuItem, Me.MinimiseMaToolStripMenuItem, Me.InToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(186, 70)
        '
        'ShowMainScreenToolStripMenuItem
        '
        Me.ShowMainScreenToolStripMenuItem.Name = "ShowMainScreenToolStripMenuItem"
        Me.ShowMainScreenToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ShowMainScreenToolStripMenuItem.Text = "Show Main Screen"
        '
        'MinimiseMaToolStripMenuItem
        '
        Me.MinimiseMaToolStripMenuItem.Name = "MinimiseMaToolStripMenuItem"
        Me.MinimiseMaToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.MinimiseMaToolStripMenuItem.Text = "Minimise Main Screen"
        '
        'InToolStripMenuItem
        '
        Me.InToolStripMenuItem.Name = "InToolStripMenuItem"
        Me.InToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.InToolStripMenuItem.Text = "Exit"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label5.Location = New System.Drawing.Point(559, 270)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Minimise Application"
        '
        'filesdownloaded
        '
        Me.filesdownloaded.AutoSize = True
        Me.filesdownloaded.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.filesdownloaded.Location = New System.Drawing.Point(161, 245)
        Me.filesdownloaded.Name = "filesdownloaded"
        Me.filesdownloaded.Size = New System.Drawing.Size(13, 13)
        Me.filesdownloaded.TabIndex = 22
        Me.filesdownloaded.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 245)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Total Files Downloaded:"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(138, 148)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(107, 23)
        Me.Button6.TabIndex = 23
        Me.Button6.Text = "Edit Download"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'DownloadPrefix
        '
        Me.DownloadPrefix.FormattingEnabled = True
        Me.DownloadPrefix.Location = New System.Drawing.Point(585, 57)
        Me.DownloadPrefix.Name = "DownloadPrefix"
        Me.DownloadPrefix.Size = New System.Drawing.Size(76, 82)
        Me.DownloadPrefix.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(582, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Save Prefix:"
        '
        'filesdownloadedthiscycle
        '
        Me.filesdownloadedthiscycle.AutoSize = True
        Me.filesdownloadedthiscycle.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.filesdownloadedthiscycle.Location = New System.Drawing.Point(161, 270)
        Me.filesdownloadedthiscycle.Name = "filesdownloadedthiscycle"
        Me.filesdownloadedthiscycle.Size = New System.Drawing.Size(13, 13)
        Me.filesdownloadedthiscycle.TabIndex = 28
        Me.filesdownloadedthiscycle.Text = "0"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 270)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(134, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Files Grabbed in this Cycle:"
        '
        'SaveFileDialog2
        '
        Me.SaveFileDialog2.DefaultExt = "asp"
        Me.SaveFileDialog2.Filter = "ASP Files|*.asp|All files|*.*"
        Me.SaveFileDialog2.Title = "Save your Download List to the following location:"
        '
        'DownloadComic
        '
        Me.DownloadComic.FormattingEnabled = True
        Me.DownloadComic.Location = New System.Drawing.Point(364, 57)
        Me.DownloadComic.Name = "DownloadComic"
        Me.DownloadComic.Size = New System.Drawing.Size(111, 82)
        Me.DownloadComic.TabIndex = 29
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(361, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Comic Name:"
        '
        'LastActionLabel
        '
        Me.LastActionLabel.ForeColor = System.Drawing.Color.Maroon
        Me.LastActionLabel.Location = New System.Drawing.Point(237, 292)
        Me.LastActionLabel.Name = "LastActionLabel"
        Me.LastActionLabel.Size = New System.Drawing.Size(423, 13)
        Me.LastActionLabel.TabIndex = 31
        '
        'NumberDownloadEntries
        '
        Me.NumberDownloadEntries.ForeColor = System.Drawing.Color.Maroon
        Me.NumberDownloadEntries.Location = New System.Drawing.Point(482, 142)
        Me.NumberDownloadEntries.Name = "NumberDownloadEntries"
        Me.NumberDownloadEntries.Size = New System.Drawing.Size(178, 13)
        Me.NumberDownloadEntries.TabIndex = 36
        Me.NumberDownloadEntries.Text = "0 Download Entries"
        Me.NumberDownloadEntries.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'CurrentlySelected
        '
        Me.CurrentlySelected.ForeColor = System.Drawing.Color.Gray
        Me.CurrentlySelected.Location = New System.Drawing.Point(482, 158)
        Me.CurrentlySelected.Name = "CurrentlySelected"
        Me.CurrentlySelected.Size = New System.Drawing.Size(178, 13)
        Me.CurrentlySelected.TabIndex = 37
        Me.CurrentlySelected.Text = "Currently Selected: 0 of 0"
        Me.CurrentlySelected.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SendMail_CheckBox
        '
        Me.SendMail_CheckBox.AutoSize = True
        Me.SendMail_CheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SendMail_CheckBox.Location = New System.Drawing.Point(547, 12)
        Me.SendMail_CheckBox.Name = "SendMail_CheckBox"
        Me.SendMail_CheckBox.Size = New System.Drawing.Size(73, 17)
        Me.SendMail_CheckBox.TabIndex = 38
        Me.SendMail_CheckBox.Text = "Send Mail"
        Me.ToolTip1.SetToolTip(Me.SendMail_CheckBox, "Toggle to control the sending out of email notifications")
        Me.SendMail_CheckBox.UseVisualStyleBackColor = True
        '
        'Main_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 336)
        Me.Controls.Add(Me.SendMail_CheckBox)
        Me.Controls.Add(Me.CurrentlySelected)
        Me.Controls.Add(Me.NumberDownloadEntries)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.LastActionLabel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DownloadComic)
        Me.Controls.Add(Me.filesdownloadedthiscycle)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DownloadPrefix)
        Me.Controls.Add(Me.Silent_CheckBox)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.filesdownloaded)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.downloadcycle)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SavePath)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DownloadImage)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DownloadPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Main_Screen"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comic Strip Downloader"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SavePath As System.Windows.Forms.TextBox
    Friend WithEvents DownloadPath As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DownloadImage As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents downloadcycle As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportDownloadListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportDownloadListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowMainScreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinimiseMaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents filesdownloaded As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Silent_CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DownloadPrefix As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents filesdownloadedthiscycle As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog2 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents DownloadComic As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ProxyUsernamePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateWebPageCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutoUpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LastActionLabel As System.Windows.Forms.Label
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents NumberDownloadEntries As System.Windows.Forms.Label
    Friend WithEvents CurrentlySelected As System.Windows.Forms.Label
    Friend WithEvents SendMail_CheckBox As System.Windows.Forms.CheckBox

End Class
