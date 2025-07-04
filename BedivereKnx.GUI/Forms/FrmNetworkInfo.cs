using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmNetworkInfo : Form
    {

        internal IPAddress? SelectedIp;

        public FrmNetworkInfo()
        {
            InitializeComponent();
            lvNI.Items.Clear();
            lvNI.Groups.Clear();
            lvNI.Columns.Add("IpFamily", Resources.Strings.Hdr_IpFamily);
            lvNI.Columns.Add("IpAddress", Resources.Strings.Hdr_IpAddress);
            //lvNI.BeginUpdate(); //ListView开始更新操作
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                NetworkInterfaceType type = ni.NetworkInterfaceType;
                switch (type)
                {
                    case NetworkInterfaceType.Loopback: //回送地址
                    case NetworkInterfaceType.Tunnel: //隧道
                        continue;
                    default:
                        if (!lvNI.ContainsGroup(ni.Id))
                        {
                            byte[] macArry = ni.GetPhysicalAddress().GetAddressBytes();
                            string mac = string.Join(':', macArry.Select(b => b.ToString("X2")));
                            string status = ni.OperationalStatus switch
                            {
                                OperationalStatus.Up => Resources.Strings.NetInterface_Up,
                                OperationalStatus.Down => Resources.Strings.NetInterface_Down,
                                _ => ni.OperationalStatus.ToString()
                            };
                            lvNI.Groups.Add(ni.Id, $"{ni.Name}({mac}) {status}"); //组名：{网卡名}({MAC}), 连接状态
                        }
                        foreach (UnicastIPAddressInformation address in ni.GetIPProperties().UnicastAddresses)
                        {
                            string ipFamily = address.Address.AddressFamily switch //IP地址类型
                            {
                                AddressFamily.InterNetwork => "IPv4",
                                AddressFamily.InterNetworkV6 => "IPv6",
                                _ => address.Address.AddressFamily.ToString()
                            };
                            ListViewItem item = new(ipFamily); //新建ListView项
                            item.SubItems.Add(address.Address.ToString()); //新建子项，内容为IP地址
                            item.Group = lvNI.Groups[ni.Id]; //设置组
                            lvNI.Items.Add(item);
                        }
                        break;
                }
            }
            lvNI.EndUpdate();
            lvNI.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize); //自动调整列宽
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lvNI.CheckedItems.Count == 0) return;
            SelectedIp = IPAddress.Parse(lvNI.SelectedItems[0].SubItems[1].Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }

}
