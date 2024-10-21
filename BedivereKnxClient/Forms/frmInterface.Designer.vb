<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterface
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
        dgvIf = New DataGridView()
        btnConnect = New Button()
        CType(dgvIf, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvIf
        ' 
        dgvIf.AllowUserToAddRows = False
        dgvIf.AllowUserToDeleteRows = False
        dgvIf.AllowUserToResizeColumns = False
        dgvIf.AllowUserToResizeRows = False
        dgvIf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvIf.BackgroundColor = SystemColors.Window
        dgvIf.ColumnHeadersHeight = 29
        dgvIf.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvIf.Dock = DockStyle.Fill
        dgvIf.Location = New Point(0, 0)
        dgvIf.MultiSelect = False
        dgvIf.Name = "dgvIf"
        dgvIf.ReadOnly = True
        dgvIf.RowHeadersVisible = False
        dgvIf.RowHeadersWidth = 51
        dgvIf.RowTemplate.Height = 29
        dgvIf.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvIf.ShowCellErrors = False
        dgvIf.ShowCellToolTips = False
        dgvIf.ShowEditingIcon = False
        dgvIf.ShowRowErrors = False
        dgvIf.Size = New Size(800, 406)
        dgvIf.TabIndex = 4
        ' 
        ' btnConnect
        ' 
        btnConnect.Dock = DockStyle.Bottom
        btnConnect.Location = New Point(0, 406)
        btnConnect.Name = "btnConnect"
        btnConnect.Size = New Size(800, 44)
        btnConnect.TabIndex = 5
        btnConnect.Text = "重新连接"
        btnConnect.UseVisualStyleBackColor = True
        ' 
        ' frmInterface
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvIf)
        Controls.Add(btnConnect)
        Name = "frmInterface"
        ShowIcon = False
        StartPosition = FormStartPosition.Manual
        Text = "KNX接口"
        CType(dgvIf, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvIf As DataGridView
    Friend WithEvents btnConnect As Button
End Class
