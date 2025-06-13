using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.KnxSystem
{

    /// <summary>
    /// KNX对象集合
    /// </summary>
    public class KnxObjectCollection : IEnumerable<KnxObject>
    {

        /// <summary>
        /// 组地址类型的列名
        /// </summary>
        private readonly string[] AddressColumns = ["Sw_Ctl_GrpAddr", "Sw_Fdb_GrpAddr", "Val_Ctl_GrpAddr", "Val_Fdb_GrpAddr"];

        /// <summary>
        /// 组地址读取请求
        /// </summary>
        protected internal event GroupReadHandler? GroupReadRequest;

        /// <summary>
        /// 组地址写入请求
        /// </summary>
        protected internal event GroupWriteHandler? GroupWriteRequest;

        /// <summary>
        /// 数据表
        /// </summary>
        public readonly DataTable Table;

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<int, KnxObject> Items = [];

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
                if (Items.TryGetValue(index, out KnxObject? obj))
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
                DataRow[] drs = Table.Select($"ObjectCode='{code}'"); //在表中按照ObjectCode查询
                if (drs.Length > 0)
                {
                    List<KnxObject> list = [];
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[(int)dr["Id"]]); //根据ID列搜索对象
                    }
                    return list.ToArray();
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
        /// 索引器（根据组地址和成员）
        /// </summary>
        /// <param name="address">组地址</param>
        /// <param name="partString">Sw_Ctl,Sw_Fdb,Val_Ctl,Val_Fdb</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxObject[] this[KnxGroup address, string partString]
        {
            get
            {
                DataRow[] drs = Table.Select($"{partString}_GrpAddr = '{address}'");
                if (drs.Length > 0)
                {
                    List<KnxObject> list = [];
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[dr.Field<int>("Id")]);
                    }
                    return list.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxObject", $"[{partString}] GroupAddress = {address}"));
                }
            }
        }

        /// <summary>
        /// 新建KNX对象集合
        /// </summary>
        public KnxObjectCollection()
        {
            Table = new DataTable();
            Table.Columns.Add("Sw_Fdb_Value", ResString.DataCol_SwCtlValue, typeof(byte)); //附加开关反馈状态列
            Table.Columns.Add("Val_Fdb_Value", ResString.DataCol_ValFdbValue, typeof(decimal)); //附加数值反馈状态列
            //Table.Columns.Add(new DataColumn("Sw_Fdb_Value", typeof(byte)) //开关反馈值
            //{
            //    Caption = ResString.DataCol_SwCtlValue
            //});
            //Table.Columns.Add(new DataColumn("Val_Fdb_Value", typeof(decimal)) //数值反馈值
            //{
            //    Caption = ResString.DataCol_ValFdbValue
            //});
        }

        /// <summary>
        /// 新建KNX对象集合
        /// </summary>
        /// <param name="dt">数据表</param>
        public KnxObjectCollection(DataTable dt)
        {
            Table = dt;
            Table.Columns.Add("Sw_Fdb_Value", ResString.DataCol_SwCtlValue, typeof(byte)); //附加开关反馈状态列
            Table.Columns.Add("Val_Fdb_Value", ResString.DataCol_ValFdbValue, typeof(decimal)); //附加数值反馈状态列
            foreach (DataRow dr in Table.Rows)
            {
                int id = dr.Field<int>("Id"); //ID
                if (dr["ObjectCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "ObjectCode", $"ID={id}"));
                KnxObjectType objType = dr.Field<KnxObjectType>("ObjectType"); //对象类型
                string objCode = dr.Field<string>("ObjectCode")!; //对象编号
                string? objName = dr.Field<string>("ObjectName"); //对象名称
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                KnxObject obj = new(objType, id, objCode, objName, ifCode);
                //组地址
                string? swDpt = dr.Field<string>("Sw_GrpDpt"); //开关DPT
                //开关控制
                if (dr["Sw_Ctl_GrpAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.SwitchControl] = new(dr.Field<GroupAddress>("Sw_Ctl_GrpAddr"), swDpt);
                }
                //开关反馈
                if (dr["Sw_Fdb_GrpAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.SwitchFeedback] = new(dr.Field<GroupAddress>("Sw_Fdb_GrpAddr"), swDpt);
                }
                string? valDpt = dr.Field<string>("Val_GrpDpt"); //数值DPT
                //数值控制
                if (dr["Val_Ctl_GrpAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.ValueControl] = new(dr.Field<GroupAddress>("Val_Ctl_GrpAddr"), valDpt);
                }
                //数值反馈
                if (dr["Val_Fdb_GrpAddr"] is not DBNull)
                {
                    obj[KnxObjectPart.ValueFeedback] = new(dr.Field<GroupAddress>("Val_Fdb_GrpAddr"), valDpt);
                }
                obj.ReadRequest += _GroupReadRequest;
                obj.WriteRequest += _GroupWriteRequest;
                Items.Add(obj.Id, obj); //字典内添加KnxObject对象
            }
        }

        /// <summary>
        /// KnxOject中的KnxGroup触发的读取请求传递至KnxObjectCollection的事件
        /// </summary>
        /// <param name="e"></param>
        private void _GroupReadRequest(KnxGroupEventArgs e)
        {
            GroupReadRequest?.Invoke(e);
        }

        /// <summary>
        /// KnxOject中的KnxGroup触发的写入请求传递至KnxObjectCollection的事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _GroupWriteRequest(KnxGroupEventArgs e, GroupValue value)
        {
            GroupWriteRequest?.Invoke(e, value);
        }

        /// <summary>
        /// 接收报文事件，由KnxSystem中的_GroupMessageReceived触发
        /// 接收报文并将其写入包含的KnxGroup对象中
        /// </summary>
        /// <param name="groupAddress"></param>
        /// <param name="groupValue"></param>
        public void ReceiveGroupMessage(GroupAddress groupAddress, GroupValue groupValue)
        {
            // 在各地址列中查找收到的组地址
            var matches = from DataRow row in Table.AsEnumerable()
                          from string col in AddressColumns
                          where row[col].ToString() == groupAddress.ToString()
                          select new
                          {
                              id = row.Field<int>("Id"),
                              col
                          };
            foreach (var match in matches)
            {
                KnxGroup group = Items[match.id][match.col]; //获取
                group.Value = groupValue; //把对象中的KNX组写入值
                string colName = match.col.Replace("GrpAddr", "Value"); //对应组地址值的列名
                if (Table.Columns.Contains(colName))
                {
                    Table.Rows[match.id][colName] = groupValue.TypedValue; //更新表格中的值
                }
            }
        }

        public IEnumerator<KnxObject> GetEnumerator()
        {
            return Items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
