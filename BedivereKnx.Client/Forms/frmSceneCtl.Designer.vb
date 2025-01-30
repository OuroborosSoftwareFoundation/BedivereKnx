<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSceneCtl
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
        btnOk = New Button()
        btnCancel = New Button()
        tlpBottom = New TableLayoutPanel()
        tlpMain = New TableLayoutPanel()
        lblScnName = New Label()
        lblScnAddr = New Label()
        lvScn = New ListView()
        tlpBottom.SuspendLayout()
        tlpMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOk
        ' 
        btnOk.Anchor = AnchorStyles.None
        btnOk.Location = New Point(43, 5)
        btnOk.Margin = New Padding(4, 5, 4, 5)
        btnOk.Name = "btnOk"
        btnOk.Size = New Size(100, 30)
        btnOk.TabIndex = 0
        btnOk.Text = "确定"
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.None
        btnCancel.Location = New Point(230, 5)
        btnCancel.Margin = New Padding(4, 5, 4, 5)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(100, 30)
        btnCancel.TabIndex = 1
        btnCancel.Text = "取消"
        ' 
        ' tlpBottom
        ' 
        tlpBottom.ColumnCount = 2
        tlpBottom.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBottom.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBottom.Controls.Add(btnOk, 0, 0)
        tlpBottom.Controls.Add(btnCancel, 1, 0)
        tlpBottom.Dock = DockStyle.Fill
        tlpBottom.Location = New Point(4, 208)
        tlpBottom.Margin = New Padding(4, 5, 4, 5)
        tlpBottom.Name = "tlpBottom"
        tlpBottom.RowCount = 1
        tlpBottom.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        tlpBottom.Size = New Size(374, 40)
        tlpBottom.TabIndex = 0
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 1
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.Controls.Add(tlpBottom, 0, 3)
        tlpMain.Controls.Add(lblScnName, 0, 0)
        tlpMain.Controls.Add(lblScnAddr, 0, 1)
        tlpMain.Controls.Add(lvScn, 0, 2)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 4
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 30F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 50F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpMain.Size = New Size(382, 253)
        tlpMain.TabIndex = 1
        ' 
        ' lblScnName
        ' 
        lblScnName.Dock = DockStyle.Fill
        lblScnName.Location = New Point(3, 0)
        lblScnName.Name = "lblScnName"
        lblScnName.Padding = New Padding(10, 0, 0, 0)
        lblScnName.Size = New Size(376, 30)
        lblScnName.TabIndex = 1
        lblScnName.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblScnAddr
        ' 
        lblScnAddr.Dock = DockStyle.Fill
        lblScnAddr.Location = New Point(3, 30)
        lblScnAddr.Name = "lblScnAddr"
        lblScnAddr.Padding = New Padding(10, 0, 0, 0)
        lblScnAddr.Size = New Size(376, 30)
        lblScnAddr.TabIndex = 3
        ' 
        ' lvScn
        ' 
        lvScn.Alignment = ListViewAlignment.Default
        lvScn.Dock = DockStyle.Fill
        lvScn.FullRowSelect = True
        lvScn.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lvScn.Location = New Point(3, 63)
        lvScn.MultiSelect = False
        lvScn.Name = "lvScn"
        lvScn.Size = New Size(376, 137)
        lvScn.TabIndex = 4
        lvScn.UseCompatibleStateImageBehavior = False
        lvScn.View = View.Details
        ' 
        ' frmSceneCtl
        ' 
        AcceptButton = btnOk
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(382, 253)
        Controls.Add(tlpMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSceneCtl"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "场景控制"
        tlpBottom.ResumeLayout(False)
        tlpMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents lblScnName As Label
    Friend WithEvents lblScnAddr As Label
    Friend WithEvents lvScn As ListView

End Class
