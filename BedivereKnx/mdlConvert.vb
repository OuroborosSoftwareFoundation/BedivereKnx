Module mdlConvert

    Friend Function StringToArray(Str As String, Optional separator As Char = ","c) As String()
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

    ''' <summary>
    ''' 字符串转数组
    ''' </summary>
    ''' <typeparam name="T">数组元素的数据类型</typeparam>
    ''' <param name="text">字符串</param>
    ''' <param name="separator">分隔符</param>
    ''' <param name="skipNull">是否跳过空项</param>
    ''' <returns></returns>
    Friend Function StringToArry(Of T)(text As String, Optional separator As Char = ","c, Optional skipNull As Boolean = True) As T()
        If String.IsNullOrEmpty(text) Then
            Return {Nothing}
        Else
            Dim arryStr As String() = text.Split(separator)
            Dim lst As New List(Of T)
            For i = 0 To arryStr.Length - 1
                If skipNull AndAlso String.IsNullOrEmpty(arryStr(i).Trim) Then Continue For '跳过空项
                lst.Add(Convert.ChangeType(arryStr(i).Trim, GetType(T)))
            Next
            Return lst.ToArray
        End If
    End Function

End Module
