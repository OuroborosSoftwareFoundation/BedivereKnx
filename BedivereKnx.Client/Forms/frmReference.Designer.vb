<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReference
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
        lvRef = New ListView()
        Assy = New ColumnHeader()
        Ver = New ColumnHeader()
        btnOK = New Button()
        SuspendLayout()
        ' 
        ' lvRef
        ' 
        lvRef.Alignment = ListViewAlignment.Default
        lvRef.Columns.AddRange(New ColumnHeader() {Assy, Ver})
        lvRef.Dock = DockStyle.Fill
        lvRef.FullRowSelect = True
        lvRef.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lvRef.Location = New Point(10, 10)
        lvRef.MultiSelect = False
        lvRef.Name = "lvRef"
        lvRef.Size = New Size(562, 303)
        lvRef.Sorting = SortOrder.Ascending
        lvRef.TabIndex = 7
        lvRef.UseCompatibleStateImageBehavior = False
        lvRef.View = View.Details
        ' 
        ' Assy
        ' 
        Assy.Text = "Assembly Name"
        Assy.Width = 150
        ' 
        ' Ver
        ' 
        Ver.Text = "Version"
        Ver.Width = 150
        ' 
        ' btnOK
        ' 
        btnOK.Dock = DockStyle.Bottom
        btnOK.Location = New Point(10, 313)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(562, 30)
        btnOK.TabIndex = 6
        btnOK.Text = "OK"
        ' 
        ' frmReference
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(582, 353)
        Controls.Add(lvRef)
        Controls.Add(btnOK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmReference"
        Padding = New Padding(10)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Referenced Assemblies"
        ResumeLayout(False)
    End Sub

    Friend WithEvents lvRef As ListView
    Friend WithEvents Assy As ColumnHeader
    Friend WithEvents Ver As ColumnHeader
    Friend WithEvents btnOK As Button
End Class
