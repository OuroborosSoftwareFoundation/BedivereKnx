Imports System.ComponentModel
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData
Imports Knx.Falcon.ApplicationData.DatapointTypes

''' <summary>
''' KNX组
''' </summary>
Public Class KnxGroup

    Private _Value As GroupValue

    ''' <summary>
    ''' 值变化事件
    ''' </summary>
    Public Event GroupValueChanged As ValueChangeHandler(Of GroupValue)

    ''' <summary>
    ''' 数据类型
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
        Get
            Return _Value
        End Get
        Set
            If (_Value Is Nothing) OrElse (Not _Value.Equals(Value)) Then
                _Value = Value
                RaiseEvent GroupValueChanged(Value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="dptMain">DPT主类型</param>
    ''' <param name="dptSub">DPT子类型</param>
    Public Sub New(Optional dptMain As Integer = 1, Optional dptSub As Integer = -1)
        Me.DPT = DptFactory.Default.Get(dptMain, dptSub)
        If Me.DPT Is Nothing Then
            Throw New ArgumentException($"Invalid data type:'{dptMain}.{dptSub.ToString.PadLeft(3, "0"c)}'.")
        End If
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="dpt">{DPT主类型，DPT子类型}，子类型可以省略</param>
    Public Sub New(dpt As Integer())
        If dpt Is Nothing Then
            Me.DPT = DptFactory.Default.Get(1, -1) 'DptFactory.Default.Create(DptFactory.Default.GetDatapointType(1))
        ElseIf dpt.Length = 1 Then
            Me.DPT = DptFactory.Default.Get(dpt(0), -1) 'DptFactory.Default.Create(DptFactory.Default.GetDatapointType(dpt(0)))
        ElseIf dpt.Length >= 2 Then
            Me.DPT = DptFactory.Default.Get(dpt(0), dpt(1)) 'DptFactory.Default.Create(DptFactory.Default.GetDatapointSubtype(dpt(0), dpt(1)))
        End If
        'If Me.DPT Is Nothing Then
        '    Throw New ArgumentException($"Invalid data type:'{dpt(0)}.{dpt(1)}'.")
        'End If
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="address">组地址</param>
    ''' <param name="dptMain">DPT主类型</param>
    ''' <param name="dptSub">DPT子类型</param>
    Public Sub New(address As GroupAddress, Optional dptMain As Integer = 1, Optional dptSub As Integer = -1)
        Me.New(dptMain, dptSub)
        _Address = address
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="address">组地址</param>
    ''' <param name="dpt">{DPT主类型，DPT子类型}，子类型可以省略</param>
    Public Sub New(address As GroupAddress, Optional dpt As Integer() = Nothing)
        Me.New(dpt)
        _Address = address
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="addressString">组地址字符串</param>
    ''' <param name="dptMain">DPT主类型</param>
    ''' <param name="dptSub">DPT子类型</param>
    Public Sub New(addressString As String, Optional dptMain As Integer = 1, Optional dptSub As Integer = 0)
        Me.New(addressString, {dptMain, dptSub})
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="addressString">组地址字符串</param>
    ''' <param name="dpt">{DPT主类型，DPT子类型}，子类型可以省略</param>
    Public Sub New(addressString As String, Optional dpt As Integer() = Nothing)
        Me.New(dpt)
        Dim ga As New GroupAddress
        If GroupAddress.TryParse(addressString, ga) Then
            _Address = ga
        Else
            Throw New ArgumentException($"Wrong GroupAddress format: '{addressString}'.")
        End If
    End Sub

    ''' <summary>
    ''' 新建KNX组
    ''' </summary>
    ''' <param name="addressString">组地址字符串</param>
    ''' <param name="dptString">DPT类型字符串</param>
    Public Sub New(addressString As String, dptString As String)
        Me.New(addressString, GetDptIdFromDt(dptString))
    End Sub

    ''' <summary>
    ''' 变量值转GroupValue
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function ToGroupValue(value As Object) As GroupValue
        If value Is Nothing Then Return Nothing
        If IsNumeric(value) Then '提前处理特殊类型
            Select Case Me.DPT.MainNumber
                Case 1 '开关量
                    value = (Convert.ToInt32(value) <> 0) '不等于0即认为是True
                Case 2 '优先级控制（bit0-值，bit1-控制位）
                    Dim b As Byte = Convert.ToByte(value)
                    value = New Knx1BitControlled(b And &B10, b And &B1)
                Case 3 '调光（bit0~2-步，bit3-方向）
                    Dim b As Byte = Convert.ToByte(value)
                    value = New Knx3BitControlled((b >> 3) = 1, b And &B111)
                Case 18 '场景控制（bit0~5-场景值，bit6-保留值[忽略]，bit7-是否学习）
                    Dim b As Byte = Convert.ToByte(value)
                    value = New KnxSceneControl((b >> 7) = 1, b And &B111111)
                Case 26 '场景信息（bit0~6-场景值，bit6-是否激活）
                    Dim b As Byte = Convert.ToByte(value)
                    value = New KnxSceneInfo((b And &B1000000) = 1, b And &B111111)
            End Select
        End If
        Return Me.DPT.ToGroupValue(value)
    End Function

    ''' <summary>
    ''' DPT字符串转数字数组
    ''' </summary>
    ''' <param name="DptString"></param>
    ''' <returns></returns>
    Private Shared Function GetDptIdFromDt(dptString As String) As Integer()
        If String.IsNullOrEmpty(dptString) Then
            Return {-1, -1}
        End If
        Dim DptStr() As String = dptString.Split(":"c)(0).Split("."c) 'DPT数字
        If DptStr.Length <> 2 Then
            Throw New ArgumentException($"Invalid DatapointType: {dptString}.")
            Return {-1, -1}
        End If
        Dim NumM As Integer = 0 'DPT主类型数字
        If IsNumeric(DptStr(0)) Then
            NumM = Convert.ToInt32(DptStr(0))
            NumM = Math.Abs(NumM) '去除负号
        Else
            Throw New ArgumentException($"Invalid DatapointType: {dptString}.")
            Return {-1, -1}
        End If
        Dim NumS As Integer = 0 'DPT子类型数字，非数字的情况视为0
        If IsNumeric(DptStr(1)) Then
            NumS = Convert.ToInt32(DptStr(1))
            NumS = Math.Abs(NumS) '去除负号
        End If
        Return {NumM, NumS}
    End Function

End Class
