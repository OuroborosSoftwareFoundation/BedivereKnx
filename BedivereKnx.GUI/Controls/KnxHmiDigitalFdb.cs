using Knx.Falcon;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiDigitalFdb : KnxHmiShape
    {

        internal KnxHmiDigitalFdb(KnxHmiComponent comp)
            : base(comp)
        { }

        protected override void KnxGroup_GroupValueChanged(GroupValue? value)
        {
            if (value is null) return;
            bool isOn = value.TypedValue switch
            {
                true or > 0 => true,
                false or 0 => false,
                _ => false,
            };
            if (mapping.HasFillColorChange)
            {
                FillColor = isOn ? mapping.FillColors.Last() : mapping.FillColors.First();
            }
            if (mapping.HasStrokeColorChange)
            {
                StrokeColor = isOn ? mapping.StrokeColors.Last() : mapping.StrokeColors.First();
            }
        }

    }

}
