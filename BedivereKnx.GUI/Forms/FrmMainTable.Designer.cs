namespace BedivereKnx.GUI.Forms
{
    partial class FrmMainTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainTable));
            btnSchedule = new Button();
            tpSchedule = new TabPage();
            dgvSchedule = new DataGridView();
            menuColFilter = new ContextMenuStrip(components);
            dgvDevice = new DataGridView();
            btnDevicePoll = new Button();
            tpDevice = new TabPage();
            btnCtlScn = new Button();
            dgvScene = new DataGridView();
            tpScene = new TabPage();
            numObjVal = new TrackBar();
            Label1 = new Label();
            lblObjVal = new Label();
            btnObjToggle = new Button();
            dgvLight = new DataGridView();
            btnObjOff = new Button();
            btnObjOn = new Button();
            tlpObject = new TableLayoutPanel();
            tpLight = new TabPage();
            tvArea = new TreeView();
            tabMain = new TabControl();
            tpEnable = new TabPage();
            dgvEnable = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnEnTrue = new Button();
            btnEnFalse = new Button();
            btnInterface = new Button();
            btnLink = new Button();
            tlpTool = new TableLayoutPanel();
            tlpMain = new TableLayoutPanel();
            lstTelLog = new ListBox();
            menuTelLog = new ContextMenuStrip(components);
            btnTelLogClear = new ToolStripMenuItem();
            ToolStripSeparator1 = new ToolStripSeparator();
            btnTelLogExport = new ToolStripMenuItem();
            tmPoll = new System.Windows.Forms.Timer(components);
            tpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDevice).BeginInit();
            tpDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScene).BeginInit();
            tpScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numObjVal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLight).BeginInit();
            tlpObject.SuspendLayout();
            tpLight.SuspendLayout();
            tabMain.SuspendLayout();
            tpEnable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEnable).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tlpTool.SuspendLayout();
            tlpMain.SuspendLayout();
            menuTelLog.SuspendLayout();
            SuspendLayout();
            // 
            // btnSchedule
            // 
            resources.ApplyResources(btnSchedule, "btnSchedule");
            btnSchedule.Name = "btnSchedule";
            btnSchedule.UseVisualStyleBackColor = true;
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
            dgvSchedule.ContextMenuStrip = menuColFilter;
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
            // menuColFilter
            // 
            menuColFilter.ImageScalingSize = new Size(20, 20);
            menuColFilter.Name = "menuColFilter";
            resources.ApplyResources(menuColFilter, "menuColFilter");
            menuColFilter.Closed += menuColFilter_Closed;
            menuColFilter.Closing += menuColFilter_Closing;
            menuColFilter.Opening += menuColFilter_Opening;
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
            dgvDevice.ContextMenuStrip = menuColFilter;
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
            dgvDevice.DataBindingComplete += dgvDevice_DataBindingComplete;
            dgvDevice.SelectionChanged += dgvDevice_SelectionChanged;
            dgvDevice.Sorted += dgv_Sorted;
            // 
            // btnDevicePoll
            // 
            resources.ApplyResources(btnDevicePoll, "btnDevicePoll");
            btnDevicePoll.Name = "btnDevicePoll";
            btnDevicePoll.UseVisualStyleBackColor = true;
            btnDevicePoll.Click += btnDevicePoll_Click;
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
            // btnCtlScn
            // 
            resources.ApplyResources(btnCtlScn, "btnCtlScn");
            btnCtlScn.Name = "btnCtlScn";
            btnCtlScn.Click += btnCtlScn_Click;
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
            dgvScene.DataBindingComplete += dgvScene_DataBindingComplete;
            dgvScene.Sorted += dgv_Sorted;
            // 
            // tpScene
            // 
            tpScene.BackColor = SystemColors.Control;
            tpScene.Controls.Add(dgvScene);
            tpScene.Controls.Add(btnCtlScn);
            resources.ApplyResources(tpScene, "tpScene");
            tpScene.Name = "tpScene";
            tpScene.Tag = "Scene";
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
            numObjVal.ValueChanged += numObjVal_ValueChanged;
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
            // dgvLight
            // 
            dgvLight.AllowUserToAddRows = false;
            dgvLight.AllowUserToDeleteRows = false;
            dgvLight.AllowUserToResizeRows = false;
            dgvLight.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLight.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvLight, "dgvLight");
            dgvLight.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLight.ContextMenuStrip = menuColFilter;
            dgvLight.Name = "dgvLight";
            dgvLight.ReadOnly = true;
            dgvLight.RowHeadersVisible = false;
            dgvLight.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLight.ShowCellErrors = false;
            dgvLight.ShowCellToolTips = false;
            dgvLight.ShowEditingIcon = false;
            dgvLight.ShowRowErrors = false;
            dgvLight.Tag = "Objects";
            dgvLight.VirtualMode = true;
            dgvLight.CellDoubleClick += dgvLight_CellDoubleClick;
            dgvLight.ColumnHeaderMouseClick += dgvLight_ColumnHeaderMouseClick;
            dgvLight.DataBindingComplete += dgvLight_DataBindingComplete;
            dgvLight.SelectionChanged += dgvLight_SelectionChanged;
            dgvLight.Sorted += dgv_Sorted;
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
            // tpLight
            // 
            tpLight.BackColor = SystemColors.Control;
            tpLight.Controls.Add(dgvLight);
            tpLight.Controls.Add(tlpObject);
            resources.ApplyResources(tpLight, "tpLight");
            tpLight.Name = "tpLight";
            tpLight.Tag = "Object";
            // 
            // tvArea
            // 
            resources.ApplyResources(tvArea, "tvArea");
            tvArea.Name = "tvArea";
            tvArea.AfterSelect += tvArea_AfterSelect;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tpLight);
            tabMain.Controls.Add(tpEnable);
            tabMain.Controls.Add(tpScene);
            tabMain.Controls.Add(tpDevice);
            tabMain.Controls.Add(tpSchedule);
            resources.ApplyResources(tabMain, "tabMain");
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.SizeMode = TabSizeMode.Fixed;
            // 
            // tpEnable
            // 
            tpEnable.Controls.Add(dgvEnable);
            tpEnable.Controls.Add(tableLayoutPanel1);
            resources.ApplyResources(tpEnable, "tpEnable");
            tpEnable.Name = "tpEnable";
            tpEnable.UseVisualStyleBackColor = true;
            // 
            // dgvEnable
            // 
            dgvEnable.AllowUserToAddRows = false;
            dgvEnable.AllowUserToDeleteRows = false;
            dgvEnable.AllowUserToResizeRows = false;
            dgvEnable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEnable.BackgroundColor = SystemColors.Window;
            resources.ApplyResources(dgvEnable, "dgvEnable");
            dgvEnable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEnable.ContextMenuStrip = menuColFilter;
            dgvEnable.Name = "dgvEnable";
            dgvEnable.ReadOnly = true;
            dgvEnable.RowHeadersVisible = false;
            dgvEnable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEnable.ShowCellErrors = false;
            dgvEnable.ShowCellToolTips = false;
            dgvEnable.ShowEditingIcon = false;
            dgvEnable.ShowRowErrors = false;
            dgvEnable.Tag = "Objects";
            dgvEnable.VirtualMode = true;
            dgvEnable.CellDoubleClick += dgvEnable_CellDoubleClick;
            dgvEnable.DataBindingComplete += dgvEnable_DataBindingComplete;
            dgvEnable.SelectionChanged += dgvEnable_SelectionChanged;
            dgvEnable.Sorted += dgv_Sorted;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(btnEnTrue, 0, 0);
            tableLayoutPanel1.Controls.Add(btnEnFalse, 1, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnEnTrue
            // 
            resources.ApplyResources(btnEnTrue, "btnEnTrue");
            btnEnTrue.Name = "btnEnTrue";
            btnEnTrue.UseVisualStyleBackColor = true;
            btnEnTrue.Click += btnEnTrue_Click;
            // 
            // btnEnFalse
            // 
            resources.ApplyResources(btnEnFalse, "btnEnFalse");
            btnEnFalse.Name = "btnEnFalse";
            btnEnFalse.UseVisualStyleBackColor = true;
            btnEnFalse.Click += btnEnFalse_Click;
            // 
            // btnInterface
            // 
            resources.ApplyResources(btnInterface, "btnInterface");
            btnInterface.Name = "btnInterface";
            btnInterface.UseVisualStyleBackColor = true;
            // 
            // btnLink
            // 
            resources.ApplyResources(btnLink, "btnLink");
            btnLink.Name = "btnLink";
            btnLink.UseVisualStyleBackColor = true;
            // 
            // tlpTool
            // 
            resources.ApplyResources(tlpTool, "tlpTool");
            tlpTool.Controls.Add(btnInterface, 0, 0);
            tlpTool.Controls.Add(btnLink, 0, 1);
            tlpTool.Name = "tlpTool";
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
            // 
            // tmPoll
            // 
            tmPoll.Interval = 3000;
            // 
            // FrmMainTable
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMainTable";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += FrmMainTable_Load;
            tpSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDevice).EndInit();
            tpDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvScene).EndInit();
            tpScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numObjVal).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLight).EndInit();
            tlpObject.ResumeLayout(false);
            tlpObject.PerformLayout();
            tpLight.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            tpEnable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEnable).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tlpTool.ResumeLayout(false);
            tlpMain.ResumeLayout(false);
            menuTelLog.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Button btnSchedule;
        internal TabPage tpSchedule;
        internal DataGridView dgvSchedule;
        internal DataGridView dgvDevice;
        internal Button btnDevicePoll;
        internal TabPage tpDevice;
        internal Button btnCtlScn;
        internal DataGridView dgvScene;
        private ContextMenuStrip menuColFilter;
        internal TabPage tpScene;
        internal TrackBar numObjVal;
        internal Label Label1;
        internal Label lblObjVal;
        internal Button btnObjToggle;
        internal DataGridView dgvLight;
        internal Button btnObjOff;
        internal Button btnObjOn;
        internal TableLayoutPanel tlpObject;
        internal TabPage tpLight;
        internal TreeView tvArea;
        internal TabControl tabMain;
        internal Button btnInterface;
        private Button btnLink;
        internal TableLayoutPanel tlpTool;
        internal TableLayoutPanel tlpMain;
        internal ListBox lstTelLog;
        internal ContextMenuStrip menuTelLog;
        internal ToolStripMenuItem btnTelLogClear;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem btnTelLogExport;
        internal System.Windows.Forms.Timer tmPoll;
        private TabPage tpEnable;
        internal DataGridView dgvEnable;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnEnTrue;
        private Button btnEnFalse;
    }
}