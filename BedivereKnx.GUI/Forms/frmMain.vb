Imports Knx.Falcon
Imports Knx.Falcon.Sdk

Public Class frmMain

    Dim ProjectOpened As Boolean = False '是否打开项目
    Dim WithEvents tmDoe As New Timer
    Dim rand As New Random

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not _AuthInfo.Valid Then Application.Exit()
        Me.Text = $"{My.Application.Info.ProductName} (Ver.{My.Application.Info.Version})"
        lblAuth.Text = _AuthInfo.Text
        tmDoe.Interval = 1000
        tmDoe.Start()
        If (AppConfig.KnxLocalIP Is Nothing) OrElse AppConfig.KnxLocalIP.Equals(Net.IPAddress.Parse("127.0.0.1")) Then
            Dim r As DialogResult = MessageBox.Show("检测到无效的KNX路由接口本地IP，是否修改配置？", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If r = DialogResult.OK Then
                If frmNetworkInfo.ShowDialog() = DialogResult.OK Then
                    'SaveAppSetting("LocalIP", frmNetworkInfo.SelectedIp)
                    AppConfig.Save()
                End If
            End If
        End If
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '自动打开默认数据文件
        If Not String.IsNullOrEmpty(AppConfig.DefaultDataFile) Then
            'frmMainTable.Close()
            'frmMainTable.fp = _DataFile
            'ShowSubForm(frmMainTable)
            OpenProject(AppConfig.DefaultDataFile)
        End If
    End Sub

    Private Sub SetProjectState(state As Boolean)
        ProjectOpened = state
        btnGrid.Enabled = state
        btnPanel.Enabled = state
        btnHmi.Enabled = state
        If state Then
        Else
            KS = Nothing
        End If
    End Sub

    '打开项目
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

    '关闭项目
    Private Sub Menu_Close_Click(sender As Object, e As EventArgs) Handles Menu_Close.Click
        frmMainTable.Dispose()
        frmMainHmi.Dispose()
        SetProjectState(False)
    End Sub

    '导入
    Private Sub Menu_Import_Click(sender As Object, e As EventArgs) Handles Menu_Import.Click

    End Sub

    Private Sub btnGrid_Click(sender As Object, e As EventArgs) Handles btnGrid.Click

    End Sub

    Private Sub btnPanel_Click(sender As Object, e As EventArgs) Handles btnPanel.Click

    End Sub

    '打开图形界面
    Private Sub btnHmi_Click(sender As Object, e As EventArgs) Handles btnHmi.Click
        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath,
            .Multiselect = False,
            .Filter = "draw.io Diagrams(*.drawio)|*.drawio"
        }
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            'frmMainHmi.HmiPath = ofd.FileName
            ''ShowSubForm(frmMainHmi)
            'frmMainHmi.Show()
            OpenHmiForm(ofd.FileName)
        End If
    End Sub

    ''' <summary>
    ''' 打开项目文件的操作
    ''' </summary>
    ''' <param name="path">文件路径</param>
    Private Sub OpenProject(path As String)
        KS = New KnxSystem(path, AppConfig.KnxLocalIP)
        AddHandler KS.Bus.ConnectionChanged, AddressOf KnxConnectionChanged
        AddHandler KS.PollingStatusChanged, AddressOf PollingStatusChanged
        AddHandler KS.Schedules.ScheduleTimerStateChanged, AddressOf ScheduleTimerStateChanged
        'AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
        'KS.Bus.AllConnect(AppConfig.InitPolling) '打开全部KNX接口并初始化读取
        OpenAllKnxInterface(AppConfig.InitPolling) '打开全部KNX接口并初始化读取
        KS.Schedules.TimerStart() '开启定时器
        'frmInterface.Show() '启动时展示接口
        frmMainTable.Close()
        'frmMainTable.fp = path
        ShowSubForm(frmMainTable)
        SetProjectState(True)
        If Not String.IsNullOrWhiteSpace(AppConfig.DefaultHmiFile) Then
            OpenHmiForm(AppConfig.DefaultHmiFile)
        End If
    End Sub

    ''' <summary>
    ''' 打开图形文件
    ''' </summary>
    ''' <param name="path"></param>
    Private Sub OpenHmiForm(path As String)
        frmMainHmi.HmiPath = path
        'ShowSubForm(frmMainHmi)
        frmMainHmi.Show()
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
        'If IsNothing(Application.OpenForms) Then Exit Sub
        'Dim fs As FormCollection = Application.OpenForms
        'For Each f As Form In fs
        '    If IsNothing(f.Tag) Then
        '        Continue For
        '    Else
        '        If f.Tag.ToString = "Children" Then f.Close()
        '    End If
        'Next
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
            If _AuthInfo.Current.DOE.Date = Date.MaxValue.Date Then
                sender.Stop()
                Exit Sub
            Else
                Dim span = _AuthInfo.Current.DOE.Subtract(Now)
                'Dim cdv As String = $"{span.Days.ToString("X2")}{span.Hours.ToString("X2")}{span.Minutes.ToString("X2")}{span.Seconds.ToString("X2")}"
                Dim cdv = Convert.ToInt64(span.TotalSeconds).ToString("X")
                lblCtDn.Text = cdv
            End If
        Catch ex As Exception
            Application.Exit()
        Finally
            sender.Interval = rand.Next(1000, 10000)
        End Try
        If Now.Date > _AuthInfo.Current.DOE.Date Then Application.Exit()
    End Sub

    Private Sub tmSec_Tick(sender As Object, e As EventArgs) Handles tmSec.Tick
        lblDateTime.Text = Now.ToString("F")
    End Sub

    Private Sub Menu_Exit_Click(sender As Object, e As EventArgs) Handles Menu_Exit.Click
        Application.Exit()
    End Sub

    'Github链接
    Private Sub slblGithub_Click(sender As Object, e As EventArgs) Handles slblGithub.Click
        OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnx")
    End Sub

    ''' <summary>
    ''' KNX接口状态变化事件
    ''' </summary>
    Public Sub KnxConnectionChanged(sender As KnxBus, e As EventArgs)
        slblIfDefault.Visible = (KS.Bus.DefaultBus.ConnectionState = BusConnectionState.Connected)
        If KS.Bus.Count = 0 Then
            slblIfDefault.Text = KS.Bus.DefaultBus.ConnectionState.ToString
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
    ''' 轮询状态变化事件
    ''' </summary>
    Private Sub PollingStatusChanged(value As Boolean)
        slblPolling.Visible = value
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