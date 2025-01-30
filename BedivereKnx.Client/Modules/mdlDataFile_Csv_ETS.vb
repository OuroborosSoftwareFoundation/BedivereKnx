Imports System.IO
Imports System.Text.RegularExpressions

Module mdlDataFile_Csv_ETS

    Private Sub InitDataTable(dt As DataTable)
        dt.Columns.Add("Building", GetType(String))
        dt.Columns.Add("Floor", GetType(String))
        dt.Columns.Add("Area", GetType(String))
        dt.Columns.Add("Type", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("SwCtl", GetType(String))
        dt.Columns.Add("SwFdb", GetType(String))
        dt.Columns.Add("ValCtl", GetType(String))
        dt.Columns.Add("ValFdb", GetType(String))
        dt.Columns.Add("DimCtl", GetType(String))

        dt.Columns.Add("Sw", GetType(String))
        dt.Columns.Add("Val", GetType(String))
    End Sub

    Public Function ReadEtsCsvToDataTable(FilePath As String) As DataTable
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException("Data File path cannot be null.")
            Return Nothing
        End If
        If Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim sr As New StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"))
        sr.ReadLine() '跳过标题行
        Dim GaMain As String = vbNullString
        Dim GaMdl As String = vbNullString
        Dim dt As New DataTable
        InitDataTable(dt)
        Dim dtTmp As New DataTable '缓存中间组的全部组地址
        InitDataTable(dtTmp)
        Do
            Dim c() As String = sr.ReadLine.Replace(""""c, "").Split(","c)
            If Not String.IsNullOrEmpty(c(0).Trim) Then '包含主组名的行
                GaMain = c(0)
            ElseIf Not String.IsNullOrEmpty(c(1).Trim) OrElse sr.EndOfStream Then '包含中间组名的行或读取结束写入最后一个中间组内容
                GaMdl = c(1)
                For Each drT As DataRow In dtTmp.Rows
                    dt.Rows.Add(drT.ItemArray) '写入上个中间组全部组地址
                Next
                dtTmp.Clear()
            Else '包含组地址的行
                Dim Ga() As String = c(2).Split("."c)
                Dim GaRow As Integer = -1
                For i = 0 To dtTmp.Rows.Count - 1
                    If dtTmp(i)("Name") = Ga(0) Then '设备已经有组地址记录
                        GaRow = i
                        Exit For
                    End If
                Next
                If GaRow >= 0 Then '设备已有记录
                    GaSet(c, dtTmp(GaRow))
                Else '新设备
                    Dim dr As DataRow = dtTmp.NewRow
                    dr("Building") = "Main"
                    dr("Floor") = GaMain
                    dr("Area") = GaMdl
                    dr("Name") = Ga(0)
                    GaSet(c, dr)
                    dtTmp.Rows.Add(dr)
                End If
            End If
        Loop Until sr.EndOfStream
        Return dt
    End Function

    Private Sub GaSet(cells() As String, dr As DataRow)
        Dim GaName() As String = cells(2).Split("."c) '组地址名
        Dim GaAddr As String = $"{cells(3)}/{cells(4)}/{cells(5)}"
        Select Case GaName.Last
            Case "Ctl", "SwCtl", "Lock", "En"
                dr("Type") = "Switch"
                dr("SwCtl") = GaAddr
            Case "Fdb", "SwFdb"
                dr("Type") = "Switch"
                dr("SwFdb") = GaAddr
            Case "ValCtl"
                dr("Type") = "Dimming"
                dr("ValCtl") = GaAddr
            Case "ValFdb"
                dr("Type") = "Dimming"
                dr("ValFdb") = GaAddr
            Case "Dim", "Dimming", "DimCtl"
                dr("Type") = "Dimming"
                dr("DimCtl") = GaAddr
            Case "Scn"
                dr("Type") = "Scene"
                dr("ValCtl") = GaAddr
            Case Else
                dr("Type") = "Unknown"
        End Select
    End Sub

End Module
