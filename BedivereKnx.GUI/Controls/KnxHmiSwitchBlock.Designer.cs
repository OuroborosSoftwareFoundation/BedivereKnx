namespace BedivereKnx.GUI.Controls
{
    partial class KnxHmiSwitchBlock
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            picFdb = new PictureBox();
            lblName = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnOn = new Button();
            brnOff = new Button();
            ((System.ComponentModel.ISupportInitialize)picFdb).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // picFdb
            // 
            picFdb.Dock = DockStyle.Fill;
            picFdb.Image = Resources.Images.Img_BulbOn;
            picFdb.Location = new Point(0, 0);
            picFdb.Name = "picFdb";
            picFdb.Size = new Size(136, 156);
            picFdb.SizeMode = PictureBoxSizeMode.Zoom;
            picFdb.TabIndex = 0;
            picFdb.TabStop = false;
            // 
            // lblName
            // 
            lblName.BorderStyle = BorderStyle.FixedSingle;
            lblName.Dock = DockStyle.Bottom;
            lblName.Location = new Point(0, 156);
            lblName.Name = "lblName";
            lblName.Size = new Size(196, 40);
            lblName.TabIndex = 1;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(btnOn, 0, 0);
            tableLayoutPanel1.Controls.Add(brnOff, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Right;
            tableLayoutPanel1.Location = new Point(136, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(60, 156);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // btnOn
            // 
            btnOn.BackColor = Color.Honeydew;
            btnOn.Dock = DockStyle.Fill;
            btnOn.FlatStyle = FlatStyle.Flat;
            btnOn.Location = new Point(3, 3);
            btnOn.Name = "btnOn";
            btnOn.Size = new Size(54, 72);
            btnOn.TabIndex = 0;
            btnOn.Text = "On";
            btnOn.UseVisualStyleBackColor = false;
            btnOn.Click += BtnOn_Click;
            // 
            // brnOff
            // 
            brnOff.BackColor = Color.MistyRose;
            brnOff.Dock = DockStyle.Fill;
            brnOff.FlatStyle = FlatStyle.Flat;
            brnOff.Location = new Point(3, 81);
            brnOff.Name = "brnOff";
            brnOff.Size = new Size(54, 72);
            brnOff.TabIndex = 1;
            brnOff.Text = "Off";
            brnOff.UseVisualStyleBackColor = false;
            brnOff.Click += BtnOff_Click;
            // 
            // KnxHmiSwitchBlock
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(picFdb);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(lblName);
            MinimumSize = new Size(100, 100);
            Name = "KnxHmiSwitchBlock";
            Size = new Size(196, 196);
            ((System.ComponentModel.ISupportInitialize)picFdb).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picFdb;
        private Label lblName;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnOn;
        private Button brnOff;
    }
}
