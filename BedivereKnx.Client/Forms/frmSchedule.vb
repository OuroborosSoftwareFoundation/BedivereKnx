Imports System.ComponentModel

Public Class frmSchedule

    Private Sub frmSchedule_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim HiddenCols As String() = {"Id"}
        dgv.DataSource = KS.Schedules.Table
        For i = 0 To dgv.ColumnCount - 1
            With dgv.Columns(i)
                .HeaderText = KS.Schedules.Table.Columns(i).Caption '设置列标题
                If HiddenCols.Contains(.Name) Then
                    .Visible = False '隐藏不需要显示的列
                End If
            End With
        Next
    End Sub

    Private Sub frmSchedule_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub btnScdSeq_Click(sender As Object, e As EventArgs) Handles btnScdSeq.Click
        frmScheduleSeq.dtScd = KS.Schedules.Sequence.Table
        frmScheduleSeq.Show()
    End Sub

End Class