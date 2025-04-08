Imports System.ComponentModel
Imports System.Data
Imports System.Net
Imports System.Net.NetworkInformation
Imports Knx.Falcon
Imports Knx.Falcon.Configuration
Imports Knx.Falcon.Sdk

''' <summary>
''' KNX总线
''' </summary>
Public Class KnxBusCollection

    Implements IEnumerable

    ''' <summary>
    ''' 组地址轮询申请事件
    ''' </summary>
    Protected Friend Event GroupPollRequest()

    ''' <summary>
    ''' 接口连接状态变化事件
    ''' </summary>
    Public Event ConnectionChanged As EventHandler

    ''' <summary>
    ''' 组地址报文接收事件
    ''' </summary>
    Public Event GroupMessageReceived As KnxMessageHandler

    Private ReadOnly dicItems As New Dictionary(Of String, KnxBus)

    ''' <summary>
    ''' 仅隧道模式，无默认接口
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Property AllTunnelMode As Boolean = False

    ''' <summary>
    ''' 总线就绪状态
    ''' </summary>
    ''' <returns></returns>
    Public Property Ready As Boolean = False

    ''' <summary>
    ''' 默认接口（第一个IpRouting类型）
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DefaultBus As KnxBus

    ''' <summary>
    ''' 对象DataTable
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Table As DataTable

    ''' <summary>
    ''' 获取总线对象（按ID）
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(index As Integer) As KnxBus
        Get
            Return Items(Table.Rows(index)("InterfaceCode").ToString)
        End Get
    End Property

    ''' <summary>
    ''' 获取总线对象（按接口编号）
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(code As String) As KnxBus
        Get
            Dim bus As KnxBus = Nothing
            If dicItems.TryGetValue(code, bus) Then
                Return bus
            Else
                Return DefaultBus '找不到接口编号的情况下直接引用默认接口
            End If
        End Get
    End Property

    ''' <summary>
    ''' 添加接口
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="bus"></param>
    Public Sub Add(code As String, bus As KnxBus)
        dicItems.Add(code, bus)
    End Sub

    ''' <summary>
    ''' 接口总数
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return dicItems.Count
        End Get
    End Property

    Public Sub New()
        Table = New DataTable
        With Table
            .Columns.Add("NetState", GetType(IPStatus)) '网络状态
            .Columns("NetState").Caption = "网络状态"
            .Columns.Add("CnState", GetType(BusConnectionState)) '接口连接状态
            .Columns("CnState").Caption = "连接状态"
            dicItems.Clear()
        End With
        DefaultBus = New KnxBus("Type=IpRouting") '默认接口
        AddHandler DefaultBus.ConnectionStateChanged, AddressOf _ConnectionChanged
        AddHandler DefaultBus.GroupMessageReceived, AddressOf _GroupMessageReceived
    End Sub

    Public Sub New(dt As DataTable, localIp As IPAddress)
        Table = dt
        With Table
            '.PrimaryKey = { .Columns("InterfaceCode")}
            .Columns.Add("NetState", GetType(IPStatus)) '网络状态
            .Columns("NetState").Caption = "网络状态"
            .Columns.Add("CnState", GetType(BusConnectionState)) '接口连接状态
            .Columns("CnState").Caption = "连接状态"
            dicItems.Clear()
            For Each dr As DataRow In _Table.Rows
                Dim cp As ConnectorParameters
                Select Case dr("InterfaceType").ToString.ToLower
                    Case "usb"
                        cp = New UsbConnectorParameters
                    Case "iptunnel"
                        cp = New IpTunnelingConnectorParameters(dr("InterfaceAddress"), Convert.ToInt32(dr("Port")))
                    Case "iprouter"
                        If Net.IPAddress.Parse(dr("InterfaceAddress")) = IpRoutingConnectorParameters.DefaultMulticastAddress AndAlso Convert.ToInt32(dr("Port")) = IpRoutingConnectorParameters.DefaultIpPort Then
                            Continue For '跳过默认路由接口（使用默认接口代替）
                        Else
                            cp = New IpRoutingConnectorParameters(Net.IPAddress.Parse(dr("InterfaceAddress")))
                        End If
                    Case Else '其他情况报错
                        Throw New InvalidEnumArgumentException($"Invalid interface type: '{dr("InterfaceType")}'.")
                End Select
                cp.Name = dr("InterfaceCode")
                cp.AutoReconnect = True '启用自动重连
                Dim k As New KnxBus(cp)
                AddHandler k.ConnectionStateChanged, AddressOf _ConnectionChanged
                AddHandler k.GroupMessageReceived, AddressOf _GroupMessageReceived
                dr("NetState") = IPStatus.Unknown
                dr("CnState") = k.ConnectionState '初始化连接状态
                dicItems.Add(dr("InterfaceCode").ToString, k)
            Next
        End With
        Dim cpD As New IpRoutingConnectorParameters(IpRoutingConnectorParameters.DefaultMulticastAddress, New IndividualAddress(0, 0, 254))
        DefaultBus = New KnxBus($"Type=IpRouting;LocalIPAddress={localIp}") '("Type=IpRouting") '默认接口
        AddHandler DefaultBus.ConnectionStateChanged, AddressOf _ConnectionChanged
        AddHandler DefaultBus.GroupMessageReceived, AddressOf _GroupMessageReceived
    End Sub

    ''' <summary>
    ''' 打开全部接口
    ''' </summary>
    ''' <param name="GroupPoll"></param>
    Public Sub AllConnect(Optional GroupPoll As Boolean = False)
        Dim th As New Threading.Thread(Sub() _AllConnect(GroupPoll)) '新建线程打开KNX接口
        th.Start() '启动新线程
    End Sub

    Private Async Sub _AllConnect(Optional GroupPoll As Boolean = False)
        Ready = False
        If DefaultBus.ConnectionState = BusConnectionState.Closed Then
            Await DefaultBus.ConnectAsync() '打开默认接口
        End If
        For Each dr As DataRow In _Table.Rows
            Try
                If dr("Enable").ToString = "0" Then Continue For
                If dr("CnState") = BusConnectionState.Closed Then '只处理Close状态的接口
                    Dim IfCode As String = dr("InterfaceCode").ToString
                    If dr("InterfaceType").ToString.ToLower.Contains("iptunnel") Then '网络接口
                        Dim p As New Ping
                        Dim pr As PingReply = p.Send(dr("InterfaceAddress").ToString, 100)
                        dr("NetState") = pr.Status
                        RaiseEvent ConnectionChanged(Nothing, Nothing) '触发事件
                        If pr.Status = IPStatus.Success Then
                            Await dicItems(IfCode).ConnectAsync() '异步方式打开接口提高显示速度
                        End If
                    Else
                        Await dicItems(IfCode).ConnectAsync() '异步方式打开接口提高显示速度
                    End If
                End If '跳过已经连接的接口
            Catch ex As Exception
                Throw
            End Try
        Next
        Ready = True
        If GroupPoll Then RaiseEvent GroupPollRequest()
    End Sub

    Private Sub _ConnectionChanged(sender As KnxBus, e As EventArgs)
        For Each dr In _Table.Rows
            dr("CnState") = dicItems(dr("InterfaceCode")).ConnectionState
        Next
        RaiseEvent ConnectionChanged(sender, e) '触发事件
    End Sub

    Private Sub _GroupMessageReceived(sender As Object, e As GroupEventArgs)
        RaiseEvent GroupMessageReceived(New KnxMsgEventArgs(KnxMessageType.FromBus, e), vbNullString) '触发事件
    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return dicItems.Values.GetEnumerator()
    End Function

End Class
