Imports System.Data
Imports Knx.Falcon

''' <summary>
''' KNX对象集合
''' </summary>
Public Class KnxObjectCollection

    Implements IEnumerable

    ''' <summary>
    ''' 组地址写入请求
    ''' </summary>
    Protected Friend Event GroupWriteRequest As GroupWriteHandler

    ''' <summary>
    ''' 组地址读取请求
    ''' </summary>
    Protected Friend Event GroupReadRequest As GroupReadHandler

    Private _Items As New Dictionary(Of Integer, KnxObject)
    Protected Friend ReadOnly AddressColumns As String() = {"Sw_Ctl_GrpAddr", "Sw_Fdb_GrpAddr", "Val_Ctl_GrpAddr", "Val_Fdb_GrpAddr"}

    Public ReadOnly Property Table As DataTable

    ''' <summary>
    ''' 获取对象（根据ID）
    ''' </summary>
    ''' <param name="index">对象ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(index As Integer) As KnxObject
        Get
            Dim grp As KnxObject = Nothing
            If _Items.TryGetValue(index, grp) Then
                Return grp
            Else
                Throw New KeyNotFoundException($"Can't found Object with ID = {index}.")
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据编号）
    ''' </summary>
    ''' <param name="code">对象编号</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(code As String) As KnxObject()
        Get
            Dim drs As DataRow() = _Table.Select($"ObjectCode='{code}'") '在表中按照ObjectCode查询
            If drs.Length > 0 Then
                Dim l As New List(Of KnxObject)
                For Each dr As DataRow In drs
                    l.Add(_Items(dr("Id")))
                Next
                Return l.ToArray
            Else '找不到时返回错误
                Throw New KeyNotFoundException($"Can't found ObjectCode '{code}' in Objects.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据编号数组）
    ''' </summary>
    ''' <param name="codes"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(codes As String()) As KnxObject()
        Get
            Dim l As New List(Of KnxObject)
            For Each code As String In codes '遍历目标编号
                l.AddRange(Me.Items(code))
            Next
            Return l.ToArray
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据组地址和成员）
    ''' </summary>
    ''' <param name="gaString"></param>
    ''' <param name="objPart">Sw_Ctl,Sw_Fdb,Val_Ctl,Val_Fdb</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(gaString As String, objPart As String) As KnxObject()
        Get
            Dim drs As DataRow() = _Table.Select($"{objPart}_GrpAddr = '{gaString}'") '找出组地址所属对象，可能有多个
            If drs.Length > 0 Then
                Dim l As New List(Of KnxObject)
                For Each dr As DataRow In drs
                    l.Add(_Items(dr("Id")))
                Next
                Return l.ToArray
            Else
                Throw New KeyNotFoundException($"Can't found [{objPart}] GroupAddress '{gaString}' in Objects.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 对象数量
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return _Items.Count
        End Get
    End Property

    Public Sub New()
        _Table = New DataTable
        With _Table
            .Columns.Add("Sw_Fdb_Value", GetType(Byte)) '附加反馈状态列
            .Columns.Item("Sw_Fdb_Value").Caption = "开关反馈"
            .Columns.Add("Val_Fdb_Value", GetType(Decimal)) '附加反馈状态列
            .Columns.Item("Val_Fdb_Value").Caption = "数值反馈"
        End With
    End Sub

    Public Sub New(dt As DataTable)
        _Table = dt
        With _Table
            .Columns.Add("Sw_Fdb_Value", GetType(Byte)) '附加反馈状态列
            .Columns.Item("Sw_Fdb_Value").Caption = "开关反馈"
            .Columns.Add("Val_Fdb_Value", GetType(Decimal)) '附加反馈状态列
            .Columns.Item("Val_Fdb_Value").Caption = "数值反馈"
            For Each dr As DataRow In _Table.Rows
                'MsgBox(dr("ObjectCode"))
                Dim GrpType As KnxGroupType '组地址类型
                If Not [Enum].TryParse(dr("ObjectType"), GrpType) Then '字符串转枚举
                    Throw New ArgumentException($"Wrong ObjectType in Objects: [{dr("ObjectCode")}]{dr("ObjectName")}.")
                End If
                Dim obj As New KnxObject(GrpType, dr("Id"), dr("ObjectCode").ToString, dr("ObjectName").ToString, dr("InterfaceCode").ToString)
                If Not IsDBNull(dr("Sw_Ctl_GrpAddr")) Then
                    obj.Groups(KnxObjectPart.SwitchControl) = New KnxGroup(dr("Sw_Ctl_GrpAddr").ToString, dr("Sw_GrpDpt").ToString)
                End If
                If Not IsDBNull(dr("Sw_Fdb_GrpAddr")) Then
                    obj.Groups(KnxObjectPart.SwitchFeedback) = New KnxGroup(dr("Sw_Fdb_GrpAddr").ToString, dr("Sw_GrpDpt").ToString)
                End If
                If Not IsDBNull(dr("Val_Ctl_GrpAddr")) Then
                    obj.Groups(KnxObjectPart.ValueControl) = New KnxGroup(dr("Val_Ctl_GrpAddr").ToString, dr("Val_GrpDpt").ToString)
                End If
                If Not IsDBNull(dr("Val_Fdb_GrpAddr")) Then
                    obj.Groups(KnxObjectPart.ValueFeedback) = New KnxGroup(dr("Val_Fdb_GrpAddr").ToString, dr("Val_GrpDpt").ToString)
                End If
                _Items.Add(obj.Id, obj) '添加KnxObject对象
                AddHandler obj.WriteRequest, AddressOf _GroupWriteRequest
                AddHandler obj.ReadRequest, AddressOf _GroupReadRequest
            Next
        End With
    End Sub

    'Private Shared Function GetDptNumFromDt(dptString As String) As Integer()
    '    If String.IsNullOrEmpty(dptString) Then
    '        Return {-1, -1}
    '    End If
    '    Dim DptStr() As String = dptString.Split(":"c)(0).Split("."c) 'DPT数字
    '    If DptStr.Length <> 2 Then
    '        Throw New ArgumentException($"Invalid DatapointType: {dptString}.")
    '        Return {-1, -1}
    '    End If
    '    Dim NumM As Integer = 0 'DPT主类型数字
    '    If IsNumeric(DptStr(0)) Then
    '        NumM = Convert.ToInt32(DptStr(0))
    '        NumM = Math.Abs(NumM) '去除负号
    '    Else
    '        Throw New ArgumentException($"Invalid DatapointType: {dptString}.")
    '        Return {-1, -1}
    '    End If
    '    Dim NumS As Integer = 0 'DPT子类型数字，非数字的情况视为0
    '    If IsNumeric(DptStr(1)) Then
    '        NumS = Convert.ToInt32(DptStr(1))
    '        NumS = Math.Abs(NumS) '去除负号
    '    End If
    '    Return {NumM, NumS}
    'End Function

    ''' <summary>
    ''' KnxOject中的KnxGroup触发的写入请求传递至KnxObjectCollection的事件
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub _GroupWriteRequest(e As KnxWriteEventArgs)
        RaiseEvent GroupWriteRequest(e)
    End Sub

    ''' <summary>
    ''' KnxOject中的KnxGroup触发的读取请求传递至KnxObjectCollection的事件
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub _GroupReadRequest(e As KnxReadEventArgs)
        RaiseEvent GroupReadRequest(e)
    End Sub

    ''' <summary>
    ''' 接收报文事件，由KnxSystem中的_GroupMessageReceived触发
    ''' 接收报文并将其写入包含的KnxGroup对象中
    ''' </summary>
    ''' <param name="groupAddress"></param>
    ''' <param name="groupValue"></param>
    Public Sub ReceiveGroupMessage(groupAddress As GroupAddress, groupValue As GroupValue)
        Dim matches = From row As DataRow In Table.AsEnumerable()
                      From col As String In AddressColumns
                      Where row(col).ToString() = groupAddress.ToString
                      Select New With {
                          .id = row.Field(Of Integer)("Id"),
                          col
                          } '在各地址列中查找收到的组地址
        For Each match In matches
            _Items(match.id).Groups(match.col).SetValue(groupValue) '把对象中的KNX组写入值
            Dim ValCol As String = match.col.Replace("GrpAddr", "Value") '对应组地址值的列
            If _Table.Columns.Contains(ValCol) Then
                _Table(match.id)(ValCol) = groupValue.TypedValue '更新表格中的值
            End If
        Next
    End Sub

    'Public Function GetTableRow(index As Integer) As DataRow
    '    Return _Table(index)
    'End Function

    'Public Function GetTableRow(code As String) As DataRow()
    '    Dim drs As DataRow() = _Table.Select($"ObjectCode='{code}'") '在表中按照ObjectCode查询
    '    If drs.Length > 0 Then
    '        Return drs
    '    Else '找不到时返回错误
    '        Throw New KeyNotFoundException($"Can't found ObjectCode '{code}' in Objects.")
    '    End If
    'End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return _Items.Values.GetEnumerator()
    End Function

End Class

''' <summary>
''' KNX对象
''' </summary>
Public Class KnxObject : Inherits KnxObjectBase

    ''' <summary>
    ''' 新建KNX对象
    ''' </summary>
    ''' <param name="type">对象类型</param>
    ''' <param name="id">对象ID</param>
    ''' <param name="ifCode">接口编号</param>
    Public Sub New(type As KnxGroupType, id As Integer, ifCode As String)
        MyBase.New(type, id, ifCode)
    End Sub

    ''' <summary>
    ''' 新建KNX对象
    ''' </summary>
    ''' <param name="type">对象类型</param>
    ''' <param name="id">对象ID</param>
    ''' <param name="code">对象编号</param>
    ''' <param name="name">对象名称</param>
    ''' <param name="ifCode">接口编号</param>
    Public Sub New(type As KnxGroupType, id As Integer, code As String, name As String, ifCode As String)
        MyBase.New(type, id, code, name, ifCode)
    End Sub

End Class
