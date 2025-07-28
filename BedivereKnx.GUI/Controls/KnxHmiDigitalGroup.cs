using System.ComponentModel;
using Knx.Falcon;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiDigitalGroup : Control
    {

        private readonly Button btn = new();

        private readonly Dictionary<GroupValue, Color> mappingColors = []; //颜色绑定字典

        /// <summary>
        /// 反馈框宽度
        /// </summary>
        internal int FeedbackWidth
        {
            get => feedbackWidth;
            set
            {
                feedbackWidth = Math.Max(0, value);
                Invalidate();
            }
        }
        private int feedbackWidth = 15;

        /// <summary>
        /// 反馈颜色
        /// </summary>
        [Category("Appearance")]
        internal Color FeedbackColor
        {
            get => feedbackColor;
            set
            {
                feedbackColor = value;
                Invalidate();
            }
        }
        private Color feedbackColor = Color.Purple;

        /// <summary>
        /// KNX对象
        /// </summary>
        internal KnxObject? KnxObject
        {
            get => knxObject;
            set
            {
                if (value is null) return;
                knxObject = value;
                KnxObject![KnxObjectPart.SwitchFeedback].GroupValueChanged += KnxHmiDigitalGroup_GroupValueChanged;
                Invalidate(); //重绘控件
            }
        }
        private KnxObject? knxObject;

        internal KnxHmiDigitalGroup(KnxHmiComponent comp)
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); //背景设置为透明
            BackColor = Color.Transparent; //背景色
            Location = comp.RawLocation; //位置
            Size = comp.RawSize; //尺寸
            mappingColors = [];
            for (int i = 0; i < comp.Mapping.Values.Length; i++)
            {
                mappingColors.Add(comp.Mapping.Values[i], comp.Mapping.FillColors[i]);
            }
            btn = new()
            {
                Dock = DockStyle.Right,
                Width = comp.RawSize.Width - FeedbackWidth,
                //BackColor = SystemColors.Control,//comp.FillColor,
                ForeColor = comp.FontColor,
                Text = comp.Text,
                Visible = true,
            }; //设置按钮属性
            btn.Font = new Font(btn.Font.Name, comp.FontSize); //按钮字体
            btn.Click += Btn_Click;
            Controls.Add(btn); //向控件中添加按钮
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (FeedbackColor.A > 0)
            {
                Graphics g = pe.Graphics; //创建一个Graphics对象
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //反锯齿
                SolidBrush brush = new(feedbackColor); //绘制形状实体作为反馈
                Rectangle frame = new(0, 0, FeedbackWidth, Height); //边框
                g.FillRectangle(brush, frame); //绘制矩形
                brush.Dispose();
                if (KnxObject is not null)
                {
                    KnxHmiDigitalGroup_GroupValueChanged(KnxObject[KnxObjectPart.SwitchFeedback].Value); //调用值变化事件
                }
            }
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            if (KnxObject is KnxLight light)
            {
                light?.SwitchControl(); //开关切换
            }
        }

        /// <summary>
        /// 值变化事件
        /// </summary>
        /// <param name="value"></param>
        private void KnxHmiDigitalGroup_GroupValueChanged(GroupValue? value)
        {
            if (value is null) return;
            feedbackColor = mappingColors[value];
        }

    }

}
