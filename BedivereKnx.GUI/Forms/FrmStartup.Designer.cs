namespace BedivereKnx.GUI.Forms
{
    partial class FrmStartup
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
            tlpMain = new TableLayoutPanel();
            tlpInfo = new TableLayoutPanel();
            lblProdName = new Label();
            lblVersion = new Label();
            lblCopyright = new Label();
            pbrLoad = new ProgressBar();
            lblAuth = new Label();
            picTitle = new PictureBox();
            tlpMain.SuspendLayout();
            tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTitle).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(tlpInfo, 2, 0);
            tlpMain.Controls.Add(picTitle, 0, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Margin = new Padding(0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(720, 320);
            tlpMain.TabIndex = 5;
            // 
            // tlpInfo
            // 
            tlpInfo.ColumnCount = 1;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInfo.Controls.Add(lblProdName, 0, 0);
            tlpInfo.Controls.Add(lblVersion, 0, 1);
            tlpInfo.Controls.Add(lblCopyright, 0, 4);
            tlpInfo.Controls.Add(pbrLoad, 0, 3);
            tlpInfo.Controls.Add(lblAuth, 0, 2);
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Location = new Point(323, 3);
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 5;
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tlpInfo.Size = new Size(394, 314);
            tlpInfo.TabIndex = 3;
            // 
            // lblProdName
            // 
            lblProdName.BackColor = Color.Transparent;
            lblProdName.Dock = DockStyle.Fill;
            lblProdName.Font = new Font("微软雅黑", 19.8000011F, FontStyle.Bold);
            lblProdName.ForeColor = Color.Azure;
            lblProdName.Location = new Point(3, 0);
            lblProdName.Name = "lblProdName";
            lblProdName.Size = new Size(388, 144);
            lblProdName.TabIndex = 0;
            lblProdName.Text = "Bedivere Knx GUI";
            lblProdName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Dock = DockStyle.Fill;
            lblVersion.Font = new Font("Microsoft Sans Serif", 9F);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(3, 144);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(388, 50);
            lblVersion.TabIndex = 1;
            lblVersion.Text = "Version";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCopyright
            // 
            lblCopyright.BackColor = Color.Transparent;
            lblCopyright.Dock = DockStyle.Fill;
            lblCopyright.Font = new Font("Microsoft Sans Serif", 9F);
            lblCopyright.ForeColor = Color.White;
            lblCopyright.Location = new Point(3, 254);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(388, 60);
            lblCopyright.TabIndex = 2;
            lblCopyright.Text = "© 2024 Ouroboros Software Foundation.\r\nAll rights reserved.";
            lblCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pbrLoad
            // 
            pbrLoad.Dock = DockStyle.Fill;
            pbrLoad.Location = new Point(3, 227);
            pbrLoad.Name = "pbrLoad";
            pbrLoad.Size = new Size(388, 24);
            pbrLoad.Style = ProgressBarStyle.Marquee;
            pbrLoad.TabIndex = 3;
            // 
            // lblAuth
            // 
            lblAuth.Dock = DockStyle.Fill;
            lblAuth.ForeColor = Color.White;
            lblAuth.Location = new Point(3, 194);
            lblAuth.Name = "lblAuth";
            lblAuth.Size = new Size(388, 30);
            lblAuth.TabIndex = 4;
            lblAuth.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picTitle
            // 
            picTitle.BorderStyle = BorderStyle.FixedSingle;
            picTitle.Dock = DockStyle.Fill;
            picTitle.Image = Resources.Images.Img_StartUp;
            picTitle.Location = new Point(10, 10);
            picTitle.Margin = new Padding(10);
            picTitle.Name = "picTitle";
            picTitle.Size = new Size(300, 300);
            picTitle.SizeMode = PictureBoxSizeMode.Zoom;
            picTitle.TabIndex = 4;
            picTitle.TabStop = false;
            // 
            // FrmStartup
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(720, 320);
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmStartup";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            tlpMain.ResumeLayout(false);
            tlpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picTitle).EndInit();
            ResumeLayout(false);
        }

        #endregion

        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpInfo;
        internal Label lblProdName;
        internal Label lblVersion;
        internal Label lblCopyright;
        internal ProgressBar pbrLoad;
        internal Label lblAuth;
        internal PictureBox picTitle;
    }
}