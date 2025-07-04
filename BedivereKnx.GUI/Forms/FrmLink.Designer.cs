namespace BedivereKnx.GUI.Forms
{
    partial class FrmLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLink));
            dgvLink = new DataGridView();
            tlpBottom = new TableLayoutPanel();
            btnInfo = new Button();
            btnOpen = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLink).BeginInit();
            tlpBottom.SuspendLayout();
            SuspendLayout();
            // 
            // dgvLink
            // 
            resources.ApplyResources(dgvLink, "dgvLink");
            dgvLink.AllowUserToAddRows = false;
            dgvLink.AllowUserToDeleteRows = false;
            dgvLink.AllowUserToResizeRows = false;
            dgvLink.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLink.BackgroundColor = SystemColors.Window;
            dgvLink.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLink.MultiSelect = false;
            dgvLink.Name = "dgvLink";
            dgvLink.ReadOnly = true;
            dgvLink.RowHeadersVisible = false;
            dgvLink.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLink.ShowCellErrors = false;
            dgvLink.ShowCellToolTips = false;
            dgvLink.ShowEditingIcon = false;
            dgvLink.ShowRowErrors = false;
            dgvLink.Tag = "Objects";
            dgvLink.VirtualMode = true;
            dgvLink.CellDoubleClick += dgvLink_CellDoubleClick;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnInfo, 0, 0);
            tlpBottom.Controls.Add(btnOpen, 1, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // btnInfo
            // 
            resources.ApplyResources(btnInfo, "btnInfo");
            btnInfo.Name = "btnInfo";
            btnInfo.Click += btnInfo_Click;
            // 
            // btnOpen
            // 
            resources.ApplyResources(btnOpen, "btnOpen");
            btnOpen.Name = "btnOpen";
            btnOpen.Click += btnOpen_Click;
            // 
            // FrmLink
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvLink);
            Controls.Add(tlpBottom);
            Name = "FrmLink";
            ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)dgvLink).EndInit();
            tlpBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal DataGridView dgvLink;
        internal TableLayoutPanel tlpBottom;
        internal Button btnOpen;
        internal Button btnInfo;
    }
}