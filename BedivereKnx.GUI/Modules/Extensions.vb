Imports System.Runtime.CompilerServices

Friend Module Extensions

    ''' <summary>
    ''' 检查ListView中是否包含组
    ''' </summary>
    ''' <param name="lv">ListView对象</param>
    ''' <param name="groupName">组名</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ContainsGroup(lv As ListView, groupName As String) As Boolean
        For Each grp As ListViewGroup In lv.Groups
            If grp.Name = groupName Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' 尝试向字典中添加键值对，如果存在则覆盖值
    ''' </summary>
    ''' <typeparam name="TKey">键的类型</typeparam>
    ''' <typeparam name="TValue">值的类型</typeparam>
    ''' <param name="dic">字典对象</param>
    ''' <param name="key">键</param>
    ''' <param name="value">值</param>
    <Extension()>
    Public Sub TryAddOrCover(Of TKey, TValue)(dic As Dictionary(Of TKey, TValue), key As TKey, value As TValue)
        If Not dic.TryAdd(key, value) Then
            dic(key) = value
        End If
    End Sub

End Module
