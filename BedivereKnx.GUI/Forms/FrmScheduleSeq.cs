using BedivereKnx.GUI.Extensions;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmScheduleSeq : Form
    {

        public FrmScheduleSeq()
        {
            InitializeComponent();
            dgvScd.BindDataTable(Globals.KS!.Schedule.Sequence.Table, ["Enable"]);
        }

    }

}
