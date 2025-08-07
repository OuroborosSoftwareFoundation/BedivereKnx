using System.Net.NetworkInformation;
using Knx.Falcon;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmInterface : Form
    {

        private readonly KnxInterfaceCollection inf = Globals.KnxSys!.Interfaces;

        public FrmInterface()
        {
            InitializeComponent();
            inf.ConnectionChanged += KnxConnectionChanged;
            dgvIf.BindDataTable(inf.Table,
                ["Id", "AreaCode", "InterfaceCode", "Port"]);
        }

        private void FrmInterface_Load(object sender, EventArgs e)
        {
            dgvIfColoring();
        }

        private void KnxConnectionChanged()
        {
            dgvIfColoring();
            //bool allOK = dgvIfColoring();
            //BusConnectionState dcs = inf.Default.ConnectionState; //默认接口连接状态
            //lblIfDefault.Text = dcs.ToString();
            //lblIfDefault.ForeColor = (dcs == BusConnectionState.Connected) ? Color.Green : Color.Red;
        }

        /// <summary>
        /// dgv接口变色
        /// </summary>
        private bool dgvIfColoring()
        {
            bool allOK = true;
            foreach (DataGridViewRow row in dgvIf.Rows)
            {
                if ((row.Cells["Enable"].Value is DBNull) || (bool)row.Cells["Enable"].Value == false) continue;
                if (row.Cells["NetStatus"].Value is DBNull) continue; //跳过非网络接口
                switch ((IPStatus)row.Cells["NetStatus"].Value)
                {
                    case IPStatus.Success:
                    case IPStatus.Unknown:
                        switch ((BusConnectionState)row.Cells["ConnState"].Value)
                        {
                            case BusConnectionState.Connected:
                                row.DefaultCellStyle.BackColor = Color.PaleGreen;
                                break;
                            case BusConnectionState.Broken:
                            case BusConnectionState.MediumFailure:
                                row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                                allOK = false;
                                break;
                            case BusConnectionState.Closed:
                            default:
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                                allOK = false;
                                break;
                        }
                        break;
                    default:
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        allOK = false;
                        break;
                }
            }
            BusConnectionState dcs = inf.Default.ConnectionState; //默认接口连接状态
            lblIfDefault.Text = dcs.ToString();
            lblIfDefault.ForeColor = (dcs == BusConnectionState.Connected) ? Color.Green : Color.Red;
            return allOK;
        }

        private void dgvIf_SelectionChanged(object sender, EventArgs e)
        {
            dgvIf.ClearSelection(); //不允许选中接口
        }

        private void dgvIf_Sorted(object sender, EventArgs e)
        {
            dgvIfColoring();
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvIf.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }
            inf.AllConnect(Globals.AppConfig.InitPolling); //打开全部KNX接口并初始化读取
            dgvIfColoring();
        }

        private void btnNetTest_Click(object sender, EventArgs e)
        {
            inf.NetworkTestAll();
        }

    }

}
