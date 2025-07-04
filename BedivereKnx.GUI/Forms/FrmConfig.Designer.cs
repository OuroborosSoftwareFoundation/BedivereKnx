namespace BedivereKnx.GUI.Forms
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            btnLocalIpSel = new Button();
            Label3 = new Label();
            TableLayoutPanel1 = new TableLayoutPanel();
            tbLocalIp = new TextBox();
            tbHmiFile = new TextBox();
            btnOpenHmiFile = new Button();
            Label2 = new Label();
            tlpHmiFile = new TableLayoutPanel();
            tbDataFile = new TextBox();
            btnOpenDataFile = new Button();
            Label1 = new Label();
            chkInitRead = new CheckBox();
            tlpDataFile = new TableLayoutPanel();
            tlpMain = new TableLayoutPanel();
            tlpButton = new TableLayoutPanel();
            btnOK = new Button();
            btnCancel = new Button();
            label4 = new Label();
            cbLanguage = new ComboBox();
            TableLayoutPanel1.SuspendLayout();
            tlpHmiFile.SuspendLayout();
            tlpDataFile.SuspendLayout();
            tlpMain.SuspendLayout();
            tlpButton.SuspendLayout();
            SuspendLayout();
            // 
            // btnLocalIpSel
            // 
            resources.ApplyResources(btnLocalIpSel, "btnLocalIpSel");
            btnLocalIpSel.Name = "btnLocalIpSel";
            btnLocalIpSel.UseVisualStyleBackColor = true;
            btnLocalIpSel.Click += btnLocalIpSel_Click;
            // 
            // Label3
            // 
            resources.ApplyResources(Label3, "Label3");
            Label3.Name = "Label3";
            // 
            // TableLayoutPanel1
            // 
            resources.ApplyResources(TableLayoutPanel1, "TableLayoutPanel1");
            TableLayoutPanel1.Controls.Add(tbLocalIp, 0, 0);
            TableLayoutPanel1.Controls.Add(btnLocalIpSel, 1, 0);
            TableLayoutPanel1.Name = "TableLayoutPanel1";
            // 
            // tbLocalIp
            // 
            resources.ApplyResources(tbLocalIp, "tbLocalIp");
            tbLocalIp.BackColor = SystemColors.Window;
            tbLocalIp.Name = "tbLocalIp";
            tbLocalIp.ReadOnly = true;
            // 
            // tbHmiFile
            // 
            resources.ApplyResources(tbHmiFile, "tbHmiFile");
            tbHmiFile.BackColor = SystemColors.Window;
            tbHmiFile.Name = "tbHmiFile";
            tbHmiFile.ReadOnly = true;
            // 
            // btnOpenHmiFile
            // 
            resources.ApplyResources(btnOpenHmiFile, "btnOpenHmiFile");
            btnOpenHmiFile.Name = "btnOpenHmiFile";
            btnOpenHmiFile.UseVisualStyleBackColor = true;
            btnOpenHmiFile.Click += btnOpenHmiFile_Click;
            // 
            // Label2
            // 
            resources.ApplyResources(Label2, "Label2");
            Label2.Name = "Label2";
            // 
            // tlpHmiFile
            // 
            resources.ApplyResources(tlpHmiFile, "tlpHmiFile");
            tlpHmiFile.Controls.Add(tbHmiFile, 0, 0);
            tlpHmiFile.Controls.Add(btnOpenHmiFile, 1, 0);
            tlpHmiFile.Name = "tlpHmiFile";
            // 
            // tbDataFile
            // 
            resources.ApplyResources(tbDataFile, "tbDataFile");
            tbDataFile.BackColor = SystemColors.Window;
            tbDataFile.Name = "tbDataFile";
            tbDataFile.ReadOnly = true;
            // 
            // btnOpenDataFile
            // 
            resources.ApplyResources(btnOpenDataFile, "btnOpenDataFile");
            btnOpenDataFile.Name = "btnOpenDataFile";
            btnOpenDataFile.UseVisualStyleBackColor = true;
            btnOpenDataFile.Click += btnOpenDataFile_Click;
            // 
            // Label1
            // 
            resources.ApplyResources(Label1, "Label1");
            Label1.Name = "Label1";
            // 
            // chkInitRead
            // 
            resources.ApplyResources(chkInitRead, "chkInitRead");
            chkInitRead.Name = "chkInitRead";
            chkInitRead.UseVisualStyleBackColor = true;
            // 
            // tlpDataFile
            // 
            resources.ApplyResources(tlpDataFile, "tlpDataFile");
            tlpDataFile.Controls.Add(tbDataFile, 0, 0);
            tlpDataFile.Controls.Add(btnOpenDataFile, 1, 0);
            tlpDataFile.Name = "tlpDataFile";
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(Label1, 0, 2);
            tlpMain.Controls.Add(chkInitRead, 0, 8);
            tlpMain.Controls.Add(tlpDataFile, 0, 3);
            tlpMain.Controls.Add(Label2, 0, 4);
            tlpMain.Controls.Add(tlpHmiFile, 0, 5);
            tlpMain.Controls.Add(Label3, 0, 6);
            tlpMain.Controls.Add(TableLayoutPanel1, 0, 7);
            tlpMain.Controls.Add(tlpButton, 0, 10);
            tlpMain.Controls.Add(label4, 0, 0);
            tlpMain.Controls.Add(cbLanguage, 0, 1);
            tlpMain.Name = "tlpMain";
            // 
            // tlpButton
            // 
            resources.ApplyResources(tlpButton, "tlpButton");
            tlpButton.Controls.Add(btnOK, 0, 0);
            tlpButton.Controls.Add(btnCancel, 1, 0);
            tlpButton.Name = "tlpButton";
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // cbLanguage
            // 
            resources.ApplyResources(cbLanguage, "cbLanguage");
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Name = "cbLanguage";
            // 
            // FrmConfig
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfig";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            TopMost = true;
            TableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanel1.PerformLayout();
            tlpHmiFile.ResumeLayout(false);
            tlpHmiFile.PerformLayout();
            tlpDataFile.ResumeLayout(false);
            tlpDataFile.PerformLayout();
            tlpMain.ResumeLayout(false);
            tlpButton.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Button btnLocalIpSel;
        internal Label Label3;
        internal TableLayoutPanel TableLayoutPanel1;
        internal TextBox tbLocalIp;
        internal TextBox tbHmiFile;
        internal Button btnOpenHmiFile;
        internal Label Label2;
        internal TableLayoutPanel tlpHmiFile;
        internal TextBox tbDataFile;
        internal Button btnOpenDataFile;
        internal Label Label1;
        internal CheckBox chkInitRead;
        internal TableLayoutPanel tlpDataFile;
        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpButton;
        internal Button btnOK;
        internal Button btnCancel;
        internal Label label4;
        private ComboBox cbLanguage;
    }
}