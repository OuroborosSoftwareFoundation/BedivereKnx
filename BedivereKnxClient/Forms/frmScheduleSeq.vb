Public Class frmScheduleSeq

    Friend dtScd As DataTable

    Private Sub frmSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim HiddenCols As String() = {"Enable"} '"Id", "ScheduleCode", "InterfaceCode"}
        dgvScd.DataSource = KS.Schedules.Sequence.Table
        For Each col As DataGridViewColumn In dgvScd.Columns
            col.HeaderText = KS.Schedules.Sequence.Table.Columns(col.Name).Caption
            If HiddenCols.Contains(col.Name) Then col.Visible = False
        Next
    End Sub

End Class