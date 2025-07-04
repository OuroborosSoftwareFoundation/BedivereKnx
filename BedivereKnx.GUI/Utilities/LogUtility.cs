using System.Data;
using System.Text;

namespace BedivereKnx.GUI
{

    internal static class LogUtility
    {

        /// <summary>
        /// 写入csv日志文件
        /// </summary>
        /// <param name="dt">日志DataTable对象</param>
        /// <param name="path">日志路径</param>
        /// <param name="coding">编码，默认UTF-8</param>
        public static void WriteCsvLog(DataTable dt, string path, string coding = "utf-8")
        {
            StreamWriter sw = new(path, false, Encoding.GetEncoding(coding));
            //List<string> header = [];
            //foreach (DataColumn col in dt.Columns)
            //{
            //    header.Add(col.ColumnName); //每列的列名
            //}
            //sw.WriteLine(string.Join(',', header)); //写入标题行
            sw.WriteLine(dt.Columns.Cast<DataColumn>().Select(col => col.ColumnName)); //写入标题行
            foreach (DataRow dr in dt.Rows)
            {
                List<string> line = [];
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dr[dc] is DBNull) //单元格为空的情况
                    {
                        line.Add(string.Empty);
                    }
                    else
                    {
                        Type colType = dc.DataType;
                        if (colType.IsEnum) //DataTable某列为枚举的情况，要使用枚举名称。如果直接输出会显示数字
                        {
                            line.Add(colType.GetEnumName(dr[dc])!); //获取枚举值的名称
                        }
                        else if (colType == typeof(DateTime)) //时间类型的情况
                        {
                            line.Add($"{dr[dc]:yyyy-MM-dd HH:mm:ss}"); //带毫秒Excel打开无法正常显示
                        }
                        else //其他情况直接转字符串
                        {
                            line.Add(dr[dc].ToString()!);
                        }
                    }
                }
                sw.WriteLine(string.Join(','), line); //写入一行数据
            }
            sw.Flush(); //保存
            sw.Close(); //关闭
        }

    }

}
