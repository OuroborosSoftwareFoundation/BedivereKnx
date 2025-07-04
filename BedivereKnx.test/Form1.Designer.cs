namespace BedivereKnx_CS.test
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            cb = new ComboBox();
            dgv = new DataGridView();
            tv = new TreeView();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // cb
            // 
            cb.Dock = DockStyle.Top;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.FormattingEnabled = true;
            cb.Location = new Point(0, 0);
            cb.Name = "cb";
            cb.Size = new Size(800, 28);
            cb.TabIndex = 0;
            cb.SelectedIndexChanged += cb_SelectedIndexChanged;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(159, 28);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(641, 422);
            dgv.TabIndex = 1;
            // 
            // tv
            // 
            tv.Dock = DockStyle.Left;
            tv.Location = new Point(0, 28);
            tv.Name = "tv";
            tv.Size = new Size(159, 422);
            tv.TabIndex = 2;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgv);
            Controls.Add(tv);
            Controls.Add(cb);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cb;
        private DataGridView dgv;
        private TreeView tv;
        private System.Windows.Forms.Timer timer1;
    }
}