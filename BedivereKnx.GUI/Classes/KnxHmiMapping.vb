Imports System.Drawing
Imports Ouroboros.Hmi.Library
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiMapping : Inherits HmiMappingBase(Of GroupValue)

    Public Sub New(values As GroupValue(), changeType As HmiValueChangeType)
        MyBase.New(values, changeType)
    End Sub

    ''' <summary>
    ''' 这种方式新建的对象只有RawValues属性被赋值，Values属性为空
    ''' </summary>
    ''' <param name="valsString"></param>
    Public Sub New(valsString As String)
        MyBase.New(valsString)
    End Sub

    Public Sub New(valsString As String, dpt As DptBase)
        MyBase.New(valsString)
        '此时基类的RawValue被设置值
        'Dim valList As New List(Of GroupValue)
        'For Each valStr As String In RawValues
        '    valList.Add(dpt.ToGroupValue(Convert.ToDecimal(valStr)))
        'Next
        'Values = valList.ToArray
        SetValues(dpt)
    End Sub

    Private Sub SetValues(dpt As DptBase)
        Dim valList As New List(Of GroupValue)
        For Each valStr As String In RawValues
            valList.Add(dpt.ToGroupValue(Convert.ToDecimal(valStr)))
        Next
        Values = valList.ToArray
    End Sub

    'Public Sub New(valsString As String, dpt As DptBase)
    '    Dim vals As New List(Of GroupValue)
    '    If valsString.Contains("|"c) Then '值-切换模式
    '        Me.ChangeType = HmiValueChangeType.Toggle
    '        Dim valsArry As String() = valsString.Split("|"c)
    '        For Each v As String In valsArry
    '            vals.Add(dpt.ToGroupValue(Convert.ToDecimal(v)))
    '        Next
    '    ElseIf valsString.Contains("~"c) Then '值-范围模式
    '        Me.ChangeType = HmiValueChangeType.Range
    '        Dim valsArry As String() = valsString.Split("~"c)
    '        For Each v As String In valsArry
    '            vals.Add(dpt.ToGroupValue(Convert.ToDecimal(v)))
    '        Next
    '    Else '值-固定值模式
    '        Me.ChangeType = HmiValueChangeType.Fixed
    '        vals.Add(dpt.ToGroupValue(Convert.ToDecimal(valsString)))
    '    End If
    '    Values = vals.ToArray
    'End Sub

    'Friend Function GetFillColor(gv As GroupValue) As Color
    '    Return GetColor(gv, Me.FillColors)
    'End Function

    'Friend Function GetStrokeColor(gv As GroupValue) As Color
    '    Return GetColor(gv, Me.StrokeColors)
    'End Function

    'Friend Function GetFontColor(gv As GroupValue) As Color
    '    Return GetColor(gv, Me.FontColors)
    'End Function

    'Private Function GetColor(gv As GroupValue, array As Color()) As Color
    '    If array.Length > 1 Then
    '        Dim i As Integer = -1
    '        If dicVals.TryGetValue(gv, i) Then
    '            Return array(i)
    '        Else
    '            If Me.ChangeType = HmiValueChangeType.Range Then

    '            Else
    '                Return Color.Empty
    '            End If
    '        End If
    '    End If
    'End Function

End Class
