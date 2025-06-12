//BedivereKnx

//   Copyright © 2024 Ouroboros Software Foundation. All rights reserved.
//   版权所有 © 2024 Ouroboros Software Foundation。保留所有权利。
//
//   This program Is free software: you can redistribute it And/Or modify
//   it under the terms Of the GNU General Public License As published by
//   the Free Software Foundation, either version 3 Of the License, Or
//   (at your option) any later version.
//   本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，
//   你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

//   This program Is distributed In the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty Of
//   MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License For more details.
//   我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。
//   详情参见GNU通用公共许可协议。

//   You should have received a copy Of the GNU General Public License
//   along with this program.
//   If Not, see <https://www.gnu.org/licenses/>.
//   你理当已收到一份GNU通用公共许可协议的副本。
//   如果没有，请查阅 <http://www.gnu.org/licenses/> 

using BedivereKnx.DataFile;
using Knx.Falcon;
using Knx.Falcon.Sdk;
using System.Data;
using System.Net;

namespace BedivereKnx.KnxSystem
{

    /// <summary>
    /// KNX系统对象
    /// </summary>
    public class KnxSystem
    {

        private bool isPolling;

        private static string? AsmName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// 报文收发传输事件
        /// </summary>
        public event KnxMessageHandler? MessageTransmission;

        /// <summary>
        /// 轮询状态变化事件
        /// </summary>
        public event ValueChangeHandler<bool>? PollingStatusChanged;

        public bool IsPolling
        {
            get => isPolling;
            set
            {
                if (value != isPolling)
                {
                    isPolling = value;
                    PollingStatusChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// KNX区域
        /// </summary>
        public DataTable Areas { get; private set; }

        /// <summary>
        /// KNX接口
        /// </summary>
        public KnxInterfaceCollection Interfaces { get; private set; }

        /// <summary>
        /// KNX对象
        /// </summary>
        public KnxObjectCollection Objects { get; private set; }

        /// <summary>
        /// KNX场景
        /// </summary>
        public KnxSceneCollection Scenes { get; private set; }

        /// <summary>
        /// KNX设备
        /// </summary>
        public KnxDeviceCollection Devices { get; private set; }

        /// <summary>
        /// KNX时间表
        /// </summary>
        public KnxSchedule Schedule { get; private set; }

        /// <summary>
        /// 外部链接
        /// </summary>
        public DataTable Links { get; private set; }

        /// <summary>
        /// 报文日志
        /// </summary>
        public DataTable MessageLog { get; private set; } = new DataTable();

        public KnxSystem(string dataFilePath, IPAddress localIp)
        {
            try
            {
                DataTableCollection dtc = ExcelDataFile.FromExcel(dataFilePath, true, true);

                //区域
                if (dtc.Contains("Areas"))
                {
                    Areas = dtc["Areas"]!;
                }
                else
                {
                    throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Areas"));
                }

                //接口
                if (dtc.Contains("Interfaces"))
                {
                    Interfaces = new(dtc["Interfaces"]!, localIp);
                    Interfaces.GroupMessageReceived += _GroupMessageReceived;
                    Interfaces.GroupPollRequest += PollAllObjects;
                }
                else
                {
                    throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Interfaces"));
                }

                //对象
                if (dtc.Contains("Objects"))
                {
                    Objects = new(dtc["Objects"]!);
                    Objects.GroupReadRequest += _GroupReadRequest;
                    Objects.GroupWriteRequest += _GroupWriteRequest;
                }
                else
                {
                    throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Objects"));
                }

                //场景
                if (dtc.Contains("Scenes"))
                {
                    Scenes = new(dtc["Scenes"]!);
                    Scenes.SceneControlRequest += _GroupWriteRequest;
                }
                else
                {
                    throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Scenes"));
                }

                //设备
                if (dtc.Contains("Devices"))
                {
                    Devices = new(dtc["Devices"]!);
                }
                else
                {
                    throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Devices"));
                }

                //定时
                if (dtc.Contains("Schedules"))
                {
                    Schedule = new(dtc["Schedules"]!);
                    ScheduleEventsInit(); //初始化定时事件表
                    Schedule.ScheduleEventTriggered += _ScheduleEventTriggered;
                }
                else
                {
                    //throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Schedules"));
                }

                //连接
                if (dtc.Contains("Links"))
                {
                    Links = dtc["Links"]!;
                }
                else
                {
                    //throw new Exception(string.Format(ResString.ExMsg_TableMiss, "Links"));
                }

                MsgLogTableInit(); //初始化报文日志表
            }
            catch (Exception)
            {
                throw;
            }
            MessageTransmission += _MessageTransmission;
        }

        /// <summary>
        /// 初始化定时事件表
        /// </summary>
        private void ScheduleEventsInit()
        {
            if (Schedule.Table.Rows.Count == 0) return;
            foreach (DataRow dr in Schedule.Table.Rows)
            {
                if (!dr.Field<bool>("Enable")) continue; //跳过禁用的定时
                KnxObjectPart type = KnxGroupFactory.GetKnxObjectPart($"{dr.Field<string>("TargetType")}_Ctl"); //定时计划中的对象类型，后面加control确保是控制组地址
                string[] codes = Convertor.ToArray(dr.Field<string>("TargetCode")); //对象编号数组

                //获取一条定时计划的KNX组对象
                List<KnxGroup> list = [];
                foreach (string code in codes) //遍历一条定时计划中的全部对象
                {
                    switch (type)
                    {
                        case KnxObjectPart.SceneControl: //场景的情况，在Scenes对象里查找对象
                            foreach (KnxScene scn in Scenes[code])
                            {
                                list.Add(scn[KnxObjectPart.SceneControl]);
                            }
                            break;
                        default: //其他的情况在Objects对象里查找
                            foreach (KnxObject obj in Objects[code])
                            {
                                list.Add(obj[type]);
                            }
                            break;
                    }
                }

                //获取一条定时计划的时间和值
                foreach (string evtText in Convertor.ToArray(dr.Field<string>("ScheduleEvents")))
                {
                    string[] pairTV = Convertor.ToArray(evtText, '='); //{时间, 值}
                    foreach (KnxGroup grp in list)
                    {
                        DataRow drSeq = Schedule.Sequence.Table.NewRow();
                        drSeq["Enable"] = dr.Field<bool>("Enable"); //启用状态，必然为true
                        drSeq["ScheduleCode"] = dr["ScheduleCode"]; //定时编号
                        drSeq["ScheduleName"] = dr["ScheduleName"]; //定时名称
                        drSeq["TriggerTime"] = new TimeOnlyHM(pairTV[0]); //触发时间
                        drSeq["TargetType"] = type; //组地址类型
                        drSeq["GroupAddress"] = grp.Address; //组地址
                        drSeq["GroupDpt"] = grp.DPT; //组地址DPT
                        string valStr = pairTV[1].Trim(); //目标值的字符串
                        drSeq["TargetValue"] = valStr; //目标值
                        drSeq["Value"] = grp.ToGroupValue(valStr);
                        Schedule.Sequence.Table.Rows.Add(drSeq); //加入新行
                    }
                }
            }
            if (Schedule.Sequence.Count > 0)
            {
                Schedule.Sequence.Table.DefaultView.Sort = "TriggerTime"; //按照触发时间排序
                Schedule.Sequence.Table = Schedule.Sequence.Table.DefaultView.ToTable();
                for (int i = 0; i < Schedule.Sequence.Count; i++)
                {
                    Schedule.Sequence.Table.Rows[i]["Id"] = i; //根据时间顺序写入Id
                }
                Schedule.Sequence.NextId = 0; //设置下次定时事件的ID
            }
        }

        /// <summary>
        /// 初始化报文日志表
        /// </summary>
        private void MsgLogTableInit()
        {
            MessageLog = new DataTable();
            MessageLog.Columns.Add(new DataColumn("DateTime", typeof(DateTime))
            {
                Caption = "DateTime" //报文时间
            });
            MessageLog.Columns.Add(new DataColumn("MessageType", typeof(KnxMessageType))
            {
                Caption = "MessageType" //报文类型
            });
            MessageLog.Columns.Add(new DataColumn("EventType", typeof(GroupEventType))
            {
                Caption = "EventType" //事件类型
            });
            MessageLog.Columns.Add(new DataColumn("SourceAddress", typeof(IndividualAddress))
            {
                Caption = "SourceAddress" //源地址
            });
            MessageLog.Columns.Add(new DataColumn("DestinationAddress", typeof(GroupAddress))
            {
                Caption = "DestinationAddress" //目标地址
            });
            MessageLog.Columns.Add(new DataColumn("MessagePriority", typeof(MessagePriority))
            {
                Caption = "MessagePriority" //优先级
            });
            MessageLog.Columns.Add(new DataColumn("Value", typeof(GroupValue))
            {
                Caption = "Value" //值
            });
            MessageLog.Columns.Add(new DataColumn("HopCount", typeof(byte))
            {
                Caption = "HopCount" //路由计数
            });
            MessageLog.Columns.Add(new DataColumn("IsSecure", typeof(bool))
            {
                Caption = "IsSecure" //安全性
            });
            MessageLog.Columns.Add(new DataColumn("Log", typeof(string))
            {
                Caption = "Log" //日志
            });
        }

        /// <summary>
        /// 报文接受事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="log"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _GroupMessageReceived(KnxMsgEventArgs e, string? log)
        {
            if (e.Value is null) return; //无视没有值的报文
            if ((e.EventType == GroupEventType.ValueWrite) || (e.EventType == GroupEventType.ValueResponse))
            {
                Objects.ReceiveGroupMessage(e.DestinationAddress, e.Value);
            }
            MessageTransmission?.Invoke(e, null); //触发报文传输事件
        }

        /// <summary>
        /// 报文传输事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="log"></param>
        private void _MessageTransmission(KnxMsgEventArgs e, string? log)
        {
            DataRow dr = MessageLog.NewRow();
            dr["DateTime"] = DateTime.Now;
            dr["MessageType"] = e.MessageType;
            dr["EventType"] = e.EventType;
            dr["SourceAddress"] = e.SourceAddress;
            dr["DestinationAddress"] = e.DestinationAddress;
            dr["MessagePriority"] = e.MessagePriority;
            if (e.Value is not null) dr["Value"] = e.Value;
            dr["HopCount"] = e.HopCount;
            dr["IsSecure"] = e.IsSecure;
            dr["Log"] = log;
            MessageLog.Rows.Add(dr);
        }

        /// <summary>
        /// 组地址写入请求
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _GroupWriteRequest(KnxGroupEventArgs e, GroupValue value)
        {
            WriteGroupAddress(e.InterfaceCode, e.GroupAddress, value, e.Priority);
        }

        /// <summary>
        /// 写入组地址（通过接口编号）
        /// </summary>
        /// <param name="ifCode">接口编号</param>
        /// <param name="address">组地址</param>
        /// <param name="val">值</param>
        /// <param name="priority">优先级</param>
        public async void WriteGroupAddress(string? ifCode, GroupAddress address, GroupValue value, MessagePriority priority = MessagePriority.Low)
        {

            //【HMI控制的特殊ifCode处理】


            KnxBus bus = Interfaces[ifCode]; //从接口编号得到的KnxBus数组
            if (bus.ConnectionState == BusConnectionState.Connected)
            {
                KnxMsgEventArgs mea = new(KnxMessageType.ToBus, GroupEventType.ValueWrite, priority, 6, address, bus.InterfaceConfiguration.IndividualAddress, false, value);
                MessageTransmission?.Invoke(mea, $"By {AsmName}"); //触发事件
                await bus.WriteGroupValueAsync(address, value, priority);
                Thread.Sleep(150); //短暂停顿防止丢包
            }
        }

        /// <summary>
        /// 组地址读取请求
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _GroupReadRequest(KnxGroupEventArgs e)
        {
            ReadGroupAddress(e.InterfaceCode, e.GroupAddress, e.Priority);
        }

        /// <summary>
        /// 读取组地址（按总线对象）
        /// </summary>
        /// <param name="bus">KNX总线对象</param>
        /// <param name="address">组地址</param>
        /// <param name="priority">优先级</param>
        public async void ReadGroupAddress(KnxBus bus, GroupAddress address, MessagePriority priority = MessagePriority.Low)
        {
            if (bus.ConnectionState == BusConnectionState.Connected)
            {
                KnxMsgEventArgs mea = new(KnxMessageType.ToBus, GroupEventType.ValueRead, priority, 6, address, bus.InterfaceConfiguration.IndividualAddress, false);
                MessageTransmission?.Invoke(mea, $"By {AsmName}"); //触发事件
                await bus.ReadGroupValueAsync(address, new TimeSpan(0, 0, 0, 0, 100), priority);
            }
        }

        /// <summary>
        /// 读取组地址（按总线对象数组）
        /// </summary>
        /// <param name="busArray">KNX总线对象数组</param>
        /// <param name="address">组地址</param>
        /// <param name="priority">优先级</param>
        public void ReadGroupAddress(KnxBus[] busArray, GroupAddress address, MessagePriority priority = MessagePriority.Low)
        {
            foreach (KnxBus bus in busArray)
            {
                ReadGroupAddress(bus, address, priority);
            }
        }

        /// <summary>
        /// 读取组地址（按接口编号）
        /// </summary>
        /// <param name="ifCode">KNX接口编号</param>
        /// <param name="address">组地址</param>
        /// <param name="priority">优先级</param>
        public void ReadGroupAddress(string? ifCode, GroupAddress address, MessagePriority priority = MessagePriority.Low)
        {
            ReadGroupAddress(Interfaces[ifCode], address, priority);
        }

        /// <summary>
        /// 读取组地址（按对象）
        /// </summary>
        /// <param name="obj">KNX对象</param>
        /// <param name="priority">优先级</param>
        public void ReadObjectFeedback(KnxObject obj, MessagePriority priority = MessagePriority.Low)
        {
            if (obj.ContainsPart(KnxObjectPart.SwitchFeedback))
            {
                ReadGroupAddress(obj.InterfaceCode, obj[KnxObjectPart.SwitchFeedback].Address, priority);
            }
            if (obj.ContainsPart(KnxObjectPart.ValueFeedback))
            {
                ReadGroupAddress(obj.InterfaceCode, obj[KnxObjectPart.ValueFeedback].Address, priority);
            }
        }

        /// <summary>
        /// 读取组地址（按对象ID）
        /// </summary>
        /// <param name="objId">Objects表中的ID</param>
        /// <param name="priority">优先级</param>
        public void ReadObjectFeedback(int objId, MessagePriority priority = MessagePriority.Low)
        {
            ReadObjectFeedback(Objects[objId], priority); //根据ID获取Object对象
        }

        /// <summary>
        /// 定时触发事件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _ScheduleEventTriggered(string log, KnxGroupEventArgs e, GroupValue value)
        {
            _GroupWriteRequest(e, value);
        }

        /// <summary>
        /// 读取全部Objects表的反馈组地址
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void PollAllObjects()
        {
            if (isPolling) return; //上次轮询未完成，不执行任何操作
            if (Interfaces.Ready)
            {
                Thread thread = new(_PollAllObjects); //新建线程执行轮询防止卡顿
                thread.Start();
            }
        }

        private void _PollAllObjects()
        {
            isPolling = true;
            List<string> listIC
                = Interfaces
                .Where(a => a.ConnectionState == BusConnectionState.Connected)
                .Select(a => a.Code)
                .ToList(); //连接成功总线的接口编号列标
            foreach (KnxObject obj in Objects)
            {
                if (string.IsNullOrWhiteSpace(obj.Code) || listIC.Contains(obj.Code))
                {
                    ReadObjectFeedback(obj); //读取KNX对象反馈
                    Thread.Sleep(100);
                }
            }
            IsPolling = false;
        }









    }

}
