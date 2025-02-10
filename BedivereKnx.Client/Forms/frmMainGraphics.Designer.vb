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
        tvGpx = New TreeView()
        pnlGpx = New Panel()
        btnLeftHide = New Button()
        SplitContainer1 = New SplitContainer()
        pnlGpx.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tvGpx
        ' 
        tvGpx.Dock = DockStyle.Fill
        tvGpx.Location = New Point(0, 0)
        tvGpx.Name = "tvGpx"
        tvGpx.Size = New Size(118, 563)
        tvGpx.TabIndex = 0
        ' 
        ' pnlGpx
        ' 
        pnlGpx.BackColor = Color.Transparent
        pnlGpx.Controls.Add(btnLeftHide)
        pnlGpx.Dock = DockStyle.Fill
        pnlGpx.Location = New Point(0, 0)
        pnlGpx.Name = "pnlGpx"
        pnlGpx.Size = New Size(1006, 563)
        pnlGpx.TabIndex = 2
        ' 
        ' btnLeftHide
        ' 
        btnLeftHide.Location = New Point(0, 0)
        btnLeftHide.Name = "btnLeftHide"
        btnLeftHide.Size = New Size(10, 20)
        btnLeftHide.TabIndex = 0
        btnLeftHide.UseVisualStyleBackColor = True
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.BorderStyle = BorderStyle.FixedSingle
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 0)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(tvGpx)
        SplitContainer1.Panel1MinSize = 0
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.BackColor = Color.White
        SplitContainer1.Panel2.Controls.Add(pnlGpx)
        SplitContainer1.Size = New Size(1132, 565)
        SplitContainer1.SplitterDistance = 120
        SplitContainer1.TabIndex = 3
        ' 
        ' frmMainGraphics
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1132, 565)
        Controls.Add(SplitContainer1)
        Name = "frmMainGraphics"
        StartPosition = FormStartPosition.CenterParent
        Text = "frmMainGraphics"
        pnlGpx.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tvGpx As TreeView
    Friend WithEvents pnlGpx As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnLeftHide As Button
End Class
