namespace BedivereKnx.GUI.Controls
{
    partial class KnxHmiEnableBlock
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
            chkEn = new CheckBox();
            SuspendLayout();
            // 
            // chkEn
            // 
            chkEn.Appearance = Appearance.Button;
            chkEn.BackgroundImage = Resources.Images.Img_EnNull;
            chkEn.BackgroundImageLayout = ImageLayout.Zoom;
            chkEn.Dock = DockStyle.Fill;
            chkEn.Location = new Point(0, 0);
            chkEn.Name = "chkEn";
            chkEn.Size = new Size(200, 160);
            chkEn.TabIndex = 3;
            chkEn.UseVisualStyleBackColor = true;
            chkEn.CheckedChanged += chkEn_CheckedChanged;
            // 
            // KnxHmiEnableBlock
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chkEn);
            Name = "KnxHmiEnableBlock";
            Controls.SetChildIndex(lblName, 0);
            Controls.SetChildIndex(chkEn, 0);
            ResumeLayout(false);
        }

        #endregion

        private CheckBox chkEn;
    }
}
