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
            lblExpMsg = new Label();
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
            // lblExpMsg
            // 
            lblExpMsg.Dock = DockStyle.Fill;
            lblExpMsg.Location = new Point(3, 30);
            lblExpMsg.Name = "lblExpMsg";
            lblExpMsg.Size = new Size(474, 70);
            lblExpMsg.TabIndex = 0;
            lblExpMsg.Text = "Exception Message";
            // 
            // tbInput
            // 
            tlpGrp.SetColumnSpan(tbInput, 2);
            tbInput.Dock = DockStyle.Fill;
            tbInput.Location = new Point(3, 33);
            tbInput.Multiline = true;
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(462, 62);
            tbInput.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(113, 3);
            btnOK.Margin = new Padding(3, 3, 30, 3);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 38);
            btnOK.TabIndex = 2;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.Location = new Point(267, 3);
            btnCancel.Margin = new Padding(30, 3, 3, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 38);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(tlpBtns, 0, 3);
            tlpMain.Controls.Add(lblExpMsg, 0, 1);
            tlpMain.Controls.Add(grpMode, 0, 2);
            tlpMain.Controls.Add(Label1, 0, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(10, 10);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 4;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpMain.Size = new Size(480, 280);
            tlpMain.TabIndex = 5;
            // 
            // tlpBtns
            // 
            tlpBtns.ColumnCount = 2;
            tlpBtns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBtns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBtns.Controls.Add(btnOK, 0, 0);
            tlpBtns.Controls.Add(btnCancel, 1, 0);
            tlpBtns.Dock = DockStyle.Fill;
            tlpBtns.Location = new Point(3, 233);
            tlpBtns.Name = "tlpBtns";
            tlpBtns.RowCount = 1;
            tlpBtns.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBtns.Size = new Size(474, 44);
            tlpBtns.TabIndex = 5;
            // 
            // grpMode
            // 
            grpMode.Controls.Add(tlpGrp);
            grpMode.Dock = DockStyle.Fill;
            grpMode.Location = new Point(3, 103);
            grpMode.Name = "grpMode";
            grpMode.Size = new Size(474, 124);
            grpMode.TabIndex = 6;
            grpMode.TabStop = false;
            grpMode.Text = "请输入授权升级码或全新授权码：";
            // 
            // tlpGrp
            // 
            tlpGrp.ColumnCount = 2;
            tlpGrp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGrp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGrp.Controls.Add(chkModUpdate, 0, 0);
            tlpGrp.Controls.Add(chkModeNew, 1, 0);
            tlpGrp.Controls.Add(tbInput, 0, 1);
            tlpGrp.Dock = DockStyle.Fill;
            tlpGrp.Location = new Point(3, 23);
            tlpGrp.Name = "tlpGrp";
            tlpGrp.RowCount = 2;
            tlpGrp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpGrp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGrp.Size = new Size(468, 98);
            tlpGrp.TabIndex = 0;
            // 
            // chkModUpdate
            // 
            chkModUpdate.Checked = true;
            chkModUpdate.Dock = DockStyle.Fill;
            chkModUpdate.Location = new Point(3, 3);
            chkModUpdate.Name = "chkModUpdate";
            chkModUpdate.Padding = new Padding(10, 0, 0, 0);
            chkModUpdate.Size = new Size(228, 24);
            chkModUpdate.TabIndex = 0;
            chkModUpdate.TabStop = true;
            chkModUpdate.Text = "授权升级码";
            chkModUpdate.UseVisualStyleBackColor = true;
            // 
            // chkModeNew
            // 
            chkModeNew.Dock = DockStyle.Fill;
            chkModeNew.Location = new Point(237, 3);
            chkModeNew.Name = "chkModeNew";
            chkModeNew.Padding = new Padding(10, 0, 0, 0);
            chkModeNew.Size = new Size(228, 24);
            chkModeNew.TabIndex = 1;
            chkModeNew.Text = "全新授权码";
            chkModeNew.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            Label1.Dock = DockStyle.Fill;
            Label1.Location = new Point(3, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(474, 30);
            Label1.TabIndex = 7;
            Label1.Text = "授权码检测失败，错误信息：";
            Label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmAuthModify
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(500, 300);
            ControlBox = false;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "FrmAuthModify";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Authorization Error";
            TopMost = true;
            FormClosed += FrmAuthModify_FormClosed;
            tlpMain.ResumeLayout(false);
            tlpBtns.ResumeLayout(false);
            grpMode.ResumeLayout(false);
            tlpGrp.ResumeLayout(false);
            tlpGrp.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal Label lblExpMsg;
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