using BedivereKnx.GUI.Utilities;
using System.Text.RegularExpressions;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmLogin : Form
    {

        private readonly string illegalRegex = @"[\s'\"";\\]";

        public FrmLogin()
        {
            InitializeComponent();
            this.Text = $"{Globals.AssemblyInfo.ProductName} {Text}";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string input = Regex.Replace(tbPwd.Text.Trim(), illegalRegex, "_");
            if (string.IsNullOrWhiteSpace(input)) return; //输入为空跳过
            if (PasswordUtilitity.PasswordCheck(input))
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(DialogResult == DialogResult.Yes || DialogResult == DialogResult.No))
            {
                e.Cancel = true;
            }
        }

    }

}
