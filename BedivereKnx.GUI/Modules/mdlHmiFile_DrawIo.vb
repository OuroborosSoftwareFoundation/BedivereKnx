﻿Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Drawing
Imports Ouroboros.Hmi.Library
Imports Ouroboros.Hmi.Library.Elements

Public Module mdlDrawIo

    Public ReadOnly DEFAULTCOLOR_OFF As Color = Color.Gray
    Public ReadOnly DEFAULTCOLOR_ON As Color = Color.Green
    Public ReadOnly DEFAULTTEXT_OFF As String = "OFF"
    Public ReadOnly DEFAULTTEXT_ON As String = "ON"

    Public Function ReadDrawioToDic(FilePath As String) As Dictionary(Of String, HmiPage)
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException(NameOf(FilePath), "Draw.io File path cannot be null.")
            Return Nothing
        ElseIf Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim xr As XmlReader = XmlReader.Create(fs, New XmlReaderSettings)
        xr.ReadStartElement("mxfile")
        Dim Pages As New Dictionary(Of String, HmiPage)
        Dim PageName As String = vbNullString '当前页面名称
        'Dim dicParent As New Dictionary(Of String, String) '图层字典（id-名称）
        'Dim lstElem As New List(Of HmiElement) '当前页面的全部控件的列表
        While xr.Read
            If xr.NodeType = XmlNodeType.Whitespace Then Continue While '跳过标记间的空白
            If xr.NodeType = XmlNodeType.Element Then
                Select Case xr.Name.ToLower
                    Case "diagram" '页面开始
                        PageName = xr.GetAttribute("name") '设定当前页面名称
                        Pages.Add(PageName, New HmiPage) '在输出字典中新建一个页的键值对
                        'Pages(PageName).BackColor = ColorTranslator.FromHtml(xr.GetAttribute("background"))
                        Pages(PageName).Elements = New List(Of HmiElement)
                    Case "mxgraphmodel"
                        Pages(PageName).PageSize = New Size With {
                            .Width = xr.GetAttribute("pageWidth"),
                            .Height = xr.GetAttribute("pageHeight")
                        }
                    Case "mxcell" '页面中新单元开始
                        Dim parent As String = xr.GetAttribute("parent") '元素的图层名称
                        If String.IsNullOrWhiteSpace(parent) Then Continue While '跳过parent为空的项
                        'Select Case parent'元素的图层
                        '    Case vbNullString
                        '        Continue While '跳过parent为空的项
                        '    Case "0"'parent属性为0的是图层

                        '    Case "1" 'parent为1的是背景图层
                        '        'Case "bg" '背景图片

                        '    Case Else '其他图层，认为是控件

                        'End Select

                        Dim valueStr As String = xr.GetAttribute("value") '控件的值
                        Dim styleStr As String = xr.GetAttribute("style") 'style属性
                        Dim inner As String = xr.ReadInnerXml() 'xmCell内部的mxGeometry元素
                        If String.IsNullOrWhiteSpace(inner) Then Continue While '跳过无内部XML的元素
                        Dim geom As HmiGeometry = HmiGeometry.FromDrawioXml(inner) '控件坐标和尺寸
                        Dim match As Match = Regex.Match(styleStr, "shape=image;.*?image=data:image/.*?,(.*?);") '暂时忽略图片格式
                        If String.IsNullOrEmpty(match.Value) Then '控件的情况
                            'lstElem.Add(New KnxHmiComponent(geom, styleStr, valueStr)) '控件加入列表
                            Pages(PageName).Elements.Add(New KnxHmiComponent(geom, styleStr, valueStr)) '控件加入列表
                        Else '图片的情况
                            If parent = "1" Then '背景图层
                                Pages(PageName).BackImages.Add(New HmiImageElement(geom, match.Groups(1).Value)) '图片加入背景列表
                            Else
                                'lstElem.Add(New HmiImageElement(geom, match.Groups(1).Value)) '图片加入列表
                                Pages(PageName).Elements.Add(New HmiImageElement(geom, match.Groups(1).Value)) '图片加入列表
                            End If
                        End If
                End Select
            ElseIf xr.NodeType = XmlNodeType.EndElement Then
                If xr.Name.ToLower = "diagram" Then '页面结束
                    'Pages(PageName).Elements = lstElem.ToArray '写入上个页面的信息
                    'lstElem.Clear() '清空控件信息
                End If
            End If
        End While
        Return Pages
    End Function

    ''' <summary>
    ''' 从内部XML获取控件坐标和尺寸
    ''' </summary>
    ''' <param name="innerXml"></param>
    ''' <returns></returns>
    Private Function ReadGeometryFromInnerXml(innerXml As String) As HmiGeometry
        If String.IsNullOrWhiteSpace(innerXml) Then Return Nothing
        Using xrI As XmlReader = XmlReader.Create(New StringReader(innerXml.Trim))
            Dim geo As New HmiGeometry
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

    Private Function Str_XML(str As String) As String
        If String.IsNullOrEmpty(str) Then Return vbNullString
        Return str.Replace("&", "&amp;").Replace("""", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;")
    End Function

End Module
