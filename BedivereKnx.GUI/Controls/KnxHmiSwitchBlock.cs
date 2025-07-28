using Knx.Falcon;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiSwitchBlock : UserControl, IDefaultSize
    {

        private readonly KnxLight? knxLight; //KNX对象

        public static int DefaultWidth => 200;

        public static int DefaultHeight => 200;

        internal KnxHmiSwitchBlock(KnxLight light)
        {
            knxLight = light;
            knxLight[KnxObjectPart.SwitchFeedback].GroupValueChanged += OnGroupValueChanged;
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(knxLight?.Name))
            {
                lblName.Text = knxLight?.Code;
            }
            else
            {
                lblName.Text = knxLight?.Name;
            }
        }

        private void OnGroupValueChanged(GroupValue? value)
        {
            if (value is null) return;
            picFdb.Image = value.TypedValue switch
            {
                false or 0 => Resources.Images.Img_BulbOff,
                true or > 0 => Resources.Images.Img_BulbOn,
                _ => null,
            };
        }

        private void BtnOn_Click(object sender, EventArgs e)
        {
            if (knxLight is null) return;
            knxLight.SwitchControl(true);
        }

        private void BtnOff_Click(object sender, EventArgs e)
        {
            if (knxLight is null) return;
            knxLight.SwitchControl(false);
        }

    }

}
