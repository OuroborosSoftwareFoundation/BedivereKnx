Imports System.Drawing

Public Class KnxGpxLink

    Inherits KnxGpxElement

    Public Property Target As String

    Public Sub New()
        MyBase.New(KnxGpxElementMode.Link)
    End Sub

    Public Sub New(location As Point, size As Size, target As String)
        MyBase.New(KnxGpxElementMode.Link, location, size)
        Me.Target = target
    End Sub

    Public Sub New(geometry As GpxGeometry, target As String)
        Me.New(geometry.Location, geometry.Size, target)
    End Sub

End Class
