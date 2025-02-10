Imports System.ComponentModel
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Knx.Falcon.ApplicationData.MasterData

Public Class KnxGroup

    ''' <summary>
    ''' KNX数据类型
    ''' </summary>
    ''' <returns></returns>
    Public Property DPT As DptBase

    ''' <summary>
    ''' 组地址
    ''' </summary>
    ''' <returns></returns>
    Public Property Address As New GroupAddress

    ''' <summary>
    ''' 值
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As GroupValue

    Public Sub New(Optional DPTmain As Integer = 1, Optional DPTsub As Integer = 0)
        If DPTsub > 0 Then
            Me.DPT = DptFactory.Default.Create(DptFactory.Default.GetDatapointSubtype(DPTmain, DPTsub))
        Else
            Me.DPT = DptFactory.Default.Create(DptFactory.Default.GetDatapointType(DPTmain))
        End If
        If Me.DPT Is Nothing Then
            Throw New ArgumentException($"Invalid data type:'{DPTmain}.{DPTsub}'.")
        End If
    End Sub

    Public Sub New(GroupAddress As GroupAddress, Optional DPTmain As Integer = 1, Optional DPTsub As Integer = 0)
        Me.New(DPTmain, DPTsub)
        _Address = GroupAddress
    End Sub

    Public Sub New(AddressString As String, Optional DPTmain As Integer = 1, Optional DPTsub As Integer = 0)
        Me.New(DPTmain, DPTsub)
        Dim ga As New GroupAddress
        If GroupAddress.TryParse(AddressString, ga) Then
            _Address = ga
        Else
            Throw New ArgumentException($"Wrong group address format: '{AddressString}'.")
        End If
    End Sub

    Public Sub SetValue(value As Object)
        _Value = DPT.ToGroupValue(value)
    End Sub

End Class

''' <summary>
''' KNX组成员
''' </summary>
Public Class KnxGroupPair

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
                Throw New InvalidEnumArgumentException($"Wrong KnxObjectPartPoint: {PointString}.")
        End Select
        SetPointValue(pnt, PointValue)
    End Sub

    Public Sub New(DPTmain As Integer, DPTsub As Integer, AddrCtl As String, AddrFdb As String)
        If Not String.IsNullOrEmpty(AddrCtl) Then _CtlAddr = New GroupAddress(AddrCtl)
        If Not String.IsNullOrEmpty(AddrFdb) Then _FdbAddr = New GroupAddress(AddrFdb)
        Me.DPST = DptFactory.Default.GetDatapointSubtype(DPTmain, DPTsub)
        'Dim gaC As New GroupAddress
        'If GroupAddress.TryParse(AddrCtl, gaC) Then
        '    _CtlAddr = gaC
        'Else
        '    Throw New ArgumentException($"Wrong group address format: '{AddrCtl}'.")
        'End If
        'Dim gaF As New GroupAddress
        'If GroupAddress.TryParse(AddrFdb, gaF) Then
        '    _FdbAddr = gaF
        'Else
        '    Throw New ArgumentException($"Wrong group address format: '{AddrFdb}'.")
        'End If
    End Sub

End Class
