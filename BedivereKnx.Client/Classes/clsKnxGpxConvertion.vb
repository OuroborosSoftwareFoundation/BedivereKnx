Imports Knx.Falcon

Public Class KnxGpxConvertion

    Public Property Type As KnxGpxConvertionType

    Public Property OffColor As Color

    Public Property OnColor As Color

    Public Property GroupAddress As GroupAddress

    Public Property GroupValue As GroupValue

    Public Sub New()
        Me.Type = KnxGpxConvertionType.None
    End Sub
    Public Sub New(offColor As Color, onColor As Color)
        Me.Type = KnxGpxConvertionType.None
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

    Public Sub New(type As KnxGpxConvertionType, offColor As Color, onColor As Color)
        Me.Type = type
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

End Class

Public Enum KnxGpxConvertionType
    None = -1
    Fill
    Stroke
End Enum
