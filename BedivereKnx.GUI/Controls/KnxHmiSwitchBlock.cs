using BedivereKnx.KnxSystem;
using Knx.Falcon;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiSwitchBlock : UserControl, IDefaultSize
    {

        private readonly KnxObject? knxObject; //KNX对象

        public static int DefaultWidth => 200;

        public static int DefaultHeight => 200;

        internal KnxHmiSwitchBlock(KnxObject obj)
        {
            knxObject = obj;
            knxObject[KnxObjectPart.SwitchFeedback].GroupValueChanged += OnGroupValueChanged;
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(knxObject?.Name))
            {
                lblName.Text = knxObject?.Code;
            }
            else
            {
                lblName.Text = knxObject?.Name;
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
            if (knxObject is null) return;
            knxObject.Switch(true);
        }

        private void BtnOff_Click(object sender, EventArgs e)
        {
            if (knxObject is null) return;
            knxObject.Switch(false);
        }

    }

}
