Imports System.Drawing
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiMapping : Inherits HmiMappingBase

    'Implements IHmiMapping(Of GroupValue)

    Public Property Values As GroupValue() 'Implements IHmiMapping(Of GroupValue).Values

    Private dicVals As New Dictionary(Of GroupValue, Integer)

    Public Sub New(values As GroupValue(), Optional valueType As HmiValueChangeType = HmiValueChangeType.None)
        Me.ChangeType = valueType
        Me.Values = values
        For i = 0 To values.Length - 1
            dicVals.Add(values(i), i)
        Next
    End Sub

    Public Sub New(valsString As String, dpt As DptBase)
        Dim vals As New List(Of GroupValue)
        'Dim chgType As HmiValueChangeType
        If valsString.Contains("|"c) Then '值-切换模式
            Me.ChangeType = HmiValueChangeType.Toggle
            Dim valsArry As String() = valsString.Split("|"c)
            For Each v As String In valsArry
                vals.Add(dpt.ToGroupValue(Convert.ToDecimal(v)))
            Next
        ElseIf valsString.Contains("~"c) Then '值-范围模式
            Me.ChangeType = HmiValueChangeType.Range
            Dim valsArry As String() = valsString.Split("~"c)
            For Each v As String In valsArry
                vals.Add(dpt.ToGroupValue(Convert.ToDecimal(v)))
            Next
        Else '值-固定值模式
            Me.ChangeType = HmiValueChangeType.Fixed
            vals.Add(dpt.ToGroupValue(Convert.ToDecimal(valsString)))
        End If
    End Sub

    Friend Function GetFillColor(gv As GroupValue) As Color
        Return GetColor(gv, Me.FillColors)
    End Function

    Friend Function GetStrokeColor(gv As GroupValue) As Color
        Return GetColor(gv, Me.StrokeColors)
    End Function

    Friend Function GetFontColor(gv As GroupValue) As Color
        Return GetColor(gv, Me.FontColors)
    End Function

    Private Function GetColor(gv As GroupValue, array As Color()) As Color
        If array.Length > 1 Then
            Dim i As Integer = -1
            If dicVals.TryGetValue(gv, i) Then
                Return array(i)
            Else
                If Me.ChangeType = HmiValueChangeType.Range Then

                Else
                    Return Color.Empty
                End If
            End If
        End If
    End Function

End Class
