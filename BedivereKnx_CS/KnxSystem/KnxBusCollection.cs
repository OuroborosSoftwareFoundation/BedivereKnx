//using Knx.Falcon;
//using Knx.Falcon.Configuration;
//using Knx.Falcon.Sdk;
//using System.Collections;
//using System.ComponentModel;
//using System.Data;
//using System.Net;
//using System.Net.NetworkInformation;

//namespace BedivereKnx.KnxSystem
//{
//    public class KnxBusCollection : IEnumerable<KnxBus>
//    {

//        /// <summary>
//        /// 组地址轮询事件
//        /// </summary>
//        protected internal event Action? GroupPollRequest;

//        /// <summary>
//        /// 接口连接状态变化事件
//        /// </summary>
//        public event Action? ConnectionChanged;

//        /// <summary>
//        /// 接口连接故障事件
//        /// </summary>
//        public event Action<Exception>? ConnectionExceptionOccurred;

//        /// <summary>
//        /// 组地址报文接收事件
//        /// </summary>
//        public event KnxMessageHandler? GroupMessageReceived;

//        /// <summary>
//        /// 总线就绪状态
//        /// </summary>
//        public bool Ready { get; set; } = false;

//        /// <summary>
//        /// 默认接口
//        /// </summary>
//        public KnxBus DefaultBus { get; }


//        /// <summary>
//        /// 数据表
//        /// </summary>
//        public readonly DataTable Table;

//        /// <summary>
//        /// 对象数量
//        /// </summary>
//        public int Count => Items.Count;

//        /// <summary>
//        /// 索引器内部字典
//        /// </summary>
//        private readonly Dictionary<string, KnxBus> Items = [];

//        /// <summary>
//        /// 索引器（根据ID）
//        /// </summary>
//        /// <param name="index">接口ID</param>
//        /// <returns></returns>
//        /// <exception cref="KeyNotFoundException"></exception>
//        public KnxBus this[int index]
//        {
//            get
//            {
//                if (Items.TryGetValue(Table.Rows[index]["InterfaceCode"].ToString(), out KnxBus? bus))
//                {
//                    return bus;
//                }
//                else
//                {
//                    return DefaultBus; //找不到接口编号的情况下直接引用默认接口
//                }
//            }
//        }

//        /// <summary>
//        /// 索引器（根据编号）
//        /// </summary>
//        /// <param name="code">接口编号</param>
//        /// <returns></returns>
//        public KnxBus this[string? code]
//        {
//            get
//            {
//                if (string.IsNullOrWhiteSpace(code)) return DefaultBus; //空白编号直接引用默认接口
//                if (Items.TryGetValue(code, out KnxBus? bus))
//                {
//                    return bus;
//                }
//                else
//                {
//                    return DefaultBus; //找不到接口编号的情况下直接引用默认接口
//                }
//            }
//        }

//        public KnxBusCollection(IPAddress localIp)
//        {
//            Table = new DataTable();
//            Table.Columns.Add(new DataColumn("NetStatus", typeof(IPStatus)) //网络状态
//            {
//                Caption = ResString.DataCol_NetStatus
//            });
//            Table.Columns.Add(new DataColumn("CnState", typeof(BusConnectionState)) //接口连接状态
//            {
//                Caption = ResString.DataCol_ConnState
//            });

//            //添加默认接口
//            DefaultBus = new KnxBus($"Type=IpRouting;LocalIPAddress={localIp}"); //默认接口
//            DefaultBus.ConnectionStateChanged += _ConnectionChanged;
//            DefaultBus.GroupMessageReceived += _GroupMessageReceived;
//        }

//        public KnxBusCollection(DataTable dt, IPAddress localIp)
//        {
//            Table = dt;
//            Table.Columns.Add(new DataColumn("NetStatus", typeof(IPStatus)) //网络状态
//            {
//                Caption = ResString.DataCol_NetStatus
//            });
//            Table.Columns.Add(new DataColumn("CnState", typeof(BusConnectionState)) //接口连接状态
//            {
//                Caption = ResString.DataCol_ConnState
//            });

//            //表格中的接口
//            foreach (DataRow dr in dt.Rows)
//            {
//                string? ifType = dr["InterfaceType"].ToString();
//                if (!string.IsNullOrEmpty(ifType))
//                {
//                    ConnectorParameters cp; //连接参数的基类
//                    switch (ifType.ToLower())
//                    {
//                        case "usb": //USB接口
//                            cp = new UsbConnectorParameters(); //自动获取目前USB接口
//                            break;
//                        case "iptunnel": //IP隧道
//                            cp = new IpTunnelingConnectorParameters(dr["InterfaceAddress"].ToString(), (int)dr["Port"]);
//                            break;
//                        case "iprouter": //IP路由
//                            if (IPAddress.TryParse(dr["InterfaceAddress"].ToString(), out IPAddress? ip))
//                            {
//                                if ((ip == IpRoutingConnectorParameters.DefaultMulticastAddress) && ((int)dr["Port"] == IpRoutingConnectorParameters.DefaultIpPort))
//                                {
//                                    continue; //表中有默认路由接口的情况，跳过，用默认接口代替
//                                }
//                                else
//                                {
//                                    cp = new IpRoutingConnectorParameters(ip)
//                                    {
//                                        LocalIPAddress = localIp //路由接口中的本地IP
//                                    };
//                                }
//                            }
//                            else //其他情况报错
//                            {
//                                throw new FormatException($"Invalid IP address: {dr["InterfaceAddress"]}");
//                            }
//                            break;
//                        default: //其他情况报错
//                            throw new InvalidEnumArgumentException(string.Format(ResString.ExMsg_KnxInterfaceTypeInvalid, ifType));
//                    }
//                    cp.Name = dr["InterfaceCode"].ToString(); //接口名称
//                    cp.AutoReconnect = true; //自动重连
//                    KnxBus bus = new(cp);
//                    bus.ConnectionStateChanged += _ConnectionChanged;
//                    bus.GroupMessageReceived += _GroupMessageReceived;
//                    dr["NetStatus"] = IPStatus.Unknown;
//                    dr["CnState"] = bus.ConnectionState; //初始化连接状态
//                    Items.Add(dr["InterfaceCode"].ToString(), bus);
//                }
//            }

//            //添加默认接口
//            DefaultBus = new KnxBus($"Type=IpRouting;LocalIPAddress={localIp}"); //默认接口
//            DefaultBus.ConnectionStateChanged += _ConnectionChanged;
//            DefaultBus.GroupMessageReceived += _GroupMessageReceived;
//        }

//        /// <summary>
//        /// 打开全部接口
//        /// </summary>
//        /// <param name="poll">打开后轮询全部组地址</param>
//        public void AllConnect(bool poll = false)
//        {
//            Thread thread = new(() => _AllConnect(poll));
//            thread.Start();
//        }

//        private async void _AllConnect(bool poll = false)
//        {
//            try
//            {
//                Ready = false;
//                if (DefaultBus.ConnectionState == BusConnectionState.Closed)
//                {
//                    await DefaultBus.ConnectAsync();
//                }
//                foreach (DataRow dr in Table.Rows)
//                {
//                    if (!(bool)dr["Enable"]) continue; //跳过禁用的接口
//                    BusConnectionState cnState = (BusConnectionState)dr["CnState"];
//                    if (cnState == BusConnectionState.Closed) //只处理Close状态的接口
//                    {
//                        string? ifCode = dr["InterfaceCode"].ToString();
//                        string? address = dr["InterfaceAddress"].ToString();
//                        string? ifType = dr["InterfaceType"].ToString()?.ToLower();
//                        if (string.IsNullOrWhiteSpace(ifType) && ifType == "iptunnel")
//                        {
//                            Ping ping = new Ping();
//                            PingReply reply = ping.Send(address, 100);
//                            dr["NetStatus"] = reply.Status;
//                            ConnectionChanged?.Invoke();
//                            if (reply.Status == IPStatus.Success)
//                            {
//                                await this[ifCode].ConnectAsync(); //异步方式打开接口
//                            }
//                        }
//                        else
//                        {
//                            await this[ifCode].ConnectAsync(); //异步方式打开接口
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ConnectionExceptionOccurred?.Invoke(ex); //触发连接异常事件
//            }
//            finally
//            {
//                Ready = true;
//                if (poll) GroupPollRequest?.Invoke();
//            }
//        }

//        private void _ConnectionChanged(object? sender, EventArgs e)
//        {
//            foreach (DataRow dr in Table.Rows)
//            {
//                string? ifCode = dr["InterfaceCode"].ToString();
//                if (!string.IsNullOrWhiteSpace(ifCode))
//                {
//                    dr["CnState"] = Items[ifCode].ConnectionState;
//                }
//            }
//            ConnectionChanged?.Invoke();
//        }

//        private void _GroupMessageReceived(object? sender, GroupEventArgs e)
//        {
//            GroupMessageReceived?.Invoke(new KnxMsgEventArgs(KnxMessageType.FromBus, e), null);
//        }

//        public IEnumerator<KnxBus> GetEnumerator()
//        {
//            return Items.Values.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//}
