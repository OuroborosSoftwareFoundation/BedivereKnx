<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartUp
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
    Friend WithEvents lblProdName As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        lblVersion = New Label()
        lblCopyright = New Label()
        lblProdName = New Label()
        tlpInfo = New TableLayoutPanel()
        pbrLoad = New ProgressBar()
        lblAuth = New Label()
        tlpMain = New TableLayoutPanel()
        picTitle = New PictureBox()
        tlpInfo.SuspendLayout()
        tlpMain.SuspendLayout()
        CType(picTitle, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblVersion
        ' 
        lblVersion.BackColor = Color.Transparent
        lblVersion.Dock = DockStyle.Fill
        lblVersion.Font = New Font("Microsoft Sans Serif", 9.0F)
        lblVersion.ForeColor = Color.White
        lblVersion.Location = New Point(3, 132)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(386, 50)
        lblVersion.TabIndex = 1
        lblVersion.Text = "Version"
        lblVersion.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblCopyright
        ' 
        lblCopyright.BackColor = Color.Transparent
        lblCopyright.Dock = DockStyle.Fill
        lblCopyright.Font = New Font("Microsoft Sans Serif", 9.0F)
        lblCopyright.ForeColor = Color.White
        lblCopyright.Location = New Point(3, 242)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(386, 50)
        lblCopyright.TabIndex = 2
        lblCopyright.Text = "Copyright © 2024 Ouroboros Software Foundation." & vbCrLf & "All rights reserved."
        lblCopyright.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblProdName
        ' 
        lblProdName.BackColor = Color.Transparent
        lblProdName.Dock = DockStyle.Fill
        lblProdName.Font = New Font("微软雅黑", 19.8000011F, FontStyle.Bold)
        lblProdName.ForeColor = Color.Azure
        lblProdName.Location = New Point(3, 0)
        lblProdName.Name = "lblProdName"
        lblProdName.Size = New Size(386, 132)
        lblProdName.TabIndex = 0
        lblProdName.Text = "BedivereKnx.Client"
        lblProdName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tlpInfo
        ' 
        tlpInfo.ColumnCount = 1
        tlpInfo.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpInfo.Controls.Add(lblProdName, 0, 0)
        tlpInfo.Controls.Add(lblVersion, 0, 1)
        tlpInfo.Controls.Add(lblCopyright, 0, 4)
        tlpInfo.Controls.Add(pbrLoad, 0, 3)
        tlpInfo.Controls.Add(lblAuth, 0, 2)
        tlpInfo.Dock = DockStyle.Fill
        tlpInfo.Location = New Point(303, 3)
        tlpInfo.Name = "tlpInfo"
        tlpInfo.RowCount = 5
        tlpInfo.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpInfo.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpInfo.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpInfo.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpInfo.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpInfo.Size = New Size(392, 292)
        tlpInfo.TabIndex = 3
        ' 
        ' pbrLoad
        ' 
        pbrLoad.Dock = DockStyle.Fill
        pbrLoad.Location = New Point(3, 215)
        pbrLoad.Name = "pbrLoad"
        pbrLoad.Size = New Size(386, 24)
        pbrLoad.Style = ProgressBarStyle.Marquee
        pbrLoad.TabIndex = 3
        ' 
        ' lblAuth
        ' 
        lblAuth.Dock = DockStyle.Fill
        lblAuth.ForeColor = Color.White
        lblAuth.Location = New Point(3, 182)
        lblAuth.Name = "lblAuth"
        lblAuth.Size = New Size(386, 30)
        lblAuth.TabIndex = 4
        lblAuth.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 2
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 300F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tlpMain.Controls.Add(tlpInfo, 2, 0)
        tlpMain.Controls.Add(picTitle, 0, 0)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Margin = New Padding(0)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 1
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.Size = New Size(698, 298)
        tlpMain.TabIndex = 4
        ' 
        ' picTitle
        ' 
        picTitle.Dock = DockStyle.Fill
        picTitle.Image = My.Resources.Resources.StartUp
        picTitle.Location = New Point(0, 0)
        picTitle.Margin = New Padding(0)
        picTitle.Name = "picTitle"
        picTitle.Size = New Size(300, 298)
        picTitle.SizeMode = PictureBoxSizeMode.Zoom
        picTitle.TabIndex = 4
        picTitle.TabStop = False
        ' 
        ' frmStartUp
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.SteelBlue
        ClientSize = New Size(698, 298)
        ControlBox = False
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmStartUp"
        ShowIcon = False
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterScreen
        tlpInfo.ResumeLayout(False)
        tlpMain.ResumeLayout(False)
        CType(picTitle, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents tlpInfo As TableLayoutPanel
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents picTitle As PictureBox
    Friend WithEvents pbrLoad As ProgressBar
    Friend WithEvents lblAuth As Label

End Class
