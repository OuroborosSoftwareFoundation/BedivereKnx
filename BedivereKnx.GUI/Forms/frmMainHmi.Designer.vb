<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainHmi
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
        tvHmi = New TreeView()
        pnlHmi = New Panel()
        btnLeftHide = New Button()
        SplitContainer1 = New SplitContainer()
        pnlHmi.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SuspendLayout()
        ' 
        ' tvHmi
        ' 
        tvHmi.Dock = DockStyle.Fill
        tvHmi.Location = New Point(0, 0)
        tvHmi.Name = "tvHmi"
        tvHmi.Size = New Size(118, 563)
        tvHmi.TabIndex = 0
        ' 
        ' pnlHmi
        ' 
        pnlHmi.BackColor = Color.Transparent
        pnlHmi.Controls.Add(btnLeftHide)
        pnlHmi.Dock = DockStyle.Fill
        pnlHmi.Location = New Point(0, 0)
        pnlHmi.Name = "pnlHmi"
        pnlHmi.Size = New Size(1006, 563)
        pnlHmi.TabIndex = 2
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
        SplitContainer1.Panel1.Controls.Add(tvHmi)
        SplitContainer1.Panel1MinSize = 0
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.BackColor = Color.White
        SplitContainer1.Panel2.Controls.Add(pnlHmi)
        SplitContainer1.Size = New Size(1132, 565)
        SplitContainer1.SplitterDistance = 120
        SplitContainer1.TabIndex = 3
        ' 
        ' frmMainHmi
        ' 
        AutoScaleDimensions = New SizeF(9F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1132, 565)
        Controls.Add(SplitContainer1)
        Name = "frmMainHmi"
        StartPosition = FormStartPosition.CenterParent
        pnlHmi.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents tvHmi As TreeView
    Friend WithEvents pnlHmi As Panel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnLeftHide As Button
End Class
