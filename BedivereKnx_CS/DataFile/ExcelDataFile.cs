using System.Data;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Knx.Falcon;

namespace BedivereKnx.DataFile
{

    public static class ExcelDataFile
    {

        /// <summary>
        /// 需要读取的工作表名
        /// </summary>
        private static readonly string[] DataSheets = { "Interfaces", "Areas", "Objects", "Scenes", "Devices", "Schedules", "Links" };

        /// <summary>
        /// 读取Excel数据文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="hasSubTitle">工作表内有副标题</param>
        /// <param name="addIdColumn">向读取后的DataTable添加ID列</param>
        /// <returns></returns>
        public static DataTableCollection FromExcel(string filePath, bool hasSubTitle = false, bool addIdColumn = false)
        {
            //判断文件是否存在
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{ResString.ExMsg_FileNotFound}\n{Path.GetFullPath(filePath)}", filePath);
            }

            //验证文件扩展名
            string extension = Path.GetExtension(filePath).ToLower();
            if (extension != ".xlsx" && extension != ".xls")
            {
                throw new ArgumentException($"{string.Format(ResString.ExMsg_FileFormatInvalid, "Excel")}\n{Path.GetFullPath(filePath)}");
            }

            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //新建文件流
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false)) //以非独占方式打开文件
                {

                    WorkbookPart? wbp = doc.WorkbookPart;
                    if (wbp is not null) //判断Excel文件结构是否正常
                    {
                        Sheets? sheets = wbp.Workbook.Sheets; //工作表的集合
                        DataSet ds = new();
                        if ((sheets is not null) && sheets.Any())
                        {
                            foreach (Sheet sheet in sheets.Cast<Sheet>())
                            {
                                //Sheet.Name：工作表名称
                                //Sheet.SheetId：工作表ID（表建立的顺序）
                                //Sheet.Id：工作表在工作簿中的顺序
                                if ((sheet is not null) && DataSheets.Contains(sheet.Name?.Value)) //只读取需要的Sheet
                                {
                                    DataTable dt = DataTableFromSheet(wbp, sheet, hasSubTitle, addIdColumn);
                                    ds.Tables.Add(dt);
                                }
                            }
                        }
                        return ds.Tables;
                    }
                    else //WorkbookPart结构错误
                    {
                        throw new InvalidDataException(string.Format(ResString.ExMsg_FileFormatInvalid, "Excel"));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 读取工作表为DataTable
        /// </summary>
        /// <param name="wbp"></param>
        /// <param name="sheet"></param>
        /// <param name="hasSubTitle">是否有副标题</param>
        /// <param name="addIdColumn">是否添加Id列</param>
        /// <returns></returns>
        private static DataTable DataTableFromSheet(WorkbookPart wbp, Sheet sheet, bool hasSubTitle = false, bool addIdColumn = false)
        {
            DataTable dt = new(sheet.Name);
            WorksheetPart wsp = (WorksheetPart)wbp.GetPartById(sheet.Id!);
            SheetData sheetData = wsp.Worksheet.Elements<SheetData>().First();
            IEnumerable<Row> rows = sheetData.Elements<Row>();

            //添加DataTable的列
            foreach (Cell cell in rows.First().Elements<Cell>()) //遍历第一行的单元格
            {
                string colName = GetCellValue(cell, wbp);
                if (StaticDict.ColumnType.ContainsKey(colName))
                {
                    dt.Columns.Add(colName, StaticDict.ColumnType[colName]); //列格式全部设置为string
                }
                else
                {
                    dt.Columns.Add(colName, typeof(string)); //列格式全部设置为string
                }

            }
            if (addIdColumn) dt.Columns.Add("Id", typeof(int)); //额外添加Id列

            //添加内容列
            foreach (Row row in rows.Skip(1)) //遍历工作表的行
            {
                if (hasSubTitle && row.RowIndex?.Value == 2) //副标题行
                {
                    foreach (Cell cell in row.Elements<Cell>()) //遍历副标题行的单元格
                    {
                        string cv = GetCellValue(cell, wbp); //单元格内容
                        int cid = GetCellIndex(cell)[1]; //单元格列ID
                        dt.Columns[cid].Caption = cv; //设置DataTable标题
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow(); //新建一行
                    bool isAllEmpty = true; //行是否全空
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        int cid = GetCellIndex(cell)[1]; //获取单元格的列ID
                        if (cid >= dt.Columns.Count) continue; //确保单元格列ID不超过DataTable列ID
                        //无视超出标题列宽的数据
                        string cv = GetCellValue(cell, wbp); //单元格内容
                        if (!string.IsNullOrWhiteSpace(cv)) isAllEmpty = false; //判断行是否全空

                        Type colType = dt.Columns[cid].DataType; //列的数据类型
                        switch (Type.GetTypeCode(colType)) //判断数据类型
                        {
                            case TypeCode.String: //字符串
                                dr[cid] = cv;
                                break;
                            case TypeCode.Boolean: //布尔型
                                dr[cid] = (cv != "0" || cv.ToLower() == "true");
                                break;
                            //case TypeCode.Byte:
                            //case TypeCode.SByte:
                            //case TypeCode.Int16:
                            //case TypeCode.Int32:
                            //case TypeCode.Int64:
                            //case TypeCode.UInt16:
                            //case TypeCode.UInt32:
                            //case TypeCode.UInt64:
                            //case TypeCode.Single:
                            //case TypeCode.Double:
                            //case TypeCode.Decimal:
                            //    dr[cid] = cv;
                            //    break;
                            default:
                                string colName = dt.Columns[cid].ColumnName;
                                if (StaticDict.ColumnType.ContainsKey(colName)) //特殊的列数据类型
                                {
                                    if (colType == typeof(GroupAddress)) //组地址
                                    {
                                        dr[cid] = new GroupAddress(cv);
                                    }
                                    else if (colType == typeof(IndividualAddress)) //物理地址
                                    {
                                        dr[cid] = new IndividualAddress(cv);
                                    }
                                }
                                else
                                {
                                    dr[cid] = cv.ToString();
                                }
                                break;
                        }

                    }
                    if (!isAllEmpty) //行不为空的情况
                    {
                        if (addIdColumn) dr["Id"] = dt.Rows.Count; //额外添加ID列的情况
                        dt.Rows.Add(dr);
                    }
                }
            }
            if (addIdColumn)
            {
                dt.Columns["Id"]?.SetOrdinal(0); //'额外添加ID列的情况，把ID列移至第一列
                dt.PrimaryKey = [dt.Columns[0]]; //设为主键
            }
            return dt;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="wbp"></param>
        /// <returns></returns>
        private static string GetCellValue(Cell cell, WorkbookPart wbp)
        {
            if (cell?.ChildElements.Count == 0) return string.Empty;
            if (cell?.CellValue == null) return string.Empty;
            string value = cell.CellValue.Text;
            if (cell.DataType?.Value == CellValues.SharedString)
            {
                if (wbp.SharedStringTablePart?.SharedStringTable != null)
                {
                    value = wbp.SharedStringTablePart.SharedStringTable
                        .ElementAtOrDefault(int.Parse(value))?.InnerText ?? string.Empty;
                }
            }
            return value;
        }

        /// <summary>
        /// 获取单元格行列ID{行id,列id}（id从0开始）
        /// </summary>
        /// <param name="cell">单元格对象</param>
        /// <returns></returns>
        private static int[] GetCellIndex(Cell cell)
        {
            if (cell.CellReference is null) return [-1, -1];
            string? cellRef = cell.CellReference.Value; //单元格坐标（例：“A1”、“B2”）
            if (cellRef is null) return [-1, -1];
            cellRef = cellRef.ToUpper();
            string rowLbl = Regex.Match(cellRef, "([0-9]+)").Groups[1].Value; //取出行号（从1开始）
            int rowIdx = Convert.ToInt32(rowLbl) - 1;
            string colLbl = Regex.Match(cellRef, "([A-Z]+)").Groups[1].Value; //去除列标
            int colIdx = 0;
            for (int i = 0; i < colLbl.Length; i++)
            {
                colIdx += (((int)colLbl[i] - 64) * ((int)Math.Pow(26, (colLbl.Length - i - 1)))) - 1; //列id（从0开始）
            }
            return [rowIdx, colIdx];
        }

    }

}
