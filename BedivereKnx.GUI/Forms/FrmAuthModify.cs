using Ouroboros.AuthManager.Eos;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmAuthModify : Form
    {

        public FrmAuthModify()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbInput.Text)) return;
            string input = tbInput.Text.Trim();
            if (chkModeNew.Checked) //全新授权
            {
                try
                {
                    AuthInfo authInfo = new(input);
                    if (authInfo.CreateFile())
                    {
                        MessageBox.Show(Resources.Strings.Msg_AuthSuccess, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (chkModUpdate.Checked) //授权升级
            {
                try
                {
                    if (Globals.AuthInfo!.Update(input))
                    {
                        MessageBox.Show(Resources.Strings.Msg_AuthSuccess, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //非法绕过模式选择的情况
            {
                Environment.Exit(-4);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmAuthModify_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(-4);
        }

    }

}
