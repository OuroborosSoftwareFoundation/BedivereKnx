namespace BedivereKnx.GUI.Forms
{
    partial class FrmScheduleSeq
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
            dgvScd = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvScd).BeginInit();
            SuspendLayout();
            // 
            // dgvScd
            // 
            dgvScd.AllowUserToAddRows = false;
            dgvScd.AllowUserToDeleteRows = false;
            dgvScd.AllowUserToResizeRows = false;
            dgvScd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvScd.BackgroundColor = SystemColors.Window;
            dgvScd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScd.Dock = DockStyle.Fill;
            dgvScd.Location = new Point(0, 0);
            dgvScd.MultiSelect = false;
            dgvScd.Name = "dgvScd";
            dgvScd.ReadOnly = true;
            dgvScd.RowHeadersVisible = false;
            dgvScd.RowHeadersWidth = 51;
            dgvScd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvScd.ShowCellErrors = false;
            dgvScd.ShowCellToolTips = false;
            dgvScd.ShowEditingIcon = false;
            dgvScd.ShowRowErrors = false;
            dgvScd.Size = new Size(982, 553);
            dgvScd.TabIndex = 1;
            // 
            // FrmScheduleSeq
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(dgvScd);
            Name = "FrmScheduleSeq";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Schedule Sequence";
            ((System.ComponentModel.ISupportInitialize)dgvScd).EndInit();
            ResumeLayout(false);
        }

        #endregion

        internal DataGridView dgvScd;
    }
}