namespace BedivereKnx.GUI.Forms
{
    partial class FrmMainHmi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainHmi));
            tvHmi = new TreeView();
            pnlHmi = new Panel();
            btnLeftHide = new Button();
            SplitContainer1 = new SplitContainer();
            pnlHmi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
            SplitContainer1.Panel1.SuspendLayout();
            SplitContainer1.Panel2.SuspendLayout();
            SplitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // tvHmi
            // 
            tvHmi.Dock = DockStyle.Fill;
            tvHmi.Location = new Point(0, 0);
            tvHmi.Name = "tvHmi";
            tvHmi.Size = new Size(102, 551);
            tvHmi.TabIndex = 0;
            tvHmi.AfterSelect += tvHmi_AfterSelect;
            // 
            // pnlHmi
            // 
            pnlHmi.BackColor = Color.Transparent;
            pnlHmi.Controls.Add(btnLeftHide);
            pnlHmi.Dock = DockStyle.Fill;
            pnlHmi.Location = new Point(0, 0);
            pnlHmi.Name = "pnlHmi";
            pnlHmi.Size = new Size(872, 551);
            pnlHmi.TabIndex = 2;
            pnlHmi.SizeChanged += pnlHmi_SizeChanged;
            // 
            // btnLeftHide
            // 
            btnLeftHide.Location = new Point(0, 0);
            btnLeftHide.Name = "btnLeftHide";
            btnLeftHide.Size = new Size(10, 20);
            btnLeftHide.TabIndex = 0;
            btnLeftHide.UseVisualStyleBackColor = true;
            btnLeftHide.Click += btnLeftHide_Click;
            // 
            // SplitContainer1
            // 
            SplitContainer1.BorderStyle = BorderStyle.FixedSingle;
            SplitContainer1.Dock = DockStyle.Fill;
            SplitContainer1.Location = new Point(0, 0);
            SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            SplitContainer1.Panel1.Controls.Add(tvHmi);
            SplitContainer1.Panel1MinSize = 0;
            // 
            // SplitContainer1.Panel2
            // 
            SplitContainer1.Panel2.BackColor = Color.White;
            SplitContainer1.Panel2.Controls.Add(pnlHmi);
            SplitContainer1.Size = new Size(982, 553);
            SplitContainer1.SplitterDistance = 104;
            SplitContainer1.TabIndex = 4;
            // 
            // FrmMainHmi
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(SplitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMainHmi";
            Load += FrmMainHmi_Load;
            pnlHmi.ResumeLayout(false);
            SplitContainer1.Panel1.ResumeLayout(false);
            SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
            SplitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal TreeView tvHmi;
        internal Panel pnlHmi;
        internal Button btnLeftHide;
        internal SplitContainer SplitContainer1;
    }
}