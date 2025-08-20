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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainPanel));
            tvArea = new TreeView();
            tlpSwitch = new TableLayoutPanel();
            lstGroup = new ListBox();
            tlpMain = new TableLayoutPanel();
            tlpMain.SuspendLayout();
            SuspendLayout();
            // 
            // tvArea
            // 
            resources.ApplyResources(tvArea, "tvArea");
            tvArea.Name = "tvArea";
            tvArea.AfterSelect += tvArea_AfterSelect;
            // 
            // tlpSwitch
            // 
            resources.ApplyResources(tlpSwitch, "tlpSwitch");
            tlpSwitch.Name = "tlpSwitch";
            tlpSwitch.SizeChanged += tlpSwitch_SizeChanged;
            // 
            // lstGroup
            // 
            resources.ApplyResources(lstGroup, "lstGroup");
            lstGroup.FormattingEnabled = true;
            lstGroup.Name = "lstGroup";
            lstGroup.SelectedIndexChanged += lstGroup_SelectedIndexChanged;
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpSwitch, 1, 0);
            tlpMain.Controls.Add(lstGroup, 0, 0);
            tlpMain.Name = "tlpMain";
            // 
            // FrmMainPanel
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Controls.Add(tvArea);
            DoubleBuffered = true;
            Name = "FrmMainPanel";
            ResizeEnd += FrmMainPanel_ResizeEnd;
            SizeChanged += FrmMainPanel_SizeChanged;
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