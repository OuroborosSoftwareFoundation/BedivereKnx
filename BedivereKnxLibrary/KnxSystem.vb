'BedivereKnxLibrary

'   Copyright © 2024 Ouroboros Software Foundation. All rights reserved.
'   版权所有 © 2024 Ouroboros Software Foundation。保留所有权利。
'
'   This program Is free software: you can redistribute it And/Or modify
'   it under the terms Of the GNU General Public License As published by
'   the Free Software Foundation, either version 3 Of the License, Or
'   (at your option) any later version.
'   本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，
'   你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

'   This program Is distributed In the hope that it will be useful,
'   but WITHOUT ANY WARRANTY; without even the implied warranty Of
'   MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the
'   GNU General Public License For more details.
'   我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。
'   详情参见GNU通用公共许可协议。

'   You should have received a copy Of the GNU General Public License
'   along with this program.
'   If Not, see <https://www.gnu.org/licenses/>.
'   你理当已收到一份GNU通用公共许可协议的副本。
'   如果没有，请查阅 <http://www.gnu.org/licenses/> 

Imports System.Data
Imports Knx.Falcon
Imports Knx.Falcon.Sdk

Public Class KnxSystem

    Private ReadOnly _Bus As New KnxSystemBusCollection
    Private ReadOnly _Areas As New KnxSystemArea
    Private ReadOnly _Objects As New KnxSystemObjectCollection
    Private ReadOnly _Scenes As New KnxSystemSceneCollection
    Private ReadOnly _Devices As New KnxSystemDeviceCollection
    Private ReadOnly _Schedules As New KnxSystemSchedule
    Private _NameSpace As String
    Private _MessageLog As New DataTable
    Private IsPolling As Boolean = False '正在轮询

    ''' <summary>
    ''' 报文收发传输事件
    ''' </summary>
    Public Event MessageTransmission As KnxMessageHandler

    Public ReadOnly Property Bus As KnxSystemBusCollection
        Get
            Return _Bus
        End Get
    End Property

    Public ReadOnly Property Areas As KnxSystemArea
        Get
            Return _Areas
        End Get
    End Property

    Public ReadOnly Property Objects As KnxSystemObjectCollection
        Get
            Return _Objects
        End Get
    End Property

    Public ReadOnly Property Scenes As KnxSystemSceneCollection
        Get
            Return _Scenes
        End Get
    End Property

    Public ReadOnly Property Devices As KnxSystemDeviceCollection
        Get
            Return _Devices
        End Get
    End Property

    Public ReadOnly Property Schedules As KnxSystemSchedule
        Get
            Return _Schedules
        End Get
    End Property

    Public ReadOnly Property Links As DataTable

    ''' <summary>
    ''' 报文日志DataTable
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MessageLog As DataTable
        Get
            Return _MessageLog
        End Get
    End Property

    Public Sub New(ExcelDataFile As String)
        Try
            _NameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetName.Name
            Dim dicDt As Dictionary(Of String, DataTable) = ReadExcelToDataTables(ExcelDataFile, True, True)
            Dim dtBus As DataTable = Nothing
            If dicDt.TryGetValue("Interfaces", dtBus) Then
                _Bus = New KnxSystemBusCollection(dtBus)
                AddHandler _Bus.GroupMessageReceived, AddressOf _GroupMessageReceived
                AddHandler _Bus.GroupPollRequest, AddressOf PollObjectsValue
            End If
            Dim dtArea As DataTable = Nothing
            If dicDt.TryGetValue("Areas", dtArea) Then
                _Areas = New KnxSystemArea(dtArea)
            End If
            Dim dtObj As DataTable = Nothing
            If dicDt.TryGetValue("Objects", dtObj) Then
                _Objects = New KnxSystemObjectCollection(dtObj)
                AddHandler _Objects.GroupWriteRequest, AddressOf _GroupWriteRequest
                AddHandler _Objects.GroupReadRequest, AddressOf _GroupReadRequest
            End If
            Dim dtScn As DataTable = Nothing
            If dicDt.TryGetValue("Scenes", dtScn) Then
                _Scenes = New KnxSystemSceneCollection(dtScn)
                AddHandler _Scenes.SceneControlRequest, AddressOf _GroupWriteRequest
            End If
            Dim dtDev As DataTable = Nothing
            If dicDt.TryGetValue("Devices", dtDev) Then
                _Devices = New KnxSystemDeviceCollection(dtDev)
            End If
            Dim dtScd As DataTable = Nothing
            If dicDt.TryGetValue("Schedules", dtScd) Then
                _Schedules = New KnxSystemSchedule(dtScd)
                ScheduleEventsInit() '初始化定时事件表
                AddHandler _Schedules.ScheduleEventTriggered, AddressOf _ScheduleEventTriggered
            End If
            Dim dtLink As DataTable = Nothing
            If dicDt.TryGetValue("Links", dtLink) Then
                _Links = dtLink
            End If
            MsgLogTableInit() '初始化报文日志表
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 初始化报文日志表
    ''' </summary>
    Private Sub MsgLogTableInit()
        With _MessageLog
            .Clear()
            .Columns.Add("DateTime", GetType(DateTime))
            .Columns("DateTime").Caption = "报文时间"
            .Columns.Add("MessageType", GetType(KnxMessageType))
            .Columns("MessageType").Caption = "报文类型"
            .Columns.Add("EventType", GetType(GroupEventType))
            .Columns("EventType").Caption = "事件类型"
            .Columns.Add("SourceAddress", GetType(IndividualAddress))
            .Columns("SourceAddress").Caption = "源地址"
            .Columns.Add("DestinationAddress", GetType(GroupAddress))
            .Columns("DestinationAddress").Caption = "目标地址"
            .Columns.Add("MessagePriority", GetType(MessagePriority))
            .Columns("MessagePriority").Caption = "优先级"
            .Columns.Add("Value", GetType(GroupValue))
            .Columns("Value").Caption = "值"
            .Columns.Add("HopCount", GetType(Byte))
            .Columns("HopCount").Caption = "路由计数"
            .Columns.Add("IsSecure", GetType(Boolean))
            .Columns("IsSecure").Caption = "安全性"
            .Columns.Add("Log", GetType(String))
            .Columns("Log").Caption = "日志"
        End With
    End Sub

    ''' <summary>
    ''' 初始化定时事件表
    ''' </summary>
    Private Sub ScheduleEventsInit()
        If _Schedules.Table.Rows.Count = 0 Then Exit Sub
        For Each dr As DataRow In _Schedules.Table.Rows '逐行读取
            If dr("Enable") = 0 Then Continue For '无视禁用的定时
            Dim GrpType As KnxGroupType '组地址类型
            If Not [Enum].TryParse(dr("TargetType"), GrpType) Then '字符串转枚举
                Throw New ArgumentException($"Wrong TargetType in ScheduleTable: {dr("TargetType")}.")
            End If
            Dim Codes As String() = String__StrArray(dr("TargetCode").ToString) '对象编号数组
            Dim lstTargrt As New List(Of KnxObjectGeneric) '控制目标列表
            Select Case GrpType '根据组地址类型寻找对象，确定组地址控制类型
                Case KnxGroupType.Switch, KnxGroupType.EnableCtl '开关控制
                    For Each code As String In Codes
                        For Each obj As KnxObjectGroup In _Objects(code)
                            lstTargrt.Add(New KnxObjectGeneric(obj.InterfaceCode, obj.SwitchPart.CtlAddr, GroupValueType.Bool))
                        Next
                    Next
                Case KnxGroupType.Dimming '亮度控制
                    For Each code As String In Codes
                        For Each obj As KnxObjectGroup In _Objects(code)
                            lstTargrt.Add(New KnxObjectGeneric(obj.InterfaceCode, obj.ValuePart.CtlAddr, GroupValueType.BytePercent))
                        Next
                    Next
                Case KnxGroupType.Value '数值控制
                    For Each code As String In Codes
                        For Each obj As KnxObjectGroup In _Objects(code)
                            lstTargrt.Add(New KnxObjectGeneric(obj.InterfaceCode, obj.ValuePart.CtlAddr, GroupValueType.Byte))
                        Next
                    Next
                Case KnxGroupType.Scene '场景控制
                    For Each code As String In Codes '遍历目标编号
                        For Each scn As KnxSceneGroup In _Scenes(code)
                            lstTargrt.Add(New KnxObjectGeneric(scn.InterfaceCode, scn.GroupAddress, GroupValueType.Byte))
                        Next
                    Next
                Case Else
                    Throw New ArgumentException($"Wrong or unsupported TargetType '{dr("TargetType")}' of ScheduleEvent {dr("ScheduleName")}.")
                    Continue For
            End Select
            For Each EvtString As String In dr("ScheduleEvents").ToString.Split(","c) '定时事件
                Dim Time_Value As String() = EvtString.Split("="c) '{时间, 值}
                For Each tgt As KnxObjectGeneric In lstTargrt
                    Dim drNew As DataRow = _Schedules.Sequence.Table.NewRow()
                    drNew("Enable") = Not (dr("Enable").ToString.Trim = "0") '定时启用
                    drNew("ScheduleCode") = dr("ScheduleCode") '定时编号
                    drNew("ScheduleName") = dr("ScheduleName") '定时名称
                    drNew("Time") = New TimeHM(Convert.ToDateTime(Time_Value(0))) '触发时间
                    drNew("TargetType") = GrpType '组地址类型
                    drNew("GroupValueType") = tgt.GroupValueType '组地址类型
                    Dim ValStr As String = Time_Value(1).Trim '目标值的字符串
                    drNew("TargetValue") = ValStr
                    drNew("Value") = GroupCtlType_GroupValue(ValStr, tgt.GroupValueType)
                    Dim dra = drNew.ItemArray
                    drNew("InterfaceCode") = tgt.InterfaceCode
                    drNew("GroupAddress") = tgt.GroupAddress
                    _Schedules.Sequence.Table.Rows.Add(drNew)
                Next
            Next
        Next
        With _Schedules.Sequence
            .Table.DefaultView.Sort = "Time" '按照触发时间排序
            .Table = .Table.DefaultView.ToTable
            For i = 0 To .Table.Rows.Count - 1
                .Table(i)("Id") = i '根据时间顺序写入Id
            Next
            .NextId = 0 '设置下次定时事件的ID
        End With
    End Sub

    ''' <summary>
    ''' 接收报文
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub _GroupMessageReceived(e As KnxMsgEventArgs, log As String)
        If e.Value Is Nothing Then Exit Sub
        If e.EventType = GroupEventType.ValueWrite OrElse e.EventType = GroupEventType.ValueResponse Then
            _Objects.ReceiveGroupMessage(e.DestinationAddress, e.Value)
        End If
        'If e.EventType = GroupEventType.ValueWrite OrElse e.EventType = GroupEventType.ValueResponse Then
        '    Dim GA As String = e.DestinationAddress.ToString '目标组地址
        '    Dim GrpVal As GroupValue = e.Value '组地址值
        '    Dim OptCol As String = vbNullString '组类型
        '    Dim TypVal As Object = Nothing '类型化后的组地址值
        '    Dim ObjPart As KnxObjectPart '组成员
        '    Dim ObjPoint As KnxObjectPartPoint = KnxObjectPartPoint.Feedback '组成员点位
        '    Select Case GrpVal.SizeInBit '1:Boolean, 2~8:Byte, >8:Array of Byte
        '        Case < 8 'Boolean
        '            OptCol = "Sw_Fdb"
        '            TypVal = GrpVal.TypedValue
        '            ObjPart = KnxObjectPart.Switch
        '        Case 8
        '            OptCol = "Val_Fdb"
        '            TypVal = GrpVal.TypedValue
        '            ObjPart = KnxObjectPart.Value
        '        Case > 8
        '            OptCol = "Val_Fdb"
        '            Dim bs As Byte() = GrpVal.TypedValue
        '            For i = 0 To bs.Length - 1
        '                TypVal = vbNullString & bs(i)
        '            Next
        '            ObjPart = KnxObjectPart.Value
        '    End Select
        '    For Each dr As DataRow In _Objects.Table.Select($"{OptCol}_GrpAddr = '{GA}'") '找出组地址所属对象，可能有多个
        '        If Not IsNothing(TypVal) Then dr($"{OptCol}_Value") = TypVal '表格更新
        '        Dim id As Integer = dr("Id")
        '        _Objects(id).GetPart(ObjPart).SetPointValue(ObjPoint, GrpVal) '对象值更新
        '    Next
        'End If
        RaiseEvent MessageTransmission(e, vbNullString) '触发事件
    End Sub

    ''' <summary>
    ''' 报文传输事件
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub _MessageTransmission(e As KnxMsgEventArgs, log As String) Handles Me.MessageTransmission
        Dim dr As DataRow = _MessageLog.NewRow
        dr("DateTime") = DateTime.Now
        dr("MessageType") = e.MessageType
        dr("EventType") = e.EventType
        dr("SourceAddress") = e.SourceAddress
        dr("DestinationAddress") = e.DestinationAddress
        dr("MessagePriority") = e.MessagePriority
        If Not IsNothing(e.Value) Then dr("Value") = e.Value
        dr("HopCount") = e.HopCount
        dr("IsSecure") = e.IsSecure
        dr("Log") = log
        _MessageLog.Rows.Add(dr)
    End Sub

    ''' <summary>
    ''' 定时触发事件
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub _ScheduleEventTriggered(code As String, e As KnxWriteEventArgs)

        _GroupWriteRequest(e)
    End Sub

    ''' <summary>
    ''' 组地址写入请求
    ''' </summary>
    Private Sub _GroupWriteRequest(e As KnxWriteEventArgs)
        WriteGroupAddress(e.InterfaceCode, e.GroupAddr, e.GroupVal, e.Priority)
    End Sub

    ''' <summary>
    ''' 写入组地址（通过接口编号）
    ''' </summary>
    ''' <param name="IfCode">接口编号，留空为IpRouting</param>
    ''' <param name="GA">组地址</param>
    ''' <param name="Value">值</param>
    ''' <param name="Priority">优先级，默认为Low</param>
    Public Async Sub WriteGroupAddress(IfCode As String, GA As GroupAddress, Value As GroupValue, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim BusArray As KnxBus() = IfCode__KnxBus(IfCode) '从接口编号得到的KnxBus数组
        For Each Bus As KnxBus In BusArray
            If Bus.ConnectionState = BusConnectionState.Connected Then
                Dim MsgArgs As New KnxMsgEventArgs(KnxMessageType.ToBus, GroupEventType.ValueWrite, Priority, 6, GA, Bus.InterfaceConfiguration.IndividualAddress, False, Value)
                RaiseEvent MessageTransmission(MsgArgs, $"By {_NameSpace}") '触发事件
                Await Bus.WriteGroupValueAsync(GA, Value, Priority)
                Threading.Thread.Sleep(100) '短暂停顿防止丢包
            End If
        Next
    End Sub

    ''' <summary>
    ''' 组地址读取请求
    ''' </summary>
    Private Sub _GroupReadRequest(e As KnxReadEventArgs)
        ReadGroupAddress(e.InterfaceCode, e.GroupAddr, e.Priority)
    End Sub

    ''' <summary>
    ''' 读取组地址（按总线对象）
    ''' </summary>
    ''' <param name="Bus">KNX总线对象</param>
    ''' <param name="GA">组地址</param>
    ''' <param name="Priority">优先级</param>
    Public Async Sub ReadGroupAddress(Bus As KnxBus, GA As GroupAddress, Optional Priority As MessagePriority = MessagePriority.Low)
        If Bus.ConnectionState = BusConnectionState.Connected Then
            Dim MsgArgs As New KnxMsgEventArgs(KnxMessageType.ToBus, GroupEventType.ValueRead, Priority, 6, GA, Bus.InterfaceConfiguration.IndividualAddress, False)
            RaiseEvent MessageTransmission(MsgArgs, $"By {_NameSpace}") '触发事件
            Await Bus.ReadGroupValueAsync(GA, Priority)
            Threading.Thread.Sleep(100) '短暂停顿防止丢包
        End If
    End Sub

    ''' <summary>
    ''' 读取组地址（按总线对象数组）
    ''' </summary>
    ''' <param name="BusArray">KNX总线对象数组</param>
    ''' <param name="GA">组地址</param>
    ''' <param name="Priority">优先级</param>
    Public Sub ReadGroupAddress(BusArray As KnxBus(), GA As GroupAddress, Optional Priority As MessagePriority = MessagePriority.Low)
        For Each Bus As KnxBus In BusArray
            If Bus.ConnectionState = BusConnectionState.Connected Then
                ReadGroupAddress(Bus, GA, Priority)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 读取组地址（按接口编号）
    ''' </summary>
    ''' <param name="IfCode"></param>
    ''' <param name="GA"></param>
    ''' <param name="Priority">优先级</param>
    Public Sub ReadGroupAddress(IfCode As String, GA As GroupAddress, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim BusArray As KnxBus() = IfCode__KnxBus(IfCode) '从接口编号得到的KnxBus数组
        For Each Bus As KnxBus In BusArray
            ReadGroupAddress(Bus, GA, Priority)
        Next
    End Sub

    ''' <summary>
    ''' 读取组地址（按对象）
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <param name="Priority"></param>
    Public Sub ReadObjectFeedback(Obj As KnxObjectGroup, Optional Priority As MessagePriority = MessagePriority.Low)
        'If String.IsNullOrEmpty(obj.InterfaceCode) Then Exit Sub
        If Obj.SwitchPart.FdbAddr.Address <> 0 Then '判断组地址是否为空
            ReadGroupAddress(Obj.InterfaceCode, Obj.SwitchPart.FdbAddr, Priority)
        End If
        If Obj.ValuePart.FdbAddr.Address <> 0 Then '判断组地址是否为空
            ReadGroupAddress(Obj.InterfaceCode, Obj.ValuePart.FdbAddr, Priority)
        End If
    End Sub

    ''' <summary>
    ''' 读取组地址（按对象ID）
    ''' </summary>
    ''' <param name="ObjId">Objects表中的ID</param>
    ''' <param name="Priority">优先级</param>
    Public Sub ReadObjectFeedback(ObjId As Integer, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim obj As KnxObjectGroup = _Objects.Item(ObjId) '根据ID获取Object对象
        ReadObjectFeedback(obj, Priority)
    End Sub

    ''' <summary>
    ''' 读取全部Objects表的反馈组地址
    ''' </summary>
    Public Sub PollObjectsValue()
        If IsPolling Then Exit Sub '上次轮询未完成，不执行任何操作
        If _Bus.Ready Then
            Dim th As New Threading.Thread(AddressOf _PollObjectsValue) '新建线程执行轮询防止卡顿
            th.Start()
        End If
    End Sub

    Private Sub _PollObjectsValue()
        IsPolling = True
        Dim lstIC As New List(Of String) '连接成功总线的接口编号
        For Each r As DataRow In _Bus.Table.Rows
            If r("CnState") = BusConnectionState.Connected Then
                lstIC.Add(r("InterfaceCode"))
            End If
        Next
        For Each obj As KnxObjectGroup In _Objects
            If lstIC.Contains(obj.InterfaceCode) Then
                ReadObjectFeedback(obj) '读取组地址
            End If
        Next
        IsPolling = False
    End Sub

    ''' <summary>
    ''' 从接口编号到总线对象数组
    ''' </summary>
    ''' <param name="IfCode">接口编号字符串</param>
    ''' <returns>KnxBus对象数组</returns>
    Private Function IfCode__KnxBus(IfCode As String) As KnxBus()
        Dim k As New List(Of KnxBus)
        If String.IsNullOrEmpty(IfCode) Then '接口编号为空的情况使用默认接口
            If IsNothing(_Bus.Default) Then '不可能出现没有默认接口的情况
                Throw New NullReferenceException("No available KNX Interface.")
            Else
                k.Add(_Bus.Default) '默认接口
            End If
        Else
            For Each str As String In String__StrArray(IfCode) '接口编号的数组
                k.Add(_Bus(str))
            Next
        End If
        Return k.ToArray
    End Function

    ''' <summary>
    ''' 检查设备通讯
    ''' </summary>
    ''' <param name="index"></param>
    Public Async Sub DeviceCheck(index As Integer)
        If IsPolling Then Exit Sub
        Dim addr As IndividualAddress = _Devices.Item(index).IndAddress
        Dim bus As KnxBus = _Bus(_Devices(index).InterfaceCode)
        If bus.ConnectionState = BusConnectionState.Connected Then
            Using kn As KnxNetwork = bus.GetNetwork
                Dim result As Boolean = Await kn.PingIndividualAddressAsync(addr)
                _Devices.Item(index).State = If(result, IndAddressState.Online, IndAddressState.Offline)
            End Using
        Else
            _Devices.Item(index).State = IndAddressState.BusError
        End If
    End Sub

    ''' <summary>
    ''' 检查设备通讯
    ''' </summary>
    ''' <param name="IfCode"></param>
    Public Sub DevicePoll(IfCode As String)
        If IsPolling Then Exit Sub
        Dim bus As KnxBus = _Bus(IfCode)
        If bus.ConnectionState = BusConnectionState.Connected Then
            Dim th As New Threading.Thread(Sub() _DevicePoll(IfCode)) '新建线程打开KNX接口
            th.Start() '启动新线程
        End If
    End Sub

    Private Async Sub _DevicePoll(IfCode As String)
        Dim kdis As KnxDeviceInfo() = _Devices(IfCode)
        Using kn As KnxNetwork = _Bus(IfCode).GetNetwork
            For Each kdi As KnxDeviceInfo In kdis
                Dim result As Boolean = Await kn.PingIndividualAddressAsync(kdi.IndAddress)
                kdi.State = If(result, IndAddressState.Online, IndAddressState.Offline)
            Next
        End Using

    End Sub

End Class
