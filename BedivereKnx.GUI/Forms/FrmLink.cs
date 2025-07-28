using BedivereKnx.GUI.Extensions;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmLink : Form
    {

        private readonly KnxSystem knx = Globals.KS!;

        public FrmLink()
        {
            InitializeComponent();
            dgvLink.BindDataTable(knx.Links,
                ["Id", "Account", "Password"]);
            //链接DataGridView的处理，添加账号信息按钮：
            //if (dgvLink.Rows.Count > 0)
            //{
            //    DataGridViewButtonColumn colBtn = new()
            //    {
            //        Name = "btnAccount",
            //        HeaderText = Resources.Strings.Hdr_Account,
            //        Text = Resources.Strings.Ui_BtnShow,
            //        UseColumnTextForButtonValue = true,
            //    };
            //    dgvLink.Columns.Add(colBtn);
            //}
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if ((dgvLink.CurrentRow is null) || dgvLink.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvLink.CurrentRow;
            CommonUtilities.OpenUrl(row.Cells["LinkUrl"].Value.ToString());
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        private void dgvLink_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOpen.PerformClick(); //调用按钮点击事件
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        private void btnInfo_Click(object sender, EventArgs e)
        {
            if ((dgvLink.CurrentRow is null) || dgvLink.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvLink.CurrentRow;
            MessageBox.Show($"Account: {row.Cells["Account"].Value}{Environment.NewLine}Password: {row.Cells["Password"].Value}", "Account Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

}
