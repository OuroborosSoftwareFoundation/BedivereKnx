<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAuthModify
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
        lblExpMsg = New Label()
        tbInput = New TextBox()
        btnOK = New Button()
        btnCancel = New Button()
        tlpMain = New TableLayoutPanel()
        tlpBtns = New TableLayoutPanel()
        grpMode = New GroupBox()
        tlpGrp = New TableLayoutPanel()
        chkModUpdate = New RadioButton()
        chkModeNew = New RadioButton()
        Label1 = New Label()
        tlpMain.SuspendLayout()
        tlpBtns.SuspendLayout()
        grpMode.SuspendLayout()
        tlpGrp.SuspendLayout()
        SuspendLayout()
        ' 
        ' lblExpMsg
        ' 
        lblExpMsg.Dock = DockStyle.Fill
        lblExpMsg.Location = New Point(3, 30)
        lblExpMsg.Name = "lblExpMsg"
        lblExpMsg.Size = New Size(474, 70)
        lblExpMsg.TabIndex = 0
        lblExpMsg.Text = "Exception Message"
        ' 
        ' tbInput
        ' 
        tlpGrp.SetColumnSpan(tbInput, 2)
        tbInput.Dock = DockStyle.Fill
        tbInput.Location = New Point(3, 33)
        tbInput.Multiline = True
        tbInput.Name = "tbInput"
        tbInput.Size = New Size(462, 62)
        tbInput.TabIndex = 1
        ' 
        ' btnOK
        ' 
        btnOK.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btnOK.Location = New Point(113, 3)
        btnOK.Margin = New Padding(3, 3, 30, 3)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(94, 38)
        btnOK.TabIndex = 2
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancel.Location = New Point(267, 3)
        btnCancel.Margin = New Padding(30, 3, 3, 3)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(94, 38)
        btnCancel.TabIndex = 3
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 1
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(tlpBtns, 0, 3)
        tlpMain.Controls.Add(lblExpMsg, 0, 1)
        tlpMain.Controls.Add(grpMode, 0, 2)
        tlpMain.Controls.Add(Label1, 0, 0)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(10, 10)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 4
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 130F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpMain.Size = New Size(480, 280)
        tlpMain.TabIndex = 4
        ' 
        ' tlpBtns
        ' 
        tlpBtns.ColumnCount = 2
        tlpBtns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBtns.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBtns.Controls.Add(btnOK, 0, 0)
        tlpBtns.Controls.Add(btnCancel, 1, 0)
        tlpBtns.Dock = DockStyle.Fill
        tlpBtns.Location = New Point(3, 233)
        tlpBtns.Name = "tlpBtns"
        tlpBtns.RowCount = 1
        tlpBtns.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpBtns.Size = New Size(474, 44)
        tlpBtns.TabIndex = 5
        ' 
        ' grpMode
        ' 
        grpMode.Controls.Add(tlpGrp)
        grpMode.Dock = DockStyle.Fill
        grpMode.Location = New Point(3, 103)
        grpMode.Name = "grpMode"
        grpMode.Size = New Size(474, 124)
        grpMode.TabIndex = 6
        grpMode.TabStop = False
        grpMode.Text = "请输入授权升级码或全新授权码："
        ' 
        ' tlpGrp
        ' 
        tlpGrp.ColumnCount = 2
        tlpGrp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpGrp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpGrp.Controls.Add(chkModUpdate, 0, 0)
        tlpGrp.Controls.Add(chkModeNew, 1, 0)
        tlpGrp.Controls.Add(tbInput, 0, 1)
        tlpGrp.Dock = DockStyle.Fill
        tlpGrp.Location = New Point(3, 23)
        tlpGrp.Name = "tlpGrp"
        tlpGrp.RowCount = 2
        tlpGrp.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpGrp.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpGrp.Size = New Size(468, 98)
        tlpGrp.TabIndex = 0
        ' 
        ' chkModUpdate
        ' 
        chkModUpdate.Checked = True
        chkModUpdate.Dock = DockStyle.Fill
        chkModUpdate.Location = New Point(3, 3)
        chkModUpdate.Name = "chkModUpdate"
        chkModUpdate.Padding = New Padding(10, 0, 0, 0)
        chkModUpdate.Size = New Size(228, 24)
        chkModUpdate.TabIndex = 0
        chkModUpdate.TabStop = True
        chkModUpdate.Text = "授权升级码"
        chkModUpdate.UseVisualStyleBackColor = True
        ' 
        ' chkModeNew
        ' 
        chkModeNew.Dock = DockStyle.Fill
        chkModeNew.Location = New Point(237, 3)
        chkModeNew.Name = "chkModeNew"
        chkModeNew.Padding = New Padding(10, 0, 0, 0)
        chkModeNew.Size = New Size(228, 24)
        chkModeNew.TabIndex = 1
        chkModeNew.Text = "全新授权码"
        chkModeNew.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(474, 30)
        Label1.TabIndex = 7
        Label1.Text = "授权码检测失败，错误信息："
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' frmAuthModify
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(500, 300)
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        Name = "frmAuthModify"
        Padding = New Padding(10)
        ShowIcon = False
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterParent
        Text = "Authorization Error"
        TopMost = True
        tlpMain.ResumeLayout(False)
        tlpBtns.ResumeLayout(False)
        grpMode.ResumeLayout(False)
        tlpGrp.ResumeLayout(False)
        tlpGrp.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents lblExpMsg As Label
    Friend WithEvents tbInput As TextBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents tlpBtns As TableLayoutPanel
    Friend WithEvents grpMode As GroupBox
    Friend WithEvents tlpGrp As TableLayoutPanel
    Friend WithEvents chkModUpdate As RadioButton
    Friend WithEvents chkModeNew As RadioButton
    Friend WithEvents Label1 As Label
End Class
