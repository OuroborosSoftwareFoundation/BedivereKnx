Imports System.ComponentModel
Imports Knx.Falcon

''' <summary>
''' KNX对象的基类
''' </summary>
Public MustInherit Class KnxObjectBase

    Protected Friend Event ReadRequest As GroupReadHandler
    Protected Friend Event WriteRequest As GroupWriteHandler

    Private _Groups As New Dictionary(Of KnxObjectPart, KnxGroup)

    Public ReadOnly Property GroupType As KnxObjectType

    ''' <summary>
    ''' 对象ID
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Id As Integer

    ''' <summary>
    ''' 对象编号
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String

    ''' <summary>
    ''' 对象名称
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String

    ''' <summary>
    ''' 接口编号，留空使用IpRouting
    ''' </summary>
    ''' <returns></returns>
    Public Property InterfaceCode As String

    Default Public Property Groups(part As KnxObjectPart) As KnxGroup
        Get
            Dim kg As KnxGroup = Nothing
            If _Groups.TryGetValue(part, kg) Then
                Return kg
            Else
                Throw New KeyNotFoundException($"Can't found {part.ToString} group in {Me.Name}.")
                Return Nothing
            End If
        End Get
        Set(value As KnxGroup)
            If Not _Groups.TryAdd(part, value) Then
                _Groups(part) = value
            End If
        End Set
    End Property

    Default Public Property Groups(partString As String) As KnxGroup
        Get
            Dim part As KnxObjectPart = GetKnxObjectPart(partString)
            Return Me.Groups(part)
        End Get
        Set(value As KnxGroup)
            Dim part As KnxObjectPart = GetKnxObjectPart(partString)
            Me.Groups(part) = value
        End Set
    End Property

    ''' <summary>
    ''' 检查Groups集合中是否存在KnxGroup对象
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function ContainsGroup(key As KnxObjectPart) As Boolean
        Return _Groups.ContainsKey(key)
    End Function

    ''' <summary>
    ''' 尝试获取Groups集合中的KnxGroup 
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function TryGetGroup(key As KnxObjectPart, ByRef value As KnxGroup) As Boolean
        Return _Groups.TryGetValue(key, value)
    End Function

    ''' <summary>
    ''' 尝试添加一个新的KnxGroup到Groups集合中
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    Public Sub TryAdd(key As KnxObjectPart, value As KnxGroup)
        _Groups.TryAdd(key, value)
    End Sub

    ''' <summary>
    ''' KNX对象部件字符串转枚举
    ''' </summary>
    ''' <param name="partString"></param>
    ''' <returns></returns>
    Protected Friend Shared Function GetKnxObjectPart(partString As String) As KnxObjectPart
        partString = partString.Trim.ToLower
        If partString.Contains("sw") Then
            If partString.Contains("ctl") OrElse partString.Contains("control") Then
                Return KnxObjectPart.SwitchControl
            ElseIf partString.Contains("fdb") OrElse partString.Contains("feedback") Then
                Return KnxObjectPart.SwitchFeedback
            Else
                Throw New InvalidEnumArgumentException($"Wrong KnxObjectPart: {partString}.")
            End If
        ElseIf partString.Contains("val") Then
            If partString.Contains("ctl") OrElse partString.Contains("control") Then
                Return KnxObjectPart.ValueControl
            ElseIf partString.Contains("fdb") OrElse partString.Contains("feedback") Then
                Return KnxObjectPart.ValueFeedback
            Else
                Throw New InvalidEnumArgumentException($"Wrong KnxObjectPart: {partString}.")
            End If
        ElseIf partString.Contains("dim") Then
            Return KnxObjectPart.DimmingControl
        ElseIf partString.Contains("scn") OrElse partString.Contains("scene") Then
            Return KnxObjectPart.SceneControl
        Else
            Throw New InvalidEnumArgumentException($"Wrong KnxObjectPart: {partString}.")
        End If
    End Function

    Public Sub New(type As KnxObjectType, id As Integer, ifCode As String)
        _GroupType = type
        _Id = id
        _InterfaceCode = ifCode
        _Groups = New Dictionary(Of KnxObjectPart, KnxGroup)
    End Sub

    Public Sub New(type As KnxObjectType, id As Integer, code As String, name As String, ifCode As String)
        _GroupType = type
        _Id = id
        _Code = code
        _Name = name
        _InterfaceCode = ifCode
        _Groups = New Dictionary(Of KnxObjectPart, KnxGroup)
    End Sub

    ''' <summary>
    ''' 读KNX组
    ''' </summary>
    ''' <param name="part"></param>
    ''' <param name="priority"></param>
    Public Sub ReadValue(part As KnxObjectPart, Optional priority As MessagePriority = MessagePriority.Low)
        Dim GRA As New KnxReadEventArgs(Me.InterfaceCode, Me.Groups(part).Address, priority)
        RaiseEvent ReadRequest(GRA)
    End Sub

    ''' <summary>
    ''' 写KNX组
    ''' </summary>
    ''' <param name="part"></param>
    ''' <param name="value"></param>
    ''' <param name="priority"></param>
    Public Sub WriteValue(part As KnxObjectPart, value As Object, Optional priority As MessagePriority = MessagePriority.Low)
        Dim gv As GroupValue = Me.Groups(part).ToGroupValue(value)
        Dim GWA As New KnxWriteEventArgs(Me.InterfaceCode, Me.Groups(part).Address, gv, priority)
        RaiseEvent WriteRequest(GWA)
    End Sub

End Class

'Public Class KnxObjectGeneric

'    Public ReadOnly Property InterfaceCode As String

'    Public ReadOnly Property GroupAddress As GroupAddress

'    Public ReadOnly Property GroupValueType As GroupValueType

'    Sub New(IfCode As String, GroupAddr As GroupAddress, GroupType As GroupValueType)
'        _InterfaceCode = IfCode
'        _GroupAddress = GroupAddr
'        _GroupValueType = GroupType
'    End Sub

'End Class
