using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX场景集合
    /// </summary>
    public class KnxSceneCollection : IEnumerable<KnxScene>
    {

        /// <summary>
        /// 场景控制请求
        /// </summary>
        protected internal event GroupWriteHandler? SceneControlRequest;

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<int, KnxScene> items = [];

        /// <summary>
        /// 数据表
        /// </summary>
        public readonly DataTable Table;

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => items.Count;

        ///// <summary>
        ///// 新建KNX场景集合
        ///// </summary>
        //public KnxSceneCollection()
        //{
        //    Table = new DataTable();
        //}

        /// <summary>
        /// 新建KNX场景集合
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <exception cref="NoNullAllowedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public KnxSceneCollection(DataTable dt)
        {
            if (!dt.TableName.Equals("scenes", StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Invalid scene datatable.");
            Table = TableInit();
            foreach (DataRow dr in dt.Rows)
            {
                int id = dr.Field<int>("Id"); //ID
                if (dr["SceneCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "SceneCode", $"ID={id}"));
                if (dr["GroupAddress"] is DBNull) //场景地址为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "Scene GroupAddress", $"ID={id}"));
                string scnCode = dr.Field<string>("SceneCode")!; //场景编号
                string? scnName = dr.Field<string>("SceneName"); //场景名称
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                string? areaCode = dr.Field<string>("AreaCode"); //区域编号
                GroupAddress ga = dr.Field<GroupAddress>("GroupAddress"); //场景组地址
                KnxScene scn = new(id, scnCode, scnName, ifCode, areaCode);
                scn[KnxObjectPart.SceneControl] = new(dr.Field<GroupAddress>("GroupAddress"), 18, 1); //场景组地址
                string[] valuePairs = Convertor.ToArray(dr.Field<string>("SceneValues"), ','); //场景值数组
                foreach (string pairText in valuePairs) //遍历每个场景值
                {
                    if (string.IsNullOrWhiteSpace(pairText)) continue; //跳过空项
                    string[] pair = Convertor.ToArray(pairText, '='); //{场景地址, 场景名}
                    if (byte.TryParse(pair[0], out byte num)) //场景编号是数字
                    {
                        if (num < 64) //0~63的情况
                        {
                            scn.Names[num] = pair[1].Trim(); //设置对应场景号的名称
                        }
                        else //场景号超过大于的情况
                        {
                            throw new ArgumentOutOfRangeException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, num));
                        }
                    }
                    else //场景号不是数字
                    {
                        throw new FormatException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, pair[0]));
                    }
                }
                scn.WriteRequest += OnSceneControlRequest;
                items.Add(id, scn); //字典内添加KnxScene对象
                //数据表：
                DataRow row = Table.NewRow();
                row["Type"] = scn.Type;
                row["AreaCode"] = scn.AreaCode;
                row["InterfaceCode"] = scn.InterfaceCode;
                row["Id"] = scn.Id;
                row["Code"] = scn.Code;
                row["Name"] = scn.Name;
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
            return dt;
        }

        /// <summary>
        /// 索引器（根据ID）
        /// </summary>
        /// <param name="index">场景ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxScene this[int index]
        {
            get
            {
                if (items.TryGetValue(index, out KnxScene? scn))
                {
                    return scn;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "Scene", $"ID = {index}"));
                }
            }
        }

        /// <summary>
        /// 索引器（根据编号）
        /// </summary>
        /// <param name="code">场景编号</param>
        /// <returns></returns>
        public KnxScene[] this[string code]
        {
            get
            {
                var result = items.Values.Where(scn => scn.Code == code);
                if (result.Any())
                {
                    return result.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxScene", $"SceneCode = {code}"));
                }
            }
        }

        /// <summary>
        /// 索引器（根据组地址）
        /// </summary>
        /// <param name="address">组地址</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxScene[] this[GroupAddress address]
        {
            get
            {
                var result = items.Values.Where(scn => scn[KnxObjectPart.SceneControl].Address == address);
                if (result.Any())
                {
                    return result.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxScene", $"GroupAddress = {address}"));
                }
            }
        }

        /// <summary>
        /// KnxScene中的读取请求传递至SceneControlRequest事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        private void OnSceneControlRequest(KnxGroupEventArgs e, GroupValue value)
        {
            SceneControlRequest?.Invoke(e, value);
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KnxScene> GetEnumerator()
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
