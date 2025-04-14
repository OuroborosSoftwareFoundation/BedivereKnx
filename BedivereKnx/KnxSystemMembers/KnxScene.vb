Imports System.Data
Imports Knx.Falcon

Public Class KnxSceneCollection

    Implements IEnumerable

    Private _Items As New Dictionary(Of Integer, KnxScene)

    ''' <summary>
    ''' 场景控制请求
    ''' </summary>
    Protected Friend Event SceneControlRequest As GroupWriteHandler

    Public ReadOnly Property Table As DataTable

    ''' <summary>
    ''' 获取场景（根据ID）
    ''' </summary>
    ''' <param name="index">场景ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(index As Integer) As KnxScene
        Get
            Dim scn As KnxScene = Nothing
            If _Items.TryGetValue(index, scn) Then
                Return scn
            Else
                Throw New KeyNotFoundException($"Can't found Scene with ID = {index}.")
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取场景（根据编号）
    ''' </summary>
    ''' <param name="code">场景编号</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(code As String) As KnxScene()
        Get
            Dim drs As DataRow() = _Table.Select($"SceneCode='{code}'") '在表中按照SceneCode查询
            If drs.Length > 0 Then
                Dim l As New List(Of KnxScene)
                For Each dr As DataRow In drs
                    l.Add(_Items(dr("Id")))
                Next
                Return l.ToArray
            Else '找不到时返回错误
                Throw New KeyNotFoundException($"Can't found SceneCode '{code}' in Scenes.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 获取场景（根据组地址）
    ''' </summary>
    ''' <param name="gaString"></param>
    ''' <returns></returns>
    Default Public ReadOnly Property Items(gaString As GroupAddress) As KnxScene()
        Get
            Dim drs As DataRow() = _Table.Select($"GroupAddress = '{gaString}'") '找出组地址所属对象，可能有多个
            If drs.Length > 0 Then
                Dim l As New List(Of KnxScene)
                For Each dr As DataRow In drs
                    l.Add(_Items(dr("Id")))
                Next
                Return l.ToArray
            Else
                Throw New KeyNotFoundException($"Can't found GroupAddress '{gaString}' in Scenes.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 场景数量
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return _Items.Count
        End Get
    End Property

    Public Sub New()
        _Table = New DataTable
    End Sub

    Public Sub New(dt As DataTable)
        _Table = dt
        For Each dr As DataRow In _Table.Rows
            Dim ga As New GroupAddress(dr("GroupAddress").ToString)
            Dim scn As New KnxScene(dr("Id"), dr("SceneCode").ToString, dr("SceneName").ToString, ga, dr("InterfaceCode").ToString)
            Dim pairs As String() = dr("SceneValues").ToString.Split(","c) '场景值的数组
            For i = 0 To pairs.Length - 1
                Dim vn As String() = pairs(i).Split("="c) '{场景地址, 场景名}
                If IsNumeric(vn(0).Trim) AndAlso (Convert.ToInt32(vn(0).Trim) >= 0 And Convert.ToInt32(vn(0).Trim) < 64) Then
                    Dim adr As Byte = Convert.ToByte(vn(0).Trim) '场景地址
                    scn.Names(adr) = vn(1).Trim
                Else
                    Throw New IndexOutOfRangeException($"Scene [{dr("SceneName")}: {vn(1)}] have invalid scene address：'{vn(0)}'.")
                End If
            Next
            _Items.Add(scn.Id, scn)
            AddHandler scn.WriteRequest, AddressOf _GroupWriteHandler
        Next
    End Sub

    Private Sub _GroupWriteHandler(e As KnxWriteEventArgs)
        RaiseEvent SceneControlRequest(e)
    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return _Items.Values.GetEnumerator()
    End Function

End Class

''' <summary>
''' KNX场景
''' </summary>
Public Class KnxScene : Inherits KnxObjectBase

    ''' <summary>
    ''' 场景地址名称
    ''' </summary>
    ''' <returns></returns>
    Public Property Names As String()

    ''' <summary>
    ''' 新建KNX场景组对象
    ''' </summary>
    ''' <param name="id">场景组ID</param>
    ''' <param name="ga">场景组地址</param>
    ''' <param name="ifCode">接口编号</param>
    Public Sub New(id As Integer, ga As GroupAddress, ifCode As String)
        MyBase.New(KnxGroupType.Scene, id, ifCode)
        Me.Groups(KnxObjectPart.SceneControl) = New KnxGroup(ga, 18, 1) '新建DPST18.001对象
        ReDim Me.Names(63) '重置场景名数组
    End Sub

    ''' <summary>
    ''' 新建KNX场景组对象
    ''' </summary>
    ''' <param name="id">场景组ID</param>
    ''' <param name="code">场景编号</param>
    ''' <param name="name">场景名称</param>
    ''' <param name="ga">场景组地址</param>
    ''' <param name="ifCode">接口编号</param>
    Public Sub New(id As Integer, code As String, name As String, ga As GroupAddress, ifCode As String)
        Me.New(id, ga, ifCode)
        Me.Code = code
        Me.Name = name
    End Sub

    ''' <summary>
    ''' 场景控制
    ''' </summary>
    ''' <param name="scnNumber">场景地址（0~63）</param>
    ''' <param name="isLearn">是否学习场景</param>
    ''' <param name="priority">优先级</param>
    Public Sub WriteScene(scnNumber As Byte, Optional isLearn As Boolean = False, Optional priority As MessagePriority = MessagePriority.Low)
        If scnNumber > 63 Then '场景地址大于63报错
            Throw New IndexOutOfRangeException($"Invalid scene address: {scnNumber}.")
        End If
        If isLearn Then scnNumber += 128 '场景学习的值=控制值+128
        MyBase.WriteValue(KnxObjectPart.SceneControl, scnNumber, priority)
    End Sub

End Class
