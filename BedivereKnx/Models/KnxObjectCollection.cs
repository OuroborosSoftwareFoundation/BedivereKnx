using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX对象集合
    /// </summary>
    public class KnxObjectCollection : IEnumerable<KnxObject>
    {

        /// <summary>
        /// 组地址读取请求
        /// </summary>
        protected internal event GroupReadHandler? GroupReadRequest;

        /// <summary>
        /// 组地址写入请求
        /// </summary>
        protected internal event GroupWriteHandler? GroupWriteRequest;

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<int, KnxObject> items = [];

        /// <summary>
        /// 数据表
        /// </summary>
        private readonly DataTable Table;

        ///// <summary>
        ///// 灯光DataView
        ///// </summary>
        //public DataView Lights => new(Table, $"Type={(int)KnxObjectType.Light}", null, DataViewRowState.CurrentRows);



        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => items.Count;

        //public Dictionary<int, KnxLight> Lights =>
        //    items.Where(kv => kv.Value is KnxLight)
        //         .ToDictionary(kv => kv.Key, kv => (KnxLight)kv.Value);

        //public Dictionary<int, KnxEnablement> Enablements =>
        //    items.Where(kv => kv.Value is KnxEnablement)
        //         .ToDictionary(kv => kv.Key, kv => (KnxEnablement)kv.Value);

        //public Dictionary<int, KnxScene> Scenes =>
        //    items.Where(kv => kv.Value is KnxScene)
        //         .ToDictionary(kv => kv.Key, kv => (KnxScene)kv.Value);

        //public Dictionary<Type, Dictionary<int, KnxObject>> TypedDictionary =>
        //    items.GroupBy(kv => kv.Value.GetType())
        //         .ToDictionary(
        //            group => group.Key,
        //            group => group.ToDictionary(kv => kv.Key, kv => kv.Value)
        //         );

        ///// <summary>
        ///// 新建KNX对象集合
        ///// </summary>
        //public KnxObjectCollection()
        //{
        //    Table = new DataTable();
        //    Table.Columns.Add("SwitchFdbValue", ResString.Hdr_SwFdbValue, typeof(byte)); //附加开关反馈状态列
        //    Table.Columns.Add("ValueFdbValue", ResString.Hdr_ValFdbValue, typeof(decimal)); //附加数值反馈状态列
        //}

        /// <summary>
        /// 新建KNX对象集合
        /// </summary>
        /// <param name="dt">数据表</param>
        public KnxObjectCollection(DataTable dt)
        {
            //Table = dt;
            //Table.Columns.Add("SwitchFdbValue", ResString.Hdr_SwFdbValue, typeof(byte)); //附加开关反馈状态列
            //Table.Columns.Add("ValueFdbValue", ResString.Hdr_ValFdbValue, typeof(decimal)); //附加数值反馈状态列
            Table = TableInit();
            foreach (DataRow dr in dt.Rows)
            {
                int id = dr.Field<int>("Id"); //ID
                if (dr["ObjectCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "ObjectCode", $"ID={id}"));
                string objCode = dr.Field<string>("ObjectCode")!; //对象编号
                string? objName = dr.Field<string>("ObjectName"); //对象名称
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                string? areaCode = dr.Field<string>("AreaCode"); //区域编号
                KnxObjectType objType = dr.Field<KnxObjectType>("ObjectType"); //对象类型
                //KnxObject obj; // = new(objType, id, objCode, objName, ifCode, areaCode);
                var obj = KnxObject.NewObject(objType, id, objCode, objName, ifCode, areaCode);
                //组地址:
                string? swDpt = dr.Field<string>("SwitchDpt"); //开关DPT
                //开关控制
                if (dr["SwitchCtlAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.SwitchControl] = new(dr.Field<GroupAddress>("SwitchCtlAddr"), swDpt);
                }
                //开关反馈
                if (dr["SwitchFdbAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.SwitchFeedback] = new(dr.Field<GroupAddress>("SwitchFdbAddr"), swDpt);
                }
                string? valDpt = dr.Field<string>("ValueDpt"); //数值DPT
                //数值控制
                if (dr["ValueCtlAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.ValueControl] = new(dr.Field<GroupAddress>("ValueCtlAddr"), valDpt);
                }
                //数值反馈
                if (dr["ValueFdbAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.ValueFeedback] = new(dr.Field<GroupAddress>("ValueFdbAddr"), valDpt);
                }
                obj.ReadRequest += OnGroupReadRequest;
                obj.WriteRequest += OnGroupWriteRequest;
                items.Add(obj.Id, obj); //字典内添加KnxObject对象
                //数据表：
                DataRow row = RowInit(Table, obj);
                switch (objType)
                {
                    case KnxObjectType.Light:
                    case KnxObjectType.Curtain:
                    case KnxObjectType.Value:
                        if (obj.ContainsPart(KnxObjectPart.SwitchFeedback))
                        {
                            row["SwFdb"] = obj[KnxObjectPart.SwitchFeedback];
                        }
                        if (obj.ContainsPart(KnxObjectPart.ValueFeedback))
                        {
                            row["ValFdb"] = obj[KnxObjectPart.ValueFeedback];
                        }
                        break;
                    case KnxObjectType.Enablement:
                        row["SwFdb"] = obj[KnxObjectPart.SwitchControl];
                        row["ValFdb"] = null;
                        break;
                    default:
                        throw new Exception(string.Format(ResString.ExMsg_KnxObjectTypeInvalid, objType.ToString()));
                }
                Table.Rows.Add(row);
            }
        }

        private static DataTable TableInit()
        {
            DataTable dt = new();
            dt.Columns.Add("Type", ResString.Hdr_Type, typeof(KnxObjectType));
            dt.Columns.Add("AreaCode", ResString.Hdr_AreaCode, typeof(string));
            dt.Columns.Add("InterfaceCode", ResString.Hdr_InterfaceCode, typeof(string));
            dt.Columns.Add("Id", ResString.Hdr_Id, typeof(int));
            dt.Columns.Add("Code", ResString.Hdr_Code, typeof(string));
            dt.Columns.Add("Name", ResString.Hdr_Name, typeof(string));
            dt.Columns.Add("SwFdb", ResString.Hdr_SwFdb, typeof(KnxGroup));
            dt.Columns.Add("ValFdb", ResString.Hdr_ValFdb, typeof(KnxGroup));
            return dt;
        }

        private static DataRow RowInit(DataTable dt, KnxObject obj)
        {
            DataRow dr = dt.NewRow();
            dr["Type"] = obj.Type;
            dr["AreaCode"] = obj.AreaCode;
            dr["InterfaceCode"] = obj.InterfaceCode;
            dr["Id"] = obj.Id;
            dr["Code"] = obj.Code;
            dr["Name"] = obj.Name;
            return dr;
        }

        ///// <summary>
        ///// 将场景数据表加入
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <exception cref="Exception"></exception>
        ///// <exception cref="NoNullAllowedException"></exception>
        ///// <exception cref="ArgumentOutOfRangeException"></exception>
        ///// <exception cref="FormatException"></exception>
        //internal void AddSceneTable(DataTable dt)
        //{
        //    if (!dt.TableName.Equals("scenes", StringComparison.CurrentCultureIgnoreCase))
        //        throw new Exception("Invalid scene datatable.");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        int id = 0 - dr.Field<int>("Id"); //ID为负数
        //        if (dr["SceneCode"] is DBNull) //编号为空的情况报错
        //            throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "SceneCode", $"ID={id}"));
        //        if (dr["GroupAddress"] is DBNull) //场景地址为空的情况报错
        //            throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "Scene GroupAddress", $"ID={id}"));
        //        string scnCode = dr.Field<string>("SceneCode")!; //场景编号
        //        string? scnName = dr.Field<string>("SceneName"); //场景名称
        //        string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
        //        string? areaCode = dr.Field<string>("AreaCode"); //区域编号
        //        GroupAddress ga = dr.Field<GroupAddress>("GroupAddress"); //场景组地址
        //        KnxScene scn = new(id, scnCode, scnName, ifCode, areaCode);
        //        scn[KnxObjectPart.SceneControl] = new(dr.Field<GroupAddress>("GroupAddress"), [18, 1]); //场景组地址
        //        //SceneAddress = dr.Field<GroupAddress>("GroupAddress"), //场景组地址
        //        string[] valuePairs = Convertor.ToArray(dr.Field<string>("SceneValues"), ','); //场景值数组
        //        foreach (string pairText in valuePairs) //遍历每个场景值
        //        {
        //            if (string.IsNullOrWhiteSpace(pairText)) continue; //跳过空项
        //            string[] pair = Convertor.ToArray(pairText, '='); //{场景地址, 场景名}
        //            if (byte.TryParse(pair[0], out byte num)) //场景编号是数字
        //            {
        //                if (num < 64) //0~63的情况
        //                {
        //                    scn.Names[num] = pair[1].Trim(); //设置对应场景号的名称
        //                }
        //                else //场景号超过大于的情况
        //                {
        //                    throw new ArgumentOutOfRangeException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, num));
        //                }
        //            }
        //            else //场景号不是数字
        //            {
        //                throw new FormatException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, pair[0]));
        //            }
        //        }
        //        scn.ReadRequest += OnGroupReadRequest;
        //        scn.WriteRequest += OnGroupWriteRequest;
        //        items.Add(id, scn); //场景ID为负数
        //    }
        //}

        /// <summary>
        /// 索引器（根据ID）
        /// </summary>
        /// <param name="index">对象ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxObject this[int index]
        {
            get
            {
                if (items.TryGetValue(index, out KnxObject? obj))
                {
                    return obj;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxObject", $"ID = {index}"));
                }
            }
        }

        /// <summary>
        /// 索引器（根据编号）
        /// </summary>
        /// <param name="code">对象编号</param>
        /// <returns></returns>
        public KnxObject[] this[string code]
        {
            get
            {
                var result = items.Values.Where(obj => obj.Code == code);
                if (result.Any())
                {
                    return result.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxObject", $"ObjectCode = {code}"));
                }
            }
        }

        /// <summary>
        /// 索引器（根据编号数组）
        /// </summary>
        /// <param name="codes">对象编号的数组</param>
        /// <returns></returns>
        public KnxObject[] this[string[] codes]
        {
            get
            {
                List<KnxObject> list = [];
                foreach (string code in codes)
                {
                    list.AddRange(this[code]);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// 索引器（根据对象类型）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataView this[KnxObjectType type]
        {
            get
            {
                return new(Table, $"Type={(int)type}", null, DataViewRowState.CurrentRows);
            }
        }

        ///// <summary>
        ///// 索引器（根据组地址和成员）
        ///// </summary>
        ///// <param name="address">组地址</param>
        ///// <param name="partString">SwCtl,SwFdb,ValCtl,ValFdb</param>
        ///// <returns></returns>
        ///// <exception cref="KeyNotFoundException"></exception>
        //public KnxObject[] this[GroupAddress address, string partString]
        //{
        //    get
        //    {
        //        DataRow[] drs = Table.Select($"{partString}GrpAddr = '{address}'");
        //        if (drs.Length > 0)
        //        {
        //            List<KnxObject> list = [];
        //            foreach (DataRow dr in drs)
        //            {
        //                list.Add(this[dr.Field<int>("Id")]);
        //            }
        //            return list.ToArray();
        //        }
        //        else
        //        {
        //            throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxObject", $"[{partString}] GroupAddress = {address}"));
        //        }
        //    }
        //}

        /// <summary>
        /// 根据ID查找特定类型的对象
        /// </summary>
        /// <typeparam name="T">查找的对象类型</typeparam>
        /// <param name="index">对象ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public T Get<T>(int index) where T : KnxObject
        {
            if (items.TryGetValue(index, out KnxObject? obj) && obj is T typedObj)
            {
                return typedObj;
            }
            else
            {
                throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, typeof(T).ToString(), $"ID = {Math.Abs(index)}"));
            }
        }

        /// <summary>
        /// 根据编号查找特定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code">对象编号</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public T[] Get<T>(string code) where T : KnxObject
        {
            var result = items.Values.OfType<T>().Where(obj => obj.Code == code);
            if (result.Any())
            {
                return result.ToArray();
            }
            else
            {
                throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, nameof(T), $"ObjectCode = {code}"));
            }
        }

        /// <summary>
        /// 接收报文事件，由KnxSystem中的_GroupMessageReceived触发
        /// 接收报文并将其写入包含的KnxGroup对象中
        /// </summary>
        /// <param name="groupAddress"></param>
        /// <param name="groupValue"></param>
        public void ReceiveGroupMessage(GroupAddress groupAddress, GroupValue groupValue)
        {
            KnxObjectPart[] parts = [KnxObjectPart.SwitchFeedback, KnxObjectPart.ValueFeedback];
            var groups = items.Values.SelectMany(obj => parts.Where(p => obj.ContainsPart(p))
                                                    .Select(p => obj[p]))
                            .Where(g => g.Address == groupAddress); //查出包含组地址的KnxGroup对象
            foreach (KnxGroup group in groups)
            {
                group.Value = groupValue; //设置KnxGroup对象中的值
            }

            //// 在各地址列中查找收到的组地址
            //var matches = from DataRow row in Table.AsEnumerable()
            //              from string col in AddressColumns
            //              where row[col].ToString() == groupAddress.ToString()
            //              select new
            //              {
            //                  id = row.Field<int>("Id"),
            //                  col
            //              };
            //foreach (var match in matches)
            //{
            //    KnxGroup group = items[match.id][match.col]; //获取
            //    group.Value = groupValue; //把对象中的KNX组写入值
            //    string colName = match.col.Replace("Addr", "Value"); //对应组地址值的列名
            //    if (Table.Columns.Contains(colName))
            //    {
            //        Table.Rows[match.id][colName] = groupValue.TypedValue; //更新表格中的值
            //    }
            //}
        }

        /// <summary>
        /// 组地址类型的列名
        /// </summary>
        private readonly string[] AddressColumns = ["SwitchCtlAddr", "SwitchFdbAddr", "ValueCtlAddr", "ValueFdbAddr"];

        /// <summary>
        /// KnxOject中的KnxGroup触发的读取请求传递至GroupReadRequest事件
        /// </summary>
        /// <param name="e"></param>
        private void OnGroupReadRequest(KnxGroupEventArgs e)
        {
            GroupReadRequest?.Invoke(e);
        }

        /// <summary>
        /// KnxOject中的KnxGroup触发的写入请求传递至GroupWriteRequest事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        private void OnGroupWriteRequest(KnxGroupEventArgs e, GroupValue value)
        {
            GroupWriteRequest?.Invoke(e, value);
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KnxObject> GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        /// <summary>
        /// 泛型枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
