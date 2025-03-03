'BedivereKnx

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

    Private ReadOnly _NameSpace As String
    Private IsPolling As Boolean = False '正在轮询

    ''' <summary>
    ''' 报文收发传输事件
    ''' </summary>
    Public Event MessageTransmission As KnxMessageHandler

    Public ReadOnly Property Bus As KnxBusCollection

    Public ReadOnly Property Areas As DataTable 'KnxAreaCollection

    Public ReadOnly Property Objects As KnxObjectCollection

    Public ReadOnly Property Scenes As KnxSceneCollection

    Public ReadOnly Property Devices As KnxDeviceCollection

    Public ReadOnly Property Schedules As KnxScheduleCollection

    Public ReadOnly Property Links As DataTable

    ''' <summary>
    ''' 报文日志DataTable
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MessageLog As New DataTable

    Public Sub New(pathExcel As String)
        Try
            _NameSpace = System.Reflection.Assembly.GetExecutingAssembly.GetName.Name
            Dim dicDt As Dictionary(Of String, DataTable) = ReadExcelToDataTables(pathExcel, True, True)
            Dim dtBus As DataTable = Nothing
            If dicDt.TryGetValue("Interfaces", dtBus) Then
                _Bus = New KnxBusCollection(dtBus)
                AddHandler _Bus.GroupMessageReceived, AddressOf _GroupMessageReceived
                AddHandler _Bus.GroupPollRequest, AddressOf PollAllObjects
            End If
            Dim dtArea As DataTable = Nothing
            If dicDt.TryGetValue("Areas", dtArea) Then
                _Areas = dtArea ' New KnxAreaCollection(dtArea)
            End If
            Dim dtObj As DataTable = Nothing
            If dicDt.TryGetValue("Objects", dtObj) Then
                _Objects = New KnxObjectCollection(dtObj)
                AddHandler _Objects.GroupWriteRequest, AddressOf _GroupWriteRequest
                AddHandler _Objects.GroupReadRequest, AddressOf _GroupReadRequest
            End If
            Dim dtScn As DataTable = Nothing
            If dicDt.TryGetValue("Scenes", dtScn) Then
                _Scenes = New KnxSceneCollection(dtScn)
                AddHandler _Scenes.SceneControlRequest, AddressOf _GroupWriteRequest
            End If
            Dim dtDev As DataTable = Nothing
            If dicDt.TryGetValue("Devices", dtDev) Then
                _Devices = New KnxDeviceCollection(dtDev)
            End If
            Dim dtScd As DataTable = Nothing
            If dicDt.TryGetValue("Schedules", dtScd) Then
                _Schedules = New KnxScheduleCollection(dtScd)
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
            Dim tgtType As KnxObjectPart = KnxObjectBase.GetKnxObjectPart($"{dr("TargetType")}_Ctl") '定时计划中的对象类型，后面加control确保是控制组地址
            Dim tgtCodes As String() = StringToArray(dr("TargetCode").ToString) '对象编号数组
            Dim grpList As New List(Of KnxGroup) '一条定时计划里的KNX组列表

            For Each code As String In tgtCodes '遍历一条定时计划中的全部对象
                Select Case tgtType
                    Case KnxObjectPart.SceneControl '场景的情况，在Scenes对象里查找对象
                        For Each obj As KnxScene In _Scenes(code)
                            grpList.Add(obj(tgtType))
                        Next
                    Case Else '其他的情况在Objects对象里查找
                        For Each obj As KnxObject In _Objects(code)
                            grpList.Add(obj(tgtType))
                        Next
                End Select
            Next

            For Each evtString As String In StringToArray(dr("ScheduleEvents").ToString) '定时事件
                Dim tvPair As String() = evtString.Split("="c) '{时间, 值}
                For Each grp As KnxGroup In grpList
                    Dim drNew As DataRow = _Schedules.Sequence.Table.NewRow()
                    drNew("Enable") = Not (dr("Enable").ToString.Trim = "0") '定时启用
                    drNew("ScheduleCode") = dr("ScheduleCode") '定时编号
                    drNew("ScheduleName") = dr("ScheduleName") '定时名称
                    drNew("Time") = New TimeHM(Convert.ToDateTime(tvPair(0))) '触发时间
                    drNew("TargetType") = tgtType '组地址类型
                    drNew("GroupAddress") = grp.Address
                    drNew("GroupDpt") = grp.DPT
                    'drNew("GroupValueType") = grp.GroupValueType '组地址类型
                    Dim ValStr As String = tvPair(1).Trim '目标值的字符串
                    drNew("TargetValue") = ValStr
                    drNew("Value") = grp.ToGroupValue(ValStr)
                    'drNew("Value") = GroupCtlType_GroupValue(ValStr, grp.GroupValueType)
                    'Dim dra = drNew.ItemArray
                    'drNew("InterfaceCode") = grp.InterfaceCode

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
    ''' <param name="ifCode">接口编号，留空为IpRouting</param>
    ''' <param name="address">组地址</param>
    ''' <param name="value">值</param>
    ''' <param name="priority">优先级，默认为Low</param>
    Public Async Sub WriteGroupAddress(ifCode As String, address As GroupAddress, value As GroupValue, Optional priority As MessagePriority = MessagePriority.Low)
        If Not String.IsNullOrEmpty(ifCode) AndAlso ifCode.StartsWith("$"c) Then '使用特殊控制字符串的情况
            ifCode = ifCode.Substring(1) '去除开头的$
            Dim knxPart As KnxObjectPart
            If [Enum].TryParse(Of KnxObjectPart)(ifCode, knxPart) Then
                Dim lstBus As New List(Of String)
                Select Case knxPart
                    Case KnxObjectPart.SwitchControl, KnxObjectPart.ValueControl
                        Dim matches = From row As DataRow In Objects.Table.AsEnumerable()
                                      From col As String In {"Sw_Ctl_GrpAddr", "Val_Ctl_GrpAddr"}
                                      Where row(col).ToString() = address.ToString
                                      Select New With {
                                        .ifCode = row.Field(Of String)("InterfaceCode")
                          } '在各地址列中查找收到的组地址
                        For Each match In matches '遍历所有包含组地址的结果
                            lstBus.Add(match.ifCode)
                        Next
                    Case KnxObjectPart.SceneControl
                        Dim matches = From row As DataRow In Scenes.Table.AsEnumerable()
                                      From col As String In {"GroupAddress"}
                                      Where row(col).ToString() = address.ToString
                                      Select New With {
                                        .ifCode = row.Field(Of String)("InterfaceCode")
                          } '在各地址列中查找收到的组地址
                        For Each match In matches '遍历所有包含组地址的结果
                            lstBus.Add(match.ifCode)
                        Next
                End Select
                ifCode = String.Join(",", lstBus)
            Else
                ifCode = vbNullString
            End If
        End If
        Dim BusArray As KnxBus() = GetKnxBus(ifCode) '从接口编号得到的KnxBus数组
        For Each bus As KnxBus In BusArray
            If bus.ConnectionState = BusConnectionState.Connected Then
                Dim MsgArgs As New KnxMsgEventArgs(KnxMessageType.ToBus, GroupEventType.ValueWrite, priority, 6, address, bus.InterfaceConfiguration.IndividualAddress, False, value)
                RaiseEvent MessageTransmission(MsgArgs, $"By {_NameSpace}") '触发事件
                Await bus.WriteGroupValueAsync(address, value, priority)
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
    ''' <param name="bus">KNX总线对象</param>
    ''' <param name="address">组地址</param>
    ''' <param name="priority">优先级</param>
    Public Async Sub ReadGroupAddress(bus As KnxBus, address As GroupAddress, Optional priority As MessagePriority = MessagePriority.Low)
        If bus.ConnectionState = BusConnectionState.Connected Then
            Dim MsgArgs As New KnxMsgEventArgs(KnxMessageType.ToBus, GroupEventType.ValueRead, priority, 6, address, bus.InterfaceConfiguration.IndividualAddress, False)
            RaiseEvent MessageTransmission(MsgArgs, $"By {_NameSpace}") '触发事件
            Await bus.ReadGroupValueAsync(address, priority)
            Threading.Thread.Sleep(100) '短暂停顿防止丢包
        End If
    End Sub

    ''' <summary>
    ''' 读取组地址（按总线对象数组）
    ''' </summary>
    ''' <param name="busArray">KNX总线对象数组</param>
    ''' <param name="address">组地址</param>
    ''' <param name="priority">优先级</param>
    Public Sub ReadGroupAddress(busArray As KnxBus(), address As GroupAddress, Optional priority As MessagePriority = MessagePriority.Low)
        For Each Bus As KnxBus In busArray
            If Bus.ConnectionState = BusConnectionState.Connected Then
                ReadGroupAddress(Bus, address, priority)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 读取组地址（按接口编号）
    ''' </summary>
    ''' <param name="ifCode"></param>
    ''' <param name="address"></param>
    ''' <param name="priority">优先级</param>
    Public Sub ReadGroupAddress(ifCode As String, address As GroupAddress, Optional priority As MessagePriority = MessagePriority.Low)
        Dim BusArray As KnxBus() = GetKnxBus(ifCode) '从接口编号得到的KnxBus数组
        For Each bus As KnxBus In BusArray
            ReadGroupAddress(bus, address, priority)
        Next
    End Sub

    ''' <summary>
    ''' 读取组地址（按对象）
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="priority"></param>
    Public Sub ReadObjectFeedback(obj As KnxObject, Optional priority As MessagePriority = MessagePriority.Low)
        'If String.IsNullOrEmpty(obj.InterfaceCode) Then Exit Sub
        If obj.ContainsGroup(KnxObjectPart.SwitchFeedback) Then
            ReadGroupAddress(obj.InterfaceCode, obj.Groups(KnxObjectPart.SwitchFeedback).Address, priority)
        End If
        If obj.ContainsGroup(KnxObjectPart.ValueFeedback) Then
            ReadGroupAddress(obj.InterfaceCode, obj.Groups(KnxObjectPart.ValueFeedback).Address, priority)
        End If

        'If obj.Groups(KnxObjectPart.SwitchFeedback).Address.Address <> 0 Then
        '    ReadGroupAddress(obj.InterfaceCode, obj.Groups(KnxObjectPart.SwitchFeedback).Address, priority)
        'End If
        'If obj.Groups(KnxObjectPart.ValueFeedback).Address.Address <> 0 Then
        '    ReadGroupAddress(obj.InterfaceCode, obj.Groups(KnxObjectPart.ValueFeedback).Address, priority)
        'End If
    End Sub

    ''' <summary>
    ''' 读取组地址（按对象ID）
    ''' </summary>
    ''' <param name="objId">Objects表中的ID</param>
    ''' <param name="Priority">优先级</param>
    Public Sub ReadObjectFeedback(objId As Integer, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim obj As KnxObject = _Objects.Items(objId) '根据ID获取Object对象
        ReadObjectFeedback(obj, Priority)
    End Sub

    ''' <summary>
    ''' 读取全部Objects表的反馈组地址
    ''' </summary>
    Public Sub PollAllObjects()
        If IsPolling Then Exit Sub '上次轮询未完成，不执行任何操作
        If _Bus.Ready Then
            'Dim th As New Threading.Thread(AddressOf _PollAllObjects) '新建线程执行轮询防止卡顿
            'th.Start()
            Task.Run(Sub() _PollAllObjects()) '新建线程执行轮询防止卡顿
        End If
    End Sub

    Private Sub _PollAllObjects()
        IsPolling = True
        Dim lstIC As New List(Of String) '连接成功总线的接口编号
        For Each r As DataRow In _Bus.Table.Rows
            If r("CnState") = BusConnectionState.Connected Then
                lstIC.Add(r("InterfaceCode"))
            End If
        Next
        For Each obj As KnxObject In _Objects
            If lstIC.Contains(obj.InterfaceCode) Then
                ReadObjectFeedback(obj) '读取组地址
            End If
        Next
        IsPolling = False
    End Sub

    Public Sub PollAddressList(addresses As List(Of GroupAddress))
        If IsPolling Then Exit Sub '上次轮询未完成，不执行任何操作
        If _Bus.Ready Then
            Task.Run(Sub() _PollAddressList(addresses)) '新建线程执行轮询防止卡顿
        End If
    End Sub

    Private Sub _PollAddressList(addresses As List(Of GroupAddress))
        IsPolling = True
        For Each ga As GroupAddress In addresses

        Next
        IsPolling = False
    End Sub

    ''' <summary>
    ''' 从接口编号到总线对象数组
    ''' </summary>
    ''' <param name="ifCode">接口编号字符串</param>
    ''' <returns>KnxBus对象数组</returns>
    Private Function GetKnxBus(ifCode As String) As KnxBus()
        Dim k As New List(Of KnxBus)
        If String.IsNullOrEmpty(ifCode) Then '接口编号为空的情况使用默认接口
            If IsNothing(_Bus.DefaultBus) Then '不可能出现没有默认接口的情况
                Throw New ArgumentNullException(NameOf(ifCode), "No available KNX Interface.")
            Else
                k.Add(_Bus.DefaultBus) '默认接口
            End If
        Else
            For Each str As String In StringToArray(ifCode) '接口编号的数组
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
    ''' <param name="ifCode"></param>
    Public Sub DevicePoll(ifCode As String)
        If IsPolling Then Exit Sub
        Dim bus As KnxBus = _Bus(ifCode)
        If bus.ConnectionState = BusConnectionState.Connected Then
            Dim th As New Threading.Thread(Sub() _DevicePoll(ifCode)) '新建线程打开KNX接口
            th.Start() '启动新线程
        End If
    End Sub

    Private Async Sub _DevicePoll(ifCode As String)
        Dim kdis As KnxDeviceInfo() = _Devices(ifCode)
        Using kn As KnxNetwork = _Bus(ifCode).GetNetwork
            For Each kdi As KnxDeviceInfo In kdis
                Dim result As Boolean = Await kn.PingIndividualAddressAsync(kdi.IndAddress)
                kdi.State = If(result, IndAddressState.Online, IndAddressState.Offline)
            Next
        End Using

    End Sub

End Class
