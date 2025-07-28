namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX区域节点
    /// </summary>
    public class AreaNode
    {

        /// <summary>
        /// 区域ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string PartCode { get; internal set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// 完整编号（主区域.中区域.子区域）
        /// </summary>
        public string FullCode { get; internal set; }

        /// <summary>
        /// 上级区域
        /// </summary>
        public AreaNode? ParentArea { get; internal set; }

        /// <summary>
        /// 下级区域
        /// </summary>
        public List<AreaNode> ChildrenAreas { get; } = [];

        /// <summary>
        /// 是否包含下级区域
        /// </summary>
        public bool HasChildren => ChildrenAreas.Count > 0;

        /// <summary>
        /// 区域级别（主区域=1, 中区域=2, 子区域=3）
        /// </summary>
        public int Level { get; internal set; }

        /// <summary>
        /// 新建区域节点
        /// </summary>
        /// <param name="id">区域ID</param>
        /// <param name="code">区域编号</param>
        /// <param name="name">区域名称</param>
        /// <param name="fullCode">完整区域编号</param>
        /// <exception cref="Exception"></exception>
        public AreaNode(int id, string code, string? name, string fullCode)
        {
            Id = id;
            PartCode = code;
            Name = name;
            if (fullCode.EndsWith(code)) //判断完整编号末尾是否为区域编号
            {
                FullCode = fullCode;
            }
            else
            {
                throw new Exception($"'{code} is not belong to AreaCode'{fullCode}' (ID={id}).");
            }
        }

        /// <summary>
        /// 设置上级节点
        /// </summary>
        /// <param name="parent"></param>
        internal void SetParent(AreaNode parent)
        {
            parent.AddChildren(this);
        }

        /// <summary>
        /// 添加子区域
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private AreaNode AddChildren(AreaNode child)
        {
            child.ParentArea = this;
            ChildrenAreas.Add(child);
            return child;
        }

        /// <summary>
        /// 添加子区域
        /// </summary>
        /// <param name="code">区域编号</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        private AreaNode AddChildren(int id, string code, string name)
        {
            AreaNode child = new(id, code, name, $"{FullCode}.{code}");
            return AddChildren(child);
        }

        /// <summary>
        /// 移除下级区域
        /// </summary>
        /// <param name="child"></param>
        private void RemoveChild(AreaNode child)
        {
            if (child is not null && ChildrenAreas.Contains(child))
            {
                ChildrenAreas.Remove(child);
            }
        }

        /// <summary>
        /// 清空下级区域
        /// </summary>
        private void ClearChildren()
        {
            ChildrenAreas.Clear();
        }

    }

}
