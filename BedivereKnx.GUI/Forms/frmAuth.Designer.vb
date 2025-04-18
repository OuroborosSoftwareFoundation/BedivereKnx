<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAuth
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
        lvAuth = New ListView()
        Prop = New ColumnHeader()
        Value = New ColumnHeader()
        SuspendLayout()
        ' 
        ' btnOK
        ' 
        btnOK.Dock = DockStyle.Bottom
        btnOK.Location = New Point(10, 213)
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(462, 30)
        btnOK.TabIndex = 0
        btnOK.Text = "OK"
        ' 
        ' lvAuth
        ' 
        lvAuth.Alignment = ListViewAlignment.Default
        lvAuth.Columns.AddRange(New ColumnHeader() {Prop, Value})
        lvAuth.Dock = DockStyle.Fill
        lvAuth.FullRowSelect = True
        lvAuth.HeaderStyle = ColumnHeaderStyle.Nonclickable
        lvAuth.Location = New Point(10, 10)
        lvAuth.MultiSelect = False
        lvAuth.Name = "lvAuth"
        lvAuth.Size = New Size(462, 203)
        lvAuth.TabIndex = 5
        lvAuth.UseCompatibleStateImageBehavior = False
        lvAuth.View = View.Details
        ' 
        ' Prop
        ' 
        Prop.Text = "Property"
        Prop.Width = 100
        ' 
        ' Value
        ' 
        Value.Text = "Value"
        Value.Width = 200
        ' 
        ' frmAuth
        ' 
        AcceptButton = btnOK
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(482, 253)
        Controls.Add(lvAuth)
        Controls.Add(btnOK)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 5, 4, 5)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmAuth"
        Padding = New Padding(10)
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Authorization Information"
        ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lvAuth As ListView
    Friend WithEvents Prop As ColumnHeader
    Friend WithEvents Value As ColumnHeader

End Class
