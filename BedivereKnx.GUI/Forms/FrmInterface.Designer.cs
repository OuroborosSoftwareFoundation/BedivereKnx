namespace BedivereKnx.GUI.Forms
{
    partial class FrmInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInterface));
            dgvIf = new DataGridView();
            tlpTop = new TableLayoutPanel();
            Label1 = new Label();
            lblIfDefault = new Label();
            btnConnect = new Button();
            btnNetTest = new Button();
            tlpBottom = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvIf).BeginInit();
            tlpTop.SuspendLayout();
            tlpBottom.SuspendLayout();
            SuspendLayout();
            // 
            // dgvIf
            // 
            dgvIf.AllowUserToAddRows = false;
            dgvIf.AllowUserToDeleteRows = false;
            dgvIf.AllowUserToResizeColumns = false;
            dgvIf.AllowUserToResizeRows = false;
            dgvIf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIf.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvIf, "dgvIf");
            dgvIf.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvIf.MultiSelect = false;
            dgvIf.Name = "dgvIf";
            dgvIf.ReadOnly = true;
            dgvIf.RowHeadersVisible = false;
            dgvIf.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIf.ShowCellErrors = false;
            dgvIf.ShowCellToolTips = false;
            dgvIf.ShowEditingIcon = false;
            dgvIf.ShowRowErrors = false;
            dgvIf.SelectionChanged += dgvIf_SelectionChanged;
            dgvIf.Sorted += dgvIf_Sorted;
            // 
            // tlpTop
            // 
            resources.ApplyResources(tlpTop, "tlpTop");
            tlpTop.Controls.Add(Label1, 0, 0);
            tlpTop.Controls.Add(lblIfDefault, 1, 0);
            tlpTop.Name = "tlpTop";
            // 
            // Label1
            // 
            resources.ApplyResources(Label1, "Label1");
            Label1.Name = "Label1";
            // 
            // lblIfDefault
            // 
            resources.ApplyResources(lblIfDefault, "lblIfDefault");
            lblIfDefault.ForeColor = Color.Gray;
            lblIfDefault.Name = "lblIfDefault";
            // 
            // btnConnect
            // 
            resources.ApplyResources(btnConnect, "btnConnect");
            btnConnect.Name = "btnConnect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnNetTest
            // 
            resources.ApplyResources(btnNetTest, "btnNetTest");
            btnNetTest.Name = "btnNetTest";
            btnNetTest.UseVisualStyleBackColor = true;
            btnNetTest.Click += btnNetTest_Click;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnNetTest, 0, 0);
            tlpBottom.Controls.Add(btnConnect, 1, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // FrmInterface
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvIf);
            Controls.Add(tlpBottom);
            Controls.Add(tlpTop);
            Name = "FrmInterface";
            ShowIcon = false;
            Load += FrmInterface_Load;
            ((System.ComponentModel.ISupportInitialize)dgvIf).EndInit();
            tlpTop.ResumeLayout(false);
            tlpBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal DataGridView dgvIf;
        internal TableLayoutPanel tlpTop;
        internal Label Label1;
        internal Label lblIfDefault;
        internal Button btnConnect;
        private Button btnNetTest;
        private TableLayoutPanel tlpBottom;
    }
}