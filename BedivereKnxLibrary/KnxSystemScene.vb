Imports System.Data
Imports Knx.Falcon

Public Class KnxSystemSceneCollection

    Implements IEnumerable

    Private _Table As DataTable
    'Private _dicIdCode As New Dictionary(Of Integer, String) 'Id-Code
    Private _Item As New Dictionary(Of Integer, KnxSceneGroup)

    Protected Friend Event SceneControlRequest As GroupWriteHandler

    Public ReadOnly Property Table As DataTable
        Get
            Return _Table
        End Get
    End Property

    ''' <summary>
    ''' 根据ID获取场景
    ''' </summary>
    ''' <param name="index">场景ID</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(index As Integer) As KnxSceneGroup
        Get
            If _Item.ContainsKey(index) Then
                Return _Item(index)
            Else
                Throw New ArgumentNullException($"Can't found Scene with ID = {index}.")
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' 根据编号获取场景
    ''' </summary>
    ''' <param name="code">场景编号</param>
    ''' <returns></returns>
    Default Public ReadOnly Property Item(code As String) As KnxSceneGroup()
        Get
            Dim drs As DataRow() = _Table.Select($"SceneCode='{code}'") '在表中按照SceneCode查询
            If drs.Length > 0 Then
                Dim l As New List(Of KnxSceneGroup)
                For Each dr As DataRow In drs
                    l.Add(_Item(dr("Id")))
                Next
                Return l.ToArray
            Else '找不到时返回错误
                Throw New KeyNotFoundException($"Can't found SceneCode '{code}' in Scenes.")
            End If
        End Get
    End Property

    ''' <summary>
    ''' 场景数量
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return _Item.Count
        End Get
    End Property

    Public Sub New(dt As DataTable)
        _Table = dt
        For Each dr As DataRow In _Table.Rows
            '_dicIdCode.Add(dr("Id"), dr("SceneCode"))
            Dim ga As New GroupAddress(dr("GroupAddress").ToString)
            Dim scn As New KnxSceneGroup(dr("Id"), dr("SceneCode").ToString, dr("SceneName").ToString, ga, dr("InterfaceCode").ToString)
            Dim sv As String() = dr("SceneValues").ToString.Split(","c) '场景值的数组
            For i = 0 To sv.Length - 1
                Dim v As String() = sv(i).Split("="c) '{场景地址, 场景名}
                If IsNumeric(v(0).Trim) AndAlso (Convert.ToInt32(v(0).Trim) >= 0 And Convert.ToInt32(v(0).Trim) < 64) Then
                    Dim adr As Byte = Convert.ToByte(v(0).Trim) '场景地址
                    scn.AddrNames(adr) = v(1).Trim
                Else
                    Throw New ArgumentException($"Scene [{dr("SceneName")}: {v(0)}] have invalid scene address：'{v(0)}'.")
                End If
            Next
            _Item.Add(scn.Id, scn)
            AddHandler scn.SceneControlRequest, AddressOf _GroupWriteHandler
        Next
    End Sub

    Private Sub _GroupWriteHandler(e As KnxWriteEventArgs)
        RaiseEvent SceneControlRequest(e)
    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return _Item.Values.GetEnumerator()
    End Function

End Class
