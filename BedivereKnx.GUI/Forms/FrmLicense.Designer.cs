namespace BedivereKnx.GUI.Forms
{
    partial class FrmLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLicense));
            tlpMain = new TableLayoutPanel();
            tlpTop = new TableLayoutPanel();
            picLic = new PictureBox();
            lblTitle = new Label();
            tbLicense = new TextBox();
            btnOK = new Button();
            tlpMain.SuspendLayout();
            tlpTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLic).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpTop, 0, 0);
            tlpMain.Controls.Add(tbLicense, 0, 1);
            tlpMain.Controls.Add(btnOK, 0, 2);
            tlpMain.Name = "tlpMain";
            // 
            // tlpTop
            // 
            resources.ApplyResources(tlpTop, "tlpTop");
            tlpTop.Controls.Add(picLic, 0, 0);
            tlpTop.Controls.Add(lblTitle, 1, 0);
            tlpTop.Name = "tlpTop";
            // 
            // picLic
            // 
            resources.ApplyResources(picLic, "picLic");
            picLic.Image = Resources.Images.Logo_gplv3_or_later;
            picLic.Name = "picLic";
            picLic.TabStop = false;
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.Name = "lblTitle";
            // 
            // tbLicense
            // 
            tbLicense.BackColor = SystemColors.Window;
            resources.ApplyResources(tbLicense, "tbLicense");
            tbLicense.Name = "tbLicense";
            tbLicense.ReadOnly = true;
            tbLicense.TabStop = false;
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // FrmLicense
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLicense";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            TopMost = true;
            Load += FrmLicense_Load;
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            tlpTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpTop;
        internal PictureBox picLic;
        internal TextBox tbLicense;
        private Label lblTitle;
        private Button btnOK;
    }
}