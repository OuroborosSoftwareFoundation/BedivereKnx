using System.Data;

namespace BedivereKnx.GUI.Extensions
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

    }

}
