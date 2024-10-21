Imports System.ComponentModel
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Knx.Falcon.ApplicationData.MasterData

''' <summary>
''' KNX对象的基类
''' </summary>
Public MustInherit Class KnxObjectBase

    Public ReadOnly Property Type As KnxGroupType

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

    Public Sub New(ObjType As KnxGroupType, ObjId As Integer, IfCode As String)
        _Type = ObjType
        _Id = ObjId
        _InterfaceCode = IfCode
    End Sub

    Public Sub New(ObjType As KnxGroupType, ObjId As Integer, ObjCode As String, ObjName As String, IfCode As String)
        _Type = ObjType
        _Id = ObjId
        _Code = ObjCode
        _Name = ObjName
        _InterfaceCode = IfCode
    End Sub

End Class

''' <summary>
''' KNX组成员
''' </summary>
Public Class KnxGroupPart

    ''' <summary>
    ''' KNX数据类型（主类型）
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DPT As DatapointType

    ''' <summary>
    ''' KNX数据类型（子类型）
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DPST As DatapointSubtype

    ''' <summary>
    ''' 控制组地址
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CtlAddr As New GroupAddress

    ''' <summary>
    ''' 控制值
    ''' </summary>
    ''' <returns></returns>
    Public Property CtlValue As GroupValue

    ''' <summary>
    ''' 反馈组地址
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FdbAddr As New GroupAddress

    ''' <summary>
    ''' 反馈值
    ''' </summary>
    ''' <returns></returns>
    Public Property FdbValue As GroupValue

    Public Sub SetPointValue(PointEnum As KnxObjectPartPoint, PointValue As GroupValue)
        Select Case PointEnum
            Case KnxObjectPartPoint.Control
                _CtlValue = PointValue
            Case KnxObjectPartPoint.Feedback
                _FdbValue = PointValue
            Case Else
                Throw New InvalidEnumArgumentException($"Wrong KnxObjectPartPoint: {PointEnum}.")
        End Select
    End Sub

    Public Sub SetPointValue(PointString As String, PointValue As GroupValue)
        Dim pnt As KnxObjectPartPoint
        Select Case PointString.ToLower.Trim
            Case "control", "ctl"
                pnt = KnxObjectPartPoint.Control
            Case "feedback", "fdb"
                pnt = KnxObjectPartPoint.Feedback
            Case Else
                Throw New ArgumentNullException($"Wrong KnxObjectPartPoint: {PointString}.")
        End Select
        SetPointValue(pnt, PointValue)
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(DPTmain As Integer, DPTsub As Integer, AddrCtl As String, AddrFdb As String)
        _DPT = DptFactory.Default.GetDatapointType(DPTmain)
        _DPST = DptFactory.Default.GetDatapointSubtype(DPTmain, DPTsub)
        If Not String.IsNullOrEmpty(AddrCtl) Then _CtlAddr = New GroupAddress(AddrCtl)
        If Not String.IsNullOrEmpty(AddrFdb) Then _FdbAddr = New GroupAddress(AddrFdb)
    End Sub

End Class

''' <summary>
''' KNX对象组
''' </summary>
Public Class KnxObjectGroup

    Inherits KnxObjectBase

    Protected Friend Event GroupReadRequest As GroupReadHandler
    Protected Friend Event GroupWriteRequest As GroupWriteHandler

    Public Property SwitchPart As KnxGroupPart

    Public Property ValuePart As KnxGroupPart

    Public Function GetPart(PartString As String) As KnxGroupPart
        Select Case PartString.ToLower.Trim
            Case "switch", "sw"
                Return _SwitchPart
            Case "value", "val"
                Return _ValuePart
            Case Else
                Throw New ArgumentNullException($"Wrong KnxObjectPart: {PartString}.")
        End Select
    End Function

    Public Function GetPart(PartEnum As KnxObjectPart) As KnxGroupPart
        Return GetPart(PartEnum.ToString)
    End Function

    ''' <param name="ObjType">对象组类型</param>
    ''' <param name="ObjId">对象组ID</param>
    ''' <param name="ObjCode">对象组编号</param>
    ''' <param name="ObjName">对象组名称</param>
    ''' <param name="IfCode">接口编号</param>
    Public Sub New(ObjType As KnxGroupType, ObjId As Integer, ObjCode As String, ObjName As String, IfCode As String)
        MyBase.New(ObjType, ObjId, ObjCode, ObjName, IfCode)
        _SwitchPart = New KnxGroupPart(0, 0, vbNullString, vbNullString)
        _ValuePart = New KnxGroupPart(0, 0, vbNullString, vbNullString)
    End Sub

    ''' <summary>
    ''' 读取值（根据字符串判断成员）
    ''' </summary>
    ''' <param name="PartString"></param>
    ''' <param name="Priority"></param>
    Public Sub ReadValue(PartString As String, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim GRA As KnxReadEventArgs
        Select Case PartString.ToLower.Trim
            Case "switch", "sw"
                GRA = New KnxReadEventArgs(Me.InterfaceCode, Me.SwitchPart.FdbAddr, Priority)
            Case "value", "val"
                GRA = New KnxReadEventArgs(Me.InterfaceCode, Me.ValuePart.FdbAddr, Priority)
            Case Else
                Throw New ArgumentNullException($"Wrong KnxObjectPart: {PartString}.")
        End Select
        RaiseEvent GroupReadRequest(GRA)
    End Sub

    ''' <summary>
    ''' 读取值（根据枚举判断成员）
    ''' </summary>
    ''' <param name="PartEnum"></param>
    ''' <param name="Priority"></param>
    Public Sub ReadValue(PartEnum As KnxObjectPart, Optional Priority As MessagePriority = MessagePriority.Low)
        ReadValue(PartEnum.ToString, Priority)
    End Sub

    ''' <summary>
    ''' 写入值（根据字符串判断成员）
    ''' </summary>
    ''' <param name="PartString">组成员枚举值</param>
    ''' <param name="GroupVal">写入值</param>
    ''' <param name="Priority">优先级</param>
    Public Sub SetValue(PartString As String, GroupVal As GroupValue, Optional Priority As MessagePriority = MessagePriority.Low)
        Dim GWA As KnxWriteEventArgs
        Select Case PartString.ToLower.Trim
            Case "switch", "sw"
                GWA = New KnxWriteEventArgs(Me.InterfaceCode, Me.SwitchPart.CtlAddr, GroupVal, Priority)
            Case "value", "val"
                GWA = New KnxWriteEventArgs(Me.InterfaceCode, Me.ValuePart.CtlAddr, GroupVal, Priority)
            Case Else
                Throw New ArgumentNullException($"Wrong KnxObjectPart: {PartString}.")
        End Select
        RaiseEvent GroupWriteRequest(GWA)
    End Sub

    ''' <summary>
    ''' 写入值（根据枚举判断成员）
    ''' </summary>
    ''' <param name="PartEnum">组成员枚举值</param>
    ''' <param name="GroupVal">写入值</param>
    ''' <param name="Priority">优先级</param>
    Public Sub SetValue(PartEnum As KnxObjectPart, GroupVal As GroupValue, Optional Priority As MessagePriority = MessagePriority.Low)
        SetValue(PartEnum.ToString, GroupVal, Priority)
    End Sub

End Class

''' <summary>
''' KNX场景组对象
''' </summary>
Public Class KnxSceneGroup

    Inherits KnxObjectBase 'Type属性固定为KnxGroupType.Scene

    Public Event SceneControlRequest As GroupWriteHandler

    ''' <summary>
    ''' 组地址
    ''' </summary>
    ''' <returns></returns>
    Public Property GroupAddress As GroupAddress

    ''' <summary>
    ''' 场景地址名称
    ''' </summary>
    ''' <returns></returns>
    Public Property AddrNames As String()

    ''' <summary>
    ''' 新建场景组对象
    ''' </summary>
    ''' <param name="ScnId">场景组ID</param>
    ''' <param name="GA">场景组地址</param>
    ''' <param name="IfCode">接口编号</param>
    Public Sub New(ScnId As Integer, GA As GroupAddress, IfCode As String)
        MyBase.New(KnxGroupType.Scene, ScnId, IfCode)
        _GroupAddress = GA
        ReDim _AddrNames(63)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ScnId">场景组ID</param>
    ''' <param name="ScnCode">场景编号</param>
    ''' <param name="ScnName">场景名称</param>
    ''' <param name="GA">场景组地址</param>
    ''' <param name="IfCode">接口编号</param>
    Public Sub New(ScnId As Integer, ScnCode As String, ScnName As String, GA As GroupAddress, IfCode As String)
        MyBase.New(KnxGroupType.Scene, ScnId, ScnCode, ScnName, IfCode)
        _GroupAddress = GA
        ReDim _AddrNames(63)
    End Sub

    ''' <summary>
    ''' 场景控制
    ''' </summary>
    ''' <param name="ScnAddr">场景地址（0~63）</param>
    ''' <param name="isLearn">是否学习场景</param>
    ''' <param name="Priority">优先级</param>
    Public Sub ControlScene(ScnAddr As Byte, Optional isLearn As Boolean = False, Optional Priority As MessagePriority = MessagePriority.Low)
        If ScnAddr > 63 Then '场景地址大于63报错
            Throw New ArgumentException($"Invalid scene address:{ScnAddr}")
        End If
        If isLearn Then ScnAddr += 128 '场景学习的值=控制值+128
        Dim GWA As New KnxWriteEventArgs(Me.InterfaceCode, Me.GroupAddress, New GroupValue(ScnAddr), Priority)
        RaiseEvent SceneControlRequest(GWA)
    End Sub

    Public Function ToAddrNamePair() As Dictionary(Of Byte, String)
        Dim dic As New Dictionary(Of Byte, String)
        For i = 0 To _AddrNames.Length - 1
            Dim str As String = _AddrNames(i)
            If String.IsNullOrEmpty(str) Then Continue For '跳过空项
            dic.Add(i, str)
        Next
        Return dic
    End Function

End Class

Public Class KnxObjectGeneric

    Public ReadOnly Property InterfaceCode As String

    Public ReadOnly Property GroupAddress As GroupAddress

    Public ReadOnly Property GroupValueType As GroupValueType

    Sub New(IfCode As String, GroupAddr As GroupAddress, GroupType As GroupValueType)
        _InterfaceCode = IfCode
        _GroupAddress = GroupAddr
        _GroupValueType = GroupType
    End Sub

End Class
