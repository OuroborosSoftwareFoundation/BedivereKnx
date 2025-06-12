using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.KnxSystem
{

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
                    List<KnxScene> list = new();
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[(int)dr["Id"]]); //根据ID列搜索对象
                    }
                    return list.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxScene", $"SceneCode = {code}"));
                }
            }
        }

        public KnxScene[] this[GroupAddress address]
        {
            get
            {
                DataRow[] drs = Table.Select($"GroupAddress='{address}'"); //找出组地址所属对象，可能有多个
                if (drs.Length > 0)
                {
                    List<KnxScene> list = new();
                    foreach (DataRow dr in drs)
                    {
                        list.Add(this[(int)dr["Id"]]); //根据ID列搜索对象
                    }
                    return list.ToArray();
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "KnxScene", $"GroupAddress = {address}"));
                }
            }
        }

        public KnxSceneCollection()
        {
            Table = new DataTable();
        }

        public KnxSceneCollection(DataTable dt)
        {
            Table = dt;
            foreach (DataRow dr in Table.Rows)
            {
                string? code = dr["SceneCode"].ToString();
                if (!string.IsNullOrWhiteSpace(code))
                {
                    string? addressString = dr["GroupAddress"].ToString();
                    if (!string.IsNullOrWhiteSpace(addressString)) //判断场景组地址是否为空
                    {
                        GroupAddress address = new(dr["GroupAddress"].ToString()); //场景组地址
                        KnxScene scn = new((int)dr["Id"], code, dr["SceneName"].ToString(), address, dr["InterfaceCode"].ToString());
                        string[] valuePairs = Convertor.ToArray(dr["SceneValues"].ToString()); //场景值数组
                        foreach (string pairText in valuePairs) //遍历每个场景值
                        {
                            if (string.IsNullOrWhiteSpace(pairText)) continue; //跳过空项
                            string[] pair = Convertor.ToArray(pairText,'='); //{场景地址, 场景名}
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
                        Items.Add(scn.Id, scn);
                        scn.WriteRequest += _GroupWriteHandler;
                    }
                    else //场景地址为空
                    {
                        throw new NoNullAllowedException(string.Format(ResString.ExMsg_KnxSceneAddressNull, code));
                    }
                }
                else //场景编号为空
                {
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "SceneCode", $"Id={dr["Id"]}"));
                }
            }
        }

        private void _GroupWriteHandler(KnxGroupEventArgs e, GroupValue value)
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
