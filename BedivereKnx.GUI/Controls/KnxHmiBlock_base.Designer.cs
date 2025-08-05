namespace BedivereKnx.GUI.Controls
{
    partial class KnxHmiBlockBase
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
            lblName = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.BorderStyle = BorderStyle.FixedSingle;
            lblName.Dock = DockStyle.Bottom;
            lblName.Location = new Point(0, 160);
            lblName.Name = "lblName";
            lblName.Size = new Size(200, 40);
            lblName.TabIndex = 2;
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // KnxHmiBlockBase
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblName);
            Name = "KnxHmiBlockBase";
            Size = new Size(200, 200);
            ResumeLayout(false);
        }

        #endregion

        protected Label lblName;
    }
}
