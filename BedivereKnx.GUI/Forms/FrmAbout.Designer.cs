namespace BedivereKnx.GUI.Forms
{
    partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            lblProdName = new Label();
            lblCopyright = new Label();
            lblVersion = new Label();
            PictureBox1 = new PictureBox();
            tlpBtns = new TableLayoutPanel();
            btnLibrary = new Button();
            btnOK = new Button();
            btnLicense = new Button();
            tbDesc = new TextBox();
            pnlTitle = new Panel();
            picTitle = new PictureBox();
            lblPicInfo = new Label();
            tlpMain = new TableLayoutPanel();
            tlpRight = new TableLayoutPanel();
            TableLayoutPanel2 = new TableLayoutPanel();
            lblGithub = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            tlpBtns.SuspendLayout();
            pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTitle).BeginInit();
            tlpMain.SuspendLayout();
            tlpRight.SuspendLayout();
            TableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblProdName
            // 
            resources.ApplyResources(lblProdName, "lblProdName");
            lblProdName.Name = "lblProdName";
            // 
            // lblCopyright
            // 
            resources.ApplyResources(lblCopyright, "lblCopyright");
            lblCopyright.Name = "lblCopyright";
            // 
            // lblVersion
            // 
            resources.ApplyResources(lblVersion, "lblVersion");
            lblVersion.Name = "lblVersion";
            // 
            // PictureBox1
            // 
            resources.ApplyResources(PictureBox1, "PictureBox1");
            PictureBox1.Image = Resources.Images.Logo_256x192;
            PictureBox1.Name = "PictureBox1";
            TableLayoutPanel2.SetRowSpan(PictureBox1, 2);
            PictureBox1.TabStop = false;
            // 
            // tlpBtns
            // 
            resources.ApplyResources(tlpBtns, "tlpBtns");
            tlpBtns.Controls.Add(btnLibrary, 1, 0);
            tlpBtns.Controls.Add(btnOK, 2, 0);
            tlpBtns.Controls.Add(btnLicense, 0, 0);
            tlpBtns.Name = "tlpBtns";
            // 
            // btnLibrary
            // 
            resources.ApplyResources(btnLibrary, "btnLibrary");
            btnLibrary.Name = "btnLibrary";
            btnLibrary.UseVisualStyleBackColor = true;
            btnLibrary.Click += btnLibrary_Click;
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.DialogResult = DialogResult.Cancel;
            btnOK.Name = "btnOK";
            btnOK.Click += btnOK_Click;
            // 
            // btnLicense
            // 
            resources.ApplyResources(btnLicense, "btnLicense");
            btnLicense.Name = "btnLicense";
            btnLicense.UseVisualStyleBackColor = true;
            btnLicense.Click += btnLicense_Click;
            // 
            // tbDesc
            // 
            resources.ApplyResources(tbDesc, "tbDesc");
            tbDesc.Name = "tbDesc";
            tbDesc.ReadOnly = true;
            tbDesc.TabStop = false;
            // 
            // pnlTitle
            // 
            pnlTitle.Controls.Add(picTitle);
            pnlTitle.Controls.Add(lblPicInfo);
            resources.ApplyResources(pnlTitle, "pnlTitle");
            pnlTitle.Name = "pnlTitle";
            // 
            // picTitle
            // 
            resources.ApplyResources(picTitle, "picTitle");
            picTitle.Image = Resources.Images.Img_Bedivere1;
            picTitle.Name = "picTitle";
            picTitle.TabStop = false;
            picTitle.Click += picTitle_Click;
            // 
            // lblPicInfo
            // 
            lblPicInfo.BackColor = Color.Transparent;
            resources.ApplyResources(lblPicInfo, "lblPicInfo");
            lblPicInfo.ForeColor = Color.Gray;
            lblPicInfo.Name = "lblPicInfo";
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(pnlTitle, 0, 0);
            tlpMain.Controls.Add(tlpRight, 1, 0);
            tlpMain.Name = "tlpMain";
            // 
            // tlpRight
            // 
            resources.ApplyResources(tlpRight, "tlpRight");
            tlpRight.Controls.Add(lblCopyright, 0, 1);
            tlpRight.Controls.Add(tlpBtns, 0, 4);
            tlpRight.Controls.Add(tbDesc, 0, 3);
            tlpRight.Controls.Add(TableLayoutPanel2, 0, 0);
            tlpRight.Controls.Add(lblGithub, 0, 2);
            tlpRight.Name = "tlpRight";
            // 
            // TableLayoutPanel2
            // 
            resources.ApplyResources(TableLayoutPanel2, "TableLayoutPanel2");
            TableLayoutPanel2.Controls.Add(lblProdName, 0, 0);
            TableLayoutPanel2.Controls.Add(lblVersion, 0, 1);
            TableLayoutPanel2.Controls.Add(PictureBox1, 1, 0);
            TableLayoutPanel2.Name = "TableLayoutPanel2";
            // 
            // lblGithub
            // 
            resources.ApplyResources(lblGithub, "lblGithub");
            lblGithub.Name = "lblGithub";
            lblGithub.TabStop = true;
            lblGithub.LinkClicked += lblGithub_LinkClicked;
            // 
            // FrmAbout
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnOK;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAbout";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            tlpBtns.ResumeLayout(false);
            pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picTitle).EndInit();
            tlpMain.ResumeLayout(false);
            tlpRight.ResumeLayout(false);
            tlpRight.PerformLayout();
            TableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Label lblProdName;
        internal Label lblCopyright;
        internal Label lblVersion;
        internal PictureBox PictureBox1;
        internal TableLayoutPanel TableLayoutPanel2;
        internal TableLayoutPanel tlpBtns;
        internal Button btnLibrary;
        internal Button btnOK;
        internal Button btnLicense;
        internal TextBox tbDesc;
        internal Panel pnlTitle;
        internal PictureBox picTitle;
        internal Label lblPicInfo;
        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpRight;
        internal LinkLabel lblGithub;
    }
}