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
        TableLayoutPanel1 = New TableLayoutPanel()
        btnOK = New Button()
        btnCancel = New Button()
        tlpMain = New TableLayoutPanel()
        Label1 = New Label()
        chkInitRead = New CheckBox()
        TableLayoutPanel2 = New TableLayoutPanel()
        tbDataFile = New TextBox()
        btnOpenDataFile = New Button()
        TableLayoutPanel1.SuspendLayout()
        tlpMain.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(btnOK, 0, 0)
        TableLayoutPanel1.Controls.Add(btnCancel, 1, 0)
        TableLayoutPanel1.Location = New Point(154, 203)
        TableLayoutPanel1.Margin = New Padding(4, 5, 4, 5)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(219, 40)
        TableLayoutPanel1.TabIndex = 0
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
        tlpMain.Controls.Add(Label1, 0, 1)
        tlpMain.Controls.Add(chkInitRead, 0, 4)
        tlpMain.Controls.Add(TableLayoutPanel1, 0, 6)
        tlpMain.Controls.Add(TableLayoutPanel2, 0, 2)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Name = "tlpMain"
        tlpMain.Padding = New Padding(5)
        tlpMain.RowCount = 7
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 10F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 10F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 40F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpMain.Size = New Size(382, 253)
        tlpMain.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(8, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(366, 30)
        Label1.TabIndex = 1
        Label1.Text = "默认数据文件："
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkInitRead
        ' 
        chkInitRead.Dock = DockStyle.Fill
        chkInitRead.Location = New Point(8, 98)
        chkInitRead.Name = "chkInitRead"
        chkInitRead.Size = New Size(366, 34)
        chkInitRead.TabIndex = 3
        chkInitRead.Text = "打开接口时轮询组地址"
        chkInitRead.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 2
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 80F))
        TableLayoutPanel2.Controls.Add(tbDataFile, 0, 0)
        TableLayoutPanel2.Controls.Add(btnOpenDataFile, 1, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(8, 48)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 1
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel2.Size = New Size(366, 34)
        TableLayoutPanel2.TabIndex = 4
        ' 
        ' tbDataFile
        ' 
        tbDataFile.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        tbDataFile.BackColor = SystemColors.Window
        tbDataFile.Location = New Point(3, 3)
        tbDataFile.Name = "tbDataFile"
        tbDataFile.ReadOnly = True
        tbDataFile.Size = New Size(280, 27)
        tbDataFile.TabIndex = 2
        ' 
        ' btnOpenDataFile
        ' 
        btnOpenDataFile.Dock = DockStyle.Fill
        btnOpenDataFile.Location = New Point(289, 3)
        btnOpenDataFile.Name = "btnOpenDataFile"
        btnOpenDataFile.Size = New Size(74, 28)
        btnOpenDataFile.TabIndex = 3
        btnOpenDataFile.Text = "..."
        btnOpenDataFile.UseVisualStyleBackColor = True
        ' 
        ' frmConfig
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(382, 253)
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmConfig"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "设置"
        TableLayoutPanel1.ResumeLayout(False)
        tlpMain.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TableLayoutPanel2.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents tbDataFile As TextBox
    Friend WithEvents chkInitRead As CheckBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnOpenDataFile As Button

End Class
