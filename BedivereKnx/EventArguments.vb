Imports Knx.Falcon

Public Delegate Sub GroupReadHandler(e As KnxReadEventArgs)
Public Delegate Sub GroupWriteHandler(e As KnxWriteEventArgs)
Public Delegate Sub KnxMessageHandler(e As KnxMsgEventArgs, log As String)
Public Delegate Sub ScheduleEventHandler(code As String, e As KnxWriteEventArgs)
Public Delegate Sub ScheduleTimerHandler(e As KnxScheduleTimerState)
Public Delegate Sub KnxDeviceStateHandler(sender As KnxDeviceInfo, e As IndAddressState)

Public Class KnxReadEventArgs

    Inherits EventArgs

    Public ReadOnly Property InterfaceCode As String

    Public ReadOnly Property GroupAddr As GroupAddress

    Public ReadOnly Property Priority As MessagePriority = MessagePriority.Low

    Public Sub New(IfCode As String, GrpAddr As GroupAddress, Optional Priority As MessagePriority = MessagePriority.Low)
        _InterfaceCode = IfCode
        _GroupAddr = GrpAddr
        _Priority = Priority
    End Sub

End Class

Public Class KnxWriteEventArgs

    Inherits EventArgs

    Public ReadOnly Property InterfaceCode As String

    Public ReadOnly Property GroupAddr As GroupAddress

    Public ReadOnly Property GroupVal As GroupValue

    Public ReadOnly Property Priority As MessagePriority = MessagePriority.Low

    Public Sub New(IfCode As String, GrpAddr As GroupAddress, GrpVal As GroupValue, Optional Priority As MessagePriority = MessagePriority.Low)
        _InterfaceCode = IfCode
        _GroupAddr = GrpAddr
        _GroupVal = GrpVal
        _Priority = Priority
    End Sub

End Class

Public Class KnxMsgEventArgs

    Inherits EventArgs

    Public ReadOnly Property MessageType As KnxMessageType

    Public ReadOnly Property EventType As GroupEventType

    Public ReadOnly Property MessagePriority As MessagePriority

    Public ReadOnly Property HopCount As Byte

    Public ReadOnly Property DestinationAddress As GroupAddress

    Public ReadOnly Property SourceAddress As IndividualAddress

    Public ReadOnly Property IsSecure As Boolean

    Public ReadOnly Property Value As GroupValue

    Public Sub New(MsgType As KnxMessageType, EvtType As GroupEventType, MsgPriority As MessagePriority, HopCount As Byte, DesAddr As GroupAddress, SourceAddr As IndividualAddress, Optional IsSecure As Boolean = False, Optional Val As GroupValue = Nothing)
        _MessageType = MsgType
        _EventType = EvtType
        _MessagePriority = MsgPriority
        _HopCount = HopCount
        _DestinationAddress = DesAddr
        _SourceAddress = SourceAddr
        _IsSecure = IsSecure
        _Value = Val
    End Sub

    Public Sub New(MsgType As KnxMessageType, GrpEvtArgs As GroupEventArgs)
        _MessageType = MsgType
        _EventType = GrpEvtArgs.EventType
        _MessagePriority = GrpEvtArgs.MessagePriority
        _HopCount = GrpEvtArgs.HopCount
        _DestinationAddress = GrpEvtArgs.DestinationAddress
        _SourceAddress = GrpEvtArgs.SourceAddress
        _IsSecure = GrpEvtArgs.IsSecure
        _Value = GrpEvtArgs.Value
    End Sub

End Class
