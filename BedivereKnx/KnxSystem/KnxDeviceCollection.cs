using Knx.Falcon;
using System.Collections;
using System.Data;

namespace BedivereKnx.KnxSystem
{

    /// <summary>
    /// KNX设备集合
    /// </summary>
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
        /// 新建KNX设备集合
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <exception cref="NoNullAllowedException"></exception>
        public KnxDeviceCollection(DataTable dt)
        {
            Table = dt;
            Table.Columns.Add("Reachable", ResString.Hdr_Reachable, typeof(KnxDeviceState));
            foreach (DataRow dr in Table.Rows)
            {
                int id = dr.Field<int>("Id");
                if (dr["DeviceCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "DeviceCode", $"ID={id}"));
                IndividualAddress indAddress = dr.Field<IndividualAddress>("IndividualAddress"); //物理地址
                string devCode = dr.Field<string>("DeviceCode")!; //设备编号
                string? devName = dr.Field<string>("DeviceName"); //设备名称
                string? devMod = dr.Field<string>("DeviceModel"); //设备型号
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                string? areaCode = dr.Field<string>("AreaCode"); //接口编号
                KnxDeviceInfo kdi = new(id, devCode, devName, devMod, indAddress, ifCode, areaCode);
                kdi.DeviceStateChanged += Device_DeviceStateChanged;
                Items.Add(id, kdi); //字典中加入KnxDeviceInfo对象
            }
        }

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
        public KnxDeviceInfo[] this[string? ifCode]
        {
            get
            {
                var result = Items.Values.Where(dev => dev.InterfaceCode == ifCode);
                if (result.Any())
                {
                    return result.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxDevice", $"InterfaceCode = {ifCode}"));
                }
            }
            //if (string.IsNullOrWhiteSpace(ifCode))
            //{
            //    return Items.Values.ToArray(); //接口编号为空时返回全部设备
            //}
            //DataRow[] drs = Table.Select($"InterfaceCode = '{ifCode}'"); //在表中按照ObjectCode查询
            //if (drs.Length > 0)
            //{
            //    List<KnxDeviceInfo> list = [];
            //    foreach (DataRow dr in drs)
            //    {
            //        list.Add(this[dr.Field<int>("Id")]); //根据ID列搜索对象
            //    }
            //    return list.ToArray();
            //}
            //else
            //{
            //    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxDevice", $"InterfaceCode = {ifCode}"));
            //}
        }

        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="state"></param>
        private void Device_DeviceStateChanged(KnxDeviceInfo dev, KnxDeviceState state)
        {
            Table.Rows[dev.Id]["Reachable"] = state;
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KnxDeviceInfo> GetEnumerator()
        {
            return Items.Values.GetEnumerator();
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
