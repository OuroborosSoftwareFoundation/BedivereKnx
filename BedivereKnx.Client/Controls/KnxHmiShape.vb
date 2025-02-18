Imports System.ComponentModel
Imports BedivereKnx.Graphics
Imports Ouroboros.Hmi

Public Class KnxHmiShape
    Private _Shape As HmiShapeType = HmiShapeType.Ellipse
    Private _RawSize As Size
    Private _Opacity As Byte
    Private _FillColor As Color = Color.Gray
    Private _StrokeColor As Color = Color.Black
    Private _StrokeWidth As UInteger = 0

    Protected Friend Tip As New ToolTip

    <Category("Appearance"), DefaultValue(HmiShapeType.Ellipse)>
    Public Property Shape As HmiShapeType
        Get
            Return _Shape
        End Get
        Set
            _Shape = Value
            Invalidate() '重绘控件
        End Set
    End Property

    <Browsable(False)>
    Public Property RawSize As Size
        Get
            Return _RawSize
        End Get
        Set
            _RawSize = Value
            Invalidate() '重绘控件
        End Set
    End Property

    <Category("Appearance"), DefaultValue(CByte(255))>
    Public Property Opacity As Byte
        Get
            Return _Opacity
        End Get
        Set
            _Opacity = Value
            Invalidate() '重绘控件
        End Set
    End Property

    <Category("Appearance")>
    Public Property FillColor As Color
        Get
            Return _FillColor
        End Get
        Set
            _FillColor = Value
            Invalidate() '重绘控件
        End Set
    End Property

    <Category("Appearance")>
    Public Property StrokeColor As Color
        Get
            Return _StrokeColor
        End Get
        Set
            _StrokeColor = Value
            Invalidate() '重绘控件
        End Set
    End Property

    <Category("Appearance"), DefaultValue(0)>
    Public Property StrokeWidth As UInteger
        Get
            Return _StrokeWidth
        End Get
        Set
            _StrokeWidth = Value
            Invalidate() '重绘控件
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As System.Drawing.Graphics = e.Graphics '创建一个Graphics对象
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim fillBrush As New SolidBrush(Me.FillColor) '绘制形状实体作为反馈
        Dim strokePen As New Pen(Me.StrokeColor, Me.StrokeWidth) '绘制形状边框作为控制
        Select Case Me.Shape
            Case HmiShapeType.Ellipse, HmiShapeType.Text
                Dim frame As New Rectangle(Me.Padding.All, Me.Padding.All, Me.RawSize.Width, Me.RawSize.Height)
                g.FillEllipse(fillBrush, frame) '绘制圆形
                If Me.StrokeWidth > 0 Then g.DrawEllipse(strokePen, frame) '绘制边框
            Case HmiShapeType.Rectangle
                Dim frame As New Rectangle(Me.Padding.All, Me.Padding.All, Me.RawSize.Width, Me.RawSize.Height)
                g.FillRectangle(fillBrush, frame) '绘制矩形
                If Me.StrokeWidth > 0 Then g.DrawRectangle(strokePen, frame) '绘制边框
            Case Else
                Throw New Exception($"HmiShapeType '{Me.Shape.ToString}' is not supported in current version.")
        End Select
        fillBrush.Dispose() '释放画笔对象
        strokePen.Dispose() '释放画笔对象
        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        ' 不调用 MyBase.OnPaintBackground(pevent)，从而阻止绘制背景
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate() '重新绘制控件以应用新的大小
    End Sub

    Public Sub New()
        InitializeComponent()
        'Me.Padding = New Padding(Math.Ceiling(Me.StrokeWidth \ 2) + 1) '内边距，防止绘制不完整
        Me.RawSize = Me.Size
    End Sub

    Public Sub New(comp As KnxHmiComponent)
        InitializeComponent()
        With comp
            '_Pad = Math.Ceiling(.StrokeWidth \ 2) + 1 '内边距，防止绘制不完整
            Me.Padding = New Padding(Math.Ceiling(.StrokeWidth \ 2) + 1) '内边距，防止绘制不完整
            Me.Left = .RawLocation.X - -Padding.All
            Me.Top = .RawLocation.Y - -Padding.All
            Me.Width = .RawSize.Width + Padding.All * 2
            Me.Height = .RawSize.Height + Padding.All * 2
            Me.RawSize = .RawSize
            Me.Visible = True
            Me.Shape = .Shape
            Me.FillColor = .FillColor
            Me.StrokeColor = .StrokeColor
            Me.StrokeWidth = .StrokeWidth
        End With
    End Sub

    Private Sub KnxHmiShape_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.Tip.Show(Me.Text, sender)
    End Sub

    Private Sub KnxHmiShape_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.Tip.Hide(sender)
    End Sub

End Class
