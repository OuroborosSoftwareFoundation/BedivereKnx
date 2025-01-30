<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAuthCreate
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
        TableLayoutPanel1 = New TableLayoutPanel()
        lblText = New Label()
        lblKey = New Label()
        lblIv = New Label()
        lblAuth = New Label()
        tbText = New TextBox()
        tbKey = New TextBox()
        tbIv = New TextBox()
        tbAuth = New TextBox()
        btnCreate = New Button()
        lblVersion = New Label()
        lblDOE = New Label()
        dtpDOE = New DateTimePicker()
        tlpVersion = New TableLayoutPanel()
        numVer1 = New NumericUpDown()
        numVer2 = New NumericUpDown()
        numVer3 = New NumericUpDown()
        TableLayoutPanel1.SuspendLayout()
        tlpVersion.SuspendLayout()
        CType(numVer1, ComponentModel.ISupportInitialize).BeginInit()
        CType(numVer2, ComponentModel.ISupportInitialize).BeginInit()
        CType(numVer3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(lblText, 0, 0)
        TableLayoutPanel1.Controls.Add(lblKey, 0, 3)
        TableLayoutPanel1.Controls.Add(lblIv, 0, 4)
        TableLayoutPanel1.Controls.Add(lblAuth, 0, 5)
        TableLayoutPanel1.Controls.Add(tbText, 1, 0)
        TableLayoutPanel1.Controls.Add(tbKey, 1, 3)
        TableLayoutPanel1.Controls.Add(tbIv, 1, 4)
        TableLayoutPanel1.Controls.Add(tbAuth, 1, 5)
        TableLayoutPanel1.Controls.Add(btnCreate, 0, 6)
        TableLayoutPanel1.Controls.Add(lblVersion, 0, 1)
        TableLayoutPanel1.Controls.Add(lblDOE, 0, 2)
        TableLayoutPanel1.Controls.Add(dtpDOE, 1, 2)
        TableLayoutPanel1.Controls.Add(tlpVersion, 1, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 7
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 60F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        TableLayoutPanel1.Size = New Size(482, 348)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' lblText
        ' 
        lblText.Dock = DockStyle.Fill
        lblText.Location = New Point(3, 0)
        lblText.Name = "lblText"
        lblText.Size = New Size(94, 60)
        lblText.TabIndex = 0
        lblText.Text = "Name"
        lblText.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblKey
        ' 
        lblKey.Dock = DockStyle.Fill
        lblKey.Location = New Point(3, 140)
        lblKey.Name = "lblKey"
        lblKey.Size = New Size(94, 40)
        lblKey.TabIndex = 1
        lblKey.Text = "Key"
        lblKey.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblIv
        ' 
        lblIv.Dock = DockStyle.Fill
        lblIv.Location = New Point(3, 180)
        lblIv.Name = "lblIv"
        lblIv.Size = New Size(94, 40)
        lblIv.TabIndex = 2
        lblIv.Text = "IV"
        lblIv.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblAuth
        ' 
        lblAuth.Dock = DockStyle.Fill
        lblAuth.Location = New Point(3, 220)
        lblAuth.Name = "lblAuth"
        lblAuth.Size = New Size(94, 78)
        lblAuth.TabIndex = 3
        lblAuth.Text = "Auth"
        lblAuth.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' tbText
        ' 
        tbText.Dock = DockStyle.Fill
        tbText.Enabled = False
        tbText.Location = New Point(103, 3)
        tbText.Multiline = True
        tbText.Name = "tbText"
        tbText.ReadOnly = True
        tbText.Size = New Size(376, 54)
        tbText.TabIndex = 4
        ' 
        ' tbKey
        ' 
        tbKey.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbKey.Enabled = False
        tbKey.Location = New Point(103, 146)
        tbKey.Name = "tbKey"
        tbKey.PasswordChar = "*"c
        tbKey.ReadOnly = True
        tbKey.Size = New Size(376, 27)
        tbKey.TabIndex = 5
        ' 
        ' tbIv
        ' 
        tbIv.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbIv.Enabled = False
        tbIv.Location = New Point(103, 186)
        tbIv.Name = "tbIv"
        tbIv.PasswordChar = "*"c
        tbIv.ReadOnly = True
        tbIv.Size = New Size(376, 27)
        tbIv.TabIndex = 6
        ' 
        ' tbAuth
        ' 
        tbAuth.Dock = DockStyle.Fill
        tbAuth.Location = New Point(103, 223)
        tbAuth.Multiline = True
        tbAuth.Name = "tbAuth"
        tbAuth.ReadOnly = True
        tbAuth.Size = New Size(376, 72)
        tbAuth.TabIndex = 7
        ' 
        ' btnCreate
        ' 
        btnCreate.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom
        TableLayoutPanel1.SetColumnSpan(btnCreate, 2)
        btnCreate.Location = New Point(194, 301)
        btnCreate.Name = "btnCreate"
        btnCreate.Padding = New Padding(0, 5, 0, 5)
        btnCreate.Size = New Size(94, 44)
        btnCreate.TabIndex = 8
        btnCreate.Text = "Create"
        btnCreate.UseVisualStyleBackColor = True
        ' 
        ' lblVersion
        ' 
        lblVersion.Dock = DockStyle.Fill
        lblVersion.Location = New Point(3, 60)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(94, 40)
        lblVersion.TabIndex = 0
        lblVersion.Text = "Version"
        lblVersion.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblDOE
        ' 
        lblDOE.Dock = DockStyle.Fill
        lblDOE.Location = New Point(3, 100)
        lblDOE.Name = "lblDOE"
        lblDOE.Size = New Size(94, 40)
        lblDOE.TabIndex = 0
        lblDOE.Text = "Expiration"
        lblDOE.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dtpDOE
        ' 
        dtpDOE.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        dtpDOE.CustomFormat = "yyyy-MM-dd"
        dtpDOE.Format = DateTimePickerFormat.Custom
        dtpDOE.Location = New Point(103, 106)
        dtpDOE.MinDate = New Date(2024, 1, 1, 0, 0, 0, 0)
        dtpDOE.Name = "dtpDOE"
        dtpDOE.ShowCheckBox = True
        dtpDOE.Size = New Size(376, 27)
        dtpDOE.TabIndex = 9
        dtpDOE.Value = New Date(2024, 1, 1, 0, 0, 0, 0)
        ' 
        ' tlpVersion
        ' 
        tlpVersion.ColumnCount = 3
        tlpVersion.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpVersion.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpVersion.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        tlpVersion.Controls.Add(numVer1, 0, 0)
        tlpVersion.Controls.Add(numVer2, 1, 0)
        tlpVersion.Controls.Add(numVer3, 2, 0)
        tlpVersion.Dock = DockStyle.Fill
        tlpVersion.Location = New Point(103, 63)
        tlpVersion.Name = "tlpVersion"
        tlpVersion.RowCount = 1
        tlpVersion.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpVersion.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpVersion.Size = New Size(376, 34)
        tlpVersion.TabIndex = 10
        ' 
        ' numVer1
        ' 
        numVer1.Location = New Point(3, 3)
        numVer1.Name = "numVer1"
        numVer1.Size = New Size(119, 27)
        numVer1.TabIndex = 0
        ' 
        ' numVer2
        ' 
        numVer2.Location = New Point(128, 3)
        numVer2.Name = "numVer2"
        numVer2.Size = New Size(119, 27)
        numVer2.TabIndex = 1
        ' 
        ' numVer3
        ' 
        numVer3.Location = New Point(253, 3)
        numVer3.Name = "numVer3"
        numVer3.Size = New Size(120, 27)
        numVer3.TabIndex = 2
        ' 
        ' frmAuthCreate
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(482, 348)
        Controls.Add(TableLayoutPanel1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmAuthCreate"
        StartPosition = FormStartPosition.CenterScreen
        Text = "AuthCreate"
        TopMost = True
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        tlpVersion.ResumeLayout(False)
        CType(numVer1, ComponentModel.ISupportInitialize).EndInit()
        CType(numVer2, ComponentModel.ISupportInitialize).EndInit()
        CType(numVer3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblText As Label
    Friend WithEvents lblKey As Label
    Friend WithEvents lblIv As Label
    Friend WithEvents lblAuth As Label
    Friend WithEvents tbText As TextBox
    Friend WithEvents tbKey As TextBox
    Friend WithEvents tbIv As TextBox
    Friend WithEvents tbAuth As TextBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblDOE As Label
    Friend WithEvents dtpDOE As DateTimePicker
    Friend WithEvents tlpVersion As TableLayoutPanel
    Friend WithEvents numVer1 As NumericUpDown
    Friend WithEvents numVer2 As NumericUpDown
    Friend WithEvents numVer3 As NumericUpDown
End Class
