<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainTable
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        dgvObject = New DataGridView()
        tlpMain = New TableLayoutPanel()
        tlpTool = New TableLayoutPanel()
        btnInterface = New Button()
        btnSchedule = New Button()
        tvArea = New TreeView()
        tabMain = New TabControl()
        tpObject = New TabPage()
        tlpObject = New TableLayoutPanel()
        btnCtlFalse = New Button()
        btnCtlTrue = New Button()
        numObjVal = New TrackBar()
        Label1 = New Label()
        lblObjVal = New Label()
        tpScene = New TabPage()
        dgvScene = New DataGridView()
        tlpScene = New TableLayoutPanel()
        btnCtlScn = New Button()
        tpDevice = New TabPage()
        dgvDevice = New DataGridView()
        tpSchedule = New TabPage()
        dgvSchedule = New DataGridView()
        tpLink = New TabPage()
        dgvLink = New DataGridView()
        tpInterface = New TabPage()
        dgvIf = New DataGridView()
        TableLayoutPanel1 = New TableLayoutPanel()
        btnConnect = New Button()
        lstTelLog = New ListBox()
        menuTelLog = New ContextMenuStrip(components)
        btnTelLogClear = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnTelLogExport = New ToolStripMenuItem()
        tmPoll = New Timer(components)
        StsStrip = New StatusStrip()
        slblIf = New ToolStripStatusLabel()
        slblIfCount = New ToolStripStatusLabel()
        slblIfDefault = New ToolStripStatusLabel()
        slblBlank = New ToolStripStatusLabel()
        slblScd = New ToolStripStatusLabel()
        slblScdState = New ToolStripStatusLabel()
        slblSpring = New ToolStripStatusLabel()
        slblGithub = New ToolStripStatusLabel()
        CType(dgvObject, ComponentModel.ISupportInitialize).BeginInit()
        tlpMain.SuspendLayout()
        tlpTool.SuspendLayout()
        tabMain.SuspendLayout()
        tpObject.SuspendLayout()
        tlpObject.SuspendLayout()
        CType(numObjVal, ComponentModel.ISupportInitialize).BeginInit()
        tpScene.SuspendLayout()
        CType(dgvScene, ComponentModel.ISupportInitialize).BeginInit()
        tlpScene.SuspendLayout()
        tpDevice.SuspendLayout()
        CType(dgvDevice, ComponentModel.ISupportInitialize).BeginInit()
        tpSchedule.SuspendLayout()
        CType(dgvSchedule, ComponentModel.ISupportInitialize).BeginInit()
        tpLink.SuspendLayout()
        CType(dgvLink, ComponentModel.ISupportInitialize).BeginInit()
        tpInterface.SuspendLayout()
        CType(dgvIf, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        menuTelLog.SuspendLayout()
        StsStrip.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvObject
        ' 
        dgvObject.AllowUserToAddRows = False
        dgvObject.AllowUserToDeleteRows = False
        dgvObject.AllowUserToResizeRows = False
        dgvObject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvObject.BackgroundColor = SystemColors.Window
        dgvObject.ColumnHeadersHeight = 29
        dgvObject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvObject.Dock = DockStyle.Fill
        dgvObject.Location = New Point(0, 0)
        dgvObject.Name = "dgvObject"
        dgvObject.ReadOnly = True
        dgvObject.RowHeadersVisible = False
        dgvObject.RowHeadersWidth = 51
        dgvObject.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvObject.ShowCellErrors = False
        dgvObject.ShowCellToolTips = False
        dgvObject.ShowEditingIcon = False
        dgvObject.ShowRowErrors = False
        dgvObject.Size = New Size(936, 411)
        dgvObject.TabIndex = 1
        dgvObject.Tag = "Objects"
        dgvObject.VirtualMode = True
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 2
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 250F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(tlpTool, 0, 1)
        tlpMain.Controls.Add(tvArea, 0, 0)
        tlpMain.Controls.Add(tabMain, 1, 0)
        tlpMain.Controls.Add(lstTelLog, 1, 1)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 2
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 100F))
        tlpMain.Size = New Size(1200, 600)
        tlpMain.TabIndex = 0
        ' 
        ' tlpTool
        ' 
        tlpTool.ColumnCount = 1
        tlpTool.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpTool.Controls.Add(btnInterface, 0, 0)
        tlpTool.Controls.Add(btnSchedule, 0, 1)
        tlpTool.Dock = DockStyle.Fill
        tlpTool.Location = New Point(3, 503)
        tlpTool.Name = "tlpTool"
        tlpTool.RowCount = 2
        tlpTool.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpTool.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpTool.Size = New Size(244, 94)
        tlpTool.TabIndex = 5
        ' 
        ' btnInterface
        ' 
        btnInterface.Dock = DockStyle.Fill
        btnInterface.Location = New Point(3, 3)
        btnInterface.Name = "btnInterface"
        btnInterface.Size = New Size(238, 41)
        btnInterface.TabIndex = 0
        btnInterface.Text = "KNX接口"
        btnInterface.UseVisualStyleBackColor = True
        ' 
        ' btnSchedule
        ' 
        btnSchedule.Dock = DockStyle.Fill
        btnSchedule.Location = New Point(3, 50)
        btnSchedule.Name = "btnSchedule"
        btnSchedule.Size = New Size(238, 41)
        btnSchedule.TabIndex = 0
        btnSchedule.Text = "定时队列"
        btnSchedule.UseVisualStyleBackColor = True
        ' 
        ' tvArea
        ' 
        tvArea.Dock = DockStyle.Fill
        tvArea.Location = New Point(3, 3)
        tvArea.Name = "tvArea"
        tvArea.Size = New Size(244, 494)
        tvArea.TabIndex = 4
        ' 
        ' tabMain
        ' 
        tabMain.Controls.Add(tpObject)
        tabMain.Controls.Add(tpScene)
        tabMain.Controls.Add(tpDevice)
        tabMain.Controls.Add(tpSchedule)
        tabMain.Controls.Add(tpLink)
        tabMain.Controls.Add(tpInterface)
        tabMain.Dock = DockStyle.Fill
        tabMain.Location = New Point(253, 3)
        tabMain.Name = "tabMain"
        tabMain.SelectedIndex = 0
        tabMain.Size = New Size(944, 494)
        tabMain.SizeMode = TabSizeMode.Fixed
        tabMain.TabIndex = 1
        ' 
        ' tpObject
        ' 
        tpObject.BackColor = SystemColors.Control
        tpObject.Controls.Add(dgvObject)
        tpObject.Controls.Add(tlpObject)
        tpObject.Location = New Point(4, 29)
        tpObject.Name = "tpObject"
        tpObject.Size = New Size(936, 461)
        tpObject.TabIndex = 1
        tpObject.Tag = "Object"
        tpObject.Text = "对象"
        ' 
        ' tlpObject
        ' 
        tlpObject.ColumnCount = 5
        tlpObject.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 36.363636F))
        tlpObject.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 36.363636F))
        tlpObject.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 60F))
        tlpObject.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 27.272728F))
        tlpObject.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 50F))
        tlpObject.Controls.Add(btnCtlFalse, 1, 0)
        tlpObject.Controls.Add(btnCtlTrue, 0, 0)
        tlpObject.Controls.Add(numObjVal, 3, 0)
        tlpObject.Controls.Add(Label1, 2, 0)
        tlpObject.Controls.Add(lblObjVal, 4, 0)
        tlpObject.Dock = DockStyle.Bottom
        tlpObject.Location = New Point(0, 411)
        tlpObject.Name = "tlpObject"
        tlpObject.RowCount = 1
        tlpObject.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpObject.Size = New Size(936, 50)
        tlpObject.TabIndex = 2
        ' 
        ' btnCtlFalse
        ' 
        btnCtlFalse.Dock = DockStyle.Fill
        btnCtlFalse.Location = New Point(303, 3)
        btnCtlFalse.Name = "btnCtlFalse"
        btnCtlFalse.Size = New Size(294, 44)
        btnCtlFalse.TabIndex = 0
        btnCtlFalse.Tag = "0"
        btnCtlFalse.Text = "关"
        btnCtlFalse.UseVisualStyleBackColor = True
        ' 
        ' btnCtlTrue
        ' 
        btnCtlTrue.Dock = DockStyle.Fill
        btnCtlTrue.Location = New Point(3, 3)
        btnCtlTrue.Name = "btnCtlTrue"
        btnCtlTrue.Size = New Size(294, 44)
        btnCtlTrue.TabIndex = 0
        btnCtlTrue.Tag = "1"
        btnCtlTrue.Text = "开"
        btnCtlTrue.UseVisualStyleBackColor = True
        ' 
        ' numObjVal
        ' 
        numObjVal.Dock = DockStyle.Fill
        numObjVal.Enabled = False
        numObjVal.LargeChange = 10
        numObjVal.Location = New Point(663, 3)
        numObjVal.Maximum = 100
        numObjVal.Name = "numObjVal"
        numObjVal.Size = New Size(219, 44)
        numObjVal.SmallChange = 5
        numObjVal.TabIndex = 1
        numObjVal.TickFrequency = 10
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(603, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(54, 50)
        Label1.TabIndex = 2
        Label1.Text = "亮度"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblObjVal
        ' 
        lblObjVal.Dock = DockStyle.Fill
        lblObjVal.Location = New Point(888, 0)
        lblObjVal.Name = "lblObjVal"
        lblObjVal.Size = New Size(45, 50)
        lblObjVal.TabIndex = 3
        lblObjVal.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tpScene
        ' 
        tpScene.BackColor = SystemColors.Control
        tpScene.Controls.Add(dgvScene)
        tpScene.Controls.Add(tlpScene)
        tpScene.Location = New Point(4, 29)
        tpScene.Name = "tpScene"
        tpScene.Size = New Size(936, 435)
        tpScene.TabIndex = 0
        tpScene.Tag = "Scene"
        tpScene.Text = "场景"
        ' 
        ' dgvScene
        ' 
        dgvScene.AllowUserToAddRows = False
        dgvScene.AllowUserToDeleteRows = False
        dgvScene.AllowUserToResizeRows = False
        dgvScene.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvScene.BackgroundColor = SystemColors.Window
        dgvScene.ColumnHeadersHeight = 29
        dgvScene.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvScene.Dock = DockStyle.Fill
        dgvScene.Location = New Point(0, 0)
        dgvScene.MultiSelect = False
        dgvScene.Name = "dgvScene"
        dgvScene.ReadOnly = True
        dgvScene.RowHeadersVisible = False
        dgvScene.RowHeadersWidth = 51
        dgvScene.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvScene.ShowCellErrors = False
        dgvScene.ShowCellToolTips = False
        dgvScene.ShowEditingIcon = False
        dgvScene.ShowRowErrors = False
        dgvScene.Size = New Size(936, 385)
        dgvScene.TabIndex = 0
        dgvScene.Tag = "Scenes"
        ' 
        ' tlpScene
        ' 
        tlpScene.ColumnCount = 2
        tlpScene.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpScene.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpScene.Controls.Add(btnCtlScn, 0, 0)
        tlpScene.Dock = DockStyle.Bottom
        tlpScene.Location = New Point(0, 385)
        tlpScene.Name = "tlpScene"
        tlpScene.RowCount = 1
        tlpScene.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpScene.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpScene.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpScene.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpScene.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpScene.Size = New Size(936, 50)
        tlpScene.TabIndex = 1
        ' 
        ' btnCtlScn
        ' 
        btnCtlScn.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        tlpScene.SetColumnSpan(btnCtlScn, 2)
        btnCtlScn.Location = New Point(396, 3)
        btnCtlScn.Name = "btnCtlScn"
        btnCtlScn.Size = New Size(144, 44)
        btnCtlScn.TabIndex = 0
        btnCtlScn.Text = "场景控制"
        btnCtlScn.UseVisualStyleBackColor = True
        ' 
        ' tpDevice
        ' 
        tpDevice.Controls.Add(dgvDevice)
        tpDevice.Location = New Point(4, 29)
        tpDevice.Name = "tpDevice"
        tpDevice.Size = New Size(936, 435)
        tpDevice.TabIndex = 4
        tpDevice.Tag = "Device"
        tpDevice.Text = "设备"
        tpDevice.UseVisualStyleBackColor = True
        ' 
        ' dgvDevice
        ' 
        dgvDevice.AllowUserToAddRows = False
        dgvDevice.AllowUserToDeleteRows = False
        dgvDevice.AllowUserToResizeRows = False
        dgvDevice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvDevice.BackgroundColor = SystemColors.Window
        dgvDevice.ColumnHeadersHeight = 29
        dgvDevice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvDevice.Dock = DockStyle.Fill
        dgvDevice.Location = New Point(0, 0)
        dgvDevice.Name = "dgvDevice"
        dgvDevice.ReadOnly = True
        dgvDevice.RowHeadersVisible = False
        dgvDevice.RowHeadersWidth = 51
        dgvDevice.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDevice.ShowCellErrors = False
        dgvDevice.ShowCellToolTips = False
        dgvDevice.ShowEditingIcon = False
        dgvDevice.ShowRowErrors = False
        dgvDevice.Size = New Size(936, 435)
        dgvDevice.TabIndex = 2
        dgvDevice.Tag = "Objects"
        dgvDevice.VirtualMode = True
        ' 
        ' tpSchedule
        ' 
        tpSchedule.BackColor = SystemColors.Control
        tpSchedule.Controls.Add(dgvSchedule)
        tpSchedule.Location = New Point(4, 29)
        tpSchedule.Name = "tpSchedule"
        tpSchedule.Size = New Size(936, 435)
        tpSchedule.TabIndex = 2
        tpSchedule.Tag = "Schedule"
        tpSchedule.Text = "定时"
        ' 
        ' dgvSchedule
        ' 
        dgvSchedule.AllowUserToAddRows = False
        dgvSchedule.AllowUserToDeleteRows = False
        dgvSchedule.AllowUserToResizeRows = False
        dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvSchedule.BackgroundColor = SystemColors.Window
        dgvSchedule.ColumnHeadersHeight = 29
        dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvSchedule.Dock = DockStyle.Fill
        dgvSchedule.Location = New Point(0, 0)
        dgvSchedule.Margin = New Padding(0)
        dgvSchedule.MultiSelect = False
        dgvSchedule.Name = "dgvSchedule"
        dgvSchedule.ReadOnly = True
        dgvSchedule.RowHeadersVisible = False
        dgvSchedule.RowHeadersWidth = 51
        dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSchedule.ShowCellErrors = False
        dgvSchedule.ShowCellToolTips = False
        dgvSchedule.ShowEditingIcon = False
        dgvSchedule.ShowRowErrors = False
        dgvSchedule.Size = New Size(936, 435)
        dgvSchedule.TabIndex = 1
        ' 
        ' tpLink
        ' 
        tpLink.Controls.Add(dgvLink)
        tpLink.Location = New Point(4, 29)
        tpLink.Name = "tpLink"
        tpLink.Size = New Size(936, 435)
        tpLink.TabIndex = 5
        tpLink.Text = "链接"
        tpLink.UseVisualStyleBackColor = True
        ' 
        ' dgvLink
        ' 
        dgvLink.AllowUserToAddRows = False
        dgvLink.AllowUserToDeleteRows = False
        dgvLink.AllowUserToResizeRows = False
        dgvLink.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvLink.BackgroundColor = SystemColors.Window
        dgvLink.ColumnHeadersHeight = 29
        dgvLink.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvLink.Dock = DockStyle.Fill
        dgvLink.Location = New Point(0, 0)
        dgvLink.Name = "dgvLink"
        dgvLink.ReadOnly = True
        dgvLink.RowHeadersVisible = False
        dgvLink.RowHeadersWidth = 51
        dgvLink.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLink.ShowCellErrors = False
        dgvLink.ShowCellToolTips = False
        dgvLink.ShowEditingIcon = False
        dgvLink.ShowRowErrors = False
        dgvLink.Size = New Size(936, 435)
        dgvLink.TabIndex = 3
        dgvLink.Tag = "Objects"
        dgvLink.VirtualMode = True
        ' 
        ' tpInterface
        ' 
        tpInterface.BackColor = SystemColors.Control
        tpInterface.Controls.Add(dgvIf)
        tpInterface.Controls.Add(TableLayoutPanel1)
        tpInterface.Location = New Point(4, 29)
        tpInterface.Name = "tpInterface"
        tpInterface.Size = New Size(936, 435)
        tpInterface.TabIndex = 3
        tpInterface.Tag = "Interface"
        tpInterface.Text = "接口"
        ' 
        ' dgvIf
        ' 
        dgvIf.AllowUserToAddRows = False
        dgvIf.AllowUserToDeleteRows = False
        dgvIf.AllowUserToResizeColumns = False
        dgvIf.AllowUserToResizeRows = False
        dgvIf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvIf.BackgroundColor = SystemColors.Window
        dgvIf.ColumnHeadersHeight = 29
        dgvIf.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvIf.Dock = DockStyle.Fill
        dgvIf.Location = New Point(0, 0)
        dgvIf.MultiSelect = False
        dgvIf.Name = "dgvIf"
        dgvIf.ReadOnly = True
        dgvIf.RowHeadersVisible = False
        dgvIf.RowHeadersWidth = 51
        dgvIf.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvIf.ShowCellErrors = False
        dgvIf.ShowCellToolTips = False
        dgvIf.ShowEditingIcon = False
        dgvIf.ShowRowErrors = False
        dgvIf.Size = New Size(936, 385)
        dgvIf.TabIndex = 3
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(btnConnect, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Bottom
        TableLayoutPanel1.Location = New Point(0, 385)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(936, 50)
        TableLayoutPanel1.TabIndex = 4
        ' 
        ' btnConnect
        ' 
        btnConnect.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        btnConnect.Location = New Point(368, 3)
        btnConnect.Name = "btnConnect"
        btnConnect.Size = New Size(200, 44)
        btnConnect.TabIndex = 0
        btnConnect.Text = "重新连接"
        btnConnect.UseVisualStyleBackColor = True
        ' 
        ' lstTelLog
        ' 
        lstTelLog.ContextMenuStrip = menuTelLog
        lstTelLog.Dock = DockStyle.Fill
        lstTelLog.FormattingEnabled = True
        lstTelLog.Location = New Point(253, 503)
        lstTelLog.Name = "lstTelLog"
        lstTelLog.Size = New Size(944, 94)
        lstTelLog.TabIndex = 1
        ' 
        ' menuTelLog
        ' 
        menuTelLog.ImageScalingSize = New Size(20, 20)
        menuTelLog.Items.AddRange(New ToolStripItem() {btnTelLogClear, ToolStripSeparator1, btnTelLogExport})
        menuTelLog.Name = "menuTelLog"
        menuTelLog.Size = New Size(139, 58)
        ' 
        ' btnTelLogClear
        ' 
        btnTelLogClear.Name = "btnTelLogClear"
        btnTelLogClear.Size = New Size(138, 24)
        btnTelLogClear.Text = "清空日志"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(135, 6)
        ' 
        ' btnTelLogExport
        ' 
        btnTelLogExport.Name = "btnTelLogExport"
        btnTelLogExport.Size = New Size(138, 24)
        btnTelLogExport.Text = "导出日志"
        ' 
        ' tmPoll
        ' 
        tmPoll.Interval = 3000
        ' 
        ' StsStrip
        ' 
        StsStrip.ImageScalingSize = New Size(20, 20)
        StsStrip.Items.AddRange(New ToolStripItem() {slblIf, slblIfCount, slblIfDefault, slblBlank, slblScd, slblScdState, slblSpring, slblGithub})
        StsStrip.Location = New Point(0, 574)
        StsStrip.Name = "StsStrip"
        StsStrip.Size = New Size(1200, 26)
        StsStrip.SizingGrip = False
        StsStrip.TabIndex = 1
        StsStrip.Visible = False
        ' 
        ' slblIf
        ' 
        slblIf.Name = "slblIf"
        slblIf.Size = New Size(84, 20)
        slblIf.Text = "接口连接："
        ' 
        ' slblIfCount
        ' 
        slblIfCount.Name = "slblIfCount"
        slblIfCount.Size = New Size(0, 20)
        ' 
        ' slblIfDefault
        ' 
        slblIfDefault.ForeColor = Color.Green
        slblIfDefault.Name = "slblIfDefault"
        slblIfDefault.Size = New Size(39, 20)
        slblIfDefault.Text = "(+1)"
        slblIfDefault.Visible = False
        ' 
        ' slblBlank
        ' 
        slblBlank.AutoSize = False
        slblBlank.Name = "slblBlank"
        slblBlank.Size = New Size(20, 20)
        ' 
        ' slblScd
        ' 
        slblScd.Name = "slblScd"
        slblScd.Size = New Size(99, 20)
        slblScd.Text = "定时器状态："
        ' 
        ' slblScdState
        ' 
        slblScdState.ForeColor = Color.Gray
        slblScdState.Name = "slblScdState"
        slblScdState.Size = New Size(63, 20)
        slblScdState.Text = "Stoped"
        ' 
        ' slblSpring
        ' 
        slblSpring.Name = "slblSpring"
        slblSpring.Size = New Size(360, 20)
        slblSpring.Spring = True
        ' 
        ' slblGithub
        ' 
        slblGithub.IsLink = True
        slblGithub.Name = "slblGithub"
        slblGithub.Size = New Size(520, 20)
        slblGithub.Text = "https://github.com/OuroborosSoftwareFoundation/BedivereKnxClient"
        ' 
        ' frmMainTable
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1200, 600)
        Controls.Add(tlpMain)
        Controls.Add(StsStrip)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmMainTable"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterParent
        Tag = "Children"
        CType(dgvObject, ComponentModel.ISupportInitialize).EndInit()
        tlpMain.ResumeLayout(False)
        tlpTool.ResumeLayout(False)
        tabMain.ResumeLayout(False)
        tpObject.ResumeLayout(False)
        tlpObject.ResumeLayout(False)
        tlpObject.PerformLayout()
        CType(numObjVal, ComponentModel.ISupportInitialize).EndInit()
        tpScene.ResumeLayout(False)
        CType(dgvScene, ComponentModel.ISupportInitialize).EndInit()
        tlpScene.ResumeLayout(False)
        tpDevice.ResumeLayout(False)
        CType(dgvDevice, ComponentModel.ISupportInitialize).EndInit()
        tpSchedule.ResumeLayout(False)
        CType(dgvSchedule, ComponentModel.ISupportInitialize).EndInit()
        tpLink.ResumeLayout(False)
        CType(dgvLink, ComponentModel.ISupportInitialize).EndInit()
        tpInterface.ResumeLayout(False)
        CType(dgvIf, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        menuTelLog.ResumeLayout(False)
        StsStrip.ResumeLayout(False)
        StsStrip.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tvArea As TreeView
    Friend WithEvents btnCtlTrue As Button
    Friend WithEvents btnCtlFalse As Button
    Friend WithEvents btnCtlScn As Button
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tpScene As TabPage
    Friend WithEvents dgvScene As DataGridView
    Friend WithEvents tpObject As TabPage
    Friend WithEvents dgvObject As DataGridView
    Friend WithEvents tpSchedule As TabPage
    Friend WithEvents dgvSchedule As DataGridView
    Friend WithEvents tpInterface As TabPage
    Friend WithEvents dgvIf As DataGridView
    Friend WithEvents tlpScene As TableLayoutPanel
    Friend WithEvents tlpObject As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btnConnect As Button
    Friend WithEvents tmPoll As Timer
    Friend WithEvents menuTelLog As ContextMenuStrip
    Friend WithEvents btnTelLogClear As ToolStripMenuItem
    Friend WithEvents btnTelLogExport As ToolStripMenuItem
    Friend WithEvents lstTelLog As ListBox
    Friend WithEvents numObjVal As TrackBar
    Friend WithEvents Label1 As Label
    Friend WithEvents lblObjVal As Label
    Friend WithEvents tlpTool As TableLayoutPanel
    Friend WithEvents btnInterface As Button
    Friend WithEvents btnSchedule As Button
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents StsStrip As StatusStrip
    Friend WithEvents slblScd As ToolStripStatusLabel
    Friend WithEvents slblScdState As ToolStripStatusLabel
    Friend WithEvents slblIf As ToolStripStatusLabel
    Friend WithEvents slblIfCount As ToolStripStatusLabel
    Friend WithEvents slblBlank As ToolStripStatusLabel
    Friend WithEvents slblSpring As ToolStripStatusLabel
    Friend WithEvents slblGithub As ToolStripStatusLabel
    Friend WithEvents tpDevice As TabPage
    Friend WithEvents dgvDevice As DataGridView
    Friend WithEvents slblIfDefault As ToolStripStatusLabel
    Friend WithEvents tpLink As TabPage
    Friend WithEvents dgvLink As DataGridView
End Class
