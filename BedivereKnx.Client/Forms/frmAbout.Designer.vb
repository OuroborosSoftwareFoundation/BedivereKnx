<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
    Friend WithEvents tbDesc As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblCopyright As System.Windows.Forms.Label

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        lblProdName = New Label()
        lblCopyright = New Label()
        lblVersion = New Label()
        PictureBox1 = New PictureBox()
        tbDesc = New TextBox()
        tlpBtns = New TableLayoutPanel()
        btnOK = New Button()
        btnLicense = New Button()
        pnlTitle = New Panel()
        picTitle = New PictureBox()
        lblPicInfo = New Label()
        tlpMain = New TableLayoutPanel()
        tlpRight = New TableLayoutPanel()
        TableLayoutPanel2 = New TableLayoutPanel()
        lblGithub = New LinkLabel()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        tlpBtns.SuspendLayout()
        pnlTitle.SuspendLayout()
        CType(picTitle, ComponentModel.ISupportInitialize).BeginInit()
        tlpMain.SuspendLayout()
        tlpRight.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblProdName
        ' 
        lblProdName.Dock = DockStyle.Fill
        lblProdName.Font = New Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point)
        lblProdName.Location = New Point(3, 0)
        lblProdName.Name = "lblProdName"
        lblProdName.Size = New Size(304, 64)
        lblProdName.TabIndex = 0
        lblProdName.Text = "BedivereKnxClient"
        lblProdName.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' lblCopyright
        ' 
        lblCopyright.Dock = DockStyle.Fill
        lblCopyright.Location = New Point(3, 100)
        lblCopyright.Name = "lblCopyright"
        lblCopyright.Size = New Size(430, 40)
        lblCopyright.TabIndex = 0
        lblCopyright.Text = "Copyright © 2024 Ouroboros Software Foundation." & vbCrLf & "All rights reserved."
        lblCopyright.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblVersion
        ' 
        lblVersion.Dock = DockStyle.Fill
        lblVersion.Location = New Point(3, 64)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(304, 30)
        lblVersion.TabIndex = 0
        lblVersion.Text = "Version"
        lblVersion.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Image = My.Resources.Resources.OSF_logo_256
        PictureBox1.Location = New Point(313, 3)
        PictureBox1.Name = "PictureBox1"
        TableLayoutPanel2.SetRowSpan(PictureBox1, 2)
        PictureBox1.Size = New Size(114, 88)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' tbDesc
        ' 
        tbDesc.Dock = DockStyle.Fill
        tbDesc.Location = New Point(9, 175)
        tbDesc.Margin = New Padding(9, 5, 4, 5)
        tbDesc.Multiline = True
        tbDesc.Name = "tbDesc"
        tbDesc.ReadOnly = True
        tbDesc.ScrollBars = ScrollBars.Both
        tbDesc.Size = New Size(423, 167)
        tbDesc.TabIndex = 0
        tbDesc.TabStop = False
        tbDesc.Text = resources.GetString("tbDesc.Text")
        ' 
        ' tlpBtns
        ' 
        tlpBtns.ColumnCount = 2
        tlpBtns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBtns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBtns.Controls.Add(btnOK, 1, 0)
        tlpBtns.Controls.Add(btnLicense, 0, 0)
        tlpBtns.Dock = DockStyle.Fill
        tlpBtns.Location = New Point(0, 347)
        tlpBtns.Margin = New Padding(0)
        tlpBtns.Name = "tlpBtns"
        tlpBtns.RowCount = 1
        tlpBtns.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpBtns.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpBtns.Size = New Size(436, 40)
        tlpBtns.TabIndex = 2
        ' 
        ' btnOK
        ' 
        btnOK.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btnOK.DialogResult = DialogResult.Cancel
        btnOK.Location = New Point(312, 5)
        btnOK.Margin = New Padding(4, 5, 4, 5)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(120, 30)
        btnOK.TabIndex = 0
        btnOK.Text = "OK"
        ' 
        ' btnLicense
        ' 
        btnLicense.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btnLicense.Location = New Point(3, 3)
        btnLicense.Name = "btnLicense"
        btnLicense.Size = New Size(120, 34)
        btnLicense.TabIndex = 1
        btnLicense.Text = "License"
        btnLicense.UseVisualStyleBackColor = True
        ' 
        ' pnlTitle
        ' 
        pnlTitle.Controls.Add(picTitle)
        pnlTitle.Controls.Add(lblPicInfo)
        pnlTitle.Dock = DockStyle.Fill
        pnlTitle.Location = New Point(0, 0)
        pnlTitle.Margin = New Padding(0)
        pnlTitle.Name = "pnlTitle"
        pnlTitle.Size = New Size(250, 393)
        pnlTitle.TabIndex = 4
        ' 
        ' picTitle
        ' 
        picTitle.Dock = DockStyle.Fill
        picTitle.Image = My.Resources.Resources.Bedivere1
        picTitle.Location = New Point(0, 0)
        picTitle.Name = "picTitle"
        picTitle.Size = New Size(250, 333)
        picTitle.SizeMode = PictureBoxSizeMode.Zoom
        picTitle.TabIndex = 0
        picTitle.TabStop = False
        ' 
        ' lblPicInfo
        ' 
        lblPicInfo.BackColor = Color.Transparent
        lblPicInfo.Dock = DockStyle.Bottom
        lblPicInfo.Font = New Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point)
        lblPicInfo.ForeColor = Color.Gray
        lblPicInfo.Location = New Point(0, 333)
        lblPicInfo.Name = "lblPicInfo"
        lblPicInfo.Size = New Size(250, 60)
        lblPicInfo.TabIndex = 3
        lblPicInfo.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 2
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 250F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(pnlTitle, 0, 0)
        tlpMain.Controls.Add(tlpRight, 1, 0)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(5, 5)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 1
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpMain.Size = New Size(692, 393)
        tlpMain.TabIndex = 1
        ' 
        ' tlpRight
        ' 
        tlpRight.ColumnCount = 1
        tlpRight.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpRight.Controls.Add(lblCopyright, 0, 1)
        tlpRight.Controls.Add(tlpBtns, 0, 4)
        tlpRight.Controls.Add(tbDesc, 0, 3)
        tlpRight.Controls.Add(TableLayoutPanel2, 0, 0)
        tlpRight.Controls.Add(lblGithub, 0, 2)
        tlpRight.Dock = DockStyle.Fill
        tlpRight.Location = New Point(253, 3)
        tlpRight.Name = "tlpRight"
        tlpRight.RowCount = 5
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Absolute, 100F))
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpRight.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpRight.Size = New Size(436, 387)
        tlpRight.TabIndex = 5
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 2
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 120F))
        TableLayoutPanel2.Controls.Add(lblProdName, 0, 0)
        TableLayoutPanel2.Controls.Add(lblVersion, 0, 1)
        TableLayoutPanel2.Controls.Add(PictureBox1, 1, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(3, 3)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 2
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        TableLayoutPanel2.Size = New Size(430, 94)
        TableLayoutPanel2.TabIndex = 0
        ' 
        ' lblGithub
        ' 
        lblGithub.Dock = DockStyle.Fill
        lblGithub.Location = New Point(3, 140)
        lblGithub.Name = "lblGithub"
        lblGithub.Size = New Size(430, 30)
        lblGithub.TabIndex = 1
        lblGithub.TabStop = True
        lblGithub.Text = "Only Released on Github."
        lblGithub.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' frmAbout
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnOK
        ClientSize = New Size(702, 403)
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmAbout"
        Padding = New Padding(5)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "About BedivereKnxClient"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        tlpBtns.ResumeLayout(False)
        pnlTitle.ResumeLayout(False)
        CType(picTitle, ComponentModel.ISupportInitialize).EndInit()
        tlpMain.ResumeLayout(False)
        tlpRight.ResumeLayout(False)
        tlpRight.PerformLayout()
        TableLayoutPanel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents btnLicense As Button
    Friend WithEvents lblPicInfo As Label
    Friend WithEvents picTitle As PictureBox
    Friend WithEvents pnlTitle As Panel
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpRight As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents lblGithub As LinkLabel

End Class
