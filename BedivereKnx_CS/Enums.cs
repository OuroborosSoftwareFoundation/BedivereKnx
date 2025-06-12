namespace BedivereKnx
{

    /// <summary>
    /// KNX组类型
    /// </summary>
    public enum KnxObjectType
    {
        None = 0,
        Switch = 1,
        Dimming,
        Curtain,
        Value,
        EnableCtl,
        Scene
    }

    public enum KnxObjectPart
    {
        None,
        SwitchControl,
        SwitchFeedback,
        ValueControl,
        ValueFeedback,
        DimmingControl,
        SceneControl
    }

    /// <summary>
    /// KNX定时器状态
    /// </summary>
    public enum KnxScheduleTimerState
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stoped = 0,
        /// <summary>
        /// 启动中
        /// </summary>
        Starting = 2,
        /// <summary>
        /// 运行
        /// </summary>
        Running = 1
    }

    /// <summary>
    /// KNX报文类型
    /// </summary>
    public enum KnxMessageType
    {
        Unknown = -1,
        System = 0,
        FromBus = 1,
        ToBus = 2
    }

    /// <summary>
    /// KNX设备状态
    /// </summary>
    public enum KnxDeviceState
    {
        BusError = -2,
        Unknown = -1,
        Offline = 0,
        Online = 1
    }

}
