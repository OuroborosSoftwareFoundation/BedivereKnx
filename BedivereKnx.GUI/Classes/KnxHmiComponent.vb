Imports System.Drawing
Imports System.Text.RegularExpressions
Imports Ouroboros.Hmi.Library
Imports Knx.Falcon

Public Class KnxHmiComponent : Inherits HmiComponentElement

    ''' <summary>
    ''' 动态控件
    ''' </summary>
    'Private isDynamics As Boolean

    ''' <summary>
    ''' 映射方式
    ''' </summary>
    Public Property MappingMode As HmiMappingMode = HmiMappingMode.None

    ''' <summary>
    ''' KNX值映射
    ''' </summary>
    ''' <returns></returns>
    Public Property Mapping As KnxHmiMapping

    '''' <summary>
    '''' 控制方式
    '''' </summary>
    '''' <returns></returns>
    'Public Property GroupControlType As HmiValueChangeType

    ''' <summary>
    ''' KNX组对象
    ''' </summary>
    ''' <returns></returns>
    Public Property Group As New KnxGroup(1)

    Public Property ObjectId As Integer = -1

    '''' <summary>
    '''' KNX填充颜色变化对象
    '''' </summary>
    '''' <returns></returns>
    'Public Overloads Property FillConvertion As KnxHmiColorConvertion

    '''' <summary>
    '''' KNX线条颜色变化对象
    '''' </summary>
    '''' <returns></returns>
    'Public Overloads Property StrokeConvertion As KnxHmiColorConvertion

    '''' <summary>
    '''' KNX文本变化对象
    '''' </summary>
    '''' <returns></returns>
    'Public Overloads Property TextConvertion As KnxHmiTextConvertion

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(geo As HmiGeometry, styleAttribute As String, valueAttribute As String)
        MyBase.New(geo)
        'If ContainsGroupAddress(valueAttribute) Then Me.isDynamics = True 'value属性含组地址的为动态对象
        'ReadStrings(styleAttribute, valueAttribute)
        ReadValueAttribute(valueAttribute)
        ReadStyleAttribute(styleAttribute)
    End Sub

    ''' <summary>
    ''' 读取value属性字符串
    ''' 控制格式：*[ObjectCode]=[数值]，$[SceneCode]=[数值]，[组地址]=[数值]，[组地址]=[数值]#[DPT].[DPST]
    '''     开关量控制：$Channel1=0|1，0/0/0=0|1，0/0/0=0
    '''     数字量控制：$Channel1=100，0/0/0=0~255，0/0/0=000
    ''' 反馈格式：*[ObjectCode]@[数值]，$[SceneCode]@[数值]，[组地址]@[数值]，[组地址]@[数值]#[DPT].[DPST]
    '''     开关量反馈：0/0/0@0|1，0/0/0
    '''     数字量反馈：0/0/0@0~255
    ''' </summary>
    Private Sub ReadValueAttribute(valueAttribute As String)
        valueAttribute = Regex.Replace(valueAttribute, "<span>.*?</span>", "", RegexOptions.IgnoreCase Or RegexOptions.Singleline) '去除格式信息
        If valueAttribute.StartsWith("*"c) OrElse valueAttribute.StartsWith("$"c) Then '*和$开头的为数据表映射模式
            Me.MappingMode = HmiMappingMode.DataTable
        ElseIf ContainsGroupAddress(valueAttribute) Then '包含合法组地址为地址映射模式
            Me.MappingMode = HmiMappingMode.Address
        Else '其他情况为静态对象
            Me.MappingMode = HmiMappingMode.None
        End If

        If Me.MappingMode = HmiMappingMode.None Then '静态对象的情况
            Me.Text = valueAttribute.Replace("</div>", vbNullString).Replace("<div>", vbCrLf)
        Else '动态对象的情况
            '将value字符串拆分为“绑定信息”和“文本内容”
            Dim mappingString = vbNullString '绑定信息的字符串
            Dim textString = vbNullString '文本内容的字符串
            If valueAttribute.Contains("<div>") Then '多行，即含有描述文字的情况
                valueAttribute = valueAttribute.Replace("</div>", vbNullString) '删除结束标签，<div>相当于回车符
                Dim valueArry As String() = valueAttribute.Split("<div>") '根据<div>（此时相当于回车符）拆分
                mappingString = valueArry(0) '取第一行内容为组地址部分
                textString = String.Join(vbCrLf, valueArry.Skip(1)) '其他的用回车分割为文本内容
            Else
                mappingString = valueAttribute 'value属性只有一行的情况认为全是组地址部分
            End If

            '==========暂时不实现文本变换=======================
            'Dim HasTextChange As Boolean = Regex.IsMatch(Text, "[%|~]")
            'If HasTextChange Then '包含文本变换
            '    Select Case Me.Mapping.ChangeType
            '        Case 
            '        End Select
            '    Dim texts As String() =
            '    End If
            '==========暂时不实现文本变换=======================

            '绑定信息1-判断方向
            If mappingString.Contains("="c) Then '带有=的方向为控制
                Me.Direction = HmiComponentDirection.Control
            ElseIf mappingString.Contains("@"c) Then '带有@的方向为反馈
                Me.Direction = HmiComponentDirection.Feedback
            Else '绑定信息不包含方向字符，认为是开关量反馈
                mappingString &= "@0|1"
                Me.Direction = HmiComponentDirection.Feedback
            End If

            '绑定信息3-对象/组地址部分
            Dim mappingArry As String() = mappingString.Split({"="c, "@"c}) '用符号分割为数组
            Select Case Me.MappingMode
                Case HmiMappingMode.DataTable
                    Me.Mapping = New KnxHmiMapping(mappingArry(1))
                    'Me.Text &= $"{textString}{mappingArry(0)}" '把ObjectCode加在文本后面备用
                    Me.Text = $"{textString}{mappingString}"
                Case HmiMappingMode.Address
                    Dim ga As New GroupAddress '组地址
                    If GroupAddress.TryParse(mappingArry(0), ga) Then '组地址有效的情况
                        Dim vals As String = vbNullString '数值
                        If mappingArry(1).Contains("#"c) Then '有#代表定义了组地址类型
                            Dim gvAry As String() = mappingArry(1).Split("#"c) '分割数值和数据类型
                            vals = gvAry.First
                            Dim dpt As String() = gvAry.Last.Split("."c) '分割DPT和DPST
                            Me.Group = New KnxGroup(ga, Convert.ToInt32(dpt(0)), Convert.ToInt32(dpt(1))) '新建KNX组对象
                        Else '不带#定义组地址类型的，认为DPT是1.000
                            vals = mappingArry(1)
                            Me.Group = New KnxGroup(ga, 1, 1)
                        End If
                        Me.Mapping = New KnxHmiMapping(vals, Me.Group.DPT) '新建绑定对象
                    Else
                        Me.MappingMode = HmiMappingMode.None
                        Me.Text = valueAttribute.Replace("</div>", vbNullString).Replace("<div>", vbCrLf)
                        'Throw New InvalidDataException($"Invalid group address: '{mappingArry(0)}'.")
                    End If
                    If String.IsNullOrWhiteSpace(textString) Then
                        Me.Text = ga.ToString
                    Else
                        Me.Text = textString
                    End If
            End Select
        End If





        '以下废弃
        'Select Case Me.MappingMode
        '    Case HmiMappingMode.DataTable

        '        Dim objString = vbNullString
        '        Dim valueText As String = vbNullString
        '        If valueAttribute.Contains("<div>") Then '含有描述文字的情况
        '            Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
        '            objString = arry(0) '取第一个行内容为组地址部分
        '            valueText = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
        '        Else
        '            objString = valueAttribute 'value属性只有一行的情况认为全是组地址部分
        '        End If
        '        Me.Text = valueText

        '        '组地址部分
        '        If objString.Contains("="c) Then
        '            Me.Direction = HmiComponentDirection.Control '带有=的为控制
        '        ElseIf objString.Contains("@"c) Then
        '            Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
        '        Else
        '            objString &= "@0|1" '不带的认为是开关量反馈
        '            Me.Direction = HmiComponentDirection.Feedback
        '        End If
        '        Dim objArray As String() = objString.Split({"="c, "@"c}) '用符号分割为数组

        '        Me.Mapping = New KnxHmiMapping(objArray(1), Me.Group.DPT)
        '        Me.Text &= $"{objArray(0)}" '把ObjectCode加在文本后面备用
        '    Case HmiMappingMode.Address
        '        Dim gaStr As String = vbNullString '组地址相关文本
        '        Dim text As String = vbNullString '描述文字

        '        If valueAttribute.Contains("<div>") Then '含有描述文字的情况
        '            Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
        '            gaStr = arry(0) '取第一个行内容为组地址部分
        '            text = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
        '        Else
        '            gaStr = valueAttribute 'value属性只有一行的情况认为全是组地址部分
        '        End If
        '        Me.Text = text

        '        '组地址部分
        '        If gaStr.Contains("="c) Then
        '            Me.Direction = HmiComponentDirection.Control '带有=的为控制
        '        ElseIf gaStr.Contains("@"c) Then
        '            Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
        '        Else
        '            gaStr &= "@0|1" '不带的认为是开关量反馈
        '            Me.Direction = HmiComponentDirection.Feedback
        '        End If
        '        Dim gaAry As String() = gaStr.Split({"="c, "@"c}) '用符号分割为数组
        '        Dim ga As New GroupAddress '组地址
        '        If GroupAddress.TryParse(gaAry(0), ga) Then '组地址有效的情况
        '            Dim valsString As String = vbNullString '数值
        '            If gaAry(1).Contains("#"c) Then '有#代表定义了组地址类型
        '                Dim gvAry As String() = gaAry(1).Split("#"c) '分割数值和数据类型
        '                valsString = gvAry.First
        '                Dim dpt As String() = gvAry.Last.Split("."c) '分割DPT和DPST
        '                Me.Group = New KnxGroup(ga, Convert.ToInt32(dpt(0)), Convert.ToInt32(dpt(1))) '新建KNX组对象
        '            Else
        '                valsString = gaAry(1) '此时DPST为默认值1.000
        '                'Dim t As DatapointSubtype = DptFactory.Default.GetDatapointSubtype(1, 1)
        '                Me.Group = New KnxGroup(ga, 1, 1)
        '            End If

        '            If String.IsNullOrWhiteSpace(Me.Text) Then Me.Text = ga.ToString

        '            '变换值部分
        '            'Dim vals As New List(Of GroupValue)
        '            'Dim chgType As HmiValueChangeType
        '            'If valsString.Contains("|"c) Then '值-切换模式
        '            '    chgType = HmiValueChangeType.Toggle
        '            '    Dim valsArry As String() = valsString.Split("|"c)
        '            '    For Each v As String In valsArry
        '            '        vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(v)))
        '            '    Next
        '            'ElseIf valsString.Contains("~"c) Then '值-范围模式
        '            '    chgType = HmiValueChangeType.Range
        '            '    Dim valsArry As String() = valsString.Split("~"c)
        '            '    For Each v As String In valsArry
        '            '        vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(v)))
        '            '    Next
        '            'Else '值-固定值模式
        '            '    chgType = HmiValueChangeType.Fixed
        '            '    vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(valsString)))
        '            'End If

        '            'Me.Mapping = New KnxHmiMapping(vals.ToArray, chgType) '新建映射对象
        '            Me.Mapping = New KnxHmiMapping(valsString, Me.Group.DPT)
        '        Else '组地址无效直接报错
        '            Me.MappingMode = HmiMappingMode.None
        '            'Me.isDynamics = False '设置为静态控件
        '            Throw New ArgumentException($"Wrong GroupAddress format: '{gaAry(0)}'.")
        '        End If
        '    Case HmiMappingMode.None
        '        If valueAttribute.Contains("<div>") Then 'value属性含有多行的情况
        '            Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
        '            Me.Text = String.Join(vbCrLf, arry)
        '        Else 'value属性只有一行的情况
        '            Me.Text = valueAttribute
        '        End If
        'End Select

    End Sub

    Private Sub ReadStyleAttribute(styleAttribute As String)
        Dim dicStyle As Dictionary(Of String, String) = StyleStringToDic(styleAttribute) 'style字典

        '控件形状部分：
        Select Case dicStyle.First.Key'style第一项为形状
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

        Dim fillcolors As Color() = ReadColorStyle("fillColor", dicStyle) '填充颜色
        Dim strokecolors As Color() = ReadColorStyle("strokeColor", dicStyle) '线条颜色
        Dim fontcolors As Color() = ReadColorStyle("fontColor", dicStyle) '字体颜色
        Me.StrokeWidth = ReadNumStyle("strokeWidth", dicStyle) '线条宽度
        Dim fontSize As Integer = ReadNumStyle("fontSize", dicStyle, 12) '字体大小
        Me.FontSize = fontSize / 1.33 'drawio中字体单位为像素，Winform中字体单位为点，1点=1.33像素
        Me.Alignment = ReadAlign(dicStyle) '文本对齐方式

        If Me.MappingMode = HmiMappingMode.None Then '静态控件颜色设置（设为开启颜色）
            Me.FillColor = fillcolors.Last
            Me.StrokeColor = strokecolors.Last
            Me.FontColor = fontcolors.Last
        Else '动态控件颜色设置（设为关闭颜色）
            Me.Mapping.FillColors = fillcolors '填充颜色
            Me.Mapping.StrokeColors = strokecolors '线条颜色
            Me.Mapping.FontColors = fontcolors '字体颜色
            Me.FillColor = fillcolors.First
            Me.StrokeColor = strokecolors.First
            Me.FontColor = fontcolors.First
        End If
    End Sub

    ''' <summary>
    ''' 读取style和value属性
    ''' 控制格式：[组地址]=[数值]，[组地址]=[数值]#[DPT].[DPST]
    '''     开关量控制：0/0/0=0|1，0/0/0=0
    '''     数字量控制：0/0/0=0~255，0/0/0=000
    ''' 反馈格式：[组地址]@[数值]，[组地址]@[数值]#[DPT].[DPST]
    '''     开关量反馈：0/0/0@0|1，0/0/0
    '''     数字量反馈：0/0/0@0~255
    ''' </summary>
    ''' <param name="valueString"></param>
    'Private Sub ReadStrings(styleAttribute As String, valueAttribute As String)
    '    'Dim dicStyle As Dictionary(Of String, String) = StyleStringToDic(styleAttribute) 'style字典

    '    ''控件形状部分：
    '    'Select Case dicStyle.First.Key'style第一项为形状
    '    '    Case "ellipse" '椭圆
    '    '        Me.Shape = HmiShapeType.Ellipse
    '    '    Case "rounded" '矩形（矩形会带圆角属性）
    '    '        Me.Shape = HmiShapeType.Rectangle
    '    '    Case "endArrow" '线条
    '    '        Me.Shape = HmiShapeType.Line
    '    '    Case "text" '文本
    '    '        Me.Shape = HmiShapeType.Text
    '    '    Case Else
    '    '        Me.Shape = HmiShapeType.None
    '    'End Select

    '    'Dim fillcolors As Color() = ReadColorStyle("fillColor", dicStyle) '填充颜色
    '    'Dim strokecolors As Color() = ReadColorStyle("strokeColor", dicStyle) '线条颜色
    '    'Me.StrokeWidth = ReadNumStyle("strokeWidth", dicStyle) '设置线条宽度

    '    If Me.isDynamics Then

    '        '动态控件颜色设置（设为关闭颜色）
    '        Me.FillColor = fillcolors.First
    '        Me.StrokeColor = strokecolors.First

    '        'Dim gaStr As String = vbNullString '组地址相关文本
    '        'Dim text As String = vbNullString '描述文字
    '        'If valueAttribute.Contains("<div>") Then '含有描述文字的情况
    '        '    Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
    '        '    gaStr = arry(0) '取第一个行内容为组地址部分
    '        '    text = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
    '        'Else
    '        '    gaStr = valueAttribute 'value属性只有一行的情况认为全是组地址部分
    '        'End If
    '        'Me.Text = text

    '        ''组地址部分
    '        'If gaStr.Contains("="c) Then
    '        '    Me.Direction = HmiComponentDirection.Control '带有=的为控制
    '        'ElseIf gaStr.Contains("@"c) Then
    '        '    Me.Direction = HmiComponentDirection.Feedback '带有@的是反馈
    '        'Else
    '        '    gaStr &= "@0|1" '不带的认为是开关量反馈
    '        '    Me.Direction = HmiComponentDirection.Feedback
    '        'End If
    '        'Dim gaAry As String() = gaStr.Split({"="c, "@"c}) '用符号分割为数组
    '        'Dim ga As New GroupAddress '组地址
    '        'If GroupAddress.TryParse(gaAry(0), ga) Then '组地址有效的情况
    '        '    Dim valsString As String = vbNullString '数值
    '        '    If gaAry(1).Contains("#"c) Then '有#代表定义了组地址类型
    '        '        Dim gvAry As String() = gaAry(1).Split("#"c) '分割数值和数据类型
    '        '        valsString = gvAry.First
    '        '        Dim dpt As String() = gvAry.Last.Split("."c) '分割DPT和DPST
    '        '        Me.Group = New KnxGroup(ga, Convert.ToInt32(dpt(0)), Convert.ToInt32(dpt(1))) '新建KNX组对象
    '        '    Else
    '        '        valsString = gaAry(1) '此时DPST为默认值1.000
    '        '        Dim t As DatapointSubtype = DptFactory.Default.GetDatapointSubtype(1, 1)
    '        '        Me.Group = New KnxGroup(ga)
    '        '    End If

    '        '    '变换值部分
    '        '    Dim vals As New List(Of GroupValue)
    '        '    Dim chgType As HmiValueChangeType
    '        '    If valsString.Contains("|"c) Then '值-切换模式
    '        '        chgType = HmiValueChangeType.Toggle
    '        '        Dim valsArry As String() = valsString.Split("|"c)
    '        '        For Each v As String In valsArry
    '        '            vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(v)))
    '        '        Next
    '        '    ElseIf valsString.Contains("~"c) Then '值-范围模式
    '        '        chgType = HmiValueChangeType.Range
    '        '        Dim valsArry As String() = valsString.Split("~"c)
    '        '        For Each v As String In valsArry
    '        '            vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(v)))
    '        '        Next
    '        '    Else '值-固定值模式
    '        '        chgType = HmiValueChangeType.Fixed
    '        '        vals.Add(Me.Group.DPT.ToGroupValue(Convert.ToDecimal(valsString)))
    '        '    End If

    '        '    Me.Mapping = New KnxHmiMapping(vals.ToArray, chgType) '新建映射对象



    '        '填充色变化
    '        If fillcolors.Length > 1 Then
    '            Me.FillConvertion = New KnxHmiColorConvertion(HmiConvertionPart.Fill)
    '            If vals.Count = 1 Then
    '                Me.FillConvertion.Values.Add(vals.Last, fillcolors.Last)
    '            ElseIf vals.Count = 2 Then
    '                Me.FillConvertion.Values.Add(vals.First, fillcolors.First)
    '                Me.FillConvertion.Values.Add(vals.Last, fillcolors.Last)
    '            Else
    '                Me.FillConvertion.Values.Add(vals.First, fillcolors.First)
    '                Me.FillConvertion.Values.Add(vals.Last, fillcolors.Last)
    '                For f = 1 To vals.Count - 2
    '                    Me.FillConvertion.Values.Add(vals(f), Color.Empty)
    '                Next
    '            End If
    '        End If
    '    End If

    '    '线条色变化
    '    If strokecolors.Length > 1 Then
    '        Me.StrokeConvertion = New KnxHmiColorConvertion(HmiConvertionPart.Stroke)
    '        If vals.Count = 1 Then
    '            Me.StrokeConvertion.Values.Add(vals.Last, strokecolors.Last)
    '        ElseIf vals.Count = 2 Then
    '            Me.StrokeConvertion.Values.Add(vals.First, strokecolors.First)
    '            Me.StrokeConvertion.Values.Add(vals.Last, strokecolors.Last)
    '        Else
    '            Me.StrokeConvertion.Values.Add(vals.First, strokecolors.First)
    '            Me.StrokeConvertion.Values.Add(vals.Last, strokecolors.Last)
    '            For s = 1 To vals.Count - 2
    '                Me.StrokeConvertion.Values.Add(vals(s), Color.Empty)
    '            Next
    '        End If
    '    End If
    '    End If
    '    'Else '组地址无效直接报错
    '    'Me.isDynamics = False '设置为静态控件
    '    'Throw New ArgumentException($"Wrong GroupAddress format: '{gaAry(0)}'.")
    '    'End If
    '    Else '静态控件

    '    '静态控件颜色设置（设为开启颜色）
    '    Me.FillColor = fillcolors.Last '静态控件显示开启颜色
    '    Me.StrokeColor = strokecolors.Last '静态控件显示开启颜色

    '    '静态控件的文字（value认为全部为文字）
    '    'If valueAttribute.Contains("<div>") Then 'value属性含有多行的情况
    '    '    Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
    '    '    Me.Text = String.Join(vbCrLf, arry)
    '    'Else 'value属性只有一行的情况
    '    '    Me.Text = valueAttribute
    '    'End If
    '    End If
    'End Sub

    ''' <summary>
    ''' 样式字符串转字典
    ''' </summary>
    ''' <param name="styleString"></param>
    ''' <param name="KeyToLower">是否把键转为小写</param>
    ''' <returns></returns>
    Private Shared Function StyleStringToDic(styleString As String, Optional KeyToLower As Boolean = False) As Dictionary(Of String, String)
        If String.IsNullOrWhiteSpace(styleString) Then Return Nothing
        Dim styles As String() = styleString.Trim.Split(";"c)
        Dim dic As New Dictionary(Of String, String)
        For Each style As String In styles
            If String.IsNullOrWhiteSpace(style) Then Continue For
            Dim kv As String() = style.Split("="c)
            Dim k As String = kv(0).Trim
            If KeyToLower Then k = k.ToLower
            If kv.Length = 2 Then
                dic.TryAdd(k, kv(1).Trim)
            Else
                dic.TryAdd(k, vbNullString)
            End If
        Next
        Return dic
    End Function

    ''' <summary>
    ''' 读取数字样式
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="dic"></param>
    ''' <param name="defaultValue">默认数值</param>
    ''' <returns></returns>
    Private Shared Function ReadNumStyle(key As String, ByRef dic As Dictionary(Of String, String), Optional defaultValue As Integer = 0) As Integer
        Dim value As String = vbNullString
        If dic.TryGetValue(key, value) Then
            Return Convert.ToInt32(value)
        Else
            Return defaultValue
        End If
    End Function

    ''' <summary>
    ''' 读取颜色样式
    ''' none: 无颜色
    ''' default: 默认颜色
    ''' #000000: RGB颜色
    ''' light-dark(#000000,#000000): 浅色-深色模式，浅色为开启时颜色
    ''' </summary>
    ''' <param name="colorString"></param>
    ''' <returns></returns>
    Private Shared Function ReadColorStyle(key As String, ByRef dic As Dictionary(Of String, String)) As Color()
        If String.IsNullOrWhiteSpace(key) Then Return {Color.Empty}
        Dim colorString As String = vbNullString
        dic.TryGetValue(key, colorString)
        If String.IsNullOrWhiteSpace(colorString) Then '不存在给定键的情况
            Return {dicColorNone(key)}
        End If
        colorString = colorString.Trim.ToLower
        If colorString = "none" Then '无颜色
            Return {Color.Empty}
        ElseIf colorString = "default" Then '默认颜色
            Return {DEFAULTCOLOR_OFF, dicColorDefault(key)}
        ElseIf colorString.Contains("#"c) Then '其他颜色
            Dim matchC As MatchCollection = Regex.Matches(colorString, "(#[0-9a-fA-F]{6})") '匹配RGB值
            Select Case matchC.Count
                Case 0 '没有颜色的情况，使用默认值（一般不会出现这种情况）
                    Return {DEFAULTCOLOR_OFF, dicColorDefault(key)}
                Case 1 '一种颜色的情况
                    Return {ColorTranslator.FromHtml(matchC(0).Value)} '{DEFAULTCOLOR_OFF, ColorTranslator.FromHtml(matchC(0).Value)}
                Case Else '浅色-深色模式，浅色为开启时颜色
                    Return {ColorTranslator.FromHtml(matchC(1).Value), ColorTranslator.FromHtml(matchC(0).Value)}
            End Select
        Else
            Return {Color.Empty}
        End If
    End Function

    Private Shared dicColorNone As New Dictionary(Of String, Color) From {
        {"fillColor", Color.Empty},
        {"strokeColor", Color.Empty},
        {"fontColor", Color.Black}
    }

    Private Shared dicColorDefault As New Dictionary(Of String, Color) From {
        {"fillColor", Color.White},
        {"strokeColor", Color.Black},
        {"fontColor", Color.Black}
    }

    ''' <summary>
    ''' 读取对齐方式
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="dic"></param>
    ''' <returns></returns>
    Private Shared Function ReadAlign(ByRef dic As Dictionary(Of String, String)) As ContentAlignment
        Dim alignH As String = vbNullString
        If Not dic.TryGetValue("align", alignH) Then
            alignH = "center"
        End If
        Dim alignV As String = vbNullString
        If Not dic.TryGetValue("verticalAlign", alignV) Then
            alignV = "middle"
        End If
        Select Case $"{alignV}{alignH}"
            Case "topleft"
                Return ContentAlignment.TopLeft
            Case "topcenter"
                Return ContentAlignment.TopCenter
            Case "topright"
                Return ContentAlignment.TopRight
            Case "middleleft"
                Return ContentAlignment.MiddleLeft
            Case "middlecenter"
                Return ContentAlignment.MiddleCenter
            Case "middleright"
                Return ContentAlignment.MiddleRight
            Case "bottomleft"
                Return ContentAlignment.BottomLeft
            Case "bottomcenter"
                Return ContentAlignment.BottomCenter
            Case "bottomright"
                Return ContentAlignment.BottomRight
            Case Else
                Return ContentAlignment.MiddleCenter
        End Select
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
    'Private Function ReadColorStyle(colorString As String) As Color()
    '    If String.IsNullOrWhiteSpace(colorString) Then Return {Color.Empty}
    '    colorString = colorString.Trim.ToLower
    '    If colorString = "none" Then '无颜色
    '        Return {Color.Empty}
    '    ElseIf colorString = "default" Then '默认颜色
    '        Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
    '    ElseIf colorString.Contains("#"c) Then '其他颜色
    '        Dim matchC As MatchCollection = Regex.Matches(colorString, "(#[0-9a-fA-F]{6})") '匹配RGB值
    '        Select Case matchC.Count
    '            Case 0 '没有颜色的情况，使用默认值（一般不会出现这种情况）
    '                Return {DEFAULTCOLOR_OFF, DEFAULTCOLOR_ON}
    '            Case 1 '一种颜色的情况
    '                Return {DEFAULTCOLOR_OFF, ColorTranslator.FromHtml(matchC(0).Value)}
    '            Case Else '浅色-深色模式，浅色为开启时颜色
    '                Return {ColorTranslator.FromHtml(matchC(1).Value), ColorTranslator.FromHtml(matchC(0).Value)}
    '        End Select
    '    Else
    '        Return {Color.Empty}
    '    End If
    'End Function

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
    'Private Sub ReadValueString0(valueAttribute As String)
    '    Dim gaStr As String = vbNullString '组地址相关文本
    '    Dim text As String = vbNullString '描述文字
    '    If valueAttribute.Contains("<div>") Then '含有描述文字的情况
    '        Dim arry As String() = valueAttribute.Replace("</div>", vbNullString).Split("<div>")
    '        gaStr = arry(0) '取第一个行内容为组地址部分
    '        text = String.Join(vbCrLf, arry.Skip(1)) '其他的用回车分割为描述部分
    '    Else
    '        gaStr = valueAttribute 'value属性只有一行的情况认为全是组地址部分
    '    End If
    '    Me.Text = text
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
    '    Dim gct As HmiValueChangeType = HmiValueChangeType.None '组地址控制模式
    '    Dim gv As GroupValue() '组地址值
    '    If gaAry(1).Contains("|"c) Then '值-切换模式
    '        gct = HmiValueChangeType.Toggle

    '        'gv = gaAry(1).Split("|"c)
    '        'Dim v As GroupValue

    '    ElseIf gaAry(1).Contains("~"c) Then '值-范围模式
    '        gct = HmiValueChangeType.Range
    '    Else '值-固定值模式
    '        gct = HmiValueChangeType.Fixed
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
