Imports System.Drawing
Imports System.Text.RegularExpressions
Imports Knx.Falcon

Public Class KnxHmiComponent : Inherits HmiComponentElement

    Private ReadOnly DefaultOffColor As Color = Color.Gray

    Private ReadOnly DefaultOnColor As Color = Color.Green

    '''' <summary>
    '''' 关联组地址
    '''' </summary>
    '''' <returns></returns>
    'Public ReadOnly Property GroupAddress As GroupAddress

    ''' <summary>
    ''' KNX数值变化对象
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property Convertion As KnxHmiConvertion()

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(geo As HmiGeometry, styleString As String, valueString As String)
        MyBase.New(geo)
        If (Not String.IsNullOrWhiteSpace(valueString)) AndAlso ContainsGroupAddress(valueString) Then
            ReadStyleString(styleString)
            ReadValueString(valueString)
        End If
    End Sub

    ''' <summary>
    ''' 读取style属性
    ''' </summary>
    ''' <param name="styleString"></param>
    Private Sub ReadStyleString(styleString As String)
        Dim styles As String() = styleString.Trim.Split(";")
        Dim shape As String = styles(0).Trim.ToLower
        If shape = "ellipse" Then '椭圆或者椭圆圈
            Me.Shape = HmiShapeType.Ellipse
        ElseIf shape.Contains("rounded") Then '矩形或者矩形框，其他形状后方style也可能出现rounded
            Me.Shape = HmiShapeType.Rectangle
        ElseIf shape.Contains("endarrow") Then '线条
            Me.Shape = HmiShapeType.Line
        ElseIf shape.Contains("text") Then '文本
            Me.Shape = HmiShapeType.Text
        End If
        Dim lstConv As New List(Of KnxHmiConvertion)
        For i = 1 To styles.Length - 1
            Dim skv As String() = styles(i).Split("="c)
            Select Case skv(0).Trim.ToLower
                Case "strokewidth" '线条宽度
                    Me.StrokeWidth = Convert.ToUInt32(skv(1))
                Case "fillcolor" '填充颜色
                    Dim colorAry As Color() = ReadColorStyle(skv(1))
                    If IsNothing(colorAry) Then Continue For '无填充颜色的情况跳过
                    lstConv.Add(New KnxHmiConvertion(HmiConvertionPart.Fill, colorAry(0), colorAry(1)))
                Case "strokecolor" '线条颜色
                    Dim colorAry As Color() = ReadColorStyle(skv(1))
                    If IsNothing(colorAry) Then Continue For '无线条颜色的情况跳过
                    lstConv.Add(New KnxHmiConvertion(HmiConvertionPart.Stroke, colorAry(0), colorAry(1)))
            End Select
        Next
        Me.Convertion = lstConv.ToArray
    End Sub

    ''' <summary>
    ''' 读取value属性
    ''' 开关量控制：0/0/0=0|1，0/0/0=0
    ''' 数字量控制：0/0/0=0~255，0/0/0=000
    ''' 开关量反馈：0/0/0@0|1，0/0/0
    ''' 数字量反馈：0/0/0@0~255
    ''' </summary>
    ''' <param name="valueString"></param>
    Private Sub ReadValueString(valueString As String)
        Dim gaStr As String = vbNullString '组地址相关文本
        Dim text As String = vbNullString '描述文字
        If valueString.Contains("<div>") Then '含有描述文字的情况
            Dim arry As String() = valueString.Replace("</div>", vbNullString).Split("<div>")
            gaStr = arry(0) '取第一个行内容为组地址部分
            text = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
        Else
            gaStr = valueString 'value属性只有一行的情况认为全是组地址部分
        End If
        MyBase.Text = text
        '开始判断组地址部分
        If gaStr.Contains("="c) Then
            Me.Direction = HmiComponentDirection.Control '带有=的为控制
        ElseIf gaStr.Contains("@"c) Then
            Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
        Else
            gaStr &= "@0|1" '不带的认为是开关量反馈
            Me.Direction = HmiComponentDirection.Feedback
        End If
        Dim gaAry As String() = gaStr.Split({"="c， "@"c}) '用符号分割为数组
        Dim ga As New GroupAddress(gaAry(0)) '第一个元素为组地址
        Dim gct As HmiValueConvertionType = HmiValueConvertionType.None '组地址控制模式
        Dim gv As GroupValue() '组地址值
        If gaAry(1).Contains("|"c) Then '值-切换模式
            gct = HmiValueConvertionType.Toggle

            'gv = gaAry(1).Split("|"c)
            'Dim v As GroupValue

        ElseIf gaAry(1).Contains("~"c) Then '值-范围模式
            gct = HmiValueConvertionType.Range
        Else '值-固定值模式
            gct = HmiValueConvertionType.Fixed
        End If

        For Each c In Me.Convertion
            c.GroupAddress = ga
            c.GroupControlType = gct
            'c.GroupValue = New GroupValue(val(1))

        Next
    End Sub

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

    Private Shared Function IsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$")
    End Function

    Private Shared Function ContainsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^.*([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5]).*$")
    End Function

End Class
