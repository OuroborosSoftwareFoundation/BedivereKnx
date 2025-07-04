<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
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
        tlpButton = New TableLayoutPanel()
        btnOK = New Button()
        btnCancel = New Button()
        tlpMain = New TableLayoutPanel()
        Label1 = New Label()
        chkInitRead = New CheckBox()
        tlpDataFile = New TableLayoutPanel()
        tbDataFile = New TextBox()
        btnOpenDataFile = New Button()
        Label2 = New Label()
        tlpHmiFile = New TableLayoutPanel()
        tbHmiFile = New TextBox()
        btnOpenHmiFIle = New Button()
        Label3 = New Label()
        TableLayoutPanel1 = New TableLayoutPanel()
        tbLocalIp = New TextBox()
        btnLocalIpSel = New Button()
        tlpButton.SuspendLayout()
        tlpMain.SuspendLayout()
        tlpDataFile.SuspendLayout()
        tlpHmiFile.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tlpButton
        ' 
        tlpButton.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        tlpButton.ColumnCount = 2
        tlpButton.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpButton.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpButton.Controls.Add(btnOK, 0, 0)
        tlpButton.Controls.Add(btnCancel, 1, 0)
        tlpButton.Location = New Point(134, 283)
        tlpButton.Margin = New Padding(4, 5, 4, 5)
        tlpButton.Name = "tlpButton"
        tlpButton.RowCount = 1
        tlpButton.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpButton.Size = New Size(219, 40)
        tlpButton.TabIndex = 0
        ' 
        ' btnOK
        ' 
        btnOK.Dock = DockStyle.Fill
        btnOK.Location = New Point(4, 5)
        btnOK.Margin = New Padding(4, 5, 4, 5)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(101, 30)
        btnOK.TabIndex = 0
        btnOK.Text = "确定"
        ' 
        ' btnCancel
        ' 
        btnCancel.Dock = DockStyle.Fill
        btnCancel.Location = New Point(113, 5)
        btnCancel.Margin = New Padding(4, 5, 4, 5)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(102, 30)
        btnCancel.TabIndex = 1
        btnCancel.Text = "取消"
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 1
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(Label1, 0, 0)
        tlpMain.Controls.Add(chkInitRead, 0, 6)
        tlpMain.Controls.Add(tlpButton, 0, 8)
        tlpMain.Controls.Add(tlpDataFile, 0, 1)
        tlpMain.Controls.Add(Label2, 0, 2)
        tlpMain.Controls.Add(tlpHmiFile, 0, 3)
        tlpMain.Controls.Add(Label3, 0, 4)
        tlpMain.Controls.Add(TableLayoutPanel1, 0, 5)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(10, 10)
        tlpMain.Name = "tlpMain"
        tlpMain.Padding = New Padding(5)
        tlpMain.RowCount = 9
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpMain.Size = New Size(362, 333)
        tlpMain.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(8, 5)
        Label1.Name = "Label1"
        Label1.Size = New Size(346, 30)
        Label1.TabIndex = 1
        Label1.Text = "默认数据文件："
        Label1.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' chkInitRead
        ' 
        chkInitRead.Dock = DockStyle.Fill
        chkInitRead.Location = New Point(8, 218)
        chkInitRead.Name = "chkInitRead"
        chkInitRead.Size = New Size(346, 34)
        chkInitRead.TabIndex = 3
        chkInitRead.Text = "打开接口时轮询组地址"
        chkInitRead.UseVisualStyleBackColor = True
        ' 
        ' tlpDataFile
        ' 
        tlpDataFile.ColumnCount = 2
        tlpDataFile.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpDataFile.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        tlpDataFile.Controls.Add(tbDataFile, 0, 0)
        tlpDataFile.Controls.Add(btnOpenDataFile, 1, 0)
        tlpDataFile.Dock = DockStyle.Fill
        tlpDataFile.Location = New Point(5, 35)
        tlpDataFile.Margin = New Padding(0)
        tlpDataFile.Name = "tlpDataFile"
        tlpDataFile.RowCount = 1
        tlpDataFile.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpDataFile.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpDataFile.Size = New Size(352, 40)
        tlpDataFile.TabIndex = 4
        ' 
        ' tbDataFile
        ' 
        tbDataFile.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDataFile.BackColor = SystemColors.Window
        tbDataFile.Location = New Point(3, 6)
        tbDataFile.Name = "tbDataFile"
        tbDataFile.ReadOnly = True
        tbDataFile.Size = New Size(266, 27)
        tbDataFile.TabIndex = 2
        ' 
        ' btnOpenDataFile
        ' 
        btnOpenDataFile.Dock = DockStyle.Fill
        btnOpenDataFile.Location = New Point(275, 3)
        btnOpenDataFile.Name = "btnOpenDataFile"
        btnOpenDataFile.Size = New Size(74, 34)
        btnOpenDataFile.TabIndex = 3
        btnOpenDataFile.Text = "..."
        btnOpenDataFile.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.Dock = DockStyle.Fill
        Label2.Location = New Point(8, 75)
        Label2.Name = "Label2"
        Label2.Size = New Size(346, 30)
        Label2.TabIndex = 1
        Label2.Text = "默认图形文件："
        Label2.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' tlpHmiFile
        ' 
        tlpHmiFile.ColumnCount = 2
        tlpHmiFile.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpHmiFile.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        tlpHmiFile.Controls.Add(tbHmiFile, 0, 0)
        tlpHmiFile.Controls.Add(btnOpenHmiFIle, 1, 0)
        tlpHmiFile.Dock = DockStyle.Fill
        tlpHmiFile.Location = New Point(5, 105)
        tlpHmiFile.Margin = New Padding(0)
        tlpHmiFile.Name = "tlpHmiFile"
        tlpHmiFile.RowCount = 1
        tlpHmiFile.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpHmiFile.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpHmiFile.Size = New Size(352, 40)
        tlpHmiFile.TabIndex = 4
        ' 
        ' tbHmiFile
        ' 
        tbHmiFile.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbHmiFile.BackColor = SystemColors.Window
        tbHmiFile.Location = New Point(3, 6)
        tbHmiFile.Name = "tbHmiFile"
        tbHmiFile.ReadOnly = True
        tbHmiFile.Size = New Size(266, 27)
        tbHmiFile.TabIndex = 2
        ' 
        ' btnOpenHmiFIle
        ' 
        btnOpenHmiFIle.Dock = DockStyle.Fill
        btnOpenHmiFIle.Location = New Point(275, 3)
        btnOpenHmiFIle.Name = "btnOpenHmiFIle"
        btnOpenHmiFIle.Size = New Size(74, 34)
        btnOpenHmiFIle.TabIndex = 3
        btnOpenHmiFIle.Text = "..."
        btnOpenHmiFIle.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.Dock = DockStyle.Fill
        Label3.Location = New Point(8, 145)
        Label3.Name = "Label3"
        Label3.Size = New Size(346, 30)
        Label3.TabIndex = 1
        Label3.Text = "KNX路由接口本地IP："
        Label3.TextAlign = ContentAlignment.BottomLeft
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        TableLayoutPanel1.Controls.Add(tbLocalIp, 0, 0)
        TableLayoutPanel1.Controls.Add(btnLocalIpSel, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(5, 175)
        TableLayoutPanel1.Margin = New Padding(0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(352, 40)
        TableLayoutPanel1.TabIndex = 4
        ' 
        ' tbLocalIp
        ' 
        tbLocalIp.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbLocalIp.BackColor = SystemColors.Window
        tbLocalIp.Location = New Point(3, 6)
        tbLocalIp.Name = "tbLocalIp"
        tbLocalIp.ReadOnly = True
        tbLocalIp.Size = New Size(196, 27)
        tbLocalIp.TabIndex = 2
        ' 
        ' btnLocalIpSel
        ' 
        btnLocalIpSel.Dock = DockStyle.Fill
        btnLocalIpSel.Location = New Point(205, 3)
        btnLocalIpSel.Name = "btnLocalIpSel"
        btnLocalIpSel.Size = New Size(144, 34)
        btnLocalIpSel.TabIndex = 3
        btnLocalIpSel.Text = "..."
        btnLocalIpSel.UseVisualStyleBackColor = True
        ' 
        ' frmConfig
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(382, 353)
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmConfig"
        Padding = New Padding(10)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Config"
        tlpButton.ResumeLayout(False)
        tlpMain.ResumeLayout(False)
        tlpDataFile.ResumeLayout(False)
        tlpDataFile.PerformLayout()
        tlpHmiFile.ResumeLayout(False)
        tlpHmiFile.PerformLayout()
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents tlpButton As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents tbDataFile As TextBox
    Friend WithEvents chkInitRead As CheckBox
    Friend WithEvents tlpDataFile As TableLayoutPanel
    Friend WithEvents btnOpenDataFile As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents tlpHmiFile As TableLayoutPanel
    Friend WithEvents tbHmiFile As TextBox
    Friend WithEvents btnOpenHmiFIle As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents tbLocalIp As TextBox
    Friend WithEvents btnLocalIpSel As Button

End Class
