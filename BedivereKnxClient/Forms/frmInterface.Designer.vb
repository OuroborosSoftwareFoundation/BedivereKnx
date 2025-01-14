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
        tlpTop = New TableLayoutPanel()
        Label1 = New Label()
        lblIfDefault = New Label()
        CType(dgvIf, ComponentModel.ISupportInitialize).BeginInit()
        tlpTop.SuspendLayout()
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
        dgvIf.Location = New Point(0, 40)
        dgvIf.MultiSelect = False
        dgvIf.Name = "dgvIf"
        dgvIf.ReadOnly = True
        dgvIf.RowHeadersVisible = False
        dgvIf.RowHeadersWidth = 51
        dgvIf.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvIf.ShowCellErrors = False
        dgvIf.ShowCellToolTips = False
        dgvIf.ShowEditingIcon = False
        dgvIf.ShowRowErrors = False
        dgvIf.Size = New Size(800, 366)
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
        ' tlpTop
        ' 
        tlpTop.ColumnCount = 2
        tlpTop.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 150F))
        tlpTop.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpTop.Controls.Add(Label1, 0, 0)
        tlpTop.Controls.Add(lblIfDefault, 1, 0)
        tlpTop.Dock = DockStyle.Top
        tlpTop.Location = New Point(0, 0)
        tlpTop.Name = "tlpTop"
        tlpTop.RowCount = 1
        tlpTop.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpTop.Size = New Size(800, 40)
        tlpTop.TabIndex = 6
        ' 
        ' Label1
        ' 
        Label1.Dock = DockStyle.Fill
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(144, 40)
        Label1.TabIndex = 0
        Label1.Text = "默认接口状态："
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblIfDefault
        ' 
        lblIfDefault.Dock = DockStyle.Fill
        lblIfDefault.Location = New Point(153, 0)
        lblIfDefault.Name = "lblIfDefault"
        lblIfDefault.Size = New Size(644, 40)
        lblIfDefault.TabIndex = 1
        lblIfDefault.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' frmInterface
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(dgvIf)
        Controls.Add(tlpTop)
        Controls.Add(btnConnect)
        Name = "frmInterface"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "KNX接口"
        CType(dgvIf, ComponentModel.ISupportInitialize).EndInit()
        tlpTop.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvIf As DataGridView
    Friend WithEvents btnConnect As Button
    Friend WithEvents tlpTop As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents lblIfDefault As Label
End Class
