Imports System.Drawing

''' <summary>
''' KNX界面元素
''' </summary>
Public Class KnxGpxElement

    ''' <summary>
    ''' 控件类型
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As KnxGpxElementMode

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

    ''' <summary>
    ''' 链接目标
    ''' </summary>
    Public LinkTarget As String

    Public Sub New(type As KnxGpxElementMode)
        Me.Type = type
    End Sub

    Public Sub New(type As KnxGpxElementMode, geometry As GpxGeometry)
        Me.New(type, geometry.Location, geometry.Size)
    End Sub

    Public Sub New(type As KnxGpxElementMode, location As Point, size As Size)
        Me.Type = type
        Me.Location = location
        Me.Size = size
    End Sub

End Class
