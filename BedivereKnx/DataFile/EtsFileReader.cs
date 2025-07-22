using System.Data;
using Knx.Falcon;
using Knx.Falcon.ApplicationData.DatapointTypes;

namespace BedivereKnx.DataFile
{

    public partial class EtsFileReader
    {

        public string FilePath { get; private set; }

        public EtsGaFileType FileType { get; private set; }

        public EtsFileReader(string filePath, EtsGaFileType type)
        {
            //判断文件是否存在：
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{ResString.ExMsg_FileNotFound}{Environment.NewLine}{Path.GetFullPath(filePath)}", filePath);
            }

            FilePath = filePath;

            //验证文件扩展名：
            string ext = type.ToString();
            if (!Path.GetExtension(filePath).Equals($".{ext}", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException($"{string.Format(ResString.ExMsg_FileNotFound, ext)}{Environment.NewLine}{Path.GetFullPath(filePath)}");
            }
            FileType = type;
        }

        public DataTable ToMiddleTable()
        {
            return FileType switch
            {
                EtsGaFileType.CSV => ToMiddleTable_Csv(),
                EtsGaFileType.XML => throw new NotImplementedException(),
                EtsGaFileType.ESF => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        public DataTableCollection ToDataTableCollection()
        {
            return FileType switch
            {
                EtsGaFileType.CSV => ToDtCollection_Csv(),
                EtsGaFileType.XML => throw new NotImplementedException(),
                EtsGaFileType.ESF => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        //private DataTableCollection ToDtCollection(DataTable dt0)
        //{
        //    //初始化输出DataTable：
        //    DataTable dt = new();
        //    dt.Columns.Add("Code", typeof(string));
        //    dt.Columns.Add("Name", typeof(string));
        //    dt.Columns.Add("SwitchDpt", typeof(DptBase));
        //    dt.Columns.Add("SwitchCtlAddr", typeof(GroupAddress));
        //    dt.Columns.Add("SwitchFdbAddr", typeof(GroupAddress));
        //    dt.Columns.Add("ValueDpt", typeof(DptBase));
        //    dt.Columns.Add("ValueCtlAddr", typeof(GroupAddress));
        //    dt.Columns.Add("ValueFdbAddr", typeof(GroupAddress));
        //    dt.Columns.Add("SceneAddr", typeof(GroupAddress));

        //    //按对象名称分组：
        //    var groups = dt0.AsEnumerable()
        //                    .GroupBy(row => string.Join('.', row.Field<string>("Name")!.Split('.').SkipLast(1)));

        //    foreach (var group in groups)
        //    {
        //        if (string.IsNullOrWhiteSpace(group.Key)) continue; //跳过空的对象名
        //        DataRow dr = dt.NewRow();
        //        dr["Code"] = group.Key; //对象编号
        //        foreach (DataRow r in group)
        //        {
        //            string? desc = r.Field<string>("Desc"); //对象名称
        //            string part = r.Field<string>("Name")!.Split('.').Last(); //去除对象名的组地址名，即部件
        //            GroupAddress ga = r.Field<GroupAddress>("Address"); //组地址
        //            DptBase? dpt = r.Field<DptBase>("DPT"); //数据类型
        //            switch (part)
        //            {
        //                case "Ctl":
        //                case "SwCtl":
        //                case "Lock":
        //                case "En":
        //                    //开关控制：
        //                    dr["SwitchCtlAddr"] = ga;
        //                    if (dr["SwitchDpt"] is DBNull) dr["SwitchDpt"] = dpt;
        //                    dr["Name"] = desc;
        //                    break;
        //                case "Fdb":
        //                case "SwFdb":
        //                    //开关反馈：
        //                    dr["SwitchFdbAddr"] = ga;
        //                    dr["SwitchDpt"] = dpt;
        //                    if (dr["Name"] is DBNull) dr["Name"] = desc;
        //                    break;
        //                case "ValCtl":
        //                    //数值控制：
        //                    dr["ValueCtlAddr"] = ga;
        //                    if (dr["ValueDpt"] is DBNull) dr["ValueDpt"] = dpt;
        //                    dr["Name"] = desc;
        //                    break;
        //                case "ValFdb":
        //                    //数值反馈：
        //                    dr["ValueFdbAddr"] = ga;
        //                    dr["ValueDpt"] = dpt;
        //                    if (dr["Name"] is DBNull) dr["Name"] = desc;
        //                    break;
        //                case "Scn":
        //                case "ScnCtl":
        //                    //场景控制
        //                    dr["SceneAddr"] = ga;
        //                    dr["Name"] = desc;
        //                    break;
        //            }
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}


        private DataTable MiddleTableInit(string? tableName)
        {
            DataTable dt = new()
            {
                Namespace = tableName,
            };
            dt.Columns.Add("Name", "GroupName", typeof(string));
            dt.Columns.Add("Range", "GroupRange", typeof(string));
            dt.Columns.Add("Address", "Address", typeof(GroupAddress));
            dt.Columns.Add("Desc", "Description", typeof(string));
            dt.Columns.Add("DPT", "DatapointType", typeof(DptBase));
            return dt;
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

    public enum EtsGaFileType
    {
        CSV,
        XML,
        ESF
    }

}
