<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Menu = New ToolStrip()
        ToolStripDropDownButton1 = New ToolStripDropDownButton()
        Menu_Open = New ToolStripMenuItem()
        Menu_Import = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        Menu_Close = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        Menu_Config = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        Menu_Auth = New ToolStripMenuItem()
        Menu_About = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        Menu_Exit = New ToolStripMenuItem()
        btnGrid = New ToolStripButton()
        btnPanel = New ToolStripButton()
        btnHmi = New ToolStripButton()
        lblDateTime = New ToolStripLabel()
        lblCtDn = New ToolStripLabel()
        lblAuth = New ToolStripLabel()
        pnlMain = New Panel()
        tmSec = New Timer(components)
        stsBottom = New StatusStrip()
        slblIf = New ToolStripStatusLabel()
        slblIfCount = New ToolStripStatusLabel()
        slblIfDefault = New ToolStripStatusLabel()
        slblPolling = New ToolStripStatusLabel()
        slblScd = New ToolStripStatusLabel()
        slblScdState = New ToolStripStatusLabel()
        slblSpring = New ToolStripStatusLabel()
        slblGithub = New ToolStripStatusLabel()
        Menu.SuspendLayout()
        stsBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' Menu
        ' 
        Menu.GripStyle = ToolStripGripStyle.Hidden
        Menu.ImageScalingSize = New Size(20, 20)
        Menu.Items.AddRange(New ToolStripItem() {ToolStripDropDownButton1, btnGrid, btnPanel, btnHmi, lblDateTime, lblCtDn, lblAuth})
        Menu.Location = New Point(0, 0)
        Menu.Name = "Menu"
        Menu.Padding = New Padding(5, 2, 1, 0)
        Menu.RenderMode = ToolStripRenderMode.System
        Menu.Size = New Size(1282, 29)
        Menu.TabIndex = 2
        Menu.Text = "ToolStrip1"
        ' 
        ' ToolStripDropDownButton1
        ' 
        ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton1.DropDownItems.AddRange(New ToolStripItem() {Menu_Open, Menu_Import, ToolStripSeparator1, Menu_Close, ToolStripSeparator2, Menu_Config, ToolStripSeparator3, Menu_Auth, Menu_About, ToolStripSeparator4, Menu_Exit})
        ToolStripDropDownButton1.Image = My.Resources.Resources.logo_32x18
        ToolStripDropDownButton1.ImageScaling = ToolStripItemImageScaling.None
        ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton1.Margin = New Padding(0, 1, 5, 2)
        ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        ToolStripDropDownButton1.Padding = New Padding(10, 0, 0, 0)
        ToolStripDropDownButton1.Size = New Size(56, 24)
        ToolStripDropDownButton1.Text = " 主菜单"
        ' 
        ' Menu_Open
        ' 
        Menu_Open.Name = "Menu_Open"
        Menu_Open.ShortcutKeys = Keys.Control Or Keys.O
        Menu_Open.Size = New Size(232, 26)
        Menu_Open.Text = "打开项目(&O)"
        ' 
        ' Menu_Import
        ' 
        Menu_Import.Enabled = False
        Menu_Import.Name = "Menu_Import"
        Menu_Import.Size = New Size(232, 26)
        Menu_Import.Text = "从数据库导入(&I)"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(229, 6)
        ' 
        ' Menu_Close
        ' 
        Menu_Close.Name = "Menu_Close"
        Menu_Close.Size = New Size(232, 26)
        Menu_Close.Text = "关闭项目(&C)"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(229, 6)
        ' 
        ' Menu_Config
        ' 
        Menu_Config.Name = "Menu_Config"
        Menu_Config.Size = New Size(232, 26)
        Menu_Config.Text = "设置"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(229, 6)
        ' 
        ' Menu_Auth
        ' 
        Menu_Auth.Name = "Menu_Auth"
        Menu_Auth.Size = New Size(232, 26)
        Menu_Auth.Text = "授权信息"
        ' 
        ' Menu_About
        ' 
        Menu_About.Name = "Menu_About"
        Menu_About.Size = New Size(232, 26)
        Menu_About.Text = "关于(&A)"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(229, 6)
        ' 
        ' Menu_Exit
        ' 
        Menu_Exit.Name = "Menu_Exit"
        Menu_Exit.Size = New Size(232, 26)
        Menu_Exit.Text = "退出(&X)"
        ' 
        ' btnGrid
        ' 
        btnGrid.Enabled = False
        btnGrid.Image = My.Resources.Resources.view_grid
        btnGrid.ImageTransparentColor = Color.Magenta
        btnGrid.Name = "btnGrid"
        btnGrid.Size = New Size(93, 24)
        btnGrid.Text = "表格视图"
        ' 
        ' btnPanel
        ' 
        btnPanel.Enabled = False
        btnPanel.Image = My.Resources.Resources.view_panel
        btnPanel.ImageTransparentColor = Color.Magenta
        btnPanel.Name = "btnPanel"
        btnPanel.Size = New Size(93, 24)
        btnPanel.Text = "控件视图"
        btnPanel.Visible = False
        ' 
        ' btnHmi
        ' 
        btnHmi.Enabled = False
        btnHmi.Image = My.Resources.Resources.view_graphics
        btnHmi.ImageTransparentColor = Color.Magenta
        btnHmi.Name = "btnHmi"
        btnHmi.Size = New Size(93, 24)
        btnHmi.Text = "图形界面"
        ' 
        ' lblDateTime
        ' 
        lblDateTime.Alignment = ToolStripItemAlignment.Right
        lblDateTime.Margin = New Padding(100, 1, 0, 2)
        lblDateTime.Name = "lblDateTime"
        lblDateTime.Size = New Size(0, 24)
        ' 
        ' lblCtDn
        ' 
        lblCtDn.Alignment = ToolStripItemAlignment.Right
        lblCtDn.BackColor = Color.Azure
        lblCtDn.ForeColor = Color.Gray
        lblCtDn.Name = "lblCtDn"
        lblCtDn.Size = New Size(0, 24)
        lblCtDn.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblAuth
        ' 
        lblAuth.Alignment = ToolStripItemAlignment.Right
        lblAuth.BackColor = Color.Azure
        lblAuth.Name = "lblAuth"
        lblAuth.Size = New Size(108, 24)
        lblAuth.Text = "Authorization"
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.White
        pnlMain.BackgroundImageLayout = ImageLayout.Zoom
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 29)
        pnlMain.Name = "pnlMain"
        pnlMain.Size = New Size(1282, 598)
        pnlMain.TabIndex = 3
        ' 
        ' tmSec
        ' 
        tmSec.Enabled = True
        tmSec.Interval = 1000
        ' 
        ' stsBottom
        ' 
        stsBottom.ImageScalingSize = New Size(20, 20)
        stsBottom.Items.AddRange(New ToolStripItem() {slblIf, slblIfCount, slblIfDefault, slblPolling, slblScd, slblScdState, slblSpring, slblGithub})
        stsBottom.Location = New Point(0, 627)
        stsBottom.Name = "stsBottom"
        stsBottom.Size = New Size(1282, 26)
        stsBottom.SizingGrip = False
        stsBottom.TabIndex = 5
        ' 
        ' slblIf
        ' 
        slblIf.Name = "slblIf"
        slblIf.Size = New Size(84, 20)
        slblIf.Text = "接口连接："
        ' 
        ' slblIfCount
        ' 
        slblIfCount.ForeColor = Color.Gray
        slblIfCount.Name = "slblIfCount"
        slblIfCount.Size = New Size(77, 20)
        slblIfCount.Text = "Unknown"
        ' 
        ' slblIfDefault
        ' 
        slblIfDefault.ForeColor = Color.Green
        slblIfDefault.Name = "slblIfDefault"
        slblIfDefault.Size = New Size(39, 20)
        slblIfDefault.Text = "(+1)"
        slblIfDefault.Visible = False
        ' 
        ' slblPolling
        ' 
        slblPolling.ForeColor = Color.Red
        slblPolling.Margin = New Padding(10, 4, 0, 2)
        slblPolling.Name = "slblPolling"
        slblPolling.Size = New Size(78, 20)
        slblPolling.Text = "轮询中……"
        slblPolling.Visible = False
        ' 
        ' slblScd
        ' 
        slblScd.Margin = New Padding(15, 4, 0, 2)
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
        slblSpring.Size = New Size(285, 20)
        slblSpring.Spring = True
        ' 
        ' slblGithub
        ' 
        slblGithub.IsLink = True
        slblGithub.Name = "slblGithub"
        slblGithub.Size = New Size(478, 20)
        slblGithub.Text = "https://github.com/OuroborosSoftwareFoundation/BedivereKnx"
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1282, 653)
        Controls.Add(pnlMain)
        Controls.Add(Menu)
        Controls.Add(stsBottom)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        IsMdiContainer = True
        Name = "frmMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = "BedivereKnx.Client"
        Menu.ResumeLayout(False)
        Menu.PerformLayout()
        stsBottom.ResumeLayout(False)
        stsBottom.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Menu As ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents Menu_Open As ToolStripMenuItem
    Friend WithEvents Menu_Import As ToolStripMenuItem
    Friend WithEvents Menu_Config As ToolStripMenuItem
    Friend WithEvents Menu_About As ToolStripMenuItem
    Friend WithEvents Menu_Exit As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents pnlMain As Panel
    Friend WithEvents Menu_Auth As ToolStripMenuItem
    Friend WithEvents lblDateTime As ToolStripLabel
    Friend WithEvents tmSec As Timer
    Friend WithEvents lblAuth As ToolStripLabel
    Friend WithEvents lblCtDn As ToolStripLabel
    Friend WithEvents stsBottom As StatusStrip
    Friend WithEvents slblIf As ToolStripStatusLabel
    Friend WithEvents slblIfCount As ToolStripStatusLabel
    Friend WithEvents slblIfDefault As ToolStripStatusLabel
    Friend WithEvents slblScd As ToolStripStatusLabel
    Friend WithEvents slblScdState As ToolStripStatusLabel
    Friend WithEvents slblSpring As ToolStripStatusLabel
    Friend WithEvents slblGithub As ToolStripStatusLabel
    Friend WithEvents btnGrid As ToolStripButton
    Friend WithEvents btnPanel As ToolStripButton
    Friend WithEvents btnHmi As ToolStripButton
    Friend WithEvents Menu_Close As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents slblPolling As ToolStripStatusLabel
End Class
