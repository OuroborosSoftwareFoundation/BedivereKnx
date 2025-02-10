Imports System.Drawing
Imports System.Runtime.Intrinsics.Arm
Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml.Bibliography
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Knx.Falcon.ApplicationData.MasterData

Public Class KnxHmiComponent : Inherits HmiComponentElement

    ''' <summary>
    ''' 动态控件
    ''' </summary>
    Public isDynamics As Boolean

    ''' <summary>
    ''' 控制方式
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupControlType As HmiValueConvertionType

    ''' <summary>
    ''' KNX组对象
    ''' </summary>
    ''' <returns></returns>
    Public Property Group As New KnxGroup

    ''' <summary>
    ''' KNX填充颜色变化对象
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property FillConvertion As KnxHmiColorConvertion

    ''' <summary>
    ''' KNX线条颜色变化对象
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property StrokeConvertion As KnxHmiColorConvertion

    ''' <summary>
    ''' KNX文本变化对象
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property TextConvertion As KnxHmiTextConvertion

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(geo As HmiGeometry, styleString As String, valueString As String)
        MyBase.New(geo)
        If ContainsGroupAddress(valueString) Then Me.isDynamics = True 'value属性含组地址的为动态对象
        'Dim vals As New List(Of GroupValue)
        'ReadValueString(valueString, vals)
        'ReadStyleString(styleString, vals)

        Dim colors As Color()
        ReadStyleString(styleString, colors)
        ReadValueString(valueString, colors)
    End Sub

    ''' <summary>
    ''' 读取style属性
    ''' </summary>
    ''' <param name="styleString"></param>
    Private Sub ReadStyleString(styleString As String, ByRef vals As List(Of GroupValue))
        Dim dicStyle As Dictionary(Of String, String) = StyleStringToDic(styleString)
        Select Case dicStyle.First.Key.ToLower'style第一项为形状
            Case "ellipse" '椭圆
                Me.Shape = HmiShapeType.Ellipse
            Case "rounded" '矩形（矩形会带圆角属性）
                Me.Shape = HmiShapeType.Rectangle
            Case "endArrow" '线条
                Me.Shape = HmiShapeType.Line
            Case "text" '文本
                Me.Shape = HmiShapeType.Text
            Case Else
                Me.Shape = HmiShapeType.None
        End Select

        Dim fill As String = vbNullString
        dicStyle.TryGetValue("fillcolor", fill)
        Dim colorAry As Color() = ReadColorStyle(fill)
        If Me.isDynamics Then '动态控件
            Me.FillConvertion.ConvertionPart = HmiConvertionPart.Fill '设置变换部件
            Me.FillColor = colorAry.First '动态控件显示关闭颜色
            For i = 0 To vals.Count - 1
                Select Case i
                    Case 0
                        Me.FillConvertion.Values.Add(vals(i), colorAry(0))
                    Case vals.Count - 1
                        Me.FillConvertion.Values.Add(vals(i), colorAry(1))
                    Case Else
                        Me.FillConvertion.Values.Add(vals(i), Color.Empty)
                End Select
            Next
        Else '静态控件
            Me.FillColor = colorAry.Last '静态控件显示开启颜色
        End If




    End Sub

    ''' <summary>
    ''' 读取value属性
    ''' 控制格式：[组地址]=[数值]，[组地址]=[数值]#[DPT].[DPST]
    '''     开关量控制：0/0/0=0|1，0/0/0=0
    '''     数字量控制：0/0/0=0~255，0/0/0=000
    ''' 反馈格式：[组地址]@[数值]，[组地址]@[数值]#[DPT].[DPST]
    '''     开关量反馈：0/0/0@0|1，0/0/0
    '''     数字量反馈：0/0/0@0~255
    ''' </summary>
    ''' <param name="valueString"></param>
    Private Sub ReadValueString(valueString As String, ByRef vals As List(Of GroupValue))
        If Me.isDynamics Then
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

            '组地址部分
            If gaStr.Contains("="c) Then
                Me.Direction = HmiComponentDirection.Control '带有=的为控制
            ElseIf gaStr.Contains("@"c) Then
                Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
            Else
                gaStr &= "@0|1" '不带的认为是开关量反馈
                Me.Direction = HmiComponentDirection.Feedback
            End If
            Dim gaAry As String() = gaStr.Split({"="c, "@"c}) '用符号分割为数组
            Dim ga As New GroupAddress '组地址
            If GroupAddress.TryParse(gaAry(0), ga) Then '组地址有效的情况
                Dim valsString As String = vbNullString '数值
                If gaAry(1).Contains("#"c) Then '有#代表定义了组地址类型
                    Dim gvAry As String() = gaAry(1).Split("#"c) '分割数值和数据类型
                    valsString = gvAry.First
                    Dim dpt As String() = gvAry.Last.Split("."c) '分割DPT和DPST
                    Me.Group = New KnxGroup(ga, Convert.ToInt32(dpt(0)), Convert.ToInt32(dpt(1))) '新建KNX组对象
                    'Me.Group.DPST = DptFactory.Default.GetDatapointSubtype(dpt(0), dpt(1))
                Else
                    valsString = gaAry(1) '此时DPST为默认值1.000
                    Dim t As DatapointSubtype = DptFactory.Default.GetDatapointSubtype(1, 1)
                    Me.Group = New KnxGroup(ga)
                    'Me.Group.DPST = DptFactory.Default.GetDatapointSubtype(1, 1)
                End If

                '变换值部分
                vals = New List(Of GroupValue)
                If valsString.Contains("|"c) Then '值-切换模式
                    Me.GroupControlType = HmiValueConvertionType.Toggle
                    Dim valsArry As String() = valsString.Split("|"c)
                    For Each v As String In valsArry
                        vals.Add(Me.Group.DPT.ToGroupValue(v))
                        'vals.Add(DptCompoundGeneric.ToGroupValue(v, Me.Group.DPST))
                    Next
                ElseIf valsString.Contains("~"c) Then '值-范围模式
                    Dim valsArry As String() = valsString.Split("~"c)
                    For Each v As String In valsArry
                        vals.Add(Me.Group.DPT.ToGroupValue(v))
                        'vals.Add(DptCompoundGeneric.ToGroupValue(v, Me.Group.DPST))
                    Next
                Else '值-固定值模式
                    Me.GroupControlType = HmiValueConvertionType.Fixed
                    vals.Add(Me.Group.DPT.ToGroupValue(valsString))
                    'vals.Add(DptCompoundGeneric.ToGroupValue(valsString, Me.Group.DPST))
                End If
            Else '组地址无效直接报错
                Throw New ArgumentException($"Wrong group address format: '{gaAry(0)}'.")
            End If
        Else '静态控件的value认为全部为文字
            If valueString.Contains("<div>") Then 'value属性含有多行的情况
                Dim arry As String() = valueString.Replace("</div>", vbNullString).Split("<div>")
                Me.Text = String.Join(vbCrLf, arry)
            Else 'value属性只有一行的情况
                Me.Text = valueString
            End If
        End If
    End Sub

    Private Function StyleStringToDic(styleString As String, Optional KeyToLower As Boolean = True) As Dictionary(Of String, String)
        If String.IsNullOrWhiteSpace(styleString) Then Return Nothing
        Dim styles As String() = styleString.Trim.Split(";"c)
        Dim dic As New Dictionary(Of String, String)
        For Each style As String In styles
            If String.IsNullOrWhiteSpace(style) Then Continue For
            Dim kv As String() = style.Split("="c)
            Dim k As String = kv(0).Trim
            If KeyToLower Then k = k.ToLower
            dic.TryAdd(k, kv(1).Trim)
        Next
        Return dic
    End Function

    ''' <summary>
    ''' 读取颜色信息
    ''' none: 无颜色
    ''' default: 默认颜色
    ''' #000000: RGB颜色
    ''' light-dark(#000000,#000000): 浅色-深色模式，浅色为开启时颜色
    ''' </summary>
    ''' <param name="colorString"></param>
    ''' <returns></returns>
    Private Function ReadColorStyle(colorString As String) As Color()
        If String.IsNullOrWhiteSpace(colorString) Then Return {Color.Empty}
        colorString = colorString.Trim.ToLower
        If colorString = "none" Then '无颜色
            Return {Color.Empty}
        ElseIf colorString = "default" Then '默认颜色
            Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
        ElseIf colorString.Contains("#"c) Then '其他颜色
            Dim matchC As MatchCollection = Regex.Matches(colorString, "(#[0-9a-fA-F]{6})") '匹配RGB值
            Select Case matchC.Count
                Case 0 '没有颜色的情况，使用默认值（一般不会出现这种情况）
                    Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
                Case 1 '一种颜色的情况
                    Return {DEFAULTCOLOR_OFF, ColorTranslator.FromHtml(matchC(0).Value)}
                Case Else '浅色-深色模式，浅色为开启时颜色
                    Return {ColorTranslator.FromHtml(matchC(1).Value), ColorTranslator.FromHtml(matchC(0).Value)}
            End Select
        Else
            Return {Color.Empty}
        End If
    End Function

    'Private Function ReadColorStyle0(colorValue As String) As Color()
    '    colorValue = colorValue.Trim.ToLower
    '    If colorValue = "none" Then '无颜色
    '        Return Nothing
    '    ElseIf colorValue = "default" Then '默认颜色
    '        Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
    '    ElseIf colorValue.Contains("#"c) Then '其他颜色
    '        Dim lstColor As New List(Of Color)
    '        Dim matchC As MatchCollection = Regex.Matches(colorValue, "(#[0-9a-fA-F]{6})") '匹配rgb值
    '        Select Case matchC.Count
    '            Case 0 '没有颜色的情况，使用默认值
    '                Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
    '            Case 1 '一种颜色的情况
    '                Return {DEFAULTCOLOR_OFF, ColorTranslator.FromHtml(matchC(0).Value)}
    '            Case Else '日间/夜间模式
    '                Return {ColorTranslator.FromHtml(matchC(0).Value), ColorTranslator.FromHtml(matchC(1).Value)}
    '        End Select
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    ''' <summary>
    ''' 读取value属性
    ''' 开关量控制：0/0/0=0|1，0/0/0=0
    ''' 数字量控制：0/0/0=0~255，0/0/0=000
    ''' 开关量反馈：0/0/0@0|1，0/0/0
    ''' 数字量反馈：0/0/0@0~255
    ''' </summary>
    ''' <param name="valueString"></param>
    'Private Sub ReadValueString0(valueString As String)
    '    Dim gaStr As String = vbNullString '组地址相关文本
    '    Dim text As String = vbNullString '描述文字
    '    If valueString.Contains("<div>") Then '含有描述文字的情况
    '        Dim arry As String() = valueString.Replace("</div>", vbNullString).Split("<div>")
    '        gaStr = arry(0) '取第一个行内容为组地址部分
    '        text = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
    '    Else
    '        gaStr = valueString 'value属性只有一行的情况认为全是组地址部分
    '    End If
    '    MyBase.Text = text
    '    '开始判断组地址部分
    '    If gaStr.Contains("="c) Then
    '        Me.Direction = HmiComponentDirection.Control '带有=的为控制
    '    ElseIf gaStr.Contains("@"c) Then
    '        Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
    '    Else
    '        gaStr &= "@0|1" '不带的认为是开关量反馈
    '        Me.Direction = HmiComponentDirection.Feedback
    '    End If
    '    Dim gaAry As String() = gaStr.Split({"="c, "@"c}) '用符号分割为数组
    '    Dim ga As New GroupAddress(gaAry(0)) '第一个元素为组地址
    '    Dim gct As HmiValueConvertionType = HmiValueConvertionType.None '组地址控制模式
    '    Dim gv As GroupValue() '组地址值
    '    If gaAry(1).Contains("|"c) Then '值-切换模式
    '        gct = HmiValueConvertionType.Toggle

    '        'gv = gaAry(1).Split("|"c)
    '        'Dim v As GroupValue

    '    ElseIf gaAry(1).Contains("~"c) Then '值-范围模式
    '        gct = HmiValueConvertionType.Range
    '    Else '值-固定值模式
    '        gct = HmiValueConvertionType.Fixed
    '    End If

    '    For Each c In Me.ColorConvertion
    '        c.GroupAddress = ga
    '        c.GroupControlType = gct
    '        'c.GroupValue = New GroupValue(val(1))

    '    Next
    'End Sub

    Private Shared Function IsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$")
    End Function

    Private Shared Function ContainsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^.*([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5]).*$")
    End Function

End Class
