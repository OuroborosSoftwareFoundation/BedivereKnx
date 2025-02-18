Imports System.ComponentModel
Imports Ouroboros.Hmi

Public Class HmiFeedbackWidget : Inherits Control

    Private Component As New HmiComponentElement
    Private _ShapeType As HmiShapeType
    Protected Friend Tip As New ToolTip

    ''' <summary>
    ''' 形状
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue(HmiShapeType.Ellipse)>
    Public Property ShapeType As HmiShapeType
        Get
            Return _ShapeType
        End Get
        Set
            _ShapeType = Value
            Invalidate() '形状改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 不透明度
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue(100)>
    Public Property Opacity As Integer

    ''' <summary>
    ''' 填充颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue("DarkGray")>
    Public Property FillColor As Color

    ''' <summary>
    ''' 线条颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue("Black")>
    Public Property StrokeColor As Color

    ''' <summary>
    ''' 线条宽度
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue(0)>
    Public Property StrokeWidth As Integer

    <Category("Hmi"), DefaultValue(HmiComponentDirection.Feedback)>
    Public Property Direction As HmiComponentDirection

    ''' <summary>
    ''' 中心位置
    ''' </summary>
    ''' <returns></returns>
    <Category("Layout"), Browsable(False)>
    Public Property Center As Point
        Get
            Return New Point(Me.Location.X - Width / 2, Me.Location.Y - Height / 2)
        End Get
        Set(Value As Point)
            Me.Location = New Point(Value.X - Width / 2, Value.Y - Height / 2)
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Select Case Me.Direction
            Case HmiComponentDirection.Feedback
                Dim g As System.Drawing.Graphics = e.Graphics '创建一个Graphics对象
                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                Dim fillBrush As New SolidBrush(Component.FillColor) '绘制形状实体作为反馈
                Dim strokePen As New Pen(Component.StrokeColor, Component.StrokeWidth) '绘制形状边框作为控制
                Select Case Me.ShapeType
                    Case HmiShapeType.Ellipse, HmiShapeType.Text
                        Dim frame As New Rectangle(1, 1, Component.Size.Width, Component.Size.Height)
                        g.FillEllipse(fillBrush, frame) '绘制圆形
                        If Component.StrokeWidth > 0 Then g.DrawEllipse(strokePen, frame) '绘制边框
                    Case HmiShapeType.Rectangle
                        Dim frame As New Rectangle(1, 1, Component.Size.Width, Component.Size.Height)
                        g.FillRectangle(fillBrush, frame) '绘制圆形
                        If Component.StrokeWidth > 0 Then g.DrawRectangle(strokePen, frame) '绘制边框
                    Case Else
                        Throw New Exception($"HmiShapeType '{Me.ShapeType.ToString}' is not supported in current version.")
                End Select
                fillBrush.Dispose() '释放画笔对象
                strokePen.Dispose() '释放画笔对象
            Case HmiComponentDirection.Control

        End Select
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
    End Sub

    Public Sub New(comp As HmiComponentElement)
        InitializeComponent()
        Component = comp
        With Component
            Dim pad As Integer = Math.Ceiling(.StrokeWidth \ 2) + 1 '内边距，防止绘制不完整
            Me.Left = .Location.X - pad
            Me.Top = .Location.Y - pad
            Me.Width = .Size.Width + pad * 2
            Me.Height = .Size.Height + pad * 2
            Me.Visible = True '.Visible
            Me.ShapeType = .Shape
            Me.Direction = .Direction
        End With
    End Sub

    Private Sub KnxSwitchIndicator_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.Tip.Show(Me.Text, sender)
    End Sub

    Private Sub KnxSwitchIndicator_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.Tip.Hide(sender)
    End Sub

End Class
