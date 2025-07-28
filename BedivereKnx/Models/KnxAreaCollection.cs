using System.Collections;
using System.Data;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX区域集合
    /// </summary>
    public class KnxAreaCollection : IEnumerable<AreaNode>
    {

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<int, AreaNode> items = [];

        /// <summary>
        /// 数据表
        /// </summary>
        public readonly DataTable Table;

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// 主区域
        /// </summary>
        public AreaNode[] MainAreas
        {
            get
            {
                return items.Values.Where(a => a.Level == 1).ToArray();
            }
        }

        /// <summary>
        /// 新建KNX区域集合
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <exception cref="NoNullAllowedException"></exception>
        public KnxAreaCollection(DataTable dt)
        {
            Table = dt;
            foreach (DataRow dr in Table.Rows)
            {
                int id = dr.Field<int>("Id"); //区域ID
                if (dr["AreaCode"] is DBNull) //编号为空的情况报错
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "AreaCode", $"ID={id}"));
                string areaCode = dr.Field<string>("AreaCode")!; //主区域编号
                string mainCode = dr.Field<string>("MainAreaCode")!; //主区域编号
                string? mainName = dr.Field<string>("MainAreaName"); //主区域名称
                string? middleCode = dr.Field<string>("MiddleAreaCode"); //中区域编号
                string? middleName = dr.Field<string>("MiddleAreaName"); //中区域名称
                string? subCode = dr.Field<string>("SubAreaCode"); //子区域编号
                string? subName = dr.Field<string>("SubAreaName"); //子区域名称
                AreaNode area;
                if (dr["SubAreaCode"] is not DBNull) //子区域
                {
                    area = new(id, subCode!, subName, areaCode)
                    {
                        Level = 3
                    };
                }
                else if (dr["MiddleAreaCode"] is not DBNull) //中区域
                {
                    area = new(id, middleCode!, middleName, areaCode)
                    {
                        Level = 2
                    };
                }
                else //主区域
                {
                    area = new(id, mainCode, mainName, areaCode)
                    {
                        Level = 1
                    };
                }
                items.Add(id, area); //字典中加入对象
            }
            foreach (AreaNode a in this) //遍历全部区域，准备设置上级区域
            {
                if (a.Level == 1) continue; //跳过主区域
                string pc = a.FullCode[0..a.FullCode.LastIndexOf('.')]; //上级区域编号
                a.SetParent(this[pc]); //设置上级区域
            }
        }

        /// <summary>
        /// 索引器（根据ID）
        /// </summary>
        /// <param name="index">对象ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public AreaNode this[int index]
        {
            get
            {
                if (items.TryGetValue(index, out AreaNode? area))
                {
                    return area;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "Area", $"ID = {index}"));
                }
            }
        }

        /// <summary>
        /// 索引器（按编号）
        /// </summary>
        /// <param name="code">区域编号</param>
        /// <returns></returns>
        public AreaNode this[string code]
        {
            get
            {
                List<AreaNode> list = items.Values.Where(a => a.FullCode == code).ToList(); //在字典中按照编号查询
                if (list.Count > 0)
                {
                    return list[0]; //返回第一项（正常情况只能找到一项）
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KeyNotFound, "Area", $"Code = {code}"));
                }
            }
        }

        /// <summary>
        /// 获取一级下的全部对象
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public AreaNode[] AreaAtLevel(int level)
        {
            return items.Values.Where(a => a.Level == level).ToArray();
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<AreaNode> GetEnumerator()
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
