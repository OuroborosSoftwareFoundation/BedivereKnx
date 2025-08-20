namespace BedivereKnx.GUI.Forms
{
    partial class FrmNetworkInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNetworkInfo));
            btnOK = new Button();
            tlpBottom = new TableLayoutPanel();
            btnCancel = new Button();
            lvNI = new ListView();
            tlpBottom.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(tlpBottom, "tlpBottom");
            tlpBottom.Controls.Add(btnOK, 0, 0);
            tlpBottom.Controls.Add(btnCancel, 2, 0);
            tlpBottom.Name = "tlpBottom";
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lvNI
            // 
            resources.ApplyResources(lvNI, "lvNI");
            lvNI.FullRowSelect = true;
            lvNI.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvNI.MultiSelect = false;
            lvNI.Name = "lvNI";
            lvNI.Sorting = SortOrder.Ascending;
            lvNI.UseCompatibleStateImageBehavior = false;
            lvNI.View = View.Details;
            // 
            // FrmNetworkInfo
            // 
            AcceptButton = btnOK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(tlpBottom);
            Controls.Add(lvNI);
            Name = "FrmNetworkInfo";
            TopMost = true;
            tlpBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal Button btnOK;
        internal TableLayoutPanel tlpBottom;
        internal Button btnCancel;
        internal ListView lvNI;
    }
}