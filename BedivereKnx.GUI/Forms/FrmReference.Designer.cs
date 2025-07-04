namespace BedivereKnx.GUI.Forms
{
    partial class FrmReference
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
            lvRef = new ListView();
            Assy = new ColumnHeader();
            Ver = new ColumnHeader();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lvRef
            // 
            lvRef.Alignment = ListViewAlignment.Default;
            lvRef.Columns.AddRange(new ColumnHeader[] { Assy, Ver });
            lvRef.Dock = DockStyle.Fill;
            lvRef.FullRowSelect = true;
            lvRef.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvRef.Location = new Point(10, 10);
            lvRef.MultiSelect = false;
            lvRef.Name = "lvRef";
            lvRef.Size = new Size(562, 303);
            lvRef.Sorting = SortOrder.Ascending;
            lvRef.TabIndex = 9;
            lvRef.UseCompatibleStateImageBehavior = false;
            lvRef.View = View.Details;
            // 
            // Assy
            // 
            Assy.Text = "Assembly Name";
            Assy.Width = 150;
            // 
            // Ver
            // 
            Ver.Text = "Version";
            Ver.Width = 150;
            // 
            // btnOK
            // 
            btnOK.Dock = DockStyle.Bottom;
            btnOK.Location = new Point(10, 313);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(562, 30);
            btnOK.TabIndex = 8;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // FrmReference
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 353);
            Controls.Add(lvRef);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmReference";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Referenced Assemblies";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        internal ListView lvRef;
        internal ColumnHeader Assy;
        internal ColumnHeader Ver;
        internal Button btnOK;
    }
}