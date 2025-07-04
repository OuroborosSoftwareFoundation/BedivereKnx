<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSchedule
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
        dgv = New DataGridView()
        tlpMain = New TableLayoutPanel()
        btnScdSeq = New Button()
        CType(dgv, ComponentModel.ISupportInitialize).BeginInit()
        tlpMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Dock = DockStyle.Fill
        dgv.Location = New Point(3, 3)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersWidth = 51
        dgv.RowTemplate.Height = 29
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Size = New Size(629, 494)
        dgv.TabIndex = 0
        ' 
        ' tlpMain
        ' 
        tlpMain.ColumnCount = 2
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200F))
        tlpMain.Controls.Add(btnScdSeq, 1, 0)
        tlpMain.Controls.Add(dgv, 0, 0)
        tlpMain.Dock = DockStyle.Fill
        tlpMain.Location = New Point(0, 0)
        tlpMain.Name = "tlpMain"
        tlpMain.RowCount = 1
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        tlpMain.Size = New Size(835, 500)
        tlpMain.TabIndex = 1
        ' 
        ' btnScdSeq
        ' 
        btnScdSeq.Dock = DockStyle.Fill
        btnScdSeq.Location = New Point(638, 3)
        btnScdSeq.Name = "btnScdSeq"
        btnScdSeq.Size = New Size(194, 494)
        btnScdSeq.TabIndex = 7
        btnScdSeq.Text = "定时事件表"
        btnScdSeq.UseVisualStyleBackColor = True
        ' 
        ' frmSchedule
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(835, 500)
        Controls.Add(tlpMain)
        Name = "frmSchedule"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "定时"
        CType(dgv, ComponentModel.ISupportInitialize).EndInit()
        tlpMain.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgv As DataGridView
    Friend WithEvents tlpMain As TableLayoutPanel
    Friend WithEvents btnScdSeq As Button
End Class
