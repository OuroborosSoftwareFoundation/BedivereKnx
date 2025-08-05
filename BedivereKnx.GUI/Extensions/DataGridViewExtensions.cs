using System.Data;
using System.Resources;

namespace BedivereKnx.GUI
{

    internal static class DataGridViewExtensions
    {

        /// <summary>
        /// 将DataTable绑定到DGV数据源
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="dt">DataTable对象</param>
        /// <param name="hiddenCols">需要隐藏的列</param>
        internal static void BindDataTable(this DataGridView dgv, DataTable dt, string[] hiddenCols)
        {
            if ((dt is null) || (dt.Rows.Count == 0)) return;
            dgv.DataSource = dt;
            dgv.ClearSelection(); //取消选定
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string colName = col.Name;
                if (dt.Columns.Contains(colName))
                {
                    col.HeaderText = dt.Columns[colName]!.Caption; //设置列标名
                    if (hiddenCols.Contains(colName)) col.Visible = false; //隐藏不需要显示的列
                }
            }
        }

        /// <summary>
        /// 从资源文件获取列的语言化名称
        /// </summary>
        /// <param name="dgv"></param>
        internal static void GetLocalizableHeader(this DataGridView dgv)
        {
            ResourceManager resource = new(typeof(Resources.Strings)); //字符串资源管理器
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string? hdr = resource.GetString($"Hdr_{col.Name}"); //根据列名获取本地字符串
                if (!string.IsNullOrWhiteSpace(hdr))
                {
                    col.HeaderText = hdr;
                }
            }
        }

    }

}
