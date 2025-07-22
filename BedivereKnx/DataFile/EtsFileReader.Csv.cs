using System.Data;
using Knx.Falcon;

namespace BedivereKnx.DataFile
{

    public partial class EtsFileReader
    {

        private DataTable ToMiddleTable_Csv(char separator = ',')
        {
            FileType = EtsGaFileType.CSV;
            //验证文件扩展名：
            string extension = Path.GetExtension(FilePath).ToLower();
            if (extension != ".csv")
            {
                throw new ArgumentException($"{string.Format(ResString.ExMsg_FileNotFound, "csv")}{Environment.NewLine}{Path.GetFullPath(FilePath)}");
            }
            try
            {
                DataTable dt = MiddleTableInit("EtsGaFile_Csv"); //初始化DataTable
                FileStream fs = new(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //新建文件流
                StreamReader sr = new(fs);
                GetCsvFormat(sr.ReadLine(), separator, out int nameCols, out int addressCols); //解析标题行
                string[] grp = new string[2];
                while (!sr.EndOfStream) //从第2行读到最后
                {
                    string? line = sr.ReadLine()?.Replace("\"", null); //读一行
                    if (line is null) continue; //跳过空行
                    string[] texts = line.Split(separator); //分割为数组
                    if (texts.Length != nameCols + addressCols + 5) continue; //跳过列数不对的行（正常情况不会出现）
                    //if (string.IsNullOrWhiteSpace(texts[^2])) continue; //跳过没有数据类型的行

                    string? addressText = null; //地址字符串
                    if (addressCols == 3)
                    {
                        string[] addr1Arry = texts.Skip(nameCols).Take(3).ToArray();
                        switch (addr1Arry.Count(s => string.IsNullOrWhiteSpace(s))) //根据数组里的空项数量判断
                        {
                            case 0: //数组全满，是组地址
                                addressText = string.Join('/', addr1Arry);
                                break;
                            case 1: //有1个空项，是中间组名
                                grp[1] = nameCols == 1 ? texts[0] : texts[1];
                                break;
                            case 2: //有2个空项，是主组名
                                grp[0] = texts[0];
                                break;
                            default: //其他情况不可能存在
                                continue;
                        }
                    }
                    else
                    {
                        string addr3 = texts[nameCols];
                        switch (addr3.Count(c => c == '-')) //根据地址文本里-的数量判断
                        {
                            case 0: //不存在-，是组地址
                                addressText = addr3;
                                break;
                            case 1: //存在1个-，是中间组名
                                grp[1] = nameCols == 1 ? texts[0] : texts[1];
                                break;
                            case 2: //存在2个-，是主组名
                                grp[0] = texts[0];
                                break;
                            default: //其他情况不可能存在
                                continue;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(addressText))
                    {
                        DataRow row = dt.NewRow(); //新建一行
                        row["Range"] = $"{grp[0]}.{grp[1]}";
                        row["Address"] = new GroupAddress(addressText); //组地址
                        row["DPT"] = GetDpt(texts[^2]); //倒数第二列为数据类型
                        row["Desc"] = texts[^3]; //倒数第三列为描述
                        row["Name"] = texts[nameCols - 1]; //组地址名
                        dt.Rows.Add(row); //添加一行
                    }
                }
                DataTable dtt = dt;
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 解析CSV组地址文件名称列数和地址列数
        /// </summary>
        /// <param name="header">标题行</param>
        /// <param name="nameCols">名称列数（1-Group name, 3-Main|Middle|Sub）</param>
        /// <param name="addressCols">地址列数（1-Address, 3-Main|Middle|Sub）</param>
        /// <returns></returns>
        private void GetCsvFormat(string? header, char separator, out int nameCols, out int addressCols)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                throw new ArgumentNullException(nameof(header), "Csv hearder line is empty.");
            }
            string[] hdrArry = header.ToLower().Replace("\"", null).Split(separator); //读出标题行
            //以下判断假设为ETS转出的原文件，对多列判断不严谨：
            if (hdrArry.First() == "group name") //单名称列
            {
                nameCols = 1;
                if (hdrArry[1] == "address") //单地址列
                {
                    addressCols = 1;
                }
                else //多地址列
                {
                    addressCols = 3;
                }
            }
            else //多名称列
            {
                nameCols = 3;
                if (hdrArry[3] == "address") //单地址列
                {
                    addressCols = 1;
                }
                else //多地址列
                {
                    addressCols = 3;
                }
            }
        }

        private DataTableCollection ToDtCollection_Csv()
        {
            DataTable middleDt = ToMiddleTable_Csv();


            throw new NotImplementedException();
        }

    }

}

//public DataTable FromEtsGaCsv(string filePath, int nameCols, int addressCols, bool hasHeader = true, char separator = ',')
//{
//    FileType = EtsGaFileType.CSV;
//    //验证文件扩展名：
//    string extension = Path.GetExtension(filePath).ToLower();
//    if (extension != ".csv")
//    {
//        throw new ArgumentException($"{string.Format(ResString.ExMsg_FileNotFound, "csv")}{Environment.NewLine}{Path.GetFullPath(filePath)}");
//    }
//    nameCols = (nameCols >= 3) ? 3 : 1; //修正名称列数
//    addressCols = (addressCols >= 3) ? 3 : 1; //修正地址列数
//    try
//    {
//        DataTable dt = new()
//        {
//            Namespace = "EtsGa",
//        };
//        dt.Columns.Add("Name", "GroupName", typeof(string));
//        dt.Columns.Add("Address", "Address", typeof(GroupAddress));
//        dt.Columns.Add("Desc", "Description", typeof(string));
//        dt.Columns.Add("DPT", "DatapointType", typeof(DptBase));

//        FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //新建文件流
//        using (StreamReader sr = new(fs))
//        {
//            if (hasHeader) sr.ReadLine(); //读出标题行
//            while (!sr.EndOfStream)
//            {
//                string? line = sr.ReadLine()?.Replace("\"", null); //读一行
//                if (line is null) continue; //跳过空行
//                string[] texts = line.Split(separator); //分割为数组
//                if (texts.Length != nameCols + addressCols + 5) continue; //跳过列数不对的行
//                if (string.IsNullOrWhiteSpace(texts[^2])) continue; //跳过没有数据类型的行
//                DataRow row = dt.NewRow(); //新建一行
//                row["DPT"] = GetDpt(texts[^2]); //倒数第二列为数据类型
//                row["Desc"] = texts[^3]; //倒数第三列为描述
//                row["Name"] = texts[nameCols - 1]; //组地址名
//                string addressText; //地址字符串
//                if (addressCols == 3)
//                {
//                    addressText = string.Join(string.Empty, texts.Skip(nameCols).Take(3));
//                }
//                else
//                {
//                    addressText = texts[nameCols];
//                }
//                row["Address"] = new GroupAddress(addressText); //组地址
//                dt.Rows.Add(row); //添加一行
//            }
//            DataTable dtt = dt;
//            return dt;
//        }
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//}
