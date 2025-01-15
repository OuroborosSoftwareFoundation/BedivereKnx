Imports System.Data
Imports Knx.Falcon

Public Enum IndAddressState As Integer
    BusError = -2
    Unknown = -1
    Offline = 0
    Online = 1
End Enum

Public Class KnxSystemDeviceCollection

    Implements IEnumerable

    Private _Table As DataTable
    Private _Item As New Dictionary(Of Integer, KnxDeviceInfo)

    ''' <summary>
    ''' 对象DataTable
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Table As DataTable
        Get
            Return _Table
        End Get
    End Property

    ''' <summary>
    ''' 获取设备（根据ID）
    ''' </summary>
    ''' <param name="index">对象ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(index As Integer) As KnxDeviceInfo
        Get
            If _Item.ContainsKey(index) Then
                Return _Item(index)
            Else
                Throw New ArgumentNullException($"Can't found Device with ID = {index}.")
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取设备（根据接口编号）
    ''' </summary>
    ''' <param name="index">对象ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(IfCode As String) As KnxDeviceInfo()
        Get
            Dim drs As DataRow() = _Table.Select($"InterfaceCode = '{IfCode}'")
            If drs.Length > 0 Then
                Dim l As New List(Of KnxDeviceInfo)
                For Each dr As DataRow In drs
                    l.Add(_Item(dr("Id")))
                Next
                Return l.ToArray
            Else '找不到时返回错误
                Throw New KeyNotFoundException($"Can't found Device With InterfaceCode = '{IfCode}'.")
            End If
        End Get
    End Property

    Public Sub New()

    End Sub

    Public Sub New(dt As DataTable)
        _Table = dt
        With _Table
            .Columns.Add("Reachable", GetType(IndAddressState)) '附加通讯状态列
            .Columns.Item("Reachable").Caption = "设备状态"
            For Each dr As DataRow In _Table.Rows
                'dr("Online") = IndAddressState.Unknown
                Dim IndAddr As New IndividualAddress(dr("IndividualAddress").ToString)
                Dim kdi As New KnxDeviceInfo(dr("Id"), dr("DeviceCode").ToString, dr("DeviceName").ToString, IndAddr, dr("InterfaceCode").ToString)
                _Item.Add(kdi.Id, kdi)
                AddHandler kdi.DeviceStateChanged, AddressOf _DeviceStateChanged
            Next
        End With
    End Sub

    Private Sub _DeviceStateChanged(sender As KnxDeviceInfo, e As IndAddressState)
        _Table(sender.Id)("Reachable") = e
    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return _Item.Values.GetEnumerator()
        'Throw New NotImplementedException()
    End Function
End Class

Public Class KnxDeviceInfo

    Private _State As IndAddressState = IndAddressState.Unknown

    Protected Friend Event DeviceStateChanged As KnxDeviceStateHandler

    Public ReadOnly Property Id As Integer

    ''' <summary>
    ''' 编号
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Code As String

    ''' <summary>
    ''' 名称
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' 物理地址
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndAddress As IndividualAddress

    ''' <summary>
    ''' 接口编号，不允许为空
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property InterfaceCode As String

    ''' <summary>
    ''' 设备状态
    ''' </summary>
    ''' <returns></returns>
    Public Property State As IndAddressState
        Get
            Return _State
        End Get
        Set
            _State = Value
            RaiseEvent DeviceStateChanged(Me, Value)
        End Set
    End Property

    Public Sub New(Id As Integer, Code As String, Name As String, IndAddress As IndividualAddress, InterfaceCode As String)
        _Id = Id
        _Code = Code
        _Name = Name
        _IndAddress = IndAddress
        _InterfaceCode = InterfaceCode
        State = IndAddressState.Unknown
    End Sub

End Class