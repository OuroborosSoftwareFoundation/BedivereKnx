using System.ComponentModel;
using Knx.Falcon;
using Ouroboros.Hmi.Library.Elements;

namespace BedivereKnx.GUI.Controls
{

    internal abstract partial class KnxHmiShape : Control
    {

        internal event ValueChangeHandler<GroupValue>? GroupValueChanged;

        internal ToolTip Tip = new();

        protected internal readonly KnxGroup knxGroup; //KNX组
        protected internal readonly KnxHmiMapping mapping; //绑定对象

        /// <summary>
        /// 控件原始尺寸
        /// </summary>
        [Browsable(false)]
        internal Size RawSize
        {
            get => rawSize;
            set
            {
                rawSize = value;
                Invalidate();
            }
        }
        private Size rawSize;

        /// <summary>
        /// 形状
        /// </summary>
        [Category("Appearance")]
        internal HmiShapeType Shape
        {
            get => shape;
            set
            {
                shape = value;
                Invalidate();
            }
        }
        private HmiShapeType shape = HmiShapeType.Ellipse;

        internal byte Opacity
        {
            get => opacity;
            set
            {
                opacity = Math.Max((byte)0, value);
                Invalidate();
            }
        }
        private byte opacity = 255;

        /// <summary>
        /// 填充颜色
        /// </summary>
        [Category("Appearance")]
        internal Color FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                Invalidate();
            }
        }
        private Color fillColor;

        /// <summary>
        /// 填充颜色
        /// </summary>
        [Category("Appearance")]
        internal Color StrokeColor
        {
            get => strokeColor;
            set
            {
                strokeColor = value;
                Invalidate();
            }
        }
        private Color strokeColor;

        /// <summary>
        /// 线条宽度
        /// </summary>
        [Category("Appearance")]
        internal int StrokeWidth
        {
            get => strokeWidth;
            set
            {
                strokeWidth = Math.Max(0, value);
                Invalidate();
            }
        }
        private int strokeWidth = 0;

        internal KnxHmiShape(KnxHmiComponent comp)
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Padding = new((int)Math.Ceiling(StrokeWidth / 2.0) + 0); //内边距，防止绘制不完整
            Left = comp.RawLocation.X - Padding.All;
            Top = comp.RawLocation.Y - Padding.All;
            Width = comp.RawSize.Width + Padding.All * 2;
            Height = comp.RawSize.Height + Padding.All * 2;
            RawSize = comp.RawSize;
            Shape = comp.Shape;
            FillColor = comp.FillColor;
            strokeColor = comp.StrokeColor;
            strokeWidth = comp.StrokeWidth;
            Text = comp.Text;
            Visible = true;
            mapping = comp.Mapping;
            knxGroup = comp.Group;
            MouseEnter += KnxHmiButton_MouseEnter;
            MouseLeave += KnxHmiButton_MouseLeave;
            knxGroup.GroupValueChanged += KnxGroup_GroupValueChanged;
            KnxGroup_GroupValueChanged(knxGroup.Value); //初始化时调用值变化事件
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics; //创建一个Graphics对象
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //反锯齿
            SolidBrush brush = new(FillColor); //绘制形状实体作为反馈
            Pen pen = new(StrokeColor, StrokeWidth); //绘制形状边框作为控制
            switch (Shape)
            {
                case HmiShapeType.Ellipse: //椭圆
                    Rectangle ellipseFrame = new(Padding.All + 1, Padding.All + 1, RawSize.Width - 3, RawSize.Height - 3);
                    if (FillColor.A > 0) g.FillEllipse(brush, ellipseFrame); //绘制椭圆
                    if (StrokeWidth > 0) g.DrawEllipse(pen, ellipseFrame); //绘制边框
                    break;
                case HmiShapeType.Rectangle: //矩形
                case HmiShapeType.Text: //文本
                    Rectangle rectangleFrame = new(Padding.All + 1, Padding.All + 1, RawSize.Width - 3, RawSize.Height - 3);
                    if (FillColor.A > 0) g.FillRectangle(brush, rectangleFrame); //绘制矩形
                    if (StrokeWidth > 0) g.DrawRectangle(pen, rectangleFrame); //绘制边框
                    break;
                default: //其他情况抛出异常
                    throw new Exception($"HmiShapeType '{Shape}' is not supported in current version.");
            }
            brush.Dispose();
            pen.Dispose();
            base.OnPaint(pe);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
            //不调用base.OnPaintBackground，从而阻止绘制背景
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate(); //重新绘制控件以应用新的大小
        }

        private void KnxHmiButton_MouseEnter(object? sender, EventArgs e)
        {
            Tip.Show(Text, this);
        }

        private void KnxHmiButton_MouseLeave(object? sender, EventArgs e)
        {
            Tip.Hide(this);
        }

        protected abstract void KnxGroup_GroupValueChanged(GroupValue? value);

    }

}
