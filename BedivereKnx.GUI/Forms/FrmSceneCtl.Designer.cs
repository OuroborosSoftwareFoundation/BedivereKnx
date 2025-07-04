namespace BedivereKnx.GUI.Forms
{
    partial class FrmSceneCtl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSceneCtl));
            btnOk = new Button();
            btnCancel = new Button();
            tlpBottom = new TableLayoutPanel();
            tlpMain = new TableLayoutPanel();
            lblScnName = new Label();
            lblScnAddr = new Label();
            lvScn = new ListView();
            chkLearn = new CheckBox();
            tlpBottom.SuspendLayout();
            tlpMain.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(btnOk, "btnOk");
            btnOk.Name = "btnOk";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnOk, 0, 0);
            tlpBottom.Controls.Add(btnCancel, 1, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpBottom, 0, 4);
            tlpMain.Controls.Add(lblScnName, 0, 0);
            tlpMain.Controls.Add(lblScnAddr, 0, 1);
            tlpMain.Controls.Add(lvScn, 0, 2);
            tlpMain.Controls.Add(chkLearn, 0, 3);
            tlpMain.Name = "tlpMain";
            // 
            // lblScnName
            // 
            resources.ApplyResources(lblScnName, "lblScnName");
            lblScnName.Name = "lblScnName";
            // 
            // lblScnAddr
            // 
            resources.ApplyResources(lblScnAddr, "lblScnAddr");
            lblScnAddr.Name = "lblScnAddr";
            // 
            // lvScn
            // 
            resources.ApplyResources(lvScn, "lvScn");
            lvScn.FullRowSelect = true;
            lvScn.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvScn.MultiSelect = false;
            lvScn.Name = "lvScn";
            lvScn.UseCompatibleStateImageBehavior = false;
            lvScn.View = View.Details;
            // 
            // chkLearn
            // 
            resources.ApplyResources(chkLearn, "chkLearn");
            chkLearn.Name = "chkLearn";
            chkLearn.UseVisualStyleBackColor = true;
            chkLearn.CheckedChanged += chkLearn_CheckedChanged;
            // 
            // FrmSceneCtl
            // 
            AcceptButton = btnOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSceneCtl";
            ShowIcon = false;
            ShowInTaskbar = false;
            tlpBottom.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            tlpMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal Button btnOk;
        internal Button btnCancel;
        internal TableLayoutPanel tlpBottom;
        internal TableLayoutPanel tlpMain;
        internal Label lblScnName;
        internal Label lblScnAddr;
        internal ListView lvScn;
        private CheckBox chkLearn;
    }
}