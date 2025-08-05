using Knx.Falcon;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiLightBlock : KnxHmiBlockBase
    {

        private readonly KnxLight? knxLight; //KNX对象

        internal KnxHmiLightBlock(KnxLight light)
            : base(light)
        {
            knxLight = light;
            knxLight[KnxObjectPart.SwitchFeedback].GroupValueChanged += OnGroupValueChanged;
            InitializeComponent();
            //if (string.IsNullOrWhiteSpace(knxLight?.Name))
            //{
            //    lblName.Text = knxLight?.Code;
            //}
            //else
            //{
            //    lblName.Text = knxLight?.Name;
            //}
        }

        private void OnGroupValueChanged(GroupValue? value)
        {
            if (value is null) //值为空的情况
            {
                picFdb.Image = Resources.Images.Img_BulbNull;
                return;
            }
            picFdb.Image = value.TypedValue switch
            {
                false or 0 => Resources.Images.Img_BulbOff,
                true or > 0 => Resources.Images.Img_BulbOn,
                _ => Resources.Images.Img_BulbNull,
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
