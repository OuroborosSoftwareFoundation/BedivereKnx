namespace BedivereKnx.GUI.Forms
{
    partial class FrmAuth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAuth));
            lvAuth = new ListView();
            Prop = new ColumnHeader();
            Value = new ColumnHeader();
            btnOK = new Button();
            tlpBottom = new TableLayoutPanel();
            btnModify = new Button();
            tlpBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lvAuth
            // 
            resources.ApplyResources(lvAuth, "lvAuth");
            lvAuth.Columns.AddRange(new ColumnHeader[] { Prop, Value });
            lvAuth.FullRowSelect = true;
            lvAuth.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvAuth.MultiSelect = false;
            lvAuth.Name = "lvAuth";
            lvAuth.UseCompatibleStateImageBehavior = false;
            lvAuth.View = View.Details;
            lvAuth.MouseDoubleClick += lvAuth_MouseDoubleClick;
            // 
            // Prop
            // 
            resources.ApplyResources(Prop, "Prop");
            // 
            // Value
            // 
            resources.ApplyResources(Value, "Value");
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.Click += btnOK_Click;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnOK, 0, 1);
            tlpBottom.Controls.Add(btnModify, 0, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // btnModify
            // 
            resources.ApplyResources(btnModify, "btnModify");
            btnModify.Name = "btnModify";
            btnModify.UseVisualStyleBackColor = true;
            btnModify.Click += btnModify_Click;
            // 
            // FrmAuth
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnOK;
            Controls.Add(lvAuth);
            Controls.Add(tlpBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAuth";
            ShowIcon = false;
            ShowInTaskbar = false;
            tlpBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal ListView lvAuth;
        internal ColumnHeader Prop;
        internal ColumnHeader Value;
        internal Button btnOK;
        private TableLayoutPanel tlpBottom;
        private Button btnModify;
    }
}