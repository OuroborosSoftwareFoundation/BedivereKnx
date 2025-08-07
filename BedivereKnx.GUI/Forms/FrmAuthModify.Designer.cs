namespace BedivereKnx.GUI.Forms
{
    partial class FrmAuthModify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAuthModify));
            lblSource = new Label();
            tbInput = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            tlpMain = new TableLayoutPanel();
            tlpBtns = new TableLayoutPanel();
            grpMode = new GroupBox();
            tlpGrp = new TableLayoutPanel();
            chkModUpdate = new RadioButton();
            chkModeNew = new RadioButton();
            Label1 = new Label();
            tlpMain.SuspendLayout();
            tlpBtns.SuspendLayout();
            grpMode.SuspendLayout();
            tlpGrp.SuspendLayout();
            SuspendLayout();
            // 
            // lblSource
            // 
            resources.ApplyResources(lblSource, "lblSource");
            lblSource.Name = "lblSource";
            // 
            // tbInput
            // 
            resources.ApplyResources(tbInput, "tbInput");
            tbInput.Name = "tbInput";
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpBtns, 0, 3);
            tlpMain.Controls.Add(lblSource, 0, 1);
            tlpMain.Controls.Add(grpMode, 0, 2);
            tlpMain.Controls.Add(Label1, 0, 0);
            tlpMain.Name = "tlpMain";
            // 
            // tlpBtns
            // 
            resources.ApplyResources(tlpBtns, "tlpBtns");
            tlpBtns.Controls.Add(btnOK, 0, 0);
            tlpBtns.Controls.Add(btnCancel, 1, 0);
            tlpBtns.Name = "tlpBtns";
            // 
            // grpMode
            // 
            grpMode.Controls.Add(tbInput);
            grpMode.Controls.Add(tlpGrp);
            resources.ApplyResources(grpMode, "grpMode");
            grpMode.Name = "grpMode";
            grpMode.TabStop = false;
            // 
            // tlpGrp
            // 
            resources.ApplyResources(tlpGrp, "tlpGrp");
            tlpGrp.Controls.Add(chkModUpdate, 0, 0);
            tlpGrp.Controls.Add(chkModeNew, 1, 0);
            tlpGrp.Name = "tlpGrp";
            // 
            // chkModUpdate
            // 
            chkModUpdate.Checked = true;
            resources.ApplyResources(chkModUpdate, "chkModUpdate");
            chkModUpdate.Name = "chkModUpdate";
            chkModUpdate.TabStop = true;
            chkModUpdate.UseVisualStyleBackColor = true;
            // 
            // chkModeNew
            // 
            resources.ApplyResources(chkModeNew, "chkModeNew");
            chkModeNew.Name = "chkModeNew";
            chkModeNew.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            resources.ApplyResources(Label1, "Label1");
            Label1.Name = "Label1";
            // 
            // FrmAuthModify
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ControlBox = false;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "FrmAuthModify";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            FormClosed += FrmAuthModify_FormClosed;
            tlpMain.ResumeLayout(false);
            tlpBtns.ResumeLayout(false);
            grpMode.ResumeLayout(false);
            grpMode.PerformLayout();
            tlpGrp.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Label lblSource;
        internal TextBox tbInput;
        internal TableLayoutPanel tlpGrp;
        internal RadioButton chkModUpdate;
        internal RadioButton chkModeNew;
        internal Button btnOK;
        internal Button btnCancel;
        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpBtns;
        internal GroupBox grpMode;
        internal Label Label1;
    }
}