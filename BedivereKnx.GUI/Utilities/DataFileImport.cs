using System.Data;
using Knx.Falcon;
using Knx.Falcon.ApplicationData.DatapointTypes;

namespace BedivereKnx.GUI
{

    internal static class DataFileImporter
    {

        /// <summary>
        /// 从ETS组地址CSV文件导入
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nameCols">组地址名列数（1|3）</param>
        /// <param name="addressCols">地址列数（1|3）</param>
        /// <param name="hasHeader">是否有标题行</param>
        /// <param name="separator">分隔符（制表符|,|;）</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static DataTable FromEts5GaCsv(string filePath, int nameCols, int addressCols, bool hasHeader = true, char separator = ',')
        {
            //判断文件是否存在：
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{Resources.Strings.Ex_FileNotFound}{Environment.NewLine}{Path.GetFullPath(filePath)}", filePath);
            }
            //验证文件扩展名：
            string extension = Path.GetExtension(filePath).ToLower();
            if (extension != ".csv")
            {
                throw new ArgumentException($"{string.Format(Resources.Strings.Ex_FileFormatInvalid, "csv")}{Environment.NewLine}{Path.GetFullPath(filePath)}");
            }
            nameCols = (nameCols >= 3) ? 3 : 1; //修正名称列数
            addressCols = (addressCols >= 3) ? 3 : 1; //修正地址列数
            try
            {
                DataTable dt = new()
                {
                    Namespace = "EtsGa",
                };
                dt.Columns.Add("Name", "GroupName", typeof(string));
                dt.Columns.Add("Address", "Address", typeof(GroupAddress));
                dt.Columns.Add("Desc", "Description", typeof(string));
                dt.Columns.Add("DPT", "DatapointType", typeof(DptBase));


                FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //新建文件流
                using (StreamReader sr = new(fs))
                {
                    if (hasHeader) sr.ReadLine(); //读出标题行
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine()?.Replace("\"", null); //读一行
                        if (line is null) continue; //跳过空行
                        string[] texts = line.Split(separator); //分割为数组
                        if (texts.Length != nameCols + addressCols + 5) continue; //跳过列数不对的行
                        if (string.IsNullOrWhiteSpace(texts[^2])) continue; //跳过没有数据类型的行
                        DataRow row = dt.NewRow(); //新建一行
                        row["DPT"] = GetDpt(texts[^2]); //倒数第二列为数据类型
                        row["Desc"] = texts[^3]; //倒数第三列为描述
                        row["Name"] = texts[nameCols - 1]; //组地址名
                        string addressText; //地址字符串
                        if (addressCols == 3)
                        {
                            addressText = string.Join(string.Empty, texts.Skip(nameCols).Take(3));
                        }
                        else
                        {
                            addressText = texts[nameCols];
                        }
                        row["Address"] = new GroupAddress(addressText); //组地址
                        dt.Rows.Add(row); //添加一行
                    }
                    DataTable dtt = dt;
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 从DPT字符串获取DPT类型
        /// "DPT-{主类型}"或"DPST-{主类型}-{子类型}"
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static DptBase GetDpt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return DptFactory.Default.Get(DatapointTypeNumber.None);
            string[] dptText = text.Split('-');
            if (dptText.Length < 2) return DptFactory.Default.Get(DatapointTypeNumber.None);
            int mainNum = Convert.ToInt32(dptText[1]);
            int subNum = (dptText[0] == "DPST") ? Convert.ToInt32(dptText[2]) : -1;
            return DptFactory.Default.Get(mainNum, subNum);
        }

    }

}
