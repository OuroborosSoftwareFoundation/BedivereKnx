Imports System.Data
Imports Knx.Falcon

''' <summary>
''' KNX对象集合
''' </summary>
Public Class KnxSystemObjectCollection

    Implements IEnumerable

    Private _Table As DataTable
    Private _Item As New Dictionary(Of Integer, KnxObjectGroup)

    ''' <summary>
    ''' 组地址写入请求
    ''' </summary>
    Protected Friend Event GroupWriteRequest As GroupWriteHandler

    ''' <summary>
    ''' 组地址读取请求
    ''' </summary>
    Protected Friend Event GroupReadRequest As GroupReadHandler

    Public ReadOnly Property Table As DataTable
        Get
            Return _Table
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据ID）
    ''' </summary>
    ''' <param name="index">对象ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(index As Integer) As KnxObjectGroup
        Get
            Dim grp As KnxObjectGroup = Nothing
            If _Item.TryGetValue(index, grp) Then
                Return grp
            Else
                Throw New ArgumentNullException($"Can't found Object with ID = {index}.")
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据编号）
    ''' </summary>
    ''' <param name="code">对象编号</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(code As String) As KnxObjectGroup()
        Get
            Dim drs As DataRow() = _Table.Select($"ObjectCode='{code}'") '在表中按照ObjectCode查询
            If drs.Length > 0 Then
                Dim l As New List(Of KnxObjectGroup)
                For Each dr As DataRow In drs
                    l.Add(_Item(dr("Id")))
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
    ''' <param name="CodeArray"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(CodeArray As String()) As KnxObjectGroup()
        Get
            Dim l As New List(Of KnxObjectGroup)
            For Each code As String In CodeArray '遍历目标编号
                l.AddRange(Me.Item(code))
            Next
            Return l.ToArray
        End Get
    End Property

    ''' <summary>
    ''' 获取对象（根据组地址和成员）
    ''' </summary>
    ''' <param name="ga"></param>
    ''' <param name="AddrType"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(ga As String, AddrType As String) As KnxObjectGroup()
        Get
            Dim drs As DataRow() = _Table.Select($"{AddrType}Addr = '{ga}'") '找出组地址所属对象，可能有多个
            If drs.Length > 0 Then
                Dim l As New List(Of KnxObjectGroup)
                For Each dr As DataRow In drs
                    l.Add(_Item(dr("Id")))
                Next
                Return l.ToArray
            Else
                Throw New KeyNotFoundException($"Can't found [{AddrType}] GroupAddress '{ga}' in Objects.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 对象数量
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return _Item.Count
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
                Dim GrpType As KnxGroupType '组地址类型
                If Not [Enum].TryParse(dr("ObjectType"), GrpType) Then '字符串转枚举
                    Throw New ArgumentException($"Wrong ObjectType in Objects: [{dr("ObjectCode")}]{dr("ObjectName")}.")
                End If
                Dim obj As New KnxObjectGroup(GrpType, dr("Id"), dr("ObjectCode").ToString, dr("ObjectName").ToString, dr("InterfaceCode").ToString)
                Dim SwDpt As Integer() = StringToDptNum(dr("Sw_GrpDpt").ToString) '开关DPT数字
                obj.SwitchPart = New KnxGroupPair(SwDpt(0), SwDpt(1), dr("Sw_Ctl_GrpAddr").ToString, dr("Sw_Fdb_GrpAddr").ToString)
                Dim ValDpt As Integer() = StringToDptNum(dr("Val_GrpDpt").ToString) '数值DPT数字
                obj.ValuePart = New KnxGroupPair(ValDpt(0), ValDpt(1), dr("Val_Ctl_GrpAddr").ToString, dr("Val_Fdb_GrpAddr").ToString)
                _Item.Add(obj.Id, obj)
                AddHandler obj.GroupWriteRequest, AddressOf _GroupWriteRequest
                AddHandler obj.GroupReadRequest, AddressOf _GroupReadRequest
            Next
        End With
    End Sub

    Private Function StringToDptNum(Str As String) As Integer()
        If String.IsNullOrEmpty(Str) Then
            Return {0, 0}
        End If
        Dim DptStr() As String = Str.Split(":"c)(0).Split("."c) 'DPT数字
        If DptStr.Length <> 2 Then
            Throw New ArgumentException($"Invalid DatapointType: {Str}.")
            Return {0, 0}
        End If
        Dim NumM As Integer = 0 'DPT主类型数字
        If IsNumeric(DptStr(0)) Then
            NumM = Convert.ToInt32(DptStr(0))
            NumM = Math.Abs(NumM) '去除负号
        Else
            Throw New ArgumentException($"Invalid DatapointType: {Str}.")
            Return {0, 0}
        End If
        Dim NumS As Integer = 0 'DPT子类型数字，非数字的情况视为0
        If IsNumeric(DptStr(1)) Then
            NumS = Convert.ToInt32(DptStr(1))
            NumS = Math.Abs(NumS) '去除负号
        End If
        Return {NumM, NumS}
    End Function

    Private Sub _GroupWriteRequest(e As KnxWriteEventArgs)
        RaiseEvent GroupWriteRequest(e)
    End Sub

    Private Sub _GroupReadRequest(e As KnxReadEventArgs)
        RaiseEvent GroupReadRequest(e)
    End Sub

    ''' <summary>
    ''' 接收报文事件
    ''' </summary>
    ''' <param name="ga"></param>
    ''' <param name="val"></param>
    Public Sub ReceiveGroupMessage(GA As GroupAddress, GrpVal As GroupValue)
        Dim OptCol As String = vbNullString '组类型
        Dim TypVal As Object = Nothing '类型化后的组地址值
        Dim ObjPart As KnxObjectPart '组成员
        Dim ObjPoint As KnxObjectPartPoint = KnxObjectPartPoint.Feedback '组成员点位
        Select Case GrpVal.SizeInBit '1:Boolean, 2~8:Byte, >8:Array of Byte
            Case < 8 'Boolean
                OptCol = "Sw_Fdb"
                TypVal = GrpVal.TypedValue
                ObjPart = KnxObjectPart.Switch
            Case 8
                OptCol = "Val_Fdb"
                TypVal = GrpVal.TypedValue
                ObjPart = KnxObjectPart.Value
            Case > 8
                OptCol = "Val_Fdb"
                Dim bs As Byte() = GrpVal.TypedValue
                For i = 0 To bs.Length - 1
                    TypVal = vbNullString & bs(i)
                Next
                ObjPart = KnxObjectPart.Value
        End Select
        For Each dr As DataRow In _Table.Select($"{OptCol}_GrpAddr = '{GA}'") '找出组地址所属对象，可能有多个
            If Not IsNothing(TypVal) Then dr($"{OptCol}_Value") = TypVal '表格更新
            Dim id As Integer = dr("Id")
            _Item(id).GetPart(ObjPart).SetPointValue(ObjPoint, GrpVal) '对象值更新
        Next
    End Sub

    Public Function GetTableRow(index As Integer) As DataRow
        Return _Table(index)
    End Function

    Public Function GetTableRow(code As String) As DataRow()
        Dim drs As DataRow() = _Table.Select($"ObjectCode='{code}'") '在表中按照ObjectCode查询
        If drs.Length > 0 Then
            Return drs
        Else '找不到时返回错误
            Throw New KeyNotFoundException($"Can't found ObjectCode '{code}' in Objects.")
        End If
    End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return _Item.Values.GetEnumerator()
        'Throw New NotImplementedException()
    End Function

End Class
