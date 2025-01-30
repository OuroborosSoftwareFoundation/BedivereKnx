Module mdlConvert

    Friend Function String__StrArray(Str As String, Optional separator As Char = ","c) As String()
        If String.IsNullOrEmpty(Str) Then
            Return {vbNullString}
        Else
            Dim ary0 As String() = Str.Split(separator)
            Dim lst As New List(Of String)
            For i = 0 To ary0.Length - 1
                If String.IsNullOrEmpty(ary0(i).Trim) Then Continue For '跳过空项
                lst.Add(ary0(i).Trim)
            Next
            Return lst.ToArray
        End If
    End Function

    Friend Function String__IntArray(Str As String, Optional separator As Char = ","c) As Integer()
        If String.IsNullOrEmpty(Str) Then
            Return {vbNullString}
        Else
            Dim ary0 As String() = Str.Split(separator)
            Dim lst As New List(Of Integer)
            For i = 0 To ary0.Length - 1
                If String.IsNullOrEmpty(ary0(i).Trim) Then Continue For '跳过空项
                lst.Add(Convert.ToInt32(ary0(i).Trim))
            Next
            Return lst.ToArray
        End If
    End Function

End Module
