Imports System.ComponentModel
Imports Ouroboros.Hmi.Library.Elements
Imports Knx.Falcon

Public MustInherit Class KnxHmiShape

    Public Event GroupValueChanged As ValueChangeHandler(Of GroupValue)

    Private _Shape As HmiShapeType = HmiShapeType.Ellipse
    Private _RawSize As Size
    Private _Opacity As Byte
    Private _FillColor As Color = Color.Gray
    Private _StrokeColor As Color = Color.Black
    Private _StrokeWidth As UInteger = 0

    Protected Friend Tip As New ToolTip

    ''' <summary>
    ''' 控件原始尺寸
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 控件形状
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 可见度
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 填充颜色
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 线条颜色
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 线条宽度
    ''' </summary>
    ''' <returns></returns>
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

    ''' <summary>
    ''' 绑定效果
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping"), Browsable(False)>
    Public Property Mapping As KnxHmiMapping

    <Category("Mapping"), Browsable(False)>
    Public Property KnxGroup As KnxGroup

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim g As Graphics = e.Graphics '创建一个Graphics对象
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim fillBrush As New SolidBrush(FillColor) '绘制形状实体作为反馈
        Dim strokePen As New Pen(StrokeColor, StrokeWidth) '绘制形状边框作为控制
        Select Case Shape
            Case HmiShapeType.Ellipse, HmiShapeType.Text
                Dim frame As New Rectangle(Padding.All + 1, Padding.All + 1, RawSize.Width - 3, RawSize.Height - 3)
                If FillColor.A > 0 Then g.FillEllipse(fillBrush, frame) '绘制圆形
                If StrokeWidth > 0 Then g.DrawEllipse(strokePen, frame) '绘制边框
            Case HmiShapeType.Rectangle
                Dim frame As New Rectangle(Padding.All + 1, Padding.All + 1, RawSize.Width - 3, RawSize.Height - 3)
                If FillColor.A > 0 Then g.FillRectangle(fillBrush, frame) '绘制矩形
                If StrokeWidth > 0 Then g.DrawRectangle(strokePen, frame) '绘制边框
            Case Else
                Throw New Exception($"HmiShapeType '{Shape.ToString}' is not supported in current version.")
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

    'Public Sub New()
    '    InitializeComponent()
    '    RawSize = Size
    '    SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    '    BackColor = Color.Transparent
    '    Me.KnxGroup = New KnxGroup()
    '    AddHandler Me.KnxGroup.GroupValueChanged, AddressOf _GroupValueChanged
    'End Sub

    Public Sub New(comp As KnxHmiComponent)
        InitializeComponent()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        With comp
            Padding = New Padding(Math.Ceiling(.StrokeWidth \ 2) + 0) '内边距，防止绘制不完整
            Left = .RawLocation.X - Padding.All
            Top = .RawLocation.Y - Padding.All
            Width = .RawSize.Width + Padding.All * 2
            Height = .RawSize.Height + Padding.All * 2
            RawSize = .RawSize
            Shape = .Shape
            FillColor = .FillColor
            StrokeColor = .StrokeColor
            StrokeWidth = .StrokeWidth
            Mapping = .Mapping
            Text = .Text
            Visible = True
            KnxGroup = .Group
        End With
        AddHandler Me.KnxGroup.GroupValueChanged, AddressOf _GroupValueChanged
        _GroupValueChanged(Me.KnxGroup.Value) '初始化时调用值变化事件
    End Sub

    Private Sub KnxHmiShape_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Tip.Show(Text, sender)
    End Sub

    Private Sub KnxHmiShape_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Tip.Hide(sender)
    End Sub

    Protected Friend MustOverride Sub _GroupValueChanged(value As GroupValue)

End Class
