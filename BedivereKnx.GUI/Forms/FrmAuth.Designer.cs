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
            lvAuth = new ListView();
            Prop = new ColumnHeader();
            Value = new ColumnHeader();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lvAuth
            // 
            lvAuth.Alignment = ListViewAlignment.Default;
            lvAuth.Columns.AddRange(new ColumnHeader[] { Prop, Value });
            lvAuth.Dock = DockStyle.Fill;
            lvAuth.FullRowSelect = true;
            lvAuth.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvAuth.Location = new Point(10, 10);
            lvAuth.MultiSelect = false;
            lvAuth.Name = "lvAuth";
            lvAuth.Size = new Size(462, 203);
            lvAuth.TabIndex = 7;
            lvAuth.UseCompatibleStateImageBehavior = false;
            lvAuth.View = View.Details;
            lvAuth.MouseDoubleClick += lvAuth_MouseDoubleClick;
            // 
            // Prop
            // 
            Prop.Text = "Property";
            Prop.Width = 100;
            // 
            // Value
            // 
            Value.Text = "Value";
            Value.Width = 200;
            // 
            // btnOK
            // 
            btnOK.Dock = DockStyle.Bottom;
            btnOK.Location = new Point(10, 213);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(462, 30);
            btnOK.TabIndex = 6;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // FrmAuth
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 253);
            Controls.Add(lvAuth);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAuth";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Authorization Information";
            ResumeLayout(false);
        }

        #endregion

        internal ListView lvAuth;
        internal ColumnHeader Prop;
        internal ColumnHeader Value;
        internal Button btnOK;
    }
}