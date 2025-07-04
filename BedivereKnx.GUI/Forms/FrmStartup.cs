namespace BedivereKnx.GUI.Forms
{
    public partial class FrmStartup : Form
    {

        public FrmStartup()
        {
            InitializeComponent();
            Text = Globals.AssemblyInfo.ProductName;
            lblProdName.Text = Globals.AssemblyInfo.ProductName;
            lblVersion.Text = $"Ver {Globals.AssemblyInfo.Version}";
            lblCopyright.Text = $"{Globals.AssemblyInfo.CopyRight}.{Environment.NewLine}All rights reserved.";
            lblAuth.Text = Globals.AuthInfo!.Title;
        }

    }

}
