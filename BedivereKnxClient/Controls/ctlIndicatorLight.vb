Imports System.ComponentModel

Public Class KnxSwitchIndicator

    Inherits Control

    Private _ShapeType As IndicatorShapeType
    Private _ControlStatus As Boolean
    Private _FeedbackStatus As Boolean
    Private _ControlOffColor As Color = Color.Gray
    Private _ControlOnColor As Color = Color.Orange
    Private _FeedbackOffColor As Color = Color.Gray
    Private _FeedbackOnColor As Color = Color.Green

    Private _ToolTip As New ToolTip

    <Browsable(False)>
    Public Property Selected As Boolean

    ''' <summary>
    ''' 形状
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue(IndicatorShapeType.Ellipse)>
    Public Property ShapeType As IndicatorShapeType
        Get
            Return _ShapeType
        End Get
        Set
            _ShapeType = Value
            Invalidate() '形状改变时重绘控件
        End Set
    End Property

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

    ''' <summary>
    ''' 控制状态
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(False), Description("Control Status")>
    Public Property ControlStatus As Boolean
        Get
            Return _ControlStatus
        End Get
        Set
            _ControlStatus = Value
            Invalidate() '状态改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 反馈状态
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(False), Description("Feedback Status")>
    Public Property FeedbackStatus As Boolean
        Get
            Return _FeedbackStatus
        End Get
        Set
            _FeedbackStatus = Value
            Invalidate() '状态改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 控制0时颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(GetType(Color), "Gray")>
    Public Property ControlOffColor As Color
        Get
            Return _ControlOffColor
        End Get
        Set(Value As Color)
            _ControlOffColor = Value
            Invalidate() '颜色改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 控制1时颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(GetType(Color), "Orange")>
    Public Property ControlOnColor As Color
        Get
            Return _ControlOnColor
        End Get
        Set(Value As Color)
            _ControlOnColor = Value
            Invalidate() '颜色改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 反馈0时颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(GetType(Color), "Gray")>
    Public Property FeedbackOffColor As Color
        Get
            Return _FeedbackOffColor
        End Get
        Set(Value As Color)
            _FeedbackOffColor = Value
            Invalidate() '颜色改变时重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 反馈1时颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Status"), DefaultValue(GetType(Color), "Green")>
    Public Property FeedbackOnColor As Color
        Get
            Return _FeedbackOnColor
        End Get
        Set(Value As Color)
            _FeedbackOnColor = Value
            Invalidate() '颜色改变时重绘控件
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics '创建一个Graphics对象
        Dim brush As New SolidBrush(IIf(Me.FeedbackStatus, Me.FeedbackOnColor, Me.FeedbackOffColor)) '绘制形状实体作为反馈
        Dim pen As New Pen(color:=IIf(Me.ControlStatus, Me.ControlOnColor, Me.ControlOffColor), 3) '绘制形状边框作为控制
        Select Case Me.ShapeType
            Case IndicatorShapeType.Ellipse
                Dim el As New Rectangle(0, 0, Me.Width, Me.Height)
                g.FillEllipse(brush, el) '绘制圆形
                g.DrawEllipse(pen, el)'绘制边框
            Case IndicatorShapeType.Rectangle
                Dim rect As New Rectangle(1, 1, Me.Width - 2, Me.Height - 2)
                g.FillRectangle(brush, rect)
                g.DrawRectangle(pen, rect)
            Case Else
                Dim el As New Rectangle(0, 0, Me.Width, Me.Height)
                g.FillEllipse(brush, el) '绘制圆形
                g.DrawEllipse(pen, el) '绘制边框
        End Select
        brush.Dispose() '释放画笔对象
        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Invalidate() '重新绘制控件以应用新的大小
    End Sub

    Public Sub New()
        InitializeComponent()
        'BackColor = Color.Transparent '背景设置为透明
        _ToolTip.ShowAlways = True
    End Sub

    Public Sub Toggle()
        FeedbackStatus = Not FeedbackStatus
    End Sub

    Private Sub IndicatorLight_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Me.Selected = Not Me.Selected
    End Sub

    Private Sub KnxSwitchIndicator_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        _ToolTip.Show(Me.Text, sender)
    End Sub

    Private Sub KnxSwitchIndicator_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        _ToolTip.Hide(sender)
    End Sub

End Class
