namespace BedivereKnx.GUI.Forms
{
    partial class FrmMain
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
            Application.Exit();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            slblGithub = new ToolStripStatusLabel();
            slblScdState = new ToolStripStatusLabel();
            slblScd = new ToolStripStatusLabel();
            slblPolling = new ToolStripStatusLabel();
            slblIfDefault = new ToolStripStatusLabel();
            slblIfCount = new ToolStripStatusLabel();
            slblIf = new ToolStripStatusLabel();
            stsBottom = new StatusStrip();
            slblSpring = new ToolStripStatusLabel();
            timer = new System.Windows.Forms.Timer(components);
            pnlMain = new Panel();
            lblAuth = new ToolStripLabel();
            lblCtDn = new ToolStripLabel();
            lblDateTime = new ToolStripLabel();
            btnHmi = new ToolStripButton();
            btnPanel = new ToolStripButton();
            btnGrid = new ToolStripButton();
            Menu_Exit = new ToolStripMenuItem();
            ToolStripSeparator4 = new ToolStripSeparator();
            Menu_About = new ToolStripMenuItem();
            Menu_Auth = new ToolStripMenuItem();
            ToolStripSeparator3 = new ToolStripSeparator();
            Menu_Config = new ToolStripMenuItem();
            ToolStripSeparator2 = new ToolStripSeparator();
            Menu_Close = new ToolStripMenuItem();
            ToolStripSeparator1 = new ToolStripSeparator();
            Menu_Import = new ToolStripMenuItem();
            Menu_Open = new ToolStripMenuItem();
            ToolStripDropDownButton1 = new ToolStripDropDownButton();
            Menu = new ToolStrip();
            stsBottom.SuspendLayout();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // slblGithub
            // 
            resources.ApplyResources(slblGithub, "slblGithub");
            slblGithub.IsLink = true;
            slblGithub.Name = "slblGithub";
            slblGithub.Click += slblGithub_Click;
            // 
            // slblScdState
            // 
            resources.ApplyResources(slblScdState, "slblScdState");
            slblScdState.ForeColor = Color.Gray;
            slblScdState.Name = "slblScdState";
            // 
            // slblScd
            // 
            resources.ApplyResources(slblScd, "slblScd");
            slblScd.Margin = new Padding(15, 4, 0, 2);
            slblScd.Name = "slblScd";
            // 
            // slblPolling
            // 
            resources.ApplyResources(slblPolling, "slblPolling");
            slblPolling.ForeColor = Color.Red;
            slblPolling.Margin = new Padding(10, 4, 0, 2);
            slblPolling.Name = "slblPolling";
            // 
            // slblIfDefault
            // 
            resources.ApplyResources(slblIfDefault, "slblIfDefault");
            slblIfDefault.ForeColor = Color.Green;
            slblIfDefault.Name = "slblIfDefault";
            // 
            // slblIfCount
            // 
            resources.ApplyResources(slblIfCount, "slblIfCount");
            slblIfCount.ForeColor = Color.Gray;
            slblIfCount.Name = "slblIfCount";
            // 
            // slblIf
            // 
            resources.ApplyResources(slblIf, "slblIf");
            slblIf.Name = "slblIf";
            // 
            // stsBottom
            // 
            resources.ApplyResources(stsBottom, "stsBottom");
            stsBottom.ImageScalingSize = new Size(20, 20);
            stsBottom.Items.AddRange(new ToolStripItem[] { slblIf, slblIfCount, slblIfDefault, slblPolling, slblScd, slblScdState, slblSpring, slblGithub });
            stsBottom.Name = "stsBottom";
            stsBottom.SizingGrip = false;
            // 
            // slblSpring
            // 
            resources.ApplyResources(slblSpring, "slblSpring");
            slblSpring.Name = "slblSpring";
            slblSpring.Spring = true;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // pnlMain
            // 
            resources.ApplyResources(pnlMain, "pnlMain");
            pnlMain.BackColor = Color.White;
            pnlMain.Name = "pnlMain";
            // 
            // lblAuth
            // 
            resources.ApplyResources(lblAuth, "lblAuth");
            lblAuth.Alignment = ToolStripItemAlignment.Right;
            lblAuth.BackColor = Color.Azure;
            lblAuth.Name = "lblAuth";
            // 
            // lblCtDn
            // 
            resources.ApplyResources(lblCtDn, "lblCtDn");
            lblCtDn.Alignment = ToolStripItemAlignment.Right;
            lblCtDn.BackColor = Color.Azure;
            lblCtDn.ForeColor = Color.Gray;
            lblCtDn.Name = "lblCtDn";
            // 
            // lblDateTime
            // 
            resources.ApplyResources(lblDateTime, "lblDateTime");
            lblDateTime.Alignment = ToolStripItemAlignment.Right;
            lblDateTime.Margin = new Padding(100, 1, 0, 2);
            lblDateTime.Name = "lblDateTime";
            // 
            // btnHmi
            // 
            resources.ApplyResources(btnHmi, "btnHmi");
            btnHmi.Image = Resources.Images.Icon_Graphics;
            btnHmi.Name = "btnHmi";
            btnHmi.Click += btnHmi_Click;
            // 
            // btnPanel
            // 
            resources.ApplyResources(btnPanel, "btnPanel");
            btnPanel.Image = Resources.Images.Icon_Panel;
            btnPanel.Name = "btnPanel";
            // 
            // btnGrid
            // 
            resources.ApplyResources(btnGrid, "btnGrid");
            btnGrid.Image = Resources.Images.Icon_Grid;
            btnGrid.Name = "btnGrid";
            // 
            // Menu_Exit
            // 
            resources.ApplyResources(Menu_Exit, "Menu_Exit");
            Menu_Exit.Name = "Menu_Exit";
            Menu_Exit.Click += Menu_Exit_Click;
            // 
            // ToolStripSeparator4
            // 
            resources.ApplyResources(ToolStripSeparator4, "ToolStripSeparator4");
            ToolStripSeparator4.Name = "ToolStripSeparator4";
            // 
            // Menu_About
            // 
            resources.ApplyResources(Menu_About, "Menu_About");
            Menu_About.Name = "Menu_About";
            Menu_About.Click += Menu_About_Click;
            // 
            // Menu_Auth
            // 
            resources.ApplyResources(Menu_Auth, "Menu_Auth");
            Menu_Auth.Name = "Menu_Auth";
            Menu_Auth.Click += Menu_Auth_Click;
            // 
            // ToolStripSeparator3
            // 
            resources.ApplyResources(ToolStripSeparator3, "ToolStripSeparator3");
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            // 
            // Menu_Config
            // 
            resources.ApplyResources(Menu_Config, "Menu_Config");
            Menu_Config.Name = "Menu_Config";
            Menu_Config.Click += Menu_Config_Click;
            // 
            // ToolStripSeparator2
            // 
            resources.ApplyResources(ToolStripSeparator2, "ToolStripSeparator2");
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            // 
            // Menu_Close
            // 
            resources.ApplyResources(Menu_Close, "Menu_Close");
            Menu_Close.Name = "Menu_Close";
            Menu_Close.Click += Menu_Close_Click;
            // 
            // ToolStripSeparator1
            // 
            resources.ApplyResources(ToolStripSeparator1, "ToolStripSeparator1");
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            // 
            // Menu_Import
            // 
            resources.ApplyResources(Menu_Import, "Menu_Import");
            Menu_Import.Name = "Menu_Import";
            // 
            // Menu_Open
            // 
            resources.ApplyResources(Menu_Open, "Menu_Open");
            Menu_Open.Name = "Menu_Open";
            Menu_Open.Click += Menu_Open_Click;
            // 
            // ToolStripDropDownButton1
            // 
            resources.ApplyResources(ToolStripDropDownButton1, "ToolStripDropDownButton1");
            ToolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ToolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { Menu_Open, Menu_Import, ToolStripSeparator1, Menu_Close, ToolStripSeparator2, Menu_Config, ToolStripSeparator3, Menu_Auth, Menu_About, ToolStripSeparator4, Menu_Exit });
            ToolStripDropDownButton1.Image = Resources.Images.Logo_32x24;
            ToolStripDropDownButton1.Margin = new Padding(0, 1, 5, 2);
            ToolStripDropDownButton1.Name = "ToolStripDropDownButton1";
            ToolStripDropDownButton1.Padding = new Padding(10, 0, 0, 0);
            // 
            // Menu
            // 
            resources.ApplyResources(Menu, "Menu");
            Menu.GripStyle = ToolStripGripStyle.Hidden;
            Menu.ImageScalingSize = new Size(20, 20);
            Menu.Items.AddRange(new ToolStripItem[] { ToolStripDropDownButton1, btnGrid, btnPanel, btnHmi, lblDateTime, lblCtDn, lblAuth });
            Menu.Name = "Menu";
            Menu.RenderMode = ToolStripRenderMode.System;
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Controls.Add(stsBottom);
            Controls.Add(Menu);
            IsMdiContainer = true;
            Name = "FrmMain";
            FormClosed += FrmMain_FormClosed;
            Load += FrmMain_Load;
            Shown += FrmMain_Shown;
            stsBottom.ResumeLayout(false);
            stsBottom.PerformLayout();
            Menu.ResumeLayout(false);
            Menu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal ToolStripStatusLabel slblGithub;
        internal ToolStripStatusLabel slblScdState;
        internal ToolStripStatusLabel slblScd;
        internal ToolStripStatusLabel slblPolling;
        internal ToolStripStatusLabel slblIfDefault;
        internal ToolStripStatusLabel slblIfCount;
        internal ToolStripStatusLabel slblIf;
        internal StatusStrip stsBottom;
        internal ToolStripStatusLabel slblSpring;
        internal System.Windows.Forms.Timer timer;
        internal Panel pnlMain;
        internal ToolStripLabel lblAuth;
        internal ToolStripLabel lblCtDn;
        internal ToolStripLabel lblDateTime;
        internal ToolStripButton btnHmi;
        internal ToolStripButton btnPanel;
        internal ToolStripButton btnGrid;
        internal ToolStripMenuItem Menu_Exit;
        internal ToolStripSeparator ToolStripSeparator4;
        internal ToolStripMenuItem Menu_About;
        internal ToolStripMenuItem Menu_Auth;
        internal ToolStripSeparator ToolStripSeparator3;
        internal ToolStripMenuItem Menu_Config;
        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripMenuItem Menu_Close;
        internal ToolStripSeparator ToolStripSeparator1;
        internal ToolStripMenuItem Menu_Import;
        internal ToolStripMenuItem Menu_Open;
        internal ToolStripDropDownButton ToolStripDropDownButton1;
        internal ToolStrip Menu;
    }
}