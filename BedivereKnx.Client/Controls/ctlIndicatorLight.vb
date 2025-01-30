Imports System.ComponentModel
Imports Knx.Falcon
Imports BedivereKnx.Graphics

Public Class KnxSwitchIndicator

    Inherits Control

    Private _ShapeType As KnxComponentShape
    Private _ControlStatus As Boolean
    Private _FeedbackStatus As Boolean
    Private _ControlOffColor As Color = Color.Gray
    Private _ControlOnColor As Color = Color.Orange
    Private _FeedbackOffColor As Color = Color.Gray
    Private _FeedbackOnColor As Color = Color.Green

    Private _ToolTip As New ToolTip
    Private _FeedbackValue As GroupValue

    <Browsable(False)>
    Public Property Selected As Boolean

    ''' <summary>
    ''' 形状
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance"), DefaultValue(KnxComponentShape.Ellipse)>
    Public Property ShapeType As KnxComponentShape
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

    <Category("KNX")>
    Public Property ControlAddress As GroupAddress

    <Category("KNX")>
    Public Property FeedbackAddress As GroupAddress

    <Category("KNX"), Browsable(False)>
    Public Property FeedbackValue As GroupValue
        Get
            Return _FeedbackValue
        End Get
        Set(Value As GroupValue)
            If Not (Value Is Nothing) Then
                _FeedbackValue = Value
                Me.FeedbackStatus = Convert.ToBoolean(Value.TypedValue)
            End If
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
        Dim g As System.Drawing.Graphics = e.Graphics '创建一个Graphics对象
        Dim brush As New SolidBrush(IIf(Me.FeedbackStatus, Me.FeedbackOnColor, Me.FeedbackOffColor)) '绘制形状实体作为反馈
        Dim pen As New Pen(color:=IIf(Me.ControlStatus, Me.ControlOnColor, Me.ControlOffColor), 3) '绘制形状边框作为控制
        Select Case Me.ShapeType
            Case KnxComponentShape.Ellipse
                Dim el As New Rectangle(0, 0, Me.Width, Me.Height)
                g.FillEllipse(brush, el) '绘制圆形
                If Me.ControlAddress <> New GroupAddress(0US) Then
                    g.DrawEllipse(pen, el) '绘制边框
                End If
            Case KnxComponentShape.Rectangle
                Dim rect As New Rectangle(1, 1, Me.Width - 2, Me.Height - 2)
                g.FillRectangle(brush, rect)
                If Me.ControlAddress <> New GroupAddress(0US) Then
                    g.DrawRectangle(pen, rect)
                End If
            Case Else
                Dim el As New Rectangle(0, 0, Me.Width, Me.Height)
                g.FillEllipse(brush, el) '绘制圆形
                If Me.ControlAddress <> New GroupAddress(0US) Then
                    g.DrawEllipse(pen, el) '绘制边框
                End If
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
    End Sub

    Public Sub New(comp As KnxGpxComponent)
        InitializeComponent()
        Me.Location = comp.Location
        Me.Size = comp.Size
        'Me.FeedbackAddress = comp.GroupAddress
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
