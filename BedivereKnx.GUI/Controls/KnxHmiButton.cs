using System.ComponentModel;
using Knx.Falcon;
using Knx.Falcon.ApplicationData.DatapointTypes;
using Ouroboros.Hmi.Library.Mapping;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiButton : Button
    {

        internal event GroupWriteHandler? HmiWriteValue;

        internal ToolTip Tip = new();

        private readonly KnxGroup knxGroup; //KNX组
        private readonly KnxHmiMapping mapping; //绑定对象
        private int currentValueId; //当前值ID

        /// <summary>
        /// 组地址
        /// </summary>
        [Category("Mapping")]
        internal GroupAddress Address => knxGroup.Address;

        /// <summary>
        /// 数据点类型
        /// </summary>
        [Category("Mapping")]
        internal DptBase DPT => knxGroup.DPT;

        internal KnxHmiButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            MouseClick += KnxHmiButton_MouseClick;
            MouseEnter += KnxHmiButton_MouseEnter;
            MouseLeave += KnxHmiButton_MouseLeave;
        }

        internal KnxHmiButton(KnxHmiComponent comp)
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Left = comp.RawLocation.X;
            Top = comp.RawLocation.Y;
            Width = comp.RawSize.Width;
            Height = comp.RawSize.Height;
            BackColor = comp.FillColor;
            Text = comp.Text;
            ForeColor = comp.FontColor;
            Font = new Font(Font.Name, comp.FontSize);
            Visible = comp.Visible;
            knxGroup = comp.Group;
            mapping = comp.Mapping;
            MouseClick += KnxHmiButton_MouseClick;
            MouseEnter += KnxHmiButton_MouseEnter;
            MouseLeave += KnxHmiButton_MouseLeave;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void KnxHmiButton_MouseClick(object? sender, MouseEventArgs e)
        {
            if ((knxGroup is null) || (mapping is null)) return;
            GroupValue value;
            switch (mapping.ChangeType)
            {
                case HmiValueChangeType.Fixed:
                    value = mapping.Values[0];
                    break;
                case HmiValueChangeType.Toggle:
                    currentValueId += 1;
                    if (currentValueId == mapping.Values.Length) currentValueId = 0;
                    value = mapping.Values[currentValueId];
                    break;
                case HmiValueChangeType.Range:
                //待优化
                default:
                    value = mapping.Values[0];
                    break;
            }
            knxGroup.Value = value;
            KnxGroupEventArgs kge = new(null, Address);
            HmiWriteValue?.Invoke(kge, value);
        }

        private void KnxHmiButton_MouseEnter(object? sender, EventArgs e)
        {
            Tip.Show(Text, this);
        }

        private void KnxHmiButton_MouseLeave(object? sender, EventArgs e)
        {
            Tip.Hide(this);
        }

    }

}
