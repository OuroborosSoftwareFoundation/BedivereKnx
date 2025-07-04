<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScheduleSeq
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
        dgvScd = New DataGridView()
        CType(dgvScd, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvScd
        ' 
        dgvScd.AllowUserToAddRows = False
        dgvScd.AllowUserToDeleteRows = False
        dgvScd.AllowUserToResizeRows = False
        dgvScd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvScd.BackgroundColor = SystemColors.Window
        dgvScd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvScd.Dock = DockStyle.Fill
        dgvScd.Location = New Point(0, 0)
        dgvScd.MultiSelect = False
        dgvScd.Name = "dgvScd"
        dgvScd.ReadOnly = True
        dgvScd.RowHeadersVisible = False
        dgvScd.RowHeadersWidth = 51
        dgvScd.RowTemplate.Height = 29
        dgvScd.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvScd.ShowCellErrors = False
        dgvScd.ShowCellToolTips = False
        dgvScd.ShowEditingIcon = False
        dgvScd.ShowRowErrors = False
        dgvScd.Size = New Size(982, 553)
        dgvScd.TabIndex = 0
        ' 
        ' frmScheduleSeq
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(982, 553)
        Controls.Add(dgvScd)
        Name = "frmScheduleSeq"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "定时队列"
        CType(dgvScd, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvScd As DataGridView
End Class
