using Ouroboros.AuthManager.Eos;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmAuth : Form
    {

        private readonly AuthInfo? auth = Globals.AuthInfo;

        public FrmAuth()
        {
            if (auth is null) //非法途径绕开授权机制的情况
            {
                MessageBox.Show("Illegal bypass of authorization mechanism!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-5);
            }
            InitializeComponent();
            lvAuth.Items.Clear();
            lvAuth.Items.Add("Text", "Name", 0);
            lvAuth.Items.Add("ProductName", "ProductName", 1);
            lvAuth.Items.Add("Version", "Version", 2);
            lvAuth.Items.Add("ExpiryDate", "Expiration Date", 3);
            lvAuth.Items["Text"]!.SubItems.Add(auth.Title);
            lvAuth.Items["ProductName"]!.SubItems.Add(auth.ProductName);
            lvAuth.Items["Version"]!.SubItems.Add(auth.Version.ToString());
            if (auth.ExpiryDate.Date < DateTime.MaxValue.Date)
            {
                lvAuth.Items["ExpiryDate"]!.SubItems.Add("(Hidden)");
            }
            else
            {
                lvAuth.Items["ExpiryDate"]!.SubItems.Add("Permanent");
            }
            lvAuth.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void lvAuth_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((lvAuth.SelectedItems is null) || lvAuth.SelectedItems.Count == 0) return;
            if (e.Button == MouseButtons.Left)
            {
                if (lvAuth.SelectedItems[0].Name == "ExpiryDate")
                {
                    MessageBox.Show($"Expiration Date: {auth!.ExpiryDate:d}", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
