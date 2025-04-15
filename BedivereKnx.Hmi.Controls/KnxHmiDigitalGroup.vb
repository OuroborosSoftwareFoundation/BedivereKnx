Imports System.ComponentModel
Imports Knx.Falcon

Public Class KnxHmiDigitalGroup

    '''' <summary>
    '''' 值变化对象
    '''' </summary>
    'Public Event GroupValueChanged As ValueChangeHandler(Of GroupValue)

    Private _FdbColor As Color = Color.Purple
    Private _KnxObject As KnxObject

    Private WithEvents btn As Button

    ''' <summary>
    ''' 反馈框宽度
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FdbWidth As Integer = 15

    ''' <summary>
    ''' 填充颜色
    ''' </summary>
    ''' <returns></returns>
    <Category("Appearance")>
    Public Property FdbColor As Color
        Get
            Return _FdbColor
        End Get
        Set
            _FdbColor = Value
            Invalidate() '重绘控件
        End Set
    End Property

    ''' <summary>
    ''' 颜色绑定字典
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping"), Browsable(False)>
    Public Property MappingColors As Dictionary(Of GroupValue, Color)

    ''' <summary>
    ''' KNX对象
    ''' </summary>
    ''' <returns></returns>
    Public Property KnxObject As KnxObject
        Get
            Return _KnxObject
        End Get
        Set
            _KnxObject = Value
            AddHandler _KnxObject.Groups(KnxObjectPart.SwitchFeedback).GroupValueChanged, AddressOf _GroupValueChanged
            _GroupValueChanged(_KnxObject.Groups(KnxObjectPart.SwitchFeedback).Value) '调用值变化事件
            'Invalidate() '重绘控件
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics '创建一个Graphics对象
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim fillBrush As New SolidBrush(_FdbColor) '绘制形状实体作为反馈
        Dim frame As New Rectangle(0, 0, FdbWidth, Me.Height)
        If _FdbColor.A > 0 Then g.FillRectangle(fillBrush, frame) '绘制矩形
        fillBrush.Dispose() '释放画笔对象
    End Sub

    'Public Sub New()
    '    InitializeComponent()
    '    Me.Padding = New Padding(FdbWidth, 0, 0, 0)
    'End Sub

    Public Sub New(comp As KnxHmiComponent)
        InitializeComponent()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True) '背景设置为透明
        Me.BackColor = Color.Transparent '自身背景色
        'Me.Left = comp.RawLocation.X
        'Me.Top = comp.RawLocation.Y
        Me.Location = comp.RawLocation '自身位置
        'Me.Width = comp.RawSize.Width
        'Me.Height = comp.RawSize.Height
        Me.Size = comp.RawSize '自身尺寸
        Me.MappingColors = New Dictionary(Of GroupValue, Color) '颜色绑定
        For i = 0 To comp.Mapping.Values.Length - 1
            Me.MappingColors.Add(comp.Mapping.Values(i), comp.Mapping.StrokeColors(i)) '控件的线条变换颜色为颜色绑定对象
        Next
        Me.btn = New Button With {
        .Dock = DockStyle.Right,
        .Width = comp.RawSize.Width - Me.FdbWidth,
        .BackColor = comp.FillColor,
        .ForeColor = comp.FontColor,
        .Text = comp.Text,
        .Visible = True
        } '设置按钮属性
        btn.Font = New Font(btn.Font.Name, comp.FontSize) '按钮字体大小
        Me.Controls.Add(Me.btn) '向控件中添加按钮
    End Sub

    ''' <summary>
    ''' 按钮点击事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub KnxHmiButton_MouseClick(sender As Object, e As MouseEventArgs) Handles btn.MouseClick
        KnxObject.SwitchToggle()
    End Sub

    ''' <summary>
    ''' 值变化事件
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub _GroupValueChanged(value As GroupValue)
        Me.FdbColor = Me.MappingColors(value)
    End Sub

End Class
