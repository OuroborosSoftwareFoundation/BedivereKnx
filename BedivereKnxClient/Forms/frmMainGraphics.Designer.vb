<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainGraphics
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
        tvGraphics = New TreeView()
        picGraphics = New PictureBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        CType(picGraphics, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tvGraphics
        ' 
        tvGraphics.Dock = DockStyle.Fill
        tvGraphics.Location = New Point(3, 3)
        tvGraphics.Name = "tvGraphics"
        tvGraphics.Size = New Size(194, 559)
        tvGraphics.TabIndex = 0
        ' 
        ' picGraphics
        ' 
        picGraphics.Dock = DockStyle.Fill
        picGraphics.Location = New Point(203, 3)
        picGraphics.Name = "picGraphics"
        picGraphics.Size = New Size(626, 559)
        picGraphics.TabIndex = 1
        picGraphics.TabStop = False
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 200F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 300F))
        TableLayoutPanel1.Controls.Add(tvGraphics, 0, 0)
        TableLayoutPanel1.Controls.Add(picGraphics, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(1132, 565)
        TableLayoutPanel1.TabIndex = 2
        ' 
        ' frmMainGraphics
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1132, 565)
        Controls.Add(TableLayoutPanel1)
        FormBorderStyle = FormBorderStyle.None
        Name = "frmMainGraphics"
        StartPosition = FormStartPosition.CenterParent
        Text = "frmMainGraphics"
        CType(picGraphics, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tvGraphics As TreeView
    Friend WithEvents picGraphics As PictureBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
