Module mdlEscapeCharacter

    Public Function Str_XML(str As String) As String
        If String.IsNullOrEmpty(str) Then Return vbNullString
        Return str.Replace("&", "&amp;").Replace("""", "&quot;").Replace("'", "&apos;").Replace("<", "&lt;").Replace(">", "&gt;")
    End Function

End Module
