namespace BedivereKnx.GUI.Forms
{

    public partial class FrmLicense : Form
    {

        //private readonly System.Timers.Timer timer = new(1000);
        private readonly System.Windows.Forms.Timer timer = new();
        private readonly string btnText;
        private int countDown = 10;

        public FrmLicense()
        {
            InitializeComponent();
            tbLicense.Text = Resources.Strings.Text_GPLv3;
            btnText = btnOK.Text;
            btnOK.Text = $"{btnText} ({countDown})";
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            //timer.AutoReset = true;
            //timer.Elapsed += Timer_Elapsed;
        }

        private void FrmLicense_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            countDown--;
            if (countDown == 0)
            {
                timer.Stop();
                btnOK.Text = $"{btnText}";
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Text = $"{btnText} ({countDown})";
                btnOK.Enabled = false;
            }
        }

        //private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        //{
        //    this.Invoke((MethodInvoker)delegate //在UI线程上进行操作，用Forms.Timer可以避免这个问题
        //    {
        //        countDown--;
        //        if (countDown == 0)
        //        {
        //            timer.Stop();
        //            btnOK.Text = $"{btnText}";
        //            btnOK.Enabled = true;
        //        }
        //        else
        //        {
        //            btnOK.Text = $"{btnText} ({countDown})";
        //            btnOK.Enabled = false;
        //        }
        //    });
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
