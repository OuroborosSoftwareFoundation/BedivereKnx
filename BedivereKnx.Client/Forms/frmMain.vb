Imports System.Configuration.ConfigurationManager
Imports System.Security.Cryptography
Imports BedivereKnx
Imports Knx.Falcon
Imports Knx.Falcon.Sdk

Public Class frmMain

    Dim WithEvents tmDoe As New Timer
    Dim rand As New Random

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _AuthInfo.Status <> AuthState.Valid Then Application.Exit()
        Me.Text = $"{My.Application.Info.ProductName} (Ver.{My.Application.Info.Version})"
        lblAuth.Text = _AuthInfo.UserName
        tmDoe.Interval = 1000
        tmDoe.Start()
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '自动打开默认数据文件
        If Not String.IsNullOrEmpty(_DataFile) Then
            'frmMainTable.Close()
            'frmMainTable.fp = _DataFile
            'ShowSubForm(frmMainTable)
            OpenProject(_DataFile)
        End If
    End Sub

    '打开
    Private Sub Menu_Open_Click(sender As Object, e As EventArgs) Handles Menu_Open.Click
        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath,
            .Multiselect = False,
            .Filter = "Excel文件(*.xlsx)|*.xlsx"
        }
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            OpenProject(ofd.FileName)
        End If
    End Sub

    '导入
    Private Sub Menu_Import_Click(sender As Object, e As EventArgs) Handles Menu_Import.Click

    End Sub

    ''' <summary>
    ''' 打开项目文件的操作
    ''' </summary>
    ''' <param name="path">文件路径</param>
    Private Sub OpenProject(path As String)
        KS = New KnxSystem(path)
        AddHandler KS.Bus.ConnectionChanged, AddressOf KnxConnectionChanged
        AddHandler KS.Schedules.ScheduleTimerStateChanged, AddressOf ScheduleTimerStateChanged
        'AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
        KS.Bus.AllConnect(_InitRead) '打开全部KNX接口并初始化读取
        KS.Schedules.TimerStart() '开启定时器
        'frmInterface.Show() '启动时展示接口
        frmMainTable.Close()
        'frmMainTable.fp = path
        ShowSubForm(frmMainTable)
    End Sub

    ''' <summary>
    ''' 显示子窗体
    ''' </summary>
    Private Sub ShowSubForm(f As Form)
        CloseChildenForm()
        f.MdiParent = Me
        f.Parent = pnlMain
        f.BringToFront()
        f.Dock = DockStyle.Fill
        f.Show()
    End Sub

    ''' <summary>
    ''' 关闭全部子窗体
    ''' </summary>
    Private Sub CloseChildenForm()
        If IsNothing(Application.OpenForms) Then Exit Sub
        For Each f As Form In Application.OpenForms
            If IsNothing(f.Tag) Then
                Continue For
            Else
                If f.Tag.ToString = "Children" Then f.Close()
            End If
        Next
    End Sub

    Private Sub Menu_Config_Click(sender As Object, e As EventArgs) Handles Menu_Config.Click
        frmConfig.ShowDialog()
    End Sub

    Private Sub Menu_Auth_Click(sender As Object, e As EventArgs) Handles Menu_Auth.Click
        frmAuth.ShowDialog()
    End Sub

    Private Sub Menu_About_Click(sender As Object, e As EventArgs) Handles Menu_About.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub tmDoe_Tick(sender As Timer, e As EventArgs) Handles tmDoe.Tick
        Try
            If _AuthInfo.DOE.Date = Date.MaxValue.Date Then
                sender.Stop()
                Exit Sub
            Else
                Dim span = _AuthInfo.DOE.Subtract(Now)
                'Dim cdv As String = $"{span.Days.ToString("X2")}{span.Hours.ToString("X2")}{span.Minutes.ToString("X2")}{span.Seconds.ToString("X2")}"
                Dim cdv = Convert.ToInt64(span.TotalSeconds).ToString("X")
                lblCtDn.Text = cdv
            End If
        Catch ex As Exception
            Application.Exit()
        Finally
            sender.Interval = rand.Next(1000, 10000)
        End Try
        If Now > _AuthInfo.DOE Then Application.Exit()
    End Sub

    Private Sub tmSec_Tick(sender As Object, e As EventArgs) Handles tmSec.Tick
        lblDateTime.Text = Now.ToString("F")
    End Sub

    Private Sub Menu_Exit_Click(sender As Object, e As EventArgs) Handles Menu_Exit.Click
        Application.Exit()
    End Sub

    Private Sub btnGrid_Click(sender As Object, e As EventArgs) Handles btnGrid.Click

    End Sub

    Private Sub btnPanel_Click(sender As Object, e As EventArgs) Handles btnPanel.Click

    End Sub

    Private Sub btnGraphics_Click(sender As Object, e As EventArgs) Handles btnGraphics.Click
        frmMainGraphics.Show()
    End Sub

    'Github链接
    Private Sub slblGithub_Click(sender As Object, e As EventArgs) Handles slblGithub.Click
        OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnxClient")
    End Sub

    ''' <summary>
    ''' KNX接口状态变化事件
    ''' </summary>
    Public Sub KnxConnectionChanged(sender As KnxBus, e As EventArgs)
        slblIfDefault.Visible = (KS.Bus.Default.ConnectionState = BusConnectionState.Connected)
        If KS.Bus.Count = 0 Then
            slblIfDefault.Text = KS.Bus.Default.ConnectionState.ToString
            slblIfCount.Visible = False
        Else
            slblIfDefault.Text = "(+1)"
            slblIfCount.Visible = True
            Dim ConnectedCount As Integer = 0
            For Each k As KnxBus In KS.Bus
                If k.ConnectionState = BusConnectionState.Connected Then ConnectedCount += 1
            Next
            slblIfCount.Text = $"{ConnectedCount}/{KS.Bus.Count}"
            If ConnectedCount = KS.Bus.Count Then
                slblIfCount.ForeColor = Color.Green
            Else
                slblIfCount.ForeColor = Color.Red
            End If
        End If

        '（以下已转移至frmInterface）
        'For Each r As DataGridViewRow In dgvIf.Rows
        '    If IsDBNull(r.Cells("NetState").Value) Then Continue For
        '    Select Case r.Cells("NetState").Value
        '        Case IPStatus.Success, IPStatus.Unknown
        '            Select Case r.Cells("CnState").Value
        '                Case BusConnectionState.Connected
        '                    r.DefaultCellStyle.BackColor = Color.PaleGreen
        '                Case BusConnectionState.Broken, BusConnectionState.MediumFailure
        '                    r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
        '                Case Else
        '                    r.DefaultCellStyle.BackColor = Color.LightGray
        '            End Select
        '        Case Else
        '            r.DefaultCellStyle.BackColor = Color.LightCoral
        '    End Select
        'Next
    End Sub

    ''' <summary>
    ''' 定时器状态变化事件
    ''' </summary>
    Private Sub ScheduleTimerStateChanged(e As KnxScheduleTimerState)
        slblScdState.Text = e.ToString
        Select Case e
            Case KnxScheduleTimerState.Stoped
                slblScdState.ForeColor = Color.Gray
            Case KnxScheduleTimerState.Starting
                slblScdState.ForeColor = Color.Blue
            Case KnxScheduleTimerState.Running
                slblScdState.ForeColor = Color.Green
            Case Else
                slblScdState.ForeColor = SystemColors.ControlText
        End Select
    End Sub

    Private Sub frmMain_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
    End Sub

End Class