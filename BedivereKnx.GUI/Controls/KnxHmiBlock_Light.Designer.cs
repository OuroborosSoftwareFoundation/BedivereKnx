namespace BedivereKnx.GUI.Controls
{
    partial class KnxHmiLightBlock
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
            tlpButtons = new TableLayoutPanel();
            btnOn = new Button();
            brnOff = new Button();
            ((System.ComponentModel.ISupportInitialize)picFdb).BeginInit();
            tlpButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Location = new Point(1, 156);
            lblName.Size = new Size(195, 40);
            // 
            // picFdb
            // 
            picFdb.Dock = DockStyle.Fill;
            picFdb.Image = Resources.Images.Img_BulbNull;
            picFdb.Location = new Point(1, 1);
            picFdb.Name = "picFdb";
            picFdb.Size = new Size(135, 155);
            picFdb.SizeMode = PictureBoxSizeMode.Zoom;
            picFdb.TabIndex = 0;
            picFdb.TabStop = false;
            // 
            // tlpButtons
            // 
            tlpButtons.ColumnCount = 1;
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpButtons.Controls.Add(btnOn, 0, 0);
            tlpButtons.Controls.Add(brnOff, 0, 1);
            tlpButtons.Dock = DockStyle.Right;
            tlpButtons.Location = new Point(136, 1);
            tlpButtons.Name = "tlpButtons";
            tlpButtons.RowCount = 2;
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpButtons.Size = new Size(60, 155);
            tlpButtons.TabIndex = 2;
            // 
            // btnOn
            // 
            btnOn.BackColor = Color.Honeydew;
            btnOn.Dock = DockStyle.Fill;
            btnOn.FlatStyle = FlatStyle.Flat;
            btnOn.Location = new Point(3, 3);
            btnOn.Name = "btnOn";
            btnOn.Size = new Size(54, 71);
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
            brnOff.Location = new Point(3, 80);
            brnOff.Name = "brnOff";
            brnOff.Size = new Size(54, 72);
            brnOff.TabIndex = 1;
            brnOff.Text = "Off";
            brnOff.UseVisualStyleBackColor = false;
            brnOff.Click += BtnOff_Click;
            // 
            // KnxHmiLightBlock
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(picFdb);
            Controls.Add(tlpButtons);
            MinimumSize = new Size(100, 100);
            Name = "KnxHmiLightBlock";
            Padding = new Padding(1, 1, 0, 0);
            Size = new Size(196, 196);
            Controls.SetChildIndex(lblName, 0);
            Controls.SetChildIndex(tlpButtons, 0);
            Controls.SetChildIndex(picFdb, 0);
            ((System.ComponentModel.ISupportInitialize)picFdb).EndInit();
            tlpButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picFdb;
        private TableLayoutPanel tlpButtons;
        private Button btnOn;
        private Button brnOff;
    }
}
