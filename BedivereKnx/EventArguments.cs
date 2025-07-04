using BedivereKnx.KnxSystem;
using Knx.Falcon;

namespace BedivereKnx
{

    /// <summary>
    /// 值变化委托
    /// </summary>
    public delegate void ValueChangeHandler<T>(T? value);

    /// <summary>
    /// KNX组读取委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void GroupReadHandler(KnxGroupEventArgs e);

    /// <summary>
    /// KNX组写入委托
    /// </summary>
    public delegate void GroupWriteHandler(KnxGroupEventArgs e, GroupValue value);

    /// <summary>
    /// KNX报文传输委托
    /// </summary>
    public delegate void KnxMessageHandler(KnxMsgEventArgs e, string? log);

    /// <summary>
    /// KNX设备状态委托
    /// </summary>
    /// <param name="dev"></param>
    /// <param name="state"></param>
    public delegate void KnxDeviceStateHandler(KnxDeviceInfo dev, KnxDeviceState state);

    /// <summary>
    /// 时间表事件委托
    /// </summary>
    /// <param name="code"></param>
    /// <param name="e"></param>
    /// <param name="value"></param>
    public delegate void ScheduleEventHandler(string log, KnxGroupEventArgs e, GroupValue value);

    /// <summary>
    /// 时间表定时器委托
    /// </summary>
    /// <param name="state"></param>
    public delegate void ScheduleTimerHandler(KnxScheduleTimerState state);

    public class KnxGroupEventArgs(string? interfaceCode, GroupAddress groupAddress, MessagePriority priority = MessagePriority.Low) : EventArgs
    {

        public string? InterfaceCode { get; } = interfaceCode;

        public GroupAddress GroupAddress { get; } = groupAddress;

        public MessagePriority Priority { get; } = priority;

    }

    public class KnxMsgEventArgs : EventArgs
    {
        public KnxMessageType MessageType { get; }

        public GroupEventType EventType { get; }

        public MessagePriority MessagePriority { get; }

        public byte HopCount { get; }

        public GroupAddress DestinationAddress { get; }

        public IndividualAddress SourceAddress { get; }

        public bool IsSecure { get; }

        public GroupValue? Value { get; }

        public KnxMsgEventArgs(KnxMessageType type, GroupEventArgs arg)
        {
            MessageType = type;
            EventType = arg.EventType;
            MessagePriority = arg.MessagePriority;
            HopCount = arg.HopCount;
            DestinationAddress = arg.DestinationAddress;
            SourceAddress = arg.SourceAddress;
            IsSecure = arg.IsSecure;
            Value = arg.Value;
        }

        public KnxMsgEventArgs(KnxMessageType msgType,
                               GroupEventType eventType,
                               MessagePriority priority,
                               byte hop,
                               GroupAddress grpAddr,
                               IndividualAddress indAddr,
                               bool isSecure = false,
                               GroupValue? val = null)
        {
            MessageType = msgType;
            EventType = eventType;
            MessagePriority = priority;
            HopCount = hop;
            DestinationAddress = grpAddr;
            SourceAddress = indAddr;
            IsSecure = isSecure;
            Value = val;
        }

    }

}
