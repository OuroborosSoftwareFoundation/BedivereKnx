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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
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
            tlpSwitch.AutoSize = true;
            tlpSwitch.ColumnCount = 1;
            tlpSwitch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSwitch.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlpSwitch.Location = new Point(184, 6);
            tlpSwitch.Name = "tlpSwitch";
            tlpSwitch.RowCount = 1;
            tlpSwitch.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpSwitch.Size = new Size(290, 283);
            tlpSwitch.TabIndex = 0;
            tlpSwitch.Resize += tlpSwitch_Resize;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(156, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(798, 541);
            tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tlpSwitch);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(790, 508);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(242, 92);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // FrmMainPanel
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(tabControl1);
            Controls.Add(tvArea);
            DoubleBuffered = true;
            Name = "FrmMainPanel";
            Text = "FrmMainPanel";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal TreeView tvArea;
        private TableLayoutPanel tlpSwitch;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}