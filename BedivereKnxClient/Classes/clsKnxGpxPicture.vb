Imports System.IO

''' <summary>
''' KNX界面图片
''' </summary>
Public Class KnxGpxPicture

    Inherits KnxGpxElement

    Public Property Image As Image

    Public Sub New()
        MyBase.New(KnxGpxElementMode.Picture)
    End Sub

    Public Sub New(location As Point, size As Size, base64String As String)
        MyBase.New(KnxGpxElementMode.Picture, location, size)
        Me.Image = CreateImageFromBase64String(base64String)
    End Sub

    Public Sub New(geometry As GpxGeometry, base64String As String)
        Me.New(geometry.Location, geometry.Size, base64String)
    End Sub

    ''' <summary>
    ''' 从Base64字符串生成图片
    ''' </summary>
    ''' <param name="base64String"></param>
    ''' <returns></returns>
    Private Shared Function CreateImageFromBase64String(base64String As String) As Image
        Dim byteArray As Byte() = Convert.FromBase64String(base64String) 'base64数组
        Using memoryStream As New MemoryStream(byteArray) 'byte数组转为Stream
            Return Image.FromStream(memoryStream) '从Stream生成图片
        End Using
    End Function

End Class
