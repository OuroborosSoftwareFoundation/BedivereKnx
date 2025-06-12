using Knx.Falcon;
using System.Collections;
using System.Data;

namespace BedivereKnx.KnxSystem
{

    public class KnxDeviceCollection : IEnumerable<KnxDeviceInfo>
    {

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
        private readonly Dictionary<int, KnxDeviceInfo> Items = [];

        /// <summary>
        /// 索引器（根据ID）
        /// </summary>
        /// <param name="index">对象ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxDeviceInfo this[int index]
        {
            get
            {
                if (Items.TryGetValue(index, out KnxDeviceInfo? dev))
                {
                    return dev;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxDevice", $"ID = {index}"));
                }
            }
        }

        /// <summary>
        /// 索引器（按接口编号）
        /// </summary>
        /// <param name="ifCode">接口编号</param>
        /// <returns></returns>
        public KnxDeviceInfo[] this[string ifCode]
        {
            get
            {
                DataRow[] drs = Table.Select($"InterfaceCode = '{ifCode}'"); //在表中按照ObjectCode查询
                if (drs.Length > 0)
                {
                    List<KnxDeviceInfo> list = [];
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[(int)dr["Id"]]); //根据ID列搜索对象
                    }
                    return list.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxDevice", $"InterfaceCode = {ifCode}"));
                }
            }
        }

        public KnxDeviceCollection(DataTable dt)
        {
            Table = dt;
            Table.Columns.Add(new DataColumn("Reachable", typeof(KnxDeviceState)) //
            {
                Caption = ResString.DataCol_Reachable
            });
            foreach (DataRow dr in Table.Rows)
            {
                int id = dr.Field<int>("Id");
                IndividualAddress indAddress = dr.Field<IndividualAddress>("IndividualAddress");//new(dr.Field<string>("IndividualAddress")); //物理地址
                string? code = dr.Field<string>("DeviceCode"); //设备编号
                if (string.IsNullOrWhiteSpace(code)) //设备编号为空的情况
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "DeviceCode", $"Id = {id}"));
                KnxDeviceInfo kdi = new(id, code, dr.Field<string>("DeviceName"), indAddress, dr.Field<string>("InterfaceCode"));
                kdi.DeviceStateChanged += _DeviceStateChanged; //设备状态变化事件
                Items.Add(id, kdi);
            }
        }

        private void _DeviceStateChanged(KnxDeviceInfo dev, KnxDeviceState state)
        {
            Table.Rows[dev.Id]["Reachable"] = state;
        }

        public IEnumerator<KnxDeviceInfo> GetEnumerator()
        {
            return Items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
