namespace BedivereKnx.GUI
{

    internal static class ListViewExtension
    {

        /// <summary>
        /// 检查ListView中是否包含组
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="groupName">ListViewGroup的名称</param>
        /// <returns></returns>
        public static bool ContainsGroup(this ListView lv, string groupName)
        {
            foreach (ListViewGroup group in lv.Groups)
            {
                if (group.Name == groupName) return true;
            }
            return false;
        }

    }

}
