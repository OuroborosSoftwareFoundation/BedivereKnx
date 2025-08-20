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
            tbHmiFile = new TextBox();
            btnOpenHmiFile = new Button();
            Label2 = new Label();
            tlpHmiFile = new TableLayoutPanel();
            tbDataFile = new TextBox();
            btnOpenDataFile = new Button();
            Label1 = new Label();
            tlpDataFile = new TableLayoutPanel();
            tlpGeneral = new TableLayoutPanel();
            label4 = new Label();
            cbLanguage = new ComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            tabControl1 = new TabControl();
            tpGeneral = new TabPage();
            tpKnx = new TabPage();
            tlpKnx = new TableLayoutPanel();
            chkInitRead = new CheckBox();
            Label3 = new Label();
            TableLayoutPanel1 = new TableLayoutPanel();
            tbLocalIp = new TextBox();
            btnLocalIpSel = new Button();
            tpPwd = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            tbPwdRep = new TextBox();
            label7 = new Label();
            tbPwdNew = new TextBox();
            label6 = new Label();
            label5 = new Label();
            tbPwdCur = new TextBox();
            btnPwdChange = new Button();
            tlpBottom = new TableLayoutPanel();
            tlpHmiFile.SuspendLayout();
            tlpDataFile.SuspendLayout();
            tlpGeneral.SuspendLayout();
            tabControl1.SuspendLayout();
            tpGeneral.SuspendLayout();
            tpKnx.SuspendLayout();
            tlpKnx.SuspendLayout();
            TableLayoutPanel1.SuspendLayout();
            tpPwd.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tlpBottom.SuspendLayout();
            SuspendLayout();
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
            // tlpDataFile
            // 
            resources.ApplyResources(tlpDataFile, "tlpDataFile");
            tlpDataFile.Controls.Add(tbDataFile, 0, 0);
            tlpDataFile.Controls.Add(btnOpenDataFile, 1, 0);
            tlpDataFile.Name = "tlpDataFile";
            // 
            // tlpGeneral
            // 
            resources.ApplyResources(tlpGeneral, "tlpGeneral");
            tlpGeneral.Controls.Add(Label1, 0, 2);
            tlpGeneral.Controls.Add(tlpDataFile, 0, 3);
            tlpGeneral.Controls.Add(Label2, 0, 4);
            tlpGeneral.Controls.Add(tlpHmiFile, 0, 5);
            tlpGeneral.Controls.Add(label4, 0, 0);
            tlpGeneral.Controls.Add(cbLanguage, 0, 1);
            tlpGeneral.Name = "tlpGeneral";
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
            // tabControl1
            // 
            tabControl1.Controls.Add(tpGeneral);
            tabControl1.Controls.Add(tpKnx);
            tabControl1.Controls.Add(tpPwd);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tpGeneral
            // 
            tpGeneral.Controls.Add(tlpGeneral);
            resources.ApplyResources(tpGeneral, "tpGeneral");
            tpGeneral.Name = "tpGeneral";
            tpGeneral.UseVisualStyleBackColor = true;
            // 
            // tpKnx
            // 
            tpKnx.Controls.Add(tlpKnx);
            resources.ApplyResources(tpKnx, "tpKnx");
            tpKnx.Name = "tpKnx";
            tpKnx.UseVisualStyleBackColor = true;
            // 
            // tlpKnx
            // 
            resources.ApplyResources(tlpKnx, "tlpKnx");
            tlpKnx.Controls.Add(chkInitRead, 0, 3);
            tlpKnx.Controls.Add(Label3, 0, 0);
            tlpKnx.Controls.Add(TableLayoutPanel1, 0, 1);
            tlpKnx.Name = "tlpKnx";
            // 
            // chkInitRead
            // 
            resources.ApplyResources(chkInitRead, "chkInitRead");
            chkInitRead.Name = "chkInitRead";
            chkInitRead.UseVisualStyleBackColor = true;
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
            // btnLocalIpSel
            // 
            resources.ApplyResources(btnLocalIpSel, "btnLocalIpSel");
            btnLocalIpSel.Name = "btnLocalIpSel";
            btnLocalIpSel.UseVisualStyleBackColor = true;
            btnLocalIpSel.Click += btnLocalIpSel_Click;
            // 
            // tpPwd
            // 
            tpPwd.Controls.Add(tableLayoutPanel2);
            resources.ApplyResources(tpPwd, "tpPwd");
            tpPwd.Name = "tpPwd";
            tpPwd.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(tbPwdRep, 0, 5);
            tableLayoutPanel2.Controls.Add(label7, 0, 4);
            tableLayoutPanel2.Controls.Add(tbPwdNew, 0, 3);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(label5, 0, 0);
            tableLayoutPanel2.Controls.Add(tbPwdCur, 0, 1);
            tableLayoutPanel2.Controls.Add(btnPwdChange, 0, 7);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tbPwdRep
            // 
            resources.ApplyResources(tbPwdRep, "tbPwdRep");
            tbPwdRep.Name = "tbPwdRep";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // tbPwdNew
            // 
            resources.ApplyResources(tbPwdNew, "tbPwdNew");
            tbPwdNew.Name = "tbPwdNew";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // tbPwdCur
            // 
            resources.ApplyResources(tbPwdCur, "tbPwdCur");
            tbPwdCur.Name = "tbPwdCur";
            // 
            // btnPwdChange
            // 
            resources.ApplyResources(btnPwdChange, "btnPwdChange");
            btnPwdChange.Name = "btnPwdChange";
            btnPwdChange.UseVisualStyleBackColor = true;
            btnPwdChange.Click += btnPwdChange_Click;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnCancel, 1, 0);
            tlpBottom.Controls.Add(btnOK, 0, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // FrmConfig
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(tabControl1);
            Controls.Add(tlpBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfig";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            TopMost = true;
            tlpHmiFile.ResumeLayout(false);
            tlpHmiFile.PerformLayout();
            tlpDataFile.ResumeLayout(false);
            tlpDataFile.PerformLayout();
            tlpGeneral.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tpGeneral.ResumeLayout(false);
            tpKnx.ResumeLayout(false);
            tlpKnx.ResumeLayout(false);
            TableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanel1.PerformLayout();
            tpPwd.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tlpBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        internal TextBox tbHmiFile;
        internal Button btnOpenHmiFile;
        internal Label Label2;
        internal TableLayoutPanel tlpHmiFile;
        internal TextBox tbDataFile;
        internal Button btnOpenDataFile;
        internal Label Label1;
        internal TableLayoutPanel tlpDataFile;
        internal TableLayoutPanel tlpGeneral;
        internal Button btnOK;
        internal Button btnCancel;
        internal Label label4;
        private ComboBox cbLanguage;
        private TabControl tabControl1;
        private TabPage tpGeneral;
        private TabPage tpKnx;
        private TableLayoutPanel tlpBottom;
        private TableLayoutPanel tlpKnx;
        private TabPage tpPwd;
        internal Label Label3;
        internal TableLayoutPanel TableLayoutPanel1;
        internal TextBox tbLocalIp;
        internal Button btnLocalIpSel;
        internal CheckBox chkInitRead;
        private TableLayoutPanel tableLayoutPanel2;
        internal Label label6;
        internal Label label5;
        private TextBox tbPwdCur;
        private TextBox tbPwdNew;
        private Button btnPwdChange;
        private TextBox tbPwdRep;
        internal Label label7;
    }
}