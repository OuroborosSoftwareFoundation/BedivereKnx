Imports Knx.Falcon

Public Class KnxGpxConvertion

    Public Property Type As GpxConvertionType

    Public Property OffColor As Color

    Public Property OnColor As Color

    Public Property GroupAddress As GroupAddress

    Public Sub New(type As GpxConvertionType)
        Me.Type = GpxConvertionType.None
    End Sub
    Public Sub New(offColor As Color, onColor As Color)
        Me.Type = GpxConvertionType.None
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

    Public Sub New(type As GpxConvertionType, offColor As Color, onColor As Color)
        Me.Type = type
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

End Class

Public Enum GpxConvertionType
    None = -1
    Fill
    Stroke
End Enum
