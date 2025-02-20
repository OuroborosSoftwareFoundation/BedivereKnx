Imports System.Drawing
Module DefaultStyle
    Friend dicColorNone As New Dictionary(Of String, Color) From {
        {"fillColor", Color.Empty},
        {"strokeColor", Color.Empty},
        {"fontColor", Color.Black}
        }
    Friend dicColorDefault As New Dictionary(Of String, Color) From {
        {"fillColor", Color.White},
        {"strokeColor", Color.Black},
        {"fontColor", Color.Black}
        }

End Module
