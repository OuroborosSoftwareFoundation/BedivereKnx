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
        Menu_Config = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        Menu_Auth = New ToolStripMenuItem()
        Menu_About = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        Menu_Exit = New ToolStripMenuItem()
        lblAuth = New ToolStripLabel()
        tbCtDn = New ToolStripLabel()
        lblDateTime = New ToolStripLabel()
        pnlMain = New Panel()
        tmSec = New Timer(components)
        Menu.SuspendLayout()
        SuspendLayout()
        ' 
        ' Menu
        ' 
        Menu.GripStyle = ToolStripGripStyle.Hidden
        Menu.ImageScalingSize = New Size(20, 20)
        Menu.Items.AddRange(New ToolStripItem() {ToolStripDropDownButton1, lblAuth, tbCtDn, lblDateTime})
        Menu.Location = New Point(0, 0)
        Menu.Name = "Menu"
        Menu.Padding = New Padding(5, 2, 1, 0)
        Menu.RenderMode = ToolStripRenderMode.System
        Menu.Size = New Size(1282, 27)
        Menu.TabIndex = 2
        Menu.Text = "ToolStrip1"
        ' 
        ' ToolStripDropDownButton1
        ' 
        ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripDropDownButton1.DropDownItems.AddRange(New ToolStripItem() {Menu_Open, Menu_Import, ToolStripSeparator1, Menu_Config, ToolStripSeparator2, Menu_Auth, Menu_About, ToolStripSeparator3, Menu_Exit})
        ToolStripDropDownButton1.Image = My.Resources.Resources.logo_32x18
        ToolStripDropDownButton1.ImageScaling = ToolStripItemImageScaling.None
        ToolStripDropDownButton1.ImageTransparentColor = Color.Magenta
        ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        ToolStripDropDownButton1.Padding = New Padding(10, 0, 0, 0)
        ToolStripDropDownButton1.Size = New Size(56, 22)
        ToolStripDropDownButton1.Text = " 主菜单"
        ' 
        ' Menu_Open
        ' 
        Menu_Open.Name = "Menu_Open"
        Menu_Open.ShortcutKeys = Keys.Control Or Keys.O
        Menu_Open.Size = New Size(224, 26)
        Menu_Open.Text = "打开(&O)"
        ' 
        ' Menu_Import
        ' 
        Menu_Import.Enabled = False
        Menu_Import.Name = "Menu_Import"
        Menu_Import.Size = New Size(224, 26)
        Menu_Import.Text = "导入(&I)"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(221, 6)
        ' 
        ' Menu_Config
        ' 
        Menu_Config.Name = "Menu_Config"
        Menu_Config.Size = New Size(224, 26)
        Menu_Config.Text = "设置(&C)"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(221, 6)
        ' 
        ' Menu_Auth
        ' 
        Menu_Auth.Name = "Menu_Auth"
        Menu_Auth.Size = New Size(224, 26)
        Menu_Auth.Text = "授权信息"
        ' 
        ' Menu_About
        ' 
        Menu_About.Name = "Menu_About"
        Menu_About.Size = New Size(224, 26)
        Menu_About.Text = "关于(&A)"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(221, 6)
        ' 
        ' Menu_Exit
        ' 
        Menu_Exit.Name = "Menu_Exit"
        Menu_Exit.Size = New Size(224, 26)
        Menu_Exit.Text = "退出(&X)"
        ' 
        ' lblAuth
        ' 
        lblAuth.Name = "lblAuth"
        lblAuth.Size = New Size(0, 22)
        ' 
        ' tbCtDn
        ' 
        tbCtDn.ForeColor = Color.Gray
        tbCtDn.Margin = New Padding(20, 1, 0, 2)
        tbCtDn.Name = "tbCtDn"
        tbCtDn.Size = New Size(0, 22)
        tbCtDn.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblDateTime
        ' 
        lblDateTime.Alignment = ToolStripItemAlignment.Right
        lblDateTime.Name = "lblDateTime"
        lblDateTime.Size = New Size(0, 22)
        ' 
        ' pnlMain
        ' 
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 27)
        pnlMain.Name = "pnlMain"
        pnlMain.Size = New Size(1282, 626)
        pnlMain.TabIndex = 3
        ' 
        ' tmSec
        ' 
        tmSec.Enabled = True
        tmSec.Interval = 1000
        ' 
        ' frmMain
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1282, 653)
        Controls.Add(pnlMain)
        Controls.Add(Menu)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        IsMdiContainer = True
        Name = "frmMain"
        StartPosition = FormStartPosition.CenterScreen
        Text = "BedivereKnxClient"
        Menu.ResumeLayout(False)
        Menu.PerformLayout()
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
    Friend WithEvents tbCtDn As ToolStripLabel
End Class
