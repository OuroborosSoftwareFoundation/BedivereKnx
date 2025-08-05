namespace BedivereKnx.GUI.Forms
{
    partial class FrmMainPanel
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
            tvArea = new TreeView();
            tlpSwitch = new TableLayoutPanel();
            lstGroup = new ListBox();
            tlpMain = new TableLayoutPanel();
            tlpMain.SuspendLayout();
            SuspendLayout();
            // 
            // tvArea
            // 
            tvArea.Dock = DockStyle.Left;
            tvArea.Location = new Point(0, 0);
            tvArea.Name = "tvArea";
            tvArea.Size = new Size(150, 553);
            tvArea.TabIndex = 5;
            tvArea.AfterSelect += tvArea_AfterSelect;
            // 
            // tlpSwitch
            // 
            tlpSwitch.AutoScroll = true;
            tlpSwitch.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpSwitch.ColumnCount = 1;
            tlpSwitch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSwitch.Dock = DockStyle.Fill;
            tlpSwitch.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpSwitch.Location = new Point(153, 3);
            tlpSwitch.Name = "tlpSwitch";
            tlpSwitch.RowCount = 1;
            tlpSwitch.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpSwitch.Size = new Size(676, 547);
            tlpSwitch.TabIndex = 0;
            tlpSwitch.Resize += tlpSwitch_Resize;
            // 
            // lstGroup
            // 
            lstGroup.Dock = DockStyle.Fill;
            lstGroup.FormattingEnabled = true;
            lstGroup.Location = new Point(3, 3);
            lstGroup.Name = "lstGroup";
            lstGroup.Size = new Size(144, 547);
            lstGroup.TabIndex = 7;
            lstGroup.SelectedIndexChanged += lstGroup_SelectedIndexChanged;
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(lstGroup, 0, 0);
            tlpMain.Controls.Add(tlpSwitch, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(150, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpMain.Size = new Size(832, 553);
            tlpMain.TabIndex = 8;
            // 
            // FrmMainPanel
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(tlpMain);
            Controls.Add(tvArea);
            Name = "FrmMainPanel";
            Text = "FrmMainPanel";
            tlpMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal TreeView tvArea;
        private TableLayoutPanel tlpSwitch;
        private ListBox lstGroup;
        private TableLayoutPanel tlpMain;
    }
}