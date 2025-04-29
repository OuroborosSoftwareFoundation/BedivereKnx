Imports System.IO
Imports System.Text

Module mdlLog

    ''' <summary>
    ''' 写入日志文件
    ''' </summary>
    ''' <param name="dt">日志DataTable对象</param>
    ''' <param name="path">日志路径</param>
    ''' <param name="coding">编码，默认UTF-8</param>
    Public Sub WriteCsvLog(dt As DataTable, path As String, Optional coding As String = "utf-8")
        Dim sw As New StreamWriter(path, False, Encoding.GetEncoding(coding))
        Dim header As New List(Of String)
        For h = 0 To dt.Columns.Count - 1
            header.Add(dt.Columns(h).ColumnName) '每列的列名
        Next
        sw.WriteLine(String.Join(","c, header.ToArray)) '输出列标行
        For r = 0 To dt.Rows.Count - 1
            Dim line As New List(Of String)
            For c = 0 To dt.Columns.Count - 1
                If dt.Columns(c).DataType.IsEnum Then 'DataTable某列为枚举的情况，如果直接输出会显示数字
                    line.Add(dt.Columns(c).DataType.GetEnumName(dt(r)(c))) '获取枚举值的名称
                ElseIf dt.Columns(c).DataType = GetType(DateTime) Then
                    line.Add($"{dt(r)(c):yyyy-MM-dd HH:mm:ss}") '带毫秒Excel打开无法正常显示
                Else
                    line.Add(dt(r)(c).ToString)
                End If
            Next
            sw.WriteLine(String.Join(","c, line.ToArray)) '输出一行数据
        Next
        sw.Flush()
        sw.Close()
    End Sub

End Module
