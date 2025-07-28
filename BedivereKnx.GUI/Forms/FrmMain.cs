using System.Net;
using System.Net.NetworkInformation;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmMain : Form
    {

        //private readonly System.Timers.Timer timerDoe = new(1000); //授权倒计时定时器

        /// <summary>
        /// 是否已经打开项目
        /// </summary>
        private bool ProjectOpened
        {
            get => projectOpened;
            set
            {
                if (projectOpened != value)
                {
                    projectOpened = value;
                    btnGrid.Enabled = value;
                    btnPanel.Enabled = value;
                    btnHmi.Enabled = value;
                    if (!value)
                    {
                        Globals.KS = null;
                    }
                }
            }
        }
        bool projectOpened = false;

        public FrmMain()
        {
            if (Globals.AuthInfo is null) Environment.Exit(-2); //位置情况绕过授权验证的情况退出
            InitializeComponent();
            Text = $"{Globals.AssemblyInfo.ProductName} (Ver {Globals.AssemblyInfo.Version})";
            lblAuth.Text = Globals.AuthInfo.Title;
            //timerDoe.Elapsed += TimerDoe_Elapsed;
            //timerDoe.AutoReset = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            timer.Start(); //启动时间定时器
            //timerDoe.Start(); //启动授权倒计时定时器
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            //测试KNX路由本地IP：
            bool lclIpErr = true; //KNX路由本地IP故障
            IPAddress? lclIp = Globals.AppConfig.LocalIP;
            if ((lclIp is null) || lclIp.Equals(IPAddress.Loopback))
            {
                lclIpErr = true;
            }
            else
            {
                Ping ping = new();
                PingReply reply = ping.Send(lclIp, 500); //测试通讯
                lclIpErr = (reply.Status != IPStatus.Success);
            }
#if !DEBUG
            if (lclIpErr) //KNX路由本地IP有故障的情况
            {
                DialogResult result = MessageBox.Show(string.Format(Resources.Strings.Ex_LocalIp, Globals.AppConfig.LocalIP), "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK) //选择修改IP配置的情况
                {
                    FrmNetworkInfo frmNetworkInfo = new();
                    if (frmNetworkInfo.ShowDialog() == DialogResult.OK)
                    {
                        Globals.AppConfig.LocalIP = frmNetworkInfo.SelectedIp;
                        Globals.AppConfig.Save(); //保存bendiIP设置
                    }
                }
            }
#endif
            //自动打开默认数据文件：
            string? defaultFile = Globals.AppConfig.DefaultDataFile;
            if (!string.IsNullOrWhiteSpace(defaultFile))
            {
                OpenProject(defaultFile);
            }
        }

        /// <summary>
        /// 打开项目文件的操作
        /// </summary>
        /// <param name="path"></param>
        private void OpenProject(string path)
        {
            Globals.KS = KnxSystem.FromExcel(path, Globals.AppConfig.LocalIP); //新建KNX系统对象
            
            Globals.KS.PollingStatusChanged += Knx_PollingStatusChanged;
            Globals.KS.Interfaces.ConnectionChanged += Knx_ConnectionChanged;
            Globals.KS.Interfaces.ConnectionExceptionOccurred += Knx_ConnectionExceptionOccurred;
            Globals.KS.Schedule.ScheduleTimerStateChanged += Knx_ScdTimerStateChanged;
            Globals.KS.Interfaces.AllConnect(Globals.AppConfig.InitPolling); //打开全部KNX接口并初始化读取
            Globals.KS.Schedule.TimerStart(); //启动定时器
            //CloseFormsOfType<FrmMainTable>(); //关闭全部表格子窗体
            ShowSubForm<FrmMainTable>(); //显示表格子窗体
            ProjectOpened = true; //设置为项目已打开状态
            if (!string.IsNullOrWhiteSpace(Globals.AppConfig.DefaultHmiFile))
            {
                OpenHmiFile(Globals.AppConfig.DefaultHmiFile);
            }
        }

        /// <summary>
        /// 打开图形文件
        /// </summary>
        /// <param name="path"></param>
        private static void OpenHmiFile(string path)
        {
            FrmMainHmi frmMainHmi = new(path);
            frmMainHmi.Show();
        }

        /// <summary>
        /// KNX轮询状态变化事件
        /// </summary>
        /// <param name="value">是否正在轮询</param>
        private void Knx_PollingStatusChanged(bool value)
        {
            slblPolling.Visible = value;
        }

        /// <summary>
        /// KNX定时器状态变化事件
        /// </summary>
        /// <param name="state">定时器状态</param>
        private void Knx_ScdTimerStateChanged(KnxScheduleTimerState state)
        {
            slblScdState.Text = state.ToString();
            slblScdState.ForeColor = state switch
            {
                KnxScheduleTimerState.Stoped => Color.Gray,
                KnxScheduleTimerState.Starting => Color.Blue,
                KnxScheduleTimerState.Running => Color.Green,
                _ => SystemColors.ControlText,
            };
        }

        /// <summary>
        /// KNX接口连接状态变化
        /// </summary>
        private void Knx_ConnectionChanged()
        {
            if (Globals.KS is null) return;
            KnxInterfaceCollection infs = Globals.KS.Interfaces;
            slblIfDefault.Visible = (infs.Default.ConnectionState == Knx.Falcon.BusConnectionState.Connected);
            if (infs.Count == 0) //只有默认接口的情况
            {
                slblIfDefault.Text = infs.Default.ConnectionState.ToString();
                slblIfCount.Visible = false;
            }
            else //有多个接口的情况
            {
                slblIfDefault.Text = "(+1)";
                slblIfCount.Visible = true;
                slblIfCount.Text = $"{infs.ConnectedCount}/{infs.Count}"; //已连接数量/接口数量
                slblIfCount.ForeColor = (infs.ConnectedCount == infs.Count) ? Color.Green : Color.Red;
            }
        }

        /// <summary>
        /// KNX连接异常事件
        /// </summary>
        private void Knx_ConnectionExceptionOccurred(Exception ex)
        {
            MessageBox.Show(ex.Message, "ConnectionError", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 时间显示定时器触发事件
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("F");
        }

        /// <summary>
        /// 授权检测定时器触发事件
        /// </summary>
        //private void TimerDoe_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        if (Globals.AuthInfo!.ExpiryDate.Date == DateTime.MaxValue.Date)
        //        {
        //            lblCtDn.Text = null;
        //            timerDoe.Stop(); //永久授权停止计时器
        //        }
        //        else
        //        {
        //            TimeSpan span = Globals.AuthInfo.ExpiryDate.Subtract(DateTime.Now); //计算距离授权到期的时间间隔
        //            string hex = Convert.ToInt64(span.TotalSeconds).ToString("X"); //距离到期总秒数的十六进制字符串
        //            lblCtDn.Text = hex;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Environment.Exit(-3); //到期时间解析异常
        //    }
        //    finally
        //    {
        //        timerDoe.Interval = new Random().Next(1000, 10000); //定时器间隔设为1~10秒
        //    }
        //    if (DateTime.Now.Date > Globals.AuthInfo.ExpiryDate.Date) Environment.Exit(1); //授权到期退出程序
        //}

        /// <summary>
        /// 打开项目按钮
        /// </summary>
        private void Menu_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Title = Resources.Strings.Dlg_OpenDataFile,
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                Filter = "Excel file(*.xlsx)|*.xlsx",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenProject(ofd.FileName);
            }
        }

        /// <summary>
        /// 打开面板界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPanel_Click(object sender, EventArgs e)
        {
            new FrmMainPanel().Show();
        }

        /// <summary>
        /// 打开图形界面
        /// </summary>
        private void btnHmi_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Title = Resources.Strings.Dlg_OpenHmiFile,
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                Filter = "Draw.io Diagrams(*.drawio)|*.drawio",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenHmiFile(ofd.FileName);
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        private void Menu_ToolEtsImport_Click(object sender, EventArgs e)
        {
            new FrmImport().Show();
        }

        /// <summary>
        /// 配置按钮
        /// </summary>
        private void Menu_Config_Click(object sender, EventArgs e)
        {
            new FrmConfig().ShowDialog();
        }

        /// <summary>
        /// 授权信息按钮
        /// </summary>
        private void Menu_Auth_Click(object sender, EventArgs e)
        {
            new FrmAuth().ShowDialog();
        }

        /// <summary>
        /// 关于按钮
        /// </summary>
        private void Menu_About_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }

        /// <summary>
        /// 关闭项目
        /// </summary>
        private void Menu_Close_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlMain.Controls)
            {
                if (ctrl is Form existingForm)
                {
                    existingForm.Close();
                    pnlMain.Controls.Remove(existingForm);
                }
            }
            Globals.KS = null;
        }

        /// <summary>
        /// 退出按钮
        /// </summary>
        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Github链接按钮
        /// </summary>
        private void slblGithub_Click(object sender, EventArgs e)
        {
            CommonUtilities.OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnx");
        }

        /// <summary>
        /// 显示子窗体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void ShowSubForm<T>() where T : Form, new()
        {
            //清理现有窗体：
            foreach (Control ctrl in pnlMain.Controls)
            {
                if (ctrl is T existingForm)
                {
                    existingForm.Close();
                    pnlMain.Controls.Remove(existingForm);
                }
            }
            //创建并显示新窗体：
            T form = new();
            //{
            //    //TopLevel = false,
            //    //FormBorderStyle = FormBorderStyle.None,
            //    Dock = DockStyle.Fill,
            //};
            //pnlMain.Controls.Add(form); //窗体加入panel
            form.MdiParent = this;
            form.Parent = pnlMain;
            form.BringToFront(); //窗体前置
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        /// <summary>
        /// 关闭某类型的全部窗体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void CloseFormsOfType<T>() where T : Form
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is T targetForm)
                {
                    targetForm.Close();
                }
            }
        }

    }

}
