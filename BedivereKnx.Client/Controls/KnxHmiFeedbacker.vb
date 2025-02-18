Imports System.ComponentModel
Imports BedivereKnx.Graphics
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Ouroboros.Hmi

'Public Class KnxHmiFeedbacker : Inherits Control

'    Implements IHmiElement
'    Private Component As New KnxHmiComponent
'    Private _ShapeType As HmiShapeType
'    Private _GroupValue As GroupValue
'    Protected Friend Tip As New ToolTip

'    ''' <summary>
'    ''' 形状
'    ''' </summary>
'    ''' <returns></returns>
'    <Category("Appearance"), DefaultValue(HmiShapeType.Ellipse)>
'    Public Property ShapeType As HmiShapeType
'        Get
'            Return Component.Shape
'        End Get
'        Set
'            Component.Shape = Value
'            Invalidate() '形状改变时重绘控件
'        End Set
'    End Property

'    '''' <summary>
'    '''' 是否可见
'    '''' </summary>
'    '''' <returns></returns>
'    'Public Shadows Property Visible As Boolean Implements IHmiElement.Visible

'    '''' <summary>
'    '''' 坐标和尺寸
'    '''' </summary>
'    '''' <returns></returns>
'    'Public Property Geometry As HmiGeometry Implements IHmiElement.Geometry

'    ''' <summary>
'    ''' 控件坐标
'    ''' </summary>
'    ''' <returns></returns>
'    Public Shadows Property RawLocation As Point Implements IHmiElement.RawLocation

'    ''' <summary>
'    ''' 控件尺寸
'    ''' </summary>
'    ''' <returns></returns>
'    Public Shadows Property RawSize As Size Implements IHmiElement.RawSize

'    ''' <summary>
'    ''' 不透明度
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property Opacity As Byte Implements IHmiElement.Opacity

'    ''' <summary>
'    ''' 填充颜色
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property FillColor As Color Implements IHmiElement.FillColor

'    ''' <summary>
'    ''' 线条颜色
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property StrokeColor As Color Implements IHmiElement.StrokeColor

'    ''' <summary>
'    ''' 线条宽度
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property StrokeWidth As UInteger Implements IHmiElement.StrokeWidth

'    <Category("Hmi"), DefaultValue(HmiComponentDirection.Feedback)>
'    Public ReadOnly Property Direction As HmiComponentDirection
'        Get
'            Return Component.Direction
'        End Get
'    End Property

'    <Category("KNX")>
'    Public ReadOnly Property GroupAddress As GroupAddress
'        Get
'            Return Component.Group.Address
'        End Get
'    End Property

'    <Category("KNX")>
'    Public ReadOnly Property DPT As DptBase
'        Get
'            Return Component.Group.DPT
'        End Get
'    End Property

'    ''' <summary>
'    ''' 当前值
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property GroupValue As GroupValue
'        Get
'            Return _GroupValue
'        End Get
'        Set(value As GroupValue)
'            _GroupValue = value
'            If Me.Component.isDynamics Then

'                Invalidate() '重新绘制控件
'            End If
'        End Set
'    End Property

'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        Select Case Me.Direction
'            Case HmiComponentDirection.Feedback
'                Dim g As System.Drawing.Graphics = e.Graphics '创建一个Graphics对象
'                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
'                Dim fillBrush As New SolidBrush(Component.FillColor) '绘制形状实体作为反馈
'                Dim strokePen As New Pen(Component.StrokeColor, Component.StrokeWidth) '绘制形状边框作为控制
'                Select Case Me.ShapeType
'                    Case HmiShapeType.Ellipse, HmiShapeType.Text
'                        Dim frame As New Rectangle(1, 1, Component.RawSize.Width, Component.RawSize.Height)
'                        g.FillEllipse(fillBrush, frame) '绘制圆形
'                        If Component.StrokeWidth > 0 Then g.DrawEllipse(strokePen, frame) '绘制边框
'                    Case HmiShapeType.Rectangle
'                        Dim frame As New Rectangle(1, 1, Component.RawSize.Width, Component.RawSize.Height)
'                        g.FillRectangle(fillBrush, frame) '绘制圆形
'                        If Component.StrokeWidth > 0 Then g.DrawRectangle(strokePen, frame) '绘制边框
'                    Case Else
'                        Throw New Exception($"HmiShapeType '{Me.ShapeType.ToString}' is not supported in current version.")
'                End Select
'                fillBrush.Dispose() '释放画笔对象
'                strokePen.Dispose() '释放画笔对象
'            Case HmiComponentDirection.Control

'        End Select
'        MyBase.OnPaint(e)
'    End Sub

'    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
'        ' 不调用 MyBase.OnPaintBackground(pevent)，从而阻止绘制背景
'    End Sub

'    Protected Overrides Sub OnResize(e As EventArgs)
'        MyBase.OnResize(e)
'        Invalidate() '重新绘制控件以应用新的大小
'    End Sub

'    Public Sub New()
'        InitializeComponent()
'    End Sub

'    Public Sub New(comp As KnxHmiComponent)
'        InitializeComponent()
'        Component = comp
'        With Component
'            Dim pad As Integer = Math.Ceiling(.StrokeWidth \ 2) + 1 '内边距，防止绘制不完整
'            Me.Left = .RawLocation.X - pad
'            Me.Top = .RawLocation.Y - pad
'            Me.Width = .RawSize.Width + pad * 2
'            Me.Height = .RawSize.Height + pad * 2
'            Me.Visible = True '.Visible
'        End With
'    End Sub

'    Private Sub KnxSwitchIndicator_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
'        Me.Tip.Show($"{Me.GroupAddress}", sender)
'    End Sub

'    Private Sub KnxSwitchIndicator_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
'        Me.Tip.Hide(sender)
'    End Sub

'End Class
