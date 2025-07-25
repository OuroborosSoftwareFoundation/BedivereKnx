﻿using System.Net;
using System.Net.NetworkInformation;
using Knx.Falcon;
using Knx.Falcon.Configuration;
using Knx.Falcon.Sdk;

namespace BedivereKnx.KnxSystem
{

    public class KnxInterface
    {

        public KnxBus Bus;

        /// <summary>
        /// 接口类型
        /// </summary>
        public ConnectorType InterfaceType { get; internal set; }

        /// <summary>
        /// 接口ID
        /// </summary>
        public int Id { get; internal set; } = -1;

        /// <summary>
        /// 接口编号
        /// </summary>
        public string Code { get; internal set; } = string.Empty;

        /// <summary>
        /// 接口名称
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public IPAddress? Address { get; internal set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; internal set; } = 3671;

        /// <summary>
        /// 接口启用状态
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 网络状态
        /// </summary>
        public IPStatus? NetStatus { get; set; } = IPStatus.Unknown;

        /// <summary>
        /// 连接状态
        /// </summary>
        public BusConnectionState ConnectionState { get => Bus.ConnectionState; }

        public KnxInterface(KnxBus bus)
        {
            Bus = bus;
        }

        public KnxInterface(ConnectorParameters cp)
        {
            Bus = new KnxBus(cp);
            Name = cp.Name;
        }

        public KnxInterface(string connString)
        {
            Bus = new KnxBus(connString);
        }


        //public async void Read(GroupAddress ga, MessagePriority priority = MessagePriority.Low)
        //{
        //    await Bus.ReadGroupValueAsync(ga, priority);
        //}





        //隐式转换为KnxBus对象
        public static implicit operator KnxBus(KnxInterface iface) => iface.Bus;

    }

}
