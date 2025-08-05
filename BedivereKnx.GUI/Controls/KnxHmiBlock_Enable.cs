using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    public partial class KnxHmiEnableBlock : KnxHmiBlockBase
    {

        private readonly KnxEnablement? knxEn;

        public KnxHmiEnableBlock(KnxEnablement en)
            : base(en)
        {
            knxEn = en;
            knxEn[KnxObjectPart.SwitchControl].GroupValueChanged += OnGroupValueChanged;
            InitializeComponent();
        }

        private void OnGroupValueChanged(Knx.Falcon.GroupValue? value)
        {
            if (knxEn is null || value is null)
            {
                chkEn.BackgroundImage = Resources.Images.Img_EnNull;
                return;
            }
            switch (knxEn.EnablementType)
            {
                case KnxEnablementType.General:
                case KnxEnablementType.Enable:
                    chkEn.BackgroundImage = value.TypedValue switch
                    {
                        false or 0 => Resources.Images.Img_EnOff,
                        true or > 0 => Resources.Images.Img_EnOn,
                        _ => Resources.Images.Img_EnNull,
                    };
                    break;
                case KnxEnablementType.Lock:
                    chkEn.BackgroundImage = value.TypedValue switch
                    {
                        false or 0 => Resources.Images.Img_EnUnlock,
                        true or > 0 => Resources.Images.Img_EnLock,
                        _ => Resources.Images.Img_EnNull,
                    };
                    break;
                default:
                    break;
            }
        }

        private void chkEn_CheckedChanged(object sender, EventArgs e)
        {
            if (knxEn is null) return;
            knxEn.EnableControl(chkEn.Checked);
        }

    }

}
