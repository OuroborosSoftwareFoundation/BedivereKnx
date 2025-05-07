<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNetworkInfo
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
        btnOK = New Button()
        tlpBottom = New TableLayoutPanel()
        btnCancel = New Button()
        lvNI = New ListView()
        tlpBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' btnOK
        ' 
        btnOK.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        btnOK.Location = New Point(58, 3)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(120, 34)
        btnOK.TabIndex = 1
        btnOK.Text = "确定"
        btnOK.UseVisualStyleBackColor = True
        ' 
        ' tlpBottom
        ' 
        tlpBottom.ColumnCount = 3
        tlpBottom.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBottom.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        tlpBottom.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        tlpBottom.Controls.Add(btnOK, 0, 0)
        tlpBottom.Controls.Add(btnCancel, 2, 0)
        tlpBottom.Dock = DockStyle.Bottom
        tlpBottom.Location = New Point(0, 513)
        tlpBottom.Name = "tlpBottom"
        tlpBottom.RowCount = 1
        tlpBottom.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpBottom.Size = New Size(382, 40)
        tlpBottom.TabIndex = 2
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btnCancel.Location = New Point(204, 3)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(120, 34)
        btnCancel.TabIndex = 2
        btnCancel.Text = "取消"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' lvNI
        ' 
        lvNI.Alignment = ListViewAlignment.SnapToGrid
        lvNI.Dock = DockStyle.Fill
        lvNI.FullRowSelect = True
        lvNI.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lvNI.Location = New Point(0, 0)
        lvNI.MultiSelect = False
        lvNI.Name = "lvNI"
        lvNI.Size = New Size(382, 513)
        lvNI.Sorting = SortOrder.Ascending
        lvNI.TabIndex = 3
        lvNI.UseCompatibleStateImageBehavior = False
        lvNI.View = View.Details
        ' 
        ' frmNetworkInfo
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = btnCancel
        ClientSize = New Size(382, 553)
        Controls.Add(lvNI)
        Controls.Add(tlpBottom)
        Name = "frmNetworkInfo"
        StartPosition = FormStartPosition.CenterParent
        Text = "选择本地IP地址"
        tlpBottom.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents btnOK As Button
    Friend WithEvents tlpBottom As TableLayoutPanel
    Friend WithEvents btnCancel As Button
    Friend WithEvents lvNI As ListView
End Class
