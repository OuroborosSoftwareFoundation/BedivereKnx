<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAboutLicense
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
        tlpMain = New TableLayoutPanel()
        TableLayoutPanel1 = New TableLayoutPanel()
        cbLang = New ComboBox()
        PictureBox1 = New PictureBox()
        tbLicense = New TextBox()
        Label1 = New Label()
        tlpMain.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 1
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(TableLayoutPanel1, 0, 0)
        tlpMain.Controls.Add(tbLicense, 0, 1)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 2
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 60F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.Size = New Size(582, 753)
        tlpMain.TabIndex = 0
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel1.Controls.Add(cbLang, 1, 0)
        TableLayoutPanel1.Controls.Add(PictureBox1, 2, 0)
        TableLayoutPanel1.Controls.Add(Label1, 0, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(3, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(576, 54)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' cbLang
        ' 
        cbLang.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        cbLang.DropDownStyle = ComboBoxStyle.DropDownList
        cbLang.FormattingEnabled = True
        cbLang.Location = New Point(103, 13)
        cbLang.Name = "cbLang"
        cbLang.Size = New Size(320, 28)
        cbLang.TabIndex = 1
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Image = My.Resources.Resources.gplv3_or_later
        PictureBox1.Location = New Point(429, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(144, 48)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        ' 
        ' tbLicense
        ' 
        tbLicense.BackColor = SystemColors.Window
        tbLicense.Dock = DockStyle.Fill
        tbLicense.Location = New Point(3, 63)
        tbLicense.Multiline = True
        tbLicense.Name = "tbLicense"
        tbLicense.ReadOnly = True
        tbLicense.ScrollBars = ScrollBars.Both
        tbLicense.Size = New Size(576, 687)
        tbLicense.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(94, 54)
        Label1.TabIndex = 3
        Label1.Text = "Language"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' frmAboutLicense
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(582, 753)
        Controls.Add(tlpMain)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmAboutLicense"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "License - GPL 3.0 or later"
        TopMost = True
        tlpMain.ResumeLayout(False)
        tlpMain.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents cbLang As ComboBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents tbLicense As TextBox
    Friend WithEvents Label1 As Label
End Class
