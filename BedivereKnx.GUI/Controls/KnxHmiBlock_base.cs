using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    public partial class KnxHmiBlockBase : UserControl, IDefaultSize
    {

        public static int DefaultWidth => 200;

        public static int DefaultHeight => 200;

        public KnxHmiBlockBase()
        {
            InitializeComponent();
        }

        protected KnxHmiBlockBase(KnxObject obj)
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(obj?.Name))
            {
                lblName.Text = obj?.Code;
            }
            else
            {
                lblName.Text = obj?.Name;
            }
        }

    }

}
