Imports System.Drawing
Imports System.Text.RegularExpressions
Imports Knx.Falcon

Public Class KnxGpxComponent

    Inherits KnxGpxElement

    Private ReadOnly DefaultOffColor As Color = Color.Gray

    Private ReadOnly DefaultOnColor As Color = Color.Green

    Public Property Shape As KnxComponentShape

    Public Property StrokeWidth As Integer = 1

    ''' <summary>
    ''' 控件方向（控制/反馈）
    ''' </summary>
    ''' <returns></returns>
    Public Property Direction As KnxGpxComponentType

    ''' <summary>
    ''' KNX数值变化对象
    ''' </summary>
    ''' <returns></returns>
    Public Property Convertion As KnxGpxConvertion()

    ''' <summary>
    ''' 显示文字
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Text As String

    '''' <summary>
    '''' 关联组地址
    '''' </summary>
    '''' <returns></returns>
    'Public ReadOnly Property GroupAddress As GroupAddress

    Public Sub New()
        MyBase.New(KnxGpxElementMode.Component)
    End Sub

    Public Sub New(location As Point, size As Size, styleString As String, valueString As String)
        MyBase.New(KnxGpxElementMode.Component, location, size)
        ReadStyleString(styleString)
        ReadValueString(valueString)
    End Sub

    Public Sub New(geometry As GpxGeometry, styleString As String, valueString As String)
        Me.New(geometry.Location, geometry.Size, styleString, valueString)
    End Sub

    Private Sub ReadStyleString(styleString As String)
        Dim styles As String() = styleString.Trim.Split(";")
        Dim shape As String = styles(0).Trim.ToLower
        If shape = "ellipse" Then '椭圆或者椭圆圈
            Me.Shape = KnxComponentShape.Ellipse
        ElseIf shape.Contains("rounded") Then '矩形或者矩形框，其他形状后方style也可能出现rounded
            Me.Shape = KnxComponentShape.Rectangle
        ElseIf shape.Contains("endarrow") Then '线条
            Me.Shape = KnxComponentShape.Line
        ElseIf shape.Contains("text") Then '文本
            Me.Shape = KnxComponentShape.Text
        End If
        Dim lstConv As New List(Of KnxGpxConvertion)
        For i = 1 To styles.Length - 1
            Dim skv As String() = styles(i).Split("="c)
            Select Case skv(0).Trim.ToLower
                Case "strokewidth" '线条宽度
                    Me.StrokeWidth = Convert.ToInt32(skv(1))
                Case "fillcolor" '填充颜色
                    Dim colorAry As Color() = ReadColorStyle(skv(1))
                    If IsNothing(colorAry) Then Continue For '无填充颜色的情况跳过
                    lstConv.Add(New KnxGpxConvertion(KnxGpxConvertionType.Fill, colorAry(0), colorAry(1)))
                Case "strokecolor" '线条颜色
                    Dim colorAry As Color() = ReadColorStyle(skv(1))
                    If IsNothing(colorAry) Then Continue For '无线条颜色的情况跳过
                    lstConv.Add(New KnxGpxConvertion(KnxGpxConvertionType.Stroke, colorAry(0), colorAry(1)))
            End Select
        Next
        Me.Convertion = lstConv.ToArray
    End Sub

    '开关量反馈：0/0/0@0|1，0/0/0
    '开关量控制：0/0/0=0|1，0/0/0=0
    '数字量反馈：0/0/0@0~255
    '数字量控制：0/0/0=0~255，0/0/0=000
    Private Sub ReadValueString(valueString As String)
        If valueString.Contains("="c) Then
            Me.Direction = KnxGpxComponentType.Control '带有等号的为控制
            Dim val As String() = valueString.Split("="c)
            For Each c In Me.Convertion
                'c.GroupAddress = New GroupAddress(val(0))
                'c.GroupValue = New GroupValue(val(1))

            Next
        Else
            Me.Direction = KnxGpxComponentType.Feedback '其他情况认为是反馈
        End If

        'Select Case Me.Shape
        '    Case KnxComponentShape.Ellipse, KnxComponentShape.Rectangle

        '    Case KnxComponentShape.Line

        '    Case KnxComponentShape.Text

        'End Select
    End Sub

    Private Shared Function IsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$")
    End Function

    Private Function ReadColorStyle(colorValue As String) As Color()
        colorValue = colorValue.Trim.ToLower
        If colorValue = "none" Then '无颜色
            Return Nothing
        ElseIf colorValue = "default" Then '默认颜色
            Return {DefaultOffColor, DefaultOnColor}
        ElseIf colorValue.Contains("#"c) Then '其他颜色
            Dim lstColor As New List(Of Color)
            Dim matchC As MatchCollection = Regex.Matches(colorValue, "(#[0-9a-fA-F]{6})") '匹配rgb值
            Select Case matchC.Count
                Case 0 '没有颜色的情况，使用默认值
                    Return {DefaultOffColor, DefaultOnColor}
                Case 1 '一种颜色的情况
                    Return {DefaultOffColor, ColorTranslator.FromHtml(matchC(0).Value)}
                Case Else '日间/夜间模式
                    Return {ColorTranslator.FromHtml(matchC(0).Value), ColorTranslator.FromHtml(matchC(1).Value)}
            End Select
        Else
            Return Nothing
        End If
    End Function

End Class
