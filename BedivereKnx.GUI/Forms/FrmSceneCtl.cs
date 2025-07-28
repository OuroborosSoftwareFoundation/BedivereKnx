using BedivereKnx.Models;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmSceneCtl : Form
    {

        internal byte SelectedAddress; //传递的场景地址（0~63）
        internal bool IsLearn = false; //是否学习

        public FrmSceneCtl(KnxScene scene)
        {
            InitializeComponent();
            lblScnName.Text = $"{scene.Name}"; //场景名称
            KnxGroup group = scene[KnxObjectPart.SceneControl];
            lblScnAddr.Text = $"{group.Address}"; //场景地址
            lvScn.Clear(); //清空场景列表
            lvScn.Columns.Add("ScnAddr", Resources.Strings.Hdr_ScnAddress);
            lvScn.Columns.Add("ScnName", Resources.Strings.Hdr_ScnName);
            for (byte i = 0; i < scene.Names.Length; i++)
            {
                string? scnName = scene.Names[i]?.Trim();
                if (string.IsNullOrWhiteSpace(scnName)) continue; //跳过空项
                ListViewItem item = new(i.ToString())
                {
                    Tag = i,
                };
                item.SubItems.Add(scnName);
                lvScn.Items.Add(item);
            }
            lvScn.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// 场景学习选框
        /// </summary>
        private void chkLearn_CheckedChanged(object sender, EventArgs e)
        {
            IsLearn = chkLearn.Checked;
            chkLearn.BackColor = IsLearn ? Color.Red : SystemColors.Control;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvScn.SelectedItems.Count == 0) return;
            SelectedAddress = Convert.ToByte(lvScn.SelectedItems[0].Tag);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }

}
