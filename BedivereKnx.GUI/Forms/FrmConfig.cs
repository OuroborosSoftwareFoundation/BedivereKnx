using BedivereKnx.GUI.Utilities;
using System.Net;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmConfig : Form
    {

        private readonly AppConfigManager cfg = Globals.AppConfig;
        //private bool pwdChanged = false;

        public FrmConfig()
        {
            InitializeComponent();
            tbDataFile.Text = cfg.DefaultDataFile;
            tbHmiFile.Text = cfg.DefaultHmiFile;
            tbLocalIp.Text = cfg.LocalIP?.ToString();
            chkInitRead.Checked = cfg.InitPolling;
            cbLanguage.Items.Add((Value: "en", Display: "English"));
            cbLanguage.Items.Add((Value: "zh", Display: "中文"));
            cbLanguage.ValueMember = "Value";
            cbLanguage.DisplayMember = "Display";
        }

        /// <summary>
        /// 选择数据文件
        /// </summary>
        private void btnOpenDataFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Title = Resources.Strings.Dlg_OpenDataFile,
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                Filter = "Excel file(*.xlsx)|*.xlsx",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbDataFile.Text = GetRelativePath(ofd.FileName);
            }
        }

        /// <summary>
        /// 选择图形文件
        /// </summary>
        private void btnOpenHmiFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Title = Resources.Strings.Dlg_OpenHmiFile,
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                Filter = "Draw.io Diagrams(*.drawio)|*.drawio",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbHmiFile.Text = GetRelativePath(ofd.FileName);
            }
        }

        /// <summary>
        /// 选择KNX路由本地IP
        /// </summary>
        private void btnLocalIpSel_Click(object sender, EventArgs e)
        {
            FrmNetworkInfo frmNetworkInfo = new();
            if (frmNetworkInfo.ShowDialog() == DialogResult.OK)
            {
                tbLocalIp.Text = frmNetworkInfo.SelectedIp?.ToString();
            }
        }

        /// <summary>
        /// 修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPwdChange_Click(object sender, EventArgs e)
        {
            if (cfg.HasLoginPwd && string.IsNullOrWhiteSpace(tbPwdCur.Text)) //有密码但是没输入的情况
            {
                MessageBox.Show(Resources.Strings.Msg_PwdCurError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((!cfg.HasLoginPwd && string.IsNullOrEmpty(tbPwdCur.Text)) || PasswordUtilitity.PasswordCheck(tbPwdCur.Text)) //无密码或当前密码正确
            {
                if (tbPwdNew.Text == tbPwdRep.Text) //新密码两次输入相同
                {
                    DialogResult result = MessageBox.Show(Resources.Strings.Msg_PwdChangeConfirm, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        cfg.LoginPwd = PasswordUtilitity.ToEncryptedPassword(tbPwdNew.Text);
                        cfg.SaveOne("LoginPwd", cfg.LoginPwd);
                        //pwdChanged = true;
                        MessageBox.Show(Resources.Strings.Msg_PwdChangeSuccess, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else //新密码两次输入不同
                {
                    MessageBox.Show(Resources.Strings.Msg_PwdNewDiff, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //当前密码输入不正确
            {
                MessageBox.Show(Resources.Strings.Msg_PwdCurError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            cfg.DefaultDataFile = tbDataFile.Text;
            cfg.DefaultHmiFile = tbHmiFile.Text;
            cfg.LocalIP = IPAddress.Parse(tbLocalIp.Text); //此处IP必然转换成功
            cfg.InitPolling = chkInitRead.Checked;
            //if (pwdChanged) cfg.LoginPwd = PasswordUtilitity.ToEncryptedPassword(tbPwdNew.Text);
            //pwdChanged = false;
            cfg.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static string GetRelativePath(string fullPath)
        {
            string basePath = Application.StartupPath;
            if (fullPath.StartsWith(basePath))
            {
                return fullPath[basePath.Length..];
            }
            else
            {
                return fullPath;
            }
            //以下方法会把全部路径都转为相对路径
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //Uri baseUri = new(basePath);
            //Uri fullUri = new(fullPath);

            //return Uri.UnescapeDataString(
            //    baseUri.MakeRelativeUri(fullUri).ToString()
            //    .Replace('/', Path.DirectorySeparatorChar));
        }

    }

}
