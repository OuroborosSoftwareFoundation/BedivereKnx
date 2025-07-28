namespace BedivereKnx.GUI.Forms
{
    partial class FrmMainTable_old
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainTable_old));
            lstTelLog = new ListBox();
            menuTelLog = new ContextMenuStrip(components);
            btnTelLogClear = new ToolStripMenuItem();
            ToolStripSeparator1 = new ToolStripSeparator();
            btnTelLogExport = new ToolStripMenuItem();
            tmPoll = new System.Windows.Forms.Timer(components);
            tlpMain = new TableLayoutPanel();
            tlpTool = new TableLayoutPanel();
            btnInterface = new Button();
            btnLink = new Button();
            tvArea = new TreeView();
            tabMain = new TabControl();
            tpObject = new TabPage();
            dgvObject = new DataGridView();
            menuColFilter = new ContextMenuStrip(components);
            tlpObject = new TableLayoutPanel();
            btnObjOff = new Button();
            btnObjOn = new Button();
            numObjVal = new TrackBar();
            Label1 = new Label();
            lblObjVal = new Label();
            btnObjToggle = new Button();
            tpScene = new TabPage();
            dgvScene = new DataGridView();
            tlpScene = new TableLayoutPanel();
            btnCtlScn = new Button();
            tpDevice = new TabPage();
            dgvDevice = new DataGridView();
            btnDevicePoll = new Button();
            tpSchedule = new TabPage();
            dgvSchedule = new DataGridView();
            btnSchedule = new Button();
            menuTelLog.SuspendLayout();
            tlpMain.SuspendLayout();
            tlpTool.SuspendLayout();
            tabMain.SuspendLayout();
            tpObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvObject).BeginInit();
            tlpObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numObjVal).BeginInit();
            tpScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScene).BeginInit();
            tlpScene.SuspendLayout();
            tpDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevice).BeginInit();
            tpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            SuspendLayout();
            // 
            // lstTelLog
            // 
            lstTelLog.ContextMenuStrip = menuTelLog;
            resources.ApplyResources(lstTelLog, "lstTelLog");
            lstTelLog.FormattingEnabled = true;
            lstTelLog.Name = "lstTelLog";
            // 
            // menuTelLog
            // 
            menuTelLog.ImageScalingSize = new Size(20, 20);
            menuTelLog.Items.AddRange(new ToolStripItem[] { btnTelLogClear, ToolStripSeparator1, btnTelLogExport });
            menuTelLog.Name = "menuTelLog";
            resources.ApplyResources(menuTelLog, "menuTelLog");
            // 
            // btnTelLogClear
            // 
            btnTelLogClear.Name = "btnTelLogClear";
            resources.ApplyResources(btnTelLogClear, "btnTelLogClear");
            btnTelLogClear.Click += btnTelLogClear_Click;
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            resources.ApplyResources(ToolStripSeparator1, "ToolStripSeparator1");
            // 
            // btnTelLogExport
            // 
            btnTelLogExport.Name = "btnTelLogExport";
            resources.ApplyResources(btnTelLogExport, "btnTelLogExport");
            btnTelLogExport.Click += btnTelLogExport_Click;
            // 
            // tmPoll
            // 
            tmPoll.Interval = 3000;
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpTool, 0, 1);
            tlpMain.Controls.Add(tvArea, 0, 0);
            tlpMain.Controls.Add(tabMain, 1, 0);
            tlpMain.Controls.Add(lstTelLog, 1, 1);
            tlpMain.Name = "tlpMain";
            // 
            // tlpTool
            // 
            resources.ApplyResources(tlpTool, "tlpTool");
            tlpTool.Controls.Add(btnInterface, 0, 0);
            tlpTool.Controls.Add(btnLink, 0, 1);
            tlpTool.Name = "tlpTool";
            // 
            // btnInterface
            // 
            resources.ApplyResources(btnInterface, "btnInterface");
            btnInterface.Name = "btnInterface";
            btnInterface.UseVisualStyleBackColor = true;
            btnInterface.Click += btnInterface_Click;
            // 
            // btnLink
            // 
            resources.ApplyResources(btnLink, "btnLink");
            btnLink.Name = "btnLink";
            btnLink.UseVisualStyleBackColor = true;
            btnLink.Click += btnLink_Click;
            // 
            // tvArea
            // 
            resources.ApplyResources(tvArea, "tvArea");
            tvArea.Name = "tvArea";
            tvArea.AfterSelect += tvArea_AfterSelect;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tpObject);
            tabMain.Controls.Add(tpScene);
            tabMain.Controls.Add(tpDevice);
            tabMain.Controls.Add(tpSchedule);
            resources.ApplyResources(tabMain, "tabMain");
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.SizeMode = TabSizeMode.Fixed;
            // 
            // tpObject
            // 
            tpObject.BackColor = SystemColors.Control;
            tpObject.Controls.Add(dgvObject);
            tpObject.Controls.Add(tlpObject);
            resources.ApplyResources(tpObject, "tpObject");
            tpObject.Name = "tpObject";
            tpObject.Tag = "Object";
            // 
            // dgvObject
            // 
            dgvObject.AllowUserToAddRows = false;
            dgvObject.AllowUserToDeleteRows = false;
            dgvObject.AllowUserToResizeRows = false;
            dgvObject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvObject.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvObject, "dgvObject");
            dgvObject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvObject.ContextMenuStrip = menuColFilter;
            dgvObject.Name = "dgvObject";
            dgvObject.ReadOnly = true;
            dgvObject.RowHeadersVisible = false;
            dgvObject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvObject.ShowCellErrors = false;
            dgvObject.ShowCellToolTips = false;
            dgvObject.ShowEditingIcon = false;
            dgvObject.ShowRowErrors = false;
            dgvObject.Tag = "Objects";
            dgvObject.VirtualMode = true;
            dgvObject.CellClick += dgvObject_CellClick;
            dgvObject.CellDoubleClick += dgvObject_CellDoubleClick;
            dgvObject.Sorted += dgv_Sorted;
            // 
            // menuColFilter
            // 
            menuColFilter.ImageScalingSize = new Size(20, 20);
            menuColFilter.Name = "menuColFilter";
            resources.ApplyResources(menuColFilter, "menuColFilter");
            menuColFilter.Closed += menuColFilter_Closed;
            menuColFilter.Closing += menuColFilter_Closing;
            menuColFilter.Opening += menuColFilter_Opening;
            // 
            // tlpObject
            // 
            resources.ApplyResources(tlpObject, "tlpObject");
            tlpObject.Controls.Add(btnObjOff, 2, 0);
            tlpObject.Controls.Add(btnObjOn, 1, 0);
            tlpObject.Controls.Add(numObjVal, 4, 0);
            tlpObject.Controls.Add(Label1, 3, 0);
            tlpObject.Controls.Add(lblObjVal, 5, 0);
            tlpObject.Controls.Add(btnObjToggle, 0, 0);
            tlpObject.Name = "tlpObject";
            // 
            // btnObjOff
            // 
            resources.ApplyResources(btnObjOff, "btnObjOff");
            btnObjOff.Name = "btnObjOff";
            btnObjOff.Tag = "0";
            btnObjOff.UseVisualStyleBackColor = true;
            btnObjOff.Click += btnObjOff_Click;
            // 
            // btnObjOn
            // 
            resources.ApplyResources(btnObjOn, "btnObjOn");
            btnObjOn.Name = "btnObjOn";
            btnObjOn.Tag = "1";
            btnObjOn.UseVisualStyleBackColor = true;
            btnObjOn.Click += btnObjOn_Click;
            // 
            // numObjVal
            // 
            resources.ApplyResources(numObjVal, "numObjVal");
            numObjVal.LargeChange = 10;
            numObjVal.Maximum = 100;
            numObjVal.Name = "numObjVal";
            numObjVal.SmallChange = 5;
            numObjVal.TickFrequency = 10;
            numObjVal.Scroll += numObjVal_Scroll;
            numObjVal.MouseUp += numObjVal_MouseUp;
            // 
            // Label1
            // 
            resources.ApplyResources(Label1, "Label1");
            Label1.Name = "Label1";
            // 
            // lblObjVal
            // 
            resources.ApplyResources(lblObjVal, "lblObjVal");
            lblObjVal.Name = "lblObjVal";
            // 
            // btnObjToggle
            // 
            resources.ApplyResources(btnObjToggle, "btnObjToggle");
            btnObjToggle.Name = "btnObjToggle";
            btnObjToggle.UseVisualStyleBackColor = true;
            btnObjToggle.Click += btnObjToggle_Click;
            // 
            // tpScene
            // 
            tpScene.BackColor = SystemColors.Control;
            tpScene.Controls.Add(dgvScene);
            tpScene.Controls.Add(tlpScene);
            resources.ApplyResources(tpScene, "tpScene");
            tpScene.Name = "tpScene";
            tpScene.Tag = "Scene";
            // 
            // dgvScene
            // 
            dgvScene.AllowUserToAddRows = false;
            dgvScene.AllowUserToDeleteRows = false;
            dgvScene.AllowUserToResizeRows = false;
            dgvScene.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvScene.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvScene, "dgvScene");
            dgvScene.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvScene.ContextMenuStrip = menuColFilter;
            dgvScene.MultiSelect = false;
            dgvScene.Name = "dgvScene";
            dgvScene.ReadOnly = true;
            dgvScene.RowHeadersVisible = false;
            dgvScene.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvScene.ShowCellErrors = false;
            dgvScene.ShowCellToolTips = false;
            dgvScene.ShowEditingIcon = false;
            dgvScene.ShowRowErrors = false;
            dgvScene.Tag = "Scenes";
            dgvScene.CellDoubleClick += dgvScene_CellDoubleClick;
            dgvScene.Sorted += dgv_Sorted;
            // 
            // tlpScene
            // 
            resources.ApplyResources(tlpScene, "tlpScene");
            tlpScene.Controls.Add(btnCtlScn, 0, 0);
            tlpScene.Name = "tlpScene";
            // 
            // btnCtlScn
            // 
            resources.ApplyResources(btnCtlScn, "btnCtlScn");
            tlpScene.SetColumnSpan(btnCtlScn, 2);
            btnCtlScn.Name = "btnCtlScn";
            btnCtlScn.UseVisualStyleBackColor = true;
            btnCtlScn.Click += btnCtlScn_Click;
            // 
            // tpDevice
            // 
            tpDevice.Controls.Add(dgvDevice);
            tpDevice.Controls.Add(btnDevicePoll);
            resources.ApplyResources(tpDevice, "tpDevice");
            tpDevice.Name = "tpDevice";
            tpDevice.Tag = "Device";
            tpDevice.UseVisualStyleBackColor = true;
            // 
            // dgvDevice
            // 
            dgvDevice.AllowUserToAddRows = false;
            dgvDevice.AllowUserToDeleteRows = false;
            dgvDevice.AllowUserToResizeRows = false;
            dgvDevice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDevice.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvDevice, "dgvDevice");
            dgvDevice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDevice.Name = "dgvDevice";
            dgvDevice.ReadOnly = true;
            dgvDevice.RowHeadersVisible = false;
            dgvDevice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevice.ShowCellErrors = false;
            dgvDevice.ShowCellToolTips = false;
            dgvDevice.ShowEditingIcon = false;
            dgvDevice.ShowRowErrors = false;
            dgvDevice.Tag = "Objects";
            dgvDevice.VirtualMode = true;
            dgvDevice.CellDoubleClick += dgvDevice_CellDoubleClick;
            dgvDevice.Sorted += dgv_Sorted;
            // 
            // btnDevicePoll
            // 
            resources.ApplyResources(btnDevicePoll, "btnDevicePoll");
            btnDevicePoll.Name = "btnDevicePoll";
            btnDevicePoll.UseVisualStyleBackColor = true;
            btnDevicePoll.Click += btnDevicePoll_Click;
            // 
            // tpSchedule
            // 
            tpSchedule.BackColor = SystemColors.Control;
            tpSchedule.Controls.Add(dgvSchedule);
            tpSchedule.Controls.Add(btnSchedule);
            resources.ApplyResources(tpSchedule, "tpSchedule");
            tpSchedule.Name = "tpSchedule";
            tpSchedule.Tag = "Schedule";
            // 
            // dgvSchedule
            // 
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.AllowUserToDeleteRows = false;
            dgvSchedule.AllowUserToResizeRows = false;
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSchedule.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvSchedule, "dgvSchedule");
            dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSchedule.MultiSelect = false;
            dgvSchedule.Name = "dgvSchedule";
            dgvSchedule.ReadOnly = true;
            dgvSchedule.RowHeadersVisible = false;
            dgvSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSchedule.ShowCellErrors = false;
            dgvSchedule.ShowCellToolTips = false;
            dgvSchedule.ShowEditingIcon = false;
            dgvSchedule.ShowRowErrors = false;
            // 
            // btnSchedule
            // 
            resources.ApplyResources(btnSchedule, "btnSchedule");
            btnSchedule.Name = "btnSchedule";
            btnSchedule.UseVisualStyleBackColor = true;
            btnSchedule.Click += btnSchedule_Click;
            // 
            // FrmMainTable_old
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMainTable_old";
            ShowIcon = false;
            ShowInTaskbar = false;
            menuTelLog.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            tlpTool.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            tpObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvObject).EndInit();
            tlpObject.ResumeLayout(false);
            tlpObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numObjVal).EndInit();
            tpScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvScene).EndInit();
            tlpScene.ResumeLayout(false);
            tpDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDevice).EndInit();
            tpSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            ResumeLayout(false);
        }

        #endregion
        internal ListBox lstTelLog;
        internal ContextMenuStrip menuTelLog;
        internal ToolStripMenuItem btnTelLogClear;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem btnTelLogExport;
        internal System.Windows.Forms.Timer tmPoll;
        internal TableLayoutPanel tlpMain;
        internal TableLayoutPanel tlpTool;
        internal Button btnInterface;
        internal TreeView tvArea;
        internal TabControl tabMain;
        internal TabPage tpObject;
        internal DataGridView dgvObject;
        internal TableLayoutPanel tlpObject;
        internal Button btnObjOff;
        internal Button btnObjOn;
        internal TrackBar numObjVal;
        internal Label Label1;
        internal Label lblObjVal;
        internal Button btnObjToggle;
        internal TabPage tpScene;
        internal DataGridView dgvScene;
        internal TableLayoutPanel tlpScene;
        internal Button btnCtlScn;
        internal TabPage tpDevice;
        internal DataGridView dgvDevice;
        internal Button btnDevicePoll;
        internal TabPage tpSchedule;
        internal DataGridView dgvSchedule;
        internal Button btnSchedule;
        private ContextMenuStrip menuColFilter;
        private Button btnLink;
    }
}