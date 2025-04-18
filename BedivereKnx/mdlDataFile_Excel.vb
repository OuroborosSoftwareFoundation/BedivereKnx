Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Module mdlDataFile_Excel

    Private ReadOnly DataSheets As String() = {"Interfaces", "Areas", "Objects", "Scenes", "Devices", "Schedules", "Links"}

    Friend Function ReadExcelToDataTables(FilePath As String, Optional HasSubTitle As Boolean = False, Optional AddIdCol As Boolean = False) As Dictionary(Of String, DataTable)
        If String.IsNullOrEmpty(FilePath) Then
            Throw New ArgumentNullException(NameOf(FilePath), "Data File path cannot be null.")
            Return Nothing
        ElseIf Not File.Exists(FilePath) Then
            Throw New FileNotFoundException($"Could not find file '{FilePath}'")
            Return Nothing
        End If
        Try
            Dim fs As New FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim doc As SpreadsheetDocument = SpreadsheetDocument.Open(fs, False) '非独占方式打开文件
            Dim wbp As WorkbookPart = doc.WorkbookPart
            Dim shts As IEnumerable(Of Sheet) = wbp.Workbook.Descendants(Of Sheet)
            Dim dicDt As New Dictionary(Of String, DataTable)
            For Each sht As Sheet In shts
                'Sheet.Name：工作表名称
                'Sheet.SheetId：工作表ID（表建立的顺序）
                'Sheet.Id：工作表在工作簿中的顺序
                If DataSheets.Contains(sht.Name.Value) Then '只读取需要的Sheet
                    dicDt.Add(sht.Name.Value, SheetToDataTable(wbp, sht.Id.Value, HasSubTitle, AddIdCol))
                End If
            Next
            Return dicDt
        Catch ex As Exception
            Throw
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 工作表转为DataTable
    ''' </summary>
    ''' <param name="wbp"></param>
    ''' <param name="rId"></param>
    ''' <param name="HasSubTitle"></param>
    ''' <param name="AddIdCol"></param>
    ''' <returns></returns>
    Private Function SheetToDataTable(wbp As WorkbookPart, rId As String, Optional HasSubTitle As Boolean = False, Optional AddIdCol As Boolean = False) As DataTable
        Dim wsp As WorksheetPart = wbp.GetPartById(rId) '用rid检索工作表
        Dim dt As New DataTable
        For Each r As Row In wsp.Worksheet.Descendants(Of Row)
            If r.RowIndex.Value = 1 Then '标题行
                For Each c As Cell In r.Descendants(Of Cell)
                    Dim cv As String = GetCellValue(c, wbp.SharedStringTablePart) '单元格内容
                    dt.Columns.Add(cv, GetType(String))
                Next
                If AddIdCol Then
                    dt.Columns.Add("Id", GetType(Integer)) '额外添加id列的情况
                    dt.PrimaryKey = {dt.Columns("Id")} '设为主键
                End If
            ElseIf HasSubTitle AndAlso r.RowIndex.Value = 2 Then '标题注释行
                For Each c As Cell In r.Descendants(Of Cell)
                    Dim cap As String = GetCellValue(c, wbp.SharedStringTablePart) '单元格内容
                    Dim ColIdx As Integer = GetCellIdx(c)(1) '获取单元格的列ID
                    dt.Columns(ColIdx).Caption = cap
                Next
            Else
                Dim dr As DataRow = dt.NewRow()
                Dim isAllEmpty As Boolean = True '行是否全空
                For Each c As Cell In r.Descendants(Of Cell)
                    Dim ColIdx As Integer = GetCellIdx(c)(1) '获取单元格的列ID
                    If ColIdx < dt.Columns.Count Then
                        Dim cv As String = GetCellValue(c, wbp.SharedStringTablePart)
                        If Not String.IsNullOrEmpty(cv) Then isAllEmpty = False '判断单元格值是否为空
                        dr(columnIndex:=ColIdx) = cv
                    Else
                        '无视超出标题列宽的数据
                    End If
                Next
                If Not isAllEmpty Then '行不为空的情况
                    If AddIdCol Then dr("Id") = dt.Rows.Count '额外添加id列的情况
                    dt.Rows.Add(dr)
                End If
            End If
        Next
        If AddIdCol Then dt.Columns("Id").SetOrdinal(0) '额外添加id列的情况，把ID列移至第一列
        Return dt
    End Function

    ''' <summary>
    ''' 获取单元格行列ID{行id,列id}（id从0开始）
    ''' </summary>
    ''' <param name="c">单元格Cell对象</param>
    ''' <param name="base">基数</param>
    Private Function GetCellIdx(c As Cell, Optional base As Integer = 0) As Integer()
        Dim CRef As String = c.CellReference.Value.ToUpper '单元格坐标（例：“A1”、“B2”）
        Dim RowLbl As String = Regex.Match(CRef, "([0-9]+)").Groups(1).Value '取出行号（从1开始）
        Dim RowIdx As Integer = Convert.ToInt32(RowLbl) - 1
        Dim ColLbl As String = Regex.Match(CRef, "([A-Z]+)").Groups(1).Value '取出列标
        Dim ColIdx As Integer = 0
        For i = 0 To ColLbl.Length - 1
            ColIdx += ((Asc(ColLbl(i)) - 64) * (26 ^ (ColLbl.Length - i - 1))) - 1 '列id（从0开始）
        Next
        Return {RowIdx, ColIdx}
    End Function

    ''' <summary>
    ''' 获取单元格的值
    ''' </summary>
    Private Function GetCellValue(cell As Cell, sst As SharedStringTablePart) As String
        If cell.ChildElements.Count = 0 Then Return vbNullString
        Dim str As String = cell.CellValue.InnerText
        If (Not IsNothing(cell.DataType)) AndAlso (cell.DataType = CellValues.SharedString) Then
            Return sst.SharedStringTable.ChildElements(str).InnerText
        Else
            Return cell.CellValue.Text
        End If
    End Function

End Module
