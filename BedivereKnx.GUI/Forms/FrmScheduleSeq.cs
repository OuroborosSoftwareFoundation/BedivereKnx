namespace BedivereKnx.GUI.Forms
{

    public partial class FrmScheduleSeq : Form
    {

        public FrmScheduleSeq()
        {
            InitializeComponent();
            dgvScd.BindDataTable(Globals.KnxSys!.Schedule.Sequence.Table, ["Enable"]);
        }

    }

}
