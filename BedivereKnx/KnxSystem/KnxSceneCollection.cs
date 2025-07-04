using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.KnxSystem
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
        private readonly Dictionary<int, KnxScene> Items = [];

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
                if (Items.TryGetValue(index, out KnxScene? scn))
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
                DataRow[] drs = Table.Select($"SceneCode='{code}'"); //在表中按照SceneCode查询
                if (drs.Length > 0)
                {
                    List<KnxScene> list = [];
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[dr.Field<int>("Id")]); //根据ID列搜索对象
                    }
                    return list.ToArray();
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
                DataRow[] drs = Table.Select($"GroupAddress='{address}'"); //找出组地址所属对象，可能有多个
                if (drs.Length > 0)
                {
                    List<KnxScene> list = [];
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[dr.Field<int>("Id")]); //根据ID列搜索对象
                    }
                    return list.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxScene", $"GroupAddress = {address}"));
                }
            }
        }

        /// <summary>
        /// 新建KNX场景集合
        /// </summary>
        public KnxSceneCollection()
        {
            Table = new DataTable();
        }

        /// <summary>
        /// 新建KNX场景集合
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <exception cref="NoNullAllowedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FormatException"></exception>
        public KnxSceneCollection(DataTable dt)
        {
            Table = dt;
            foreach (DataRow dr in Table.Rows)
            {
                int id = dr.Field<int>("Id"); //ID
                if (dr["SceneCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "SceneCode", $"ID={id}"));
                if (dr["GroupAddress"] is DBNull) //场景地址为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "Scene GroupAddress", $"ID={id}"));
                string scnCode = dr.Field<string>("SceneCode")!; //场景编号
                string? scnName = dr.Field<string>("SceneName"); //场景名称
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                GroupAddress ga = dr.Field<GroupAddress>("GroupAddress"); //场景组地址
                KnxScene scn = new(id, scnCode, scnName, ga, ifCode);
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
                scn.WriteRequest += _SceneControlRequest;
                Items.Add(scn.Id, scn); //字典内添加KnxScene对象
            }
        }

        /// <summary>
        /// 场景控制请求
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        private void _SceneControlRequest(KnxGroupEventArgs e, GroupValue value)
        {
            SceneControlRequest?.Invoke(e, value);
        }

        public IEnumerator<KnxScene> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
