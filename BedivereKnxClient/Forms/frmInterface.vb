Imports Knx.Falcon
Imports Knx.Falcon.Sdk
Imports System.ComponentModel
Imports System.Net.NetworkInformation

Public Class frmInterface

    Private Sub frmInterface_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler KS.Bus.ConnectionChanged, AddressOf KnxConnectionChanged
        frmMainTable.DgvBindingInit(dgvIf, KS.Bus.Table,
            {"Id", "AreaCode", "InterfaceCode", "Port"})
        dgvRefresh()
        Call KnxConnectionChanged(Nothing, Nothing)
    End Sub

    Private Sub dgvRefresh()
        For Each r As DataGridViewRow In dgvIf.Rows
            If IsDBNull(r.Cells("NetState").Value) Then Continue For
            Select Case r.Cells("NetState").Value
                Case IPStatus.Success, IPStatus.Unknown
                    Select Case r.Cells("CnState").Value
                        Case BusConnectionState.Connected
                            r.DefaultCellStyle.BackColor = Color.PaleGreen
                        Case BusConnectionState.Broken, BusConnectionState.MediumFailure
                            r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                        Case Else
                            r.DefaultCellStyle.BackColor = Color.LightGray
                    End Select
                Case Else
                    r.DefaultCellStyle.BackColor = Color.LightCoral
            End Select
        Next
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        For Each r As DataGridViewRow In dgvIf.Rows
            r.DefaultCellStyle.BackColor = Color.LightGray
        Next
        KS.Bus.AllConnect(_InitRead) '打开全部KNX接口并初始化读取
    End Sub

    Private Sub dgvIf_SelectionChanged(sender As DataGridView, e As EventArgs) Handles dgvIf.SelectionChanged
        sender.ClearSelection() '接口dgv不允许被选中
    End Sub

    ''' <summary>
    ''' KNX接口状态变化事件
    ''' </summary>
    Public Sub KnxConnectionChanged(sender As KnxBus, e As EventArgs)
        'Dim AllOK As Boolean = True
        'For Each r As DataGridViewRow In dgvIf.Rows
        '    If IsDBNull(r.Cells("NetState").Value) Then Continue For
        '    Select Case r.Cells("NetState").Value
        '        Case IPStatus.Success, IPStatus.Unknown
        '            Select Case r.Cells("CnState").Value
        '                Case BusConnectionState.Connected
        '                    r.DefaultCellStyle.BackColor = Color.PaleGreen
        '                Case BusConnectionState.Broken, BusConnectionState.MediumFailure
        '                    r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
        '                    AllOK = False
        '                Case Else
        '                    r.DefaultCellStyle.BackColor = Color.LightGray
        '                    AllOK = False
        '            End Select
        '        Case Else
        '            r.DefaultCellStyle.BackColor = Color.LightCoral
        '            AllOK = False
        '    End Select
        'Next
        Dim AllOk As Boolean = dgvIfColoring()
        Dim DCS As BusConnectionState = KS.Bus.Default.ConnectionState
        lblIfDefault.Text = DCS.ToString
        lblIfDefault.ForeColor = IIf(DCS = BusConnectionState.Connected, Color.Green, Color.Red)
        'If AllOK Then Me.Hide()
    End Sub

    Private Function dgvIfColoring() As Boolean
        Dim AllOK As Boolean = True
        For Each r As DataGridViewRow In dgvIf.Rows
            If IsDBNull(r.Cells("NetState").Value) Then Continue For
            Select Case r.Cells("NetState").Value
                Case IPStatus.Success, IPStatus.Unknown
                    Select Case r.Cells("CnState").Value
                        Case BusConnectionState.Connected
                            r.DefaultCellStyle.BackColor = Color.PaleGreen
                        Case BusConnectionState.Broken, BusConnectionState.MediumFailure
                            r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                            AllOK = False
                        Case Else
                            r.DefaultCellStyle.BackColor = Color.LightGray
                            AllOK = False
                    End Select
                Case Else
                    r.DefaultCellStyle.BackColor = Color.LightCoral
                    AllOK = False
            End Select
        Next
        Return AllOK
    End Function

    Private Sub dgvIf_Sorted(sender As Object, e As EventArgs) Handles dgvIf.Sorted
        dgvIfColoring()
    End Sub

    Private Sub frmInterface_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'e.Cancel = True
        'Me.Hide()
    End Sub

End Class