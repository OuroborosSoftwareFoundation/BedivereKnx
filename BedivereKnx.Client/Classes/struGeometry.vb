Imports System.IO
Imports System.Xml

''' <summary>
''' 控件坐标和尺寸
''' </summary>
Public Structure GpxGeometry

    ''' <summary>
    ''' 控件坐标
    ''' </summary>
    ''' <returns></returns>
    Public Property Location As Point

    ''' <summary>
    ''' 控件尺寸
    ''' </summary>
    ''' <returns></returns>
    Public Property Size As Size

    Public Sub New(location As Point, size As Size)
        Me.Location = location
        Me.Size = size
    End Sub

    Public Sub New(x As Integer, y As Integer, width As Integer, height As Integer)
        Me.New(New Point(x, y), New Size(width, height))
    End Sub

    ''' <summary>
    ''' 从内部XML获取控件坐标和尺寸
    ''' </summary>
    ''' <param name="innerXml"></param>
    ''' <returns></returns>
    Friend Shared Function FromInnerXml(innerXml As String) As GpxGeometry
        If String.IsNullOrWhiteSpace(innerXml) Then Return Nothing
        Using xrI As XmlReader = XmlReader.Create(New StringReader(innerXml.Trim))
            Dim geom As New GpxGeometry
            While xrI.Read '第一个元素为WhiteSpace
                If xrI.NodeType = XmlNodeType.Element AndAlso xrI.Name = "mxGeometry" Then
                    geom.Location = New Point With {
                        .X = Convert.ToSingle(xrI.GetAttribute("x")),
                        .Y = Convert.ToSingle(xrI.GetAttribute("y"))
                    }
                    geom.Size = New Size With {
                        .Width = Convert.ToSingle(xrI.GetAttribute("width")),
                        .Height = Convert.ToSingle(xrI.GetAttribute("height"))
                    }
                    Exit While
                End If
            End While
            Return geom
        End Using
    End Function

End Structure
