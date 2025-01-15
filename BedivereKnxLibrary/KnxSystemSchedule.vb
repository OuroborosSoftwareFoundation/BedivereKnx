Imports System.Data
Imports System.Timers
Imports Knx.Falcon

''' <summary>
''' KNX时间表
''' </summary>
Public Class KnxSystemSchedule

    Private _Table As DataTable
    Private _Sequence As KnxScheduleSequence
    Private WithEvents _TimerAli As Timer '辅助定时器
    Private WithEvents _Timer As Timer '主定时器
    Private _TimerState As KnxScheduleTimerState

    ''' <summary>
    ''' 时间表触发事件
    ''' </summary>
    Public Event ScheduleEventTriggered As ScheduleEventHandler

    ''' <summary>
    ''' 时间表定时器状态变化事件
    ''' </summary>
    Public Event ScheduleTimerStateChanged As ScheduleTimerHandler

    ''' <summary>
    ''' 时间表DataTable对象
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Table As DataTable
        Get
            Return _Table
        End Get
    End Property

    ''' <summary>
    ''' 时间表的队列
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As KnxScheduleSequence 'Sequence
        Get
            Return _Sequence
        End Get
        Set(value As KnxScheduleSequence)
            _Sequence = value
        End Set
    End Property

    ''' <summary>
    ''' 定时器运行状态
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TimerState As KnxScheduleTimerState
        Get
            Return _TimerState
        End Get
    End Property

    Public Sub New()
        _Sequence = New KnxScheduleSequence
        _TimerAli = New Timer(1000)
        _Timer = New Timer(60000)
    End Sub

    Public Sub New(dt As DataTable)
        _Sequence = New KnxScheduleSequence
        _Table = dt
        _TimerAli = New Timer(1000)
        _Timer = New Timer(60000)
    End Sub

    Public Sub TimerStart()
        If _Sequence.Count = 0 Then Exit Sub '定时队列为空的情况下不启动定时器
        TimerStop()
        _TimerAli.Start()
        RaiseEvent ScheduleTimerStateChanged(KnxScheduleTimerState.Starting)
    End Sub

    Public Sub TimerStop()
        _Timer.Stop()
        _TimerAli.Stop()
        RaiseEvent ScheduleTimerStateChanged(KnxScheduleTimerState.Stoped)
    End Sub

    Private Sub _TimerAli_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _TimerAli.Elapsed
        If e.SignalTime.Second = 0 Then '当前秒数为0时启动主定时器
            _TimerAli.Stop()
            Dim n As New TimeHM(e.SignalTime) '获取当前时间
            For i = 0 To _Sequence.Table.Rows.Count - 1
                If _Sequence.Table.Rows(i)("Time") >= n Then
                    _Sequence.NextId = _Sequence.Table.Rows(i)("Id")
                    Exit For
                End If
            Next
            ScheduleTick(n) '启动主定时器时立即检测一次
            _Timer.Start()
            RaiseEvent ScheduleTimerStateChanged(KnxScheduleTimerState.Running)
        End If
    End Sub

    Private Sub _Timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _Timer.Elapsed
        If _Sequence.Count = 0 Then
            TimerStop()
            Exit Sub
        End If
        Dim n As New TimeHM(e.SignalTime) '获取当前时间
        If n.Hour = 0 AndAlso n.Minute = 0 Then '每天0点刷新“已触发”字段
            For Each r As DataRow In _Sequence.Table.Rows
                r("Triggered") = False
            Next
        End If
        ScheduleTick(n)
    End Sub

    '定时触发
    Private Sub ScheduleTick(t As TimeHM)
        With _Sequence
            If t < .NextTime Then Exit Sub '当前时间早于下次事件则跳过
            For i = .NextId To .Count - 1 '从下次事件ID开始搜索
                If t = .Table(i)("Time") AndAlso .Table(i)("Enable") Then '找到当前时间的定时事件
                    Dim WEA = New KnxWriteEventArgs(.Table.Rows(i)("InterfaceCode").ToString, .Table.Rows(i)("GroupAddress"), .Table.Rows(i)("Value"))
                    Dim LogCode As String = $"{ .Table(i)("ScheduleCode")}_{ .Table(i)("Time")}"
                    RaiseEvent ScheduleEventTriggered(LogCode, WEA)
                    .Table(i)("Triggered") = True
                Else '此处必然是下一次定时的时间
                    .NextId = .Table(i)("Id")
                End If
                If i = .Count - 1 Then '当前事件为最后一个的情况，重置计数
                    .NextId = 0
                End If
            Next
        End With
    End Sub

    Private Sub _ScheduleTimerStateChanged(e As KnxScheduleTimerState) Handles Me.ScheduleTimerStateChanged
        _TimerState = e
    End Sub

End Class

Public Class KnxScheduleSequence

    Private _Table As DataTable
    Private _NextId As Integer = -1
    Private _NextTime As TimeHM

    Public Property Table As DataTable
        Get
            Return _Table
        End Get
        Set(value As DataTable)
            _Table = value
        End Set
    End Property

    ''' <summary>
    ''' 下一次事件的ID
    ''' </summary>
    ''' <returns></returns>
    Public Property NextId As Integer
        Get
            Return _NextId
        End Get
        Set(value As Integer)
            _NextId = Math.Min(value, Me.Count) '防止ID超限制
            If value >= 0 Then
                _NextTime = _Table(value)("Time")
            End If
        End Set
    End Property

    ''' <summary>
    ''' 下一次事件的触发事件
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NextTime As TimeHM
        Get
            Return _NextTime
        End Get
    End Property

    ''' <summary>
    ''' 定时队列中事件的数量
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return _Table.Rows.Count
        End Get
    End Property

    Public Sub New()
        SeqTableInit()
    End Sub

    Private Sub SeqTableInit() '初始化时间表
        _Table = New DataTable
        With _Table
            .Columns.Add("Id", GetType(Integer)) '队列ID（从0开始）
            .Columns.Add("Enable", GetType(Boolean)) '定时启用
            .Columns("Enable").Caption = "启用"
            .Columns.Add("ScheduleCode", GetType(String)) '定时编号
            .Columns("ScheduleCode").Caption = "定时编号"
            .Columns.Add("ScheduleName", GetType(String)) '定时名称
            .Columns("ScheduleName").Caption = "定时名称"
            .Columns.Add("Time", GetType(TimeHM)) '附加：触发时间
            .Columns("Time").Caption = "触发时间"
            .Columns.Add("TargetType", GetType(KnxGroupType)) '附加：组地址类型
            .Columns("TargetType").Caption = "目标对象类型"
            .Columns.Add("TargetValue", GetType(String)) '附加：目标数值
            .Columns("TargetValue").Caption = "目标值"
            .Columns.Add("InterfaceCode", GetType(String)) '附加：接口编号
            .Columns("InterfaceCode").Caption = "接口编号"
            .Columns.Add("GroupAddress", GetType(GroupAddress)) '附加：组地址
            .Columns("GroupAddress").Caption = "目标组地址"
            .Columns.Add("GroupValueType", GetType(GroupValueType)) '附加：组地址值类型
            .Columns("GroupValueType").Caption = "组地址类型"
            .Columns.Add("Value", GetType(GroupValue)) '附加：控制值
            .Columns("Value").Caption = "控制值"
            .Columns.Add("Triggered"， GetType(Boolean)) '附加：已触发
            .Columns("Triggered").Caption = "已触发"
        End With
    End Sub

End Class

''' <summary>
''' 只有时分的时间类型
''' </summary>
Public Class TimeHM

    Public ReadOnly Property Hour As Integer

    Public ReadOnly Property Minute As Integer

    Public Sub New(hour As Integer, minute As Integer)
        _Hour = Math.Max(0, Math.Min(hour, 23))
        _Minute = Math.Max(0, Math.Min(minute, 59))
    End Sub

    Public Sub New()
        Me.New(0, 0)
    End Sub

    Public Sub New(datetime As DateTime)
        Me.New(datetime.Hour, datetime.Minute)
    End Sub

    Public Sub New(timeonly As TimeOnly)
        Me.New(timeonly.Hour, timeonly.Minute)
    End Sub

    Public Sub New(timespan As TimeSpan)
        Me.New(timespan.Hours, timespan.Minutes)
    End Sub

    Public Sub New(timestring As String)
        Try
            If String.IsNullOrEmpty(timestring) Then timestring = "00:00"
            Dim t() As String = timestring.Split(":"c)
            If t.Length = 2 Or t.Length = 3 Then
                Dim h As Integer = Convert.ToInt32(t(0))
                _Hour = Math.Max(0, Math.Min(h Mod 24, 23))
                Dim m As Integer = Convert.ToInt32(t(1))
                _Minute = Math.Max(0, Math.Min(m Mod 60, 59))
            Else
                Throw New ArgumentException($"Wrong time format: '{timestring}'.")
            End If
        Catch ex As Exception
            Throw New ArgumentException($"Wrong time format: '{timestring}'.", ex)
        End Try
    End Sub

    Public Overrides Function ToString() As String
        Return $"{_Hour.ToString.PadLeft(2, "0"c)}:{_Minute.ToString.PadLeft(2, "0"c)}"
    End Function

    Public Shared Operator =(t1 As TimeHM, t2 As TimeHM) As Boolean
        If t1.Hour = t2.Hour AndAlso t1.Minute = t2.Minute Then
            Return True
        Else
            Return False
        End If
    End Operator

    Public Shared Operator <>(t1 As TimeHM, t2 As TimeHM) As Boolean
        Return Not (t1 = t2)
    End Operator

    Public Shared Operator <(t1 As TimeHM, t2 As TimeHM) As Boolean
        If t1.Hour < t2.Hour Then
            Return True
        ElseIf t1.Hour > t2.Hour Then
            Return False
        Else
            If t1.Minute < t2.Minute Then
                Return True
            Else
                Return False
            End If
        End If
    End Operator

    Public Shared Operator >(t1 As TimeHM, t2 As TimeHM) As Boolean
        Return Not (t1 < t2)
    End Operator

    Public Shared Operator <=(t1 As TimeHM, t2 As TimeHM) As Boolean
        Return (t1 < t2) Or (t1 = t2)
    End Operator

    Public Shared Operator >=(t1 As TimeHM, t2 As TimeHM) As Boolean
        Return (t1 > t2) Or (t1 = t2)
    End Operator

    Public Shared Narrowing Operator CType(scheduletime As TimeHM) As String
        Return scheduletime.ToString
    End Operator

End Class
