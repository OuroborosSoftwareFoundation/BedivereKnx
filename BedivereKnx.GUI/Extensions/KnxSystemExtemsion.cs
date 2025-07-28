using BedivereKnx.Models;

namespace BedivereKnx.GUI
{

    internal static class KnxSystemExtemsion
    {

        /// <summary>
        /// 转为TreeNode
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        internal static TreeNode ToTreeNode(this AreaNode area)
        {
            TreeNode treeNode = new()
            {
                Name = area.FullCode,
                Text = area.Name,
            };
            return treeNode;
        }

        /// <summary>
        /// 转为TreeNode数组
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        internal static TreeNode[] ToTreeNodes(this IEnumerable<AreaNode> areas)
        {
            List<TreeNode> list = [];
            foreach (AreaNode area in areas)
            {
                list.Add(area.ToTreeNode());
            }
            return list.ToArray();
        }

    }

}
