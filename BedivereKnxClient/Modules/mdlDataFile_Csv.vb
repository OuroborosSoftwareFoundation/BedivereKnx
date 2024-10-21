Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Module mdlDataFile_Csv

    Public Function ReadCsvToDataTable(FilePath As String) As DataTable
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException("Data File path cannot be null.")
            Return Nothing
        ElseIf Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim sr As New StreamReader(fs, Encoding.GetEncoding("gb2312"))
        Dim head As String() = sr.ReadLine.Split(","c) '读出标题行



        Return Nothing
    End Function

    Public Function ReadCsvToDic(FilePath As String) As Dictionary(Of String, String)
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException("Data File path cannot be null.")
            Return Nothing
        ElseIf Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim sr As New StreamReader(fs, Encoding.GetEncoding("gb2312"))
        Dim head As String() = sr.ReadLine.Split(","c) '读出标题行


        Return Nothing
    End Function

End Module
