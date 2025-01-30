Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Drawing

Public Module mdlDrawIo

    Public Function ReadDrawioToDic(FilePath As String) As Dictionary(Of String, KnxGpxElement())
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException("Draw.io File path cannot be null.")
            Return Nothing
        ElseIf Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim xr As XmlReader = XmlReader.Create(fs, New XmlReaderSettings)
        xr.ReadStartElement("mxfile")
        Dim dicGpx As New Dictionary(Of String, KnxGpxElement())
        Dim PageName As String = vbNullString '页面名称
        Dim lstElem As New List(Of KnxGpxElement) '当前页面的全部控件的列表
        While xr.Read
            If xr.NodeType = XmlNodeType.Whitespace Then Continue While '跳过标记间的空白
            If xr.NodeType = XmlNodeType.Element Then
                Select Case xr.Name.ToLower
                    Case "diagram" '页面开始
                        PageName = xr.GetAttribute("name") '设定当前页面名称
                        dicGpx.Add(PageName, Nothing)'在输出字典中新建一个页的键值对
                    Case "mxcell" '页面中新单元开始
                        If String.IsNullOrWhiteSpace(xr.GetAttribute("parent") = vbNullString) Then Continue While '跳过parent为空的项
                        Dim valueStr As String = xr.GetAttribute("value") '控件的值
                        Dim styleStr As String = xr.GetAttribute("style") 'style属性
                        Dim inner As String = xr.ReadInnerXml() 'xmCell内部的mxGeometry元素
                        If String.IsNullOrWhiteSpace(inner) Then Continue While '跳过无内部XML的元素
                        Dim geom As GpxGeometry = GpxGeometry.FromInnerXml(inner) '控件坐标和尺寸
                        Dim match As Match = Regex.Match(styleStr, "shape=image;.*?image=data:image/.*?,(.*?);") '暂时忽略图片格式
                        If String.IsNullOrEmpty(match.Value) Then '控件的情况
                            lstElem.Add(New KnxGpxComponent(geom, styleStr, valueStr)) '控件加入列表
                        Else '图片的情况
                            lstElem.Add(New KnxGpxPicture(geom, match.Groups(1).Value)) '图片加入列表
                        End If
                End Select
            ElseIf xr.NodeType = XmlNodeType.EndElement Then
                If xr.Name.ToLower = "diagram" Then '页面结束
                    dicGpx(PageName) = lstElem.ToArray '写入上个页面的信息
                    lstElem.Clear() '清空控件信息
                End If
            End If
        End While
        Return dicGpx
    End Function

    ''' <summary>
    ''' 从内部XML获取控件坐标和尺寸
    ''' </summary>
    ''' <param name="innerXml"></param>
    ''' <returns></returns>
    Private Function ReadGeometryFromInnerXml(innerXml As String) As GpxGeometry
        If String.IsNullOrWhiteSpace(innerXml) Then Return Nothing
        Using xrI As XmlReader = XmlReader.Create(New StringReader(innerXml.Trim))
            Dim geo As New GpxGeometry
            While xrI.Read '第一个元素为WhiteSpace
                If xrI.NodeType = XmlNodeType.Element AndAlso xrI.Name = "mxGeometry" Then
                    geo.Location = New Point With {
                        .X = Convert.ToSingle(xrI.GetAttribute("x")),
                        .Y = Convert.ToSingle(xrI.GetAttribute("y"))
                    }
                    geo.Size = New Size With {
                        .Width = Convert.ToSingle(xrI.GetAttribute("width")),
                        .Height = Convert.ToSingle(xrI.GetAttribute("height"))
                    }
                    Exit While
                End If
            End While
            Return geo
        End Using
    End Function

    '''' <summary>
    '''' 从Base64字符串生成图片
    '''' </summary>
    '''' <param name="base64String"></param>
    '''' <returns></returns>
    'Private Function CreateImageFromBase64String(base64String As String) As Image
    '    Dim byteArray As Byte() = Convert.FromBase64String(base64String) 'base64数组
    '    Using memoryStream As New MemoryStream(byteArray) 'byte数组转为Stream
    '        Return Image.FromStream(memoryStream) '从Stream生成图片
    '    End Using
    'End Function

End Module

'Public Function ReadDrawioToDic(FilePath As String) As Dictionary(Of String, KnxGpxElement())
'    If String.IsNullOrEmpty(FilePath) Then
'        Throw New ArgumentNullException("Draw.io File path cannot be null.")
'        Return Nothing
'    ElseIf Not File.Exists(FilePath) Then
'        Throw New FileNotFoundException($"Could not find file '{FilePath}'")
'        Return Nothing
'    End If

'    Try

'    Catch ex As Exception
'    End Try
'    Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
'    Dim xr As XmlReader = XmlReader.Create(fs, New XmlReaderSettings)
'    xr.ReadStartElement("mxfile")
'    Dim dicGpx As New Dictionary(Of String, KnxGpxElement())
'    Dim PageName As String = vbNullString '页面名称
'    Dim dicLayout As New Dictionary(Of String, String) '当前页面的图层字典，ID-名称
'    Dim lstElem As New List(Of KnxGpxElement) '当前页面的全部控件的列表
'    While xr.Read
'        If xr.NodeType = XmlNodeType.Whitespace Then Continue While '跳过标记间的空白
'        If xr.NodeType = XmlNodeType.Element Then
'            Select Case xr.Name.ToLower
'                Case "diagram" '新的页面开始
'                    If Not String.IsNullOrWhiteSpace(PageName) Then
'                        For Each kgc As KnxGpxComponent In lstElem.OfType(Of KnxGpxComponent) '便利之前页面的控件准备修改图层名
'                            Dim LytName As String = Nothing '图层名称
'                            If dicLayout.TryGetValue(kgc.RawParent, LytName) Then
'                                Select Case LytName.Trim.ToLower'根据图层名称配置控件的控制/反馈类型
'                                    Case "control"
'                                        kgc.Direction = KnxGpxComponentType.Control
'                                    Case "feedback"
'                                        kgc.Direction = KnxGpxComponentType.Feedback
'                                    Case Else
'                                        kgc.Direction = KnxGpxComponentType.Unknown
'                                End Select
'                            End If
'                        Next
'                        dicGpx(PageName) = lstElem.ToArray '写入上个页面的信息
'                        dicLayout.Clear() '清空图层信息
'                        lstElem.Clear() '清空控件信息
'                    End If
'                    PageName = xr.GetAttribute("name") '设定当前页面名称
'                    dicGpx.Add(PageName, Nothing)'在输出字典中新建一个页的键值对
'                Case "mxcell" '页面中新控件开始
'                    'parent属性为空的是主对象，为0的是图层，为1的是背景图层，1以上数字为自定义图层
'                    If String.IsNullOrWhiteSpace(xr.GetAttribute("parent") = vbNullString) Then Continue While
'                    Select Case xr.GetAttribute("parent")'判断元素所属图层
'                        Case Nothing
'                            Continue While
'                        Case "0" '图层
'                            If xr.HasAttributes AndAlso xr.GetAttribute("id") <> "1" Then
'                                dicLayout.Add(xr.GetAttribute("id"), xr.GetAttribute("value")) '加入字典
'                            End If
'                            'Case "1" '背景图层的对象，认为是图片
'                            '    Dim style As String = xr.GetAttribute("style") 'style属性包含图片的Base64字符串
'                            '    Dim match As Match = Regex.Match(style, "image=data:image/.*?,(.*?);") '暂时忽略图片格式
'                            '    'Dim img As Image = CreateImageFromBase64String(match.Groups(1).Value) '从Base64字符串生成图片
'                            '    If match.Groups.Count < 2 Then Continue While '正则表达式未匹配认为不是图片元素
'                            '    Dim inner As String = xr.ReadInnerXml() 'xmCell内部的mxGeometry元素
'                            '    If String.IsNullOrWhiteSpace(inner) Then '内部XML为空时认为错误
'                            '        Continue While '跳到下个循环，读取下个XML元素
'                            '    Else
'                            '        lstElem.Add(New KnxGpxPicture(ReadGeometryFromInnerXml(inner), match.Groups(1).Value)) '图片加入列表
'                            '    End If
'                        Case Else '其他情况认为是控件
'                            'If String.IsNullOrWhiteSpace(xr.GetAttribute("value")) Then Continue While 'value为即没有文字，跳过
'                            Dim styleStr As String = xr.GetAttribute("style") 'style属性
'                            Dim match As Match = Regex.Match(styleStr, "image=data:image/.*?,(.*?);") '暂时忽略图片格式
'                            If match.Groups.Count < 2 Then '是控件的情况（即正则表达式没有匹配到）
'                                Dim parentStr As String = xr.GetAttribute("parent") 'parent属性为控件所属图层ID
'                                Dim valueStr As String = xr.GetAttribute("value") '控件的值，包含显示名称和组地址
'                                Dim inner As String = xr.ReadInnerXml() 'xmCell内部的mxGeometry元素
'                                If String.IsNullOrWhiteSpace(inner) Then '内部XML为空时认为错误
'                                    Continue While '跳到下个循环，读取下个XML元素
'                                Else
'                                    Dim comp As New KnxGpxComponent(ReadGeometryFromInnerXml(inner), New KnxGpxComponentStyle(styleStr)) With {
'                                            .RawParent = parentStr, 'parent属性为控件所属图层ID
'                                            .RawValue = valueStr '控件的值，包含显示名称和组地址
'                                            }
'                                    lstElem.Add(comp)
'                                End If
'                            Else '是图片的情况
'                                Dim inner As String = xr.ReadInnerXml() 'xmCell内部的mxGeometry元素
'                                If String.IsNullOrWhiteSpace(inner) Then '内部XML为空时认为错误
'                                    Continue While '跳到下个循环，读取下个XML元素
'                                Else
'                                    lstElem.Add(New KnxGpxPicture(ReadGeometryFromInnerXml(inner), match.Groups(1).Value)) '图片加入列表
'                                End If
'                            End If
'                    End Select
'            End Select
'        End If
'    End While
'    Return dicGpx
'End Function
