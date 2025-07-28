using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using Knx.Falcon;
using Knx.Falcon.Configuration;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX接口集合
    /// </summary>
    public class KnxInterfaceCollection : IEnumerable<KnxInterface>
    {

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<int, KnxInterface> items = [];

        /// <summary>
        /// 组地址轮询事件
        /// </summary>
        protected internal event Action? GroupPollRequest;

        /// <summary>
        /// 接口连接状态变化事件
        /// </summary>
        public event Action? ConnectionChanged;

        /// <summary>
        /// 接口连接故障事件
        /// </summary>
        public event Action<Exception>? ConnectionExceptionOccurred;

        /// <summary>
        /// 组地址报文接收事件
        /// </summary>
        public event KnxMessageHandler? GroupMessageReceived;

        /// <summary>
        /// 总线就绪状态
        /// </summary>
        public bool Ready { get; set; } = false;

        /// <summary>
        /// 默认接口
        /// </summary>
        public KnxInterface Default { get; }

        /// <summary>
        /// 数据表
        /// </summary>
        public readonly DataTable Table;

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// 已连接数量
        /// </summary>
        public int ConnectedCount
        {
            get
            {
                return items.Values.Count(inf => inf.ConnectionState == BusConnectionState.Connected);
                //int count = 0;
                //foreach (KnxInterface inf in items.Values)
                //{
                //    if (inf.ConnectionState == BusConnectionState.Connected)
                //    {
                //        count += 1;
                //    }
                //}
                //return count;
            }
        }

        /// <summary>
        /// 新建KNX接口集合
        /// </summary>
        /// <param name="localIp">本地IP（用于IP路由接口）</param>
        public KnxInterfaceCollection(IPAddress localIp)
        {
            Table = new DataTable();
            Table.Columns.Add("NetStatus", ResString.Hdr_NetStatus, typeof(IPStatus)); //网络状态
            Table.Columns.Add("ConnState", ResString.Hdr_ConnState, typeof(BusConnectionState)); //接口连接状态
            //Table.Columns.Add(new DataColumn("NetStatus", typeof(IPStatus)) //网络状态
            //{
            //    Caption = ResString.Hdr_NetStatus
            //});
            //Table.Columns.Add(new DataColumn("ConnState", typeof(BusConnectionState)) //接口连接状态
            //{
            //    Caption = ResString.Hdr_ConnState
            //});
            //添加默认接口
            Default = new KnxInterface($"Type=IpRouting;LocalIPAddress={localIp}")
            {
                Id = -1,
                Code = "Default",
                Name = "DefaultIpRouter",
                InterfaceType = ConnectorType.IpRouting,
                Address = localIp,
                Port = 3671,
                Enable = true
            }; //默认接口
            Default.Bus.ConnectionStateChanged += OnConnectionChanged;
            Default.Bus.GroupMessageReceived += OnGroupMessageReceived;
        }

        /// <summary>
        /// 新建KNX接口集合
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="localIp">本地IP（用于IP路由接口）</param>
        /// <exception cref="NoNullAllowedException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public KnxInterfaceCollection(DataTable dt, IPAddress localIp)
        {
            Table = dt;
            Table.Columns.Add("NetStatus", ResString.Hdr_NetStatus, typeof(IPStatus)); //网络状态
            Table.Columns.Add("ConnState", ResString.Hdr_ConnState, typeof(BusConnectionState)); //接口连接状态

            //表格中的接口
            foreach (DataRow dr in dt.Rows)
            {
                int id = dr.Field<int>("Id");
                //_ = bool.TryParse(dr.Field<string>("Enable"), out bool enable); //启用
                bool enable = dr.Field<bool>("Enable");
                string? ifCode = dr.Field<string>("InterfaceCode"); //接口编号
                if (string.IsNullOrWhiteSpace(ifCode)) //接口编号为空的情况
                    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "InterfaceCode", $"Id = {id}"));
                //string? ifType = dr.Field<string>("InterfaceType"); //接口类型
                //if (string.IsNullOrEmpty(ifType)) //接口类型为空的情况
                //    throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "InterfaceType", $"Id = {id}"));
                ConnectorType ifType = dr.Field<ConnectorType>("InterfaceType"); //接口类型
                string? ifAddress = dr.Field<string>("InterfaceAddress"); //接口地址
                IPAddress? ipAddress = null; //接口IP地址
                //string? ifPortText = dr.Field<string>("Port");
                //if (!int.TryParse(ifPortText, out int ifPort)) //端口
                //    throw new FormatException(string.Format(ResString.ExMsg_KnxPortInvalid, ifPortText, ifCode));
                int ifPort = dr.Field<int>("Port"); //端口
                ConnectorParameters cp; //连接参数的基类
                switch (ifType)
                {
                    case ConnectorType.Usb: //USB接口
                        cp = new UsbConnectorParameters(); //自动获取目前USB接口
                        break;
                    case ConnectorType.IpTunneling: //IP隧道
                        if (IPAddress.TryParse(ifAddress, out ipAddress))
                        {
                            cp = new IpTunnelingConnectorParameters(ifAddress, ifPort);
                        }
                        else //其他情况报错
                        {
                            throw new FormatException($"Invalid IP address: {ifAddress}");
                        }
                        break;
                    case ConnectorType.IpRouting: //IP路由
                        if (IPAddress.TryParse(ifAddress, out ipAddress))
                        {
                            if (ipAddress == IpRoutingConnectorParameters.DefaultMulticastAddress && ifPort == IpRoutingConnectorParameters.DefaultIpPort)
                            {
                                continue; //表中有默认路由接口的情况，跳过，用默认接口代替
                            }
                            else
                            {
                                cp = new IpRoutingConnectorParameters(ipAddress)
                                {
                                    LocalIPAddress = localIp //路由接口中的本地IP
                                };
                            }
                        }
                        else //其他情况报错
                        {
                            throw new FormatException($"Invalid IP address: {ifAddress}");
                        }
                        break;
                    default: //其他情况报错
                        throw new InvalidEnumArgumentException(string.Format(ResString.ExMsg_KnxInterfaceTypeInvalid, ifType));
                }
                cp.Name = ifCode; //接口名称
                cp.AutoReconnect = true; //自动重连
                KnxInterface inf = new(cp)
                {
                    Id = id,
                    Code = cp.Name,
                    Name = dr.Field<string>("InterfaceName"),
                    InterfaceType = cp.Type,
                    Address = ipAddress,//(ipAddress is null) ? ipAddress : null,
                    Port = ifPort,
                    Enable = enable
                };
                inf.Bus.ConnectionStateChanged += OnConnectionChanged;
                inf.Bus.GroupMessageReceived += OnGroupMessageReceived;
                dr["NetStatus"] = IPStatus.Unknown;
                dr["ConnState"] = inf.ConnectionState; //初始化连接状态
                items.Add(id, inf); //索引器字典加入一项
            }

            //添加默认接口
            Default = new KnxInterface($"Type=IpRouting;LocalIPAddress={localIp}")
            {
                Id = -1,
                Code = "Default",
                Name = "DefaultIpRouter",
                InterfaceType = ConnectorType.IpRouting,
                Address = localIp,
                Port = 3671,
                Enable = true
            }; //默认接口
            Default.Bus.ConnectionStateChanged += OnConnectionChanged;
            Default.Bus.GroupMessageReceived += OnGroupMessageReceived;
        }

        /// <summary>
        /// 索引器（根据ID）
        /// </summary>
        /// <param name="index">接口ID</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public KnxInterface this[int index]
        {
            get
            {
                if (items.TryGetValue(index, out KnxInterface? bus))
                {
                    return bus;
                }
                else
                {
                    return Default; //找不到接口编号的情况下直接引用默认接口
                }
            }
        }

        /// <summary>
        /// 索引器（根据编号）
        /// </summary>
        /// <param name="code">接口编号</param>
        /// <returns></returns>
        public KnxInterface this[string? code]
        {
            get
            {
                var result = items.Values.Where(inf => inf.Code == code);
                if (result.Any())
                {
                    return result.First(); //正常情况只会找到一个接口
                }
                else
                {
                    return Default; //找不到接口编号的情况下直接引用默认接口
                }
            }
            //DataRow[] drs = Table.Select($"InterfaceCode='{code}'"); //在表中按照InterfaceCode查询
            //if (drs.Length > 0)
            //{
            //    return this[drs[0].Field<int>("Id")]; //返回第一个接口
            //}
            //else
            //{
            //    return Default; //找不到接口编号的情况下直接引用默认接口
            //}
        }

        /// <summary>
        /// 打开全部接口
        /// </summary>
        /// <param name="poll">打开后轮询全部组地址</param>
        public void AllConnect(bool poll = false)
        {
            Thread thread = new(() => AllConnect_Internal(poll));
            thread.Start();
        }

        private async void AllConnect_Internal(bool poll = false)
        {
            try
            {
                Ready = false;
                if (Default.ConnectionState == BusConnectionState.Closed)
                {
                    await Default.Bus.ConnectAsync();
                }
                foreach (KnxInterface inf in this) //遍历全部KnxInterface对象
                {
                    if (!inf.Enable) continue; //跳过禁用的接口
                    if (inf.ConnectionState == BusConnectionState.Closed) //只处理关闭的接口
                    {
                        if (inf.InterfaceType == ConnectorType.IpTunneling) //IP隧道接口
                        {
                            if (inf.Address is null) continue; //跳过无地址的接口
                            Ping ping = new();
                            PingReply reply = ping.Send(inf.Address, 100); //Ping接口确认网络状态
                            inf.NetStatus = reply.Status;
                            Table.Rows[inf.Id]["NetStatus"] = inf.NetStatus;
                            ConnectionChanged?.Invoke();
                            if (reply.Status == IPStatus.Success)
                            {
                                await inf.Bus.ConnectAsync(); //异步方式打开接口
                            }
                        }
                        else //其他类型接口的情况
                        {
                            await inf.Bus.ConnectAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConnectionExceptionOccurred?.Invoke(ex); //触发连接异常事件
            }
            finally
            {
                Ready = true;
                if (poll) GroupPollRequest?.Invoke(); //需要轮询的情况触发轮询请求
            }
        }

        /// <summary>
        /// 连接状态变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionChanged(object? sender, EventArgs e)
        {
            foreach (DataRow dr in Table.Rows) //遍历接口
            {
                int id = dr.Field<int>("Id");
                dr["ConnState"] = items[id].ConnectionState;
            }
            ConnectionChanged?.Invoke();
        }

        /// <summary>
        /// 报文接受事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGroupMessageReceived(object? sender, GroupEventArgs e)
        {
            GroupMessageReceived?.Invoke(new KnxMsgEventArgs(KnxMessageType.FromBus, e), null);
        }

        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KnxInterface> GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        /// <summary>
        /// 泛型枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
