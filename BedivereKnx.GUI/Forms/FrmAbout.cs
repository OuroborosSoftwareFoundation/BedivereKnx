namespace BedivereKnx.GUI.Forms
{

    public partial class FrmAbout : Form
    {

        private int picIndex = 1;

        public FrmAbout()
        {
            InitializeComponent();
            Text = $"{Text} {Globals.AssemblyInfo.ProductName}";
            lblProdName.Text = Globals.AssemblyInfo.Name;
            lblVersion.Text = $"{lblVersion.Text} {Globals.AssemblyInfo.Version}";
            lblCopyright.Text = $"{Globals.AssemblyInfo.CopyRight}.{Environment.NewLine}All rights reserved.";
            ShowTitleImg(1);
        }

        /// <summary>
        /// 显示侧边栏图片
        /// </summary>
        /// <param name="index"></param>
        private void ShowTitleImg(int index)
        {
            switch (index)
            {
                case 3:
                    picTitle.Image = Resources.Images.Img_Bedivere3;
                    lblPicInfo.Text = $"How Sir Bedivere Cast the Sword Excalibur into the Water{Environment.NewLine}(Aubrey Beardsley)";
                    break;
                case 2:
                    picTitle.Image = Resources.Images.Img_Bedivere2;
                    lblPicInfo.Text = $"Sir Bedivere Throwing Excalibur into the Lake{Environment.NewLine}(Walter Crane)";
                    break;
                default:
                    picTitle.Image = Resources.Images.Img_Bedivere1;
                    lblPicInfo.Text = $"Last Chapter of the Noble Knights{Environment.NewLine}(Yu-Gi-Oh!)";
                    picIndex = 1;
                    break;
            }
        }

        private void picTitle_Click(object sender, EventArgs e)
        {
            picIndex += 1;
            ShowTitleImg(picIndex);
        }

        private void lblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CommonUtilities.OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnx");
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            new FrmLicense().ShowDialog();
        }

        private void btnLibrary_Click(object sender, EventArgs e)
        {
            new FrmReference().ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
