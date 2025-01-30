Imports System.IO
Imports System.Text
Imports System.Net.NetworkInformation
Imports System.ComponentModel
Imports BedivereKnx
Imports Knx.Falcon
Imports Knx.Falcon.Sdk

Public Class frmMainTable

    'Friend fp As String
    Dim isBusy As Boolean = True
    WithEvents menuColFilter As New ContextMenuStrip '筛选DataGridView列的右键菜单

    Private Sub frmMainTable_Load(sender As Object, e As EventArgs) Handles Me.Load
        'KS = New KnxSystem(fp)
        'AddHandler KS.Bus.ConnectionChanged, AddressOf KnxConnectionChanged
        'AddHandler KS.Schedules.ScheduleTimerStateChanged, AddressOf ScheduleTimerStateChanged
        AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
        For Each tp As TabPage In tabMain.TabPages
            tabMain.SelectedTab = tp
            If tp.Tag = "Interface" Then tabMain.TabPages.Remove(tp)
        Next
        tabMain.SelectedIndex = 0
        DgvInit() '初始化各DataGridView
        DgvObjectsOptimize() '优化对象表，没有数值组时隐藏相关列
        InitAreaTree() '初始化Area树形结构

        'If KS.Schedules.Sequence.Count > 0 Then ScheduleTimerInit() '初始化定时器

        dgvObject.ContextMenuStrip = menuColFilter
        dgvScene.ContextMenuStrip = menuColFilter
    End Sub

    Private Sub frmMainTable_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        isBusy = False
        'KS.Bus.AllConnect(_InitRead) '打开全部KNX接口并初始化读取
        'KS.Schedules.TimerStart() '开启定时器
        ''frmInterface.Show() '启动时展示接口
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        KS.Bus.AllConnect(_InitRead) '打开全部KNX接口并初始化读取
    End Sub

    Private Sub tmPoll_Tick(sender As Timer, e As EventArgs) Handles tmPoll.Tick
        KS.PollObjectsValue() '对象内部会自动判断是否正在忙碌
    End Sub

    ''' <summary>
    ''' 初始化Area树形结构
    ''' </summary>
    Private Sub InitAreaTree()
        With tvArea
            .Nodes.Clear()
            .Nodes.Add(vbNullString, "全部") '添加根节点
            For Each r As DataRow In KS.Areas.Table.Rows
                .SelectedNode = .Nodes(0) '选择根节点
                Dim ac As String() = r("AreaCode").ToString.Split("."c) '节点编号的数组
                If String.IsNullOrEmpty(r("MainCode").ToString.Trim) Then Continue For '无视没有主区域的行
                If Not .SelectedNode.Nodes.ContainsKey(ac(0)) Then
                    .SelectedNode.Nodes.Add(ac(0), r("MainArea")) '如果主区域节点不存在则添加
                End If
                If Not String.IsNullOrEmpty(r("MiddleArea").ToString.Trim) Then '存在中区域
                    Dim acMdl As String = $"{ac(0)}.{ac(1)}"
                    .SelectedNode = .SelectedNode.Nodes(ac(0)) '选择当前主区域节点
                    If Not .SelectedNode.Nodes.ContainsKey(acMdl) Then '如果中区域节点不存在则添加
                        .SelectedNode.Nodes.Add(acMdl, r("MiddleArea")) '如果中区域节点不存在则添加
                    End If
                    If Not String.IsNullOrEmpty(r("SubArea").ToString.Trim) Then '存在子区域
                        Dim acSub As String = $"{ac(0)}.{ac(1)}.{ac(2)}"
                        .SelectedNode = .SelectedNode.Nodes(acMdl) '选择当前中区域节点
                        If Not .SelectedNode.Nodes.ContainsKey(acSub) Then '如果子区域节点不存在则添加
                            .SelectedNode.Nodes.Add(acSub, r("SubArea")) '如果子区域节点不存在则添加
                        End If
                    Else
                        Continue For
                    End If
                Else
                    Continue For
                End If
            Next
            '.Nodes.Add(KS.Areas.MainNode)
            .CollapseAll()
            .Nodes(0).Expand()
            .SelectedNode = .Nodes(0)
        End With
    End Sub

    Private Sub tvArea_AfterSelect(sender As TreeView, e As TreeViewEventArgs) Handles tvArea.AfterSelect
        If isBusy Then Exit Sub
        DgvRowFilter(dgvObject, e.Node.Name)
        DgvRowFilter(dgvScene, e.Node.Name)
        DgvRowFilter(dgvDevice, e.Node.Name)
    End Sub

    ''' <summary>
    ''' 初始化DataGridView
    ''' </summary>
    Private Sub DgvInit()
        'DgvBindingInit(dgvIf, KS.Bus.Table,
        '    {"Id", "AreaCode", "InterfaceCode", "Port"})
        DgvBindingInit(dgvObject, KS.Objects.Table,
            {"Id", "AreaCode", "InterfaceCode", "GA_Dim"})
        DgvBindingInit(dgvScene, KS.Scenes.Table,
            {"Id", "AreaCode", "InterfaceCode", "SceneValues"})
        DgvBindingInit(dgvDevice, KS.Devices.Table,
            {"Id", "AreaCode", "InterfaceCode"})
        DgvBindingInit(dgvSchedule, KS.Schedules.Table,
            {"Id"})
        DgvBindingInit(dgvLink, KS.Links,
            {"Id", "Account", "Password"})
        If dgvLink.Rows.Count > 0 Then
            Dim colBtn As New DataGridViewButtonColumn
            With colBtn
                .Name = "btnPwd"
                .HeaderText = "账号密码"
                .Text = "查看"
                .UseColumnTextForButtonValue = True
            End With
            dgvLink.Columns.Add(colBtn)
        End If
    End Sub

    ''' <summary>
    ''' 初始化DataGridView
    ''' </summary>
    ''' <param name="dgv">DataGridView对象</param>
    ''' <param name="dt">DataTable对象</param>
    ''' <param name="HiddenCols">需要隐藏的列</param>
    Friend Sub DgvBindingInit(dgv As DataGridView, dt As DataTable, HiddenCols As String())
        If (dt Is Nothing) OrElse (dt.Rows.Count = 0) Then Exit Sub
        dgv.DataSource = dt
        dgv.ClearSelection() '取消选定
        For Each col As DataGridViewColumn In dgv.Columns
            Dim ColName As String = col.Name
            If dt.Columns.Contains(ColName) Then
                col.HeaderText = dt.Columns(ColName).Caption '设置列标题
                If HiddenCols.Contains(ColName) Then
                    col.Visible = False '隐藏不需要显示的列
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' 优化对象表显示
    ''' </summary>
    Private Sub DgvObjectsOptimize()
        If dgvObject.Rows.Count = 0 Then Exit Sub
        Dim OnlySwCtl As Boolean = True
        For Each r As DataGridViewRow In dgvObject.Rows
            If Not (IsDBNull(r.Cells("Val_Ctl_GrpAddr").Value) And IsDBNull(r.Cells("Val_Fdb_GrpAddr").Value)) Then
                OnlySwCtl = False
                Exit For
            End If
        Next
        If OnlySwCtl Then
            dgvObject.Columns("Val_GrpDpt").Visible = False
            dgvObject.Columns("Val_Ctl_GrpAddr").Visible = False
            dgvObject.Columns("Val_Fdb_GrpAddr").Visible = False
            dgvObject.Columns("Val_Fdb_Value").Visible = False
        End If
    End Sub

    Private Sub dgv_Sorted(sender As DataGridView, e As EventArgs) Handles dgvObject.Sorted, dgvScene.Sorted, dgvDevice.Sorted
        sender.ClearSelection()
        DgvRowFilter(sender, tvArea.SelectedNode.Name)
    End Sub

    ''' <summary>
    ''' DataGridView行筛选
    ''' </summary>
    ''' <param name="dgv">DataGridView对象</param>
    ''' <param name="area">筛选区域，全部显示留空</param>
    Private Sub DgvRowFilter(dgv As DataGridView, Optional area As String = vbNullString)
        Dim cm As CurrencyManager = BindingContext(dgv.DataSource)
        cm.SuspendBinding() '挂起数据绑定
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None '禁止自动调整列宽加速显示
        For Each r As DataGridViewRow In dgv.Rows
            r.Visible = r.Cells("AreaCode").Value.ToString.Contains(area) '隐藏不在范围内的行
        Next
        'dgv.ClearSelection() '全部取消选定
        Dim SelShowCount As Integer = 0 '已经选中的行在筛选后依旧显示的情况
        For Each sr As DataGridViewRow In dgv.SelectedRows
            If sr.Visible Then '选中的行筛选后依旧显示的情况
                SelShowCount += 1
            Else '选中的行筛选后隐藏的情况
                sr.Selected = False
            End If
        Next
        If SelShowCount = 0 Then '已经选中的行在筛选后全部隐藏，滚动条置顶
            If dgv.Rows.GetFirstRow(DataGridViewElementStates.Visible) >= 0 Then
                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.GetFirstRow(DataGridViewElementStates.Visible) '滚动条置顶
            End If
        End If
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells '恢复自动调整列宽
        cm.ResumeBinding() '恢复数据绑定
        dgv.Refresh() '刷新DataGridView
    End Sub

    ''' <summary>
    ''' DataGridView列筛选菜单自动生成
    ''' </summary>
    Private Sub menuColFilter_Opening(sender As ContextMenuStrip, e As CancelEventArgs) Handles menuColFilter.Opening
        sender.Items.Clear() '清除菜单项
        Dim dgv As DataGridView = sender.SourceControl '获取触发菜单的控件
        Dim tsmi0 As New ToolStripTextBox '第一项标识触发控件名
        tsmi0.Text = $"({sender.SourceControl.Tag} Columns Filter)"
        tsmi0.Tag = sender.SourceControl.Tag
        tsmi0.Enabled = False
        sender.Items.Add(tsmi0)
        sender.Items.Add(New ToolStripSeparator)
        For Each col As DataGridViewColumn In dgv.Columns
            Dim tsmi As New ToolStripMenuItem
            tsmi.ImageIndex = col.Index
            tsmi.Text = col.HeaderText
            tsmi.CheckOnClick = True
            tsmi.Checked = col.Visible '勾选当前显示的列
            sender.Items.Add(tsmi)
        Next
    End Sub

    ''' <summary>
    ''' DataGridView列筛选
    ''' </summary>
    Private Sub menuColFilter_Closed(sender As ContextMenuStrip, e As ToolStripDropDownClosedEventArgs) Handles menuColFilter.Closed
        If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked Then Exit Sub
        Dim dgv As DataGridView
        Select Case sender.Items(0).Tag
            Case "Objects"
                dgv = dgvObject
            Case "Scenes"
                dgv = dgvScene
            Case Else
                dgv = Nothing
        End Select
        For Each tsmi In sender.Items
            If tsmi.GetType = GetType(ToolStripMenuItem) Then
                If tsmi.ImageIndex < 0 Then Continue For
                dgv.Columns(tsmi.ImageIndex).Visible = tsmi.Checked
            End If
        Next
    End Sub

    ''' <summary>
    ''' 菜单点击选项后不自动消失
    ''' </summary>
    Private Sub menuColFilter_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles menuColFilter.Closing
        If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' KNX报文传输事件
    ''' </summary>
    Private Sub KnxMessageTransmission(e As KnxMsgEventArgs, log As String)
        Dim v As String = vbNullString
        If Not IsNothing(e.Value) Then v = $" = {e.Value.ToString}"
        lstTelLog.Items.Insert(0, $"[{Now:yyyy-MM-dd HH:mm:ss.fff}]{e.MessageType}|{e.EventType}: {e.SourceAddress} -> {e.DestinationAddress}{v} ({e.MessagePriority}, Hop={e.HopCount})")
    End Sub

    Private Sub tabMain_Selected(sender As Object, e As TabControlEventArgs) Handles tabMain.Selected
        If e.TabPage.Tag Is Nothing Then Exit Sub
        Select Case e.TabPage.Tag.ToString.ToLower
            Case "object", "device", "value", "enable"

            Case "scene"

            Case Else

        End Select
    End Sub

    Private Sub btnCtl_Click(sender As Button, e As EventArgs) Handles btnCtlTrue.Click, btnCtlFalse.Click
        If isBusy Then Exit Sub
        If IsNothing(dgvObject.CurrentRow) OrElse (dgvObject.SelectedRows.Count = 0) Then Exit Sub
        Dim val As New GroupValue(Convert.ToByte(sender.Tag), 1) 'GroupValue(Convert.ToInt32(sender.Tag) = 1)
        DgvObjectCtl("Sw", val)
    End Sub

    Private Sub numObjVal_MouseUp(sender As TrackBar, e As MouseEventArgs) Handles numObjVal.MouseUp
        If isBusy Then Exit Sub
        If IsNothing(dgvObject.CurrentRow) OrElse (dgvObject.SelectedRows.Count = 0) Then Exit Sub
        Dim val As New GroupValue(Convert.ToByte(sender.Value * 2.55))
        DgvObjectCtl("Val", val)
    End Sub

    Private Sub dgvObject_CellClick(sender As DataGridView, e As DataGridViewCellEventArgs) Handles dgvObject.CellClick
        If isBusy Then Exit Sub
        If IsNothing(dgvObject.CurrentRow) OrElse (dgvObject.SelectedRows.Count = 0) Then Exit Sub
        For Each r As DataGridViewRow In dgvObject.SelectedRows
            If IsDBNull(r.Cells("Val_Ctl_GrpAddr").Value) Then
                numObjVal.Enabled = False
                numObjVal.Value = 0
            Else
                numObjVal.Enabled = True
                'numObjVal.Value = Convert.ToInt32(r.Cells("ValFdb").Value)
            End If
        Next
    End Sub

    Private Sub numObjVal_Scroll(sender As TrackBar, e As EventArgs) Handles numObjVal.Scroll
        lblObjVal.Text = sender.Value '实时显示亮度值
    End Sub

    ''' <summary>
    ''' 开关控制
    ''' </summary>
    Private Sub DgvObjectCtl(PartCol As String, Val As GroupValue)
        If isBusy Then Exit Sub
        If IsNothing(dgvObject.CurrentRow) OrElse (dgvObject.SelectedRows.Count = 0) Then Exit Sub
        For Each r As DataGridViewRow In dgvObject.SelectedRows
            Dim ga As New GroupAddress(r.Cells($"{PartCol}_Ctl_GrpAddr").Value.ToString)
            KS.WriteGroupAddress(r.Cells("InterfaceCode").Value.ToString, ga, Val)
        Next
    End Sub

    ''' <summary>
    ''' 场景控制
    ''' </summary>
    Private Sub KnxScnCtl(sender As Object, e As EventArgs) Handles btnCtlScn.Click, dgvScene.CellDoubleClick
        If isBusy Then Exit Sub
        If IsNothing(dgvScene.CurrentRow) OrElse (dgvScene.SelectedRows.Count = 0) Then Exit Sub
        Dim ScnId As Integer = dgvScene.SelectedRows(0).Cells(0).Value '场景ID
        Dim Scn As KnxSceneGroup = KS.Scenes(ScnId)
        frmSceneCtl.Scn = Scn
        If frmSceneCtl.ShowDialog = DialogResult.OK Then
            Scn.ControlScene(frmSceneCtl.CtlVal)
        End If
    End Sub

    ''' <summary>
    ''' 组地址读取
    ''' </summary>
    Private Sub dgvObject_CellDoubleClick(sender As DataGridView, e As DataGridViewCellEventArgs) Handles dgvObject.CellDoubleClick
        If isBusy Then Exit Sub
        If IsNothing(sender.CurrentRow) OrElse (sender.SelectedRows.Count = 0) Then Exit Sub
        KS.ReadObjectFeedback(Convert.ToInt32(sender.SelectedRows(0).Cells("Id").Value))
    End Sub

    ''' <summary>
    ''' 检测设备是否在线
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub dgvDevice_CellDoubleClick(sender As DataGridView, e As DataGridViewCellEventArgs) Handles dgvDevice.CellDoubleClick
        If isBusy Then Exit Sub
        If IsNothing(sender.CurrentRow) OrElse (sender.SelectedRows.Count = 0) Then Exit Sub
        Dim DevId As Integer = Convert.ToInt32(sender.SelectedRows(0).Cells("Id").Value)
        KS.DeviceCheck(DevId)
    End Sub

    Private Sub dgvLink_CellClick(sender As DataGridView, e As DataGridViewCellEventArgs) Handles dgvLink.CellClick
        If e.ColumnIndex = sender.Columns("btnPwd").Index Then '点击按钮的情况
            Dim r As DataGridViewRow = sender.Rows(e.RowIndex)
            MessageBox.Show($"Account: {r.Cells("Account").Value}{vbCrLf}Password: {r.Cells("Password").Value}", "Information")
        End If
    End Sub

    Private Sub dgvLink_CellDoubleClick(sender As DataGridView, e As DataGridViewCellEventArgs) Handles dgvLink.CellDoubleClick
        OpenUrl(sender.CurrentRow.Cells("LinkUrl").Value.ToString)
    End Sub

    Private Sub dgvIf_SelectionChanged(sender As DataGridView, e As EventArgs) Handles dgvIf.SelectionChanged
        'sender.ClearSelection() '接口dgv不允许被选中
    End Sub

    Private Sub btnTelLogClear_Click(sender As Object, e As EventArgs) Handles btnTelLogClear.Click
        lstTelLog.Items.Clear()
    End Sub

    ''' <summary>
    ''' 导出日志
    ''' </summary>
    Private Sub btnTelLogExport_Click(sender As Object, e As EventArgs) Handles btnTelLogExport.Click
        Try
            Dim sfd As New SaveFileDialog
            With sfd
                .Title = "导出日志"
                .InitialDirectory = Application.StartupPath
                .Filter = "CSV文件(*.csv)|*.csv"
                .FileName = $"KnxMessageLog_{Now:yyyyMMddHHmmss}.csv"
            End With
            If sfd.ShowDialog = DialogResult.OK Then
                WriteCsvLog(KS.MessageLog, sfd.FileName)
            End If
        Catch ex As Exception
            MsgShow.Err(ex.Message)
        End Try
    End Sub

    Private Sub btnInterface_Click(sender As Object, e As EventArgs) Handles btnInterface.Click
        frmInterface.Show()
    End Sub

    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        'frmScheduleSeq.dtScd = KS.Schedules.Sequence.Table
        frmScheduleSeq.Show()
    End Sub

    '''' <summary>
    '''' KNX接口状态变化事件
    '''' </summary>
    'Public Sub KnxConnectionChanged(sender As KnxBus, e As EventArgs)
    'slblIfDefault.Visible = (KS.Bus.Default.ConnectionState = BusConnectionState.Connected)
    'If KS.Bus.Count = 0 Then
    '    slblIfDefault.Text = KS.Bus.Default.ConnectionState.ToString
    '    slblIfCount.Visible = False
    'Else
    '    slblIfDefault.Text = "(+1)"
    '    slblIfCount.Visible = True
    '    Dim ConnectedCount As Integer = 0
    '    For Each k As KnxBus In KS.Bus
    '        If k.ConnectionState = BusConnectionState.Connected Then ConnectedCount += 1
    '    Next
    '    slblIfCount.Text = $"{ConnectedCount}/{KS.Bus.Count}"
    '    If ConnectedCount = KS.Bus.Count Then
    '        slblIfCount.ForeColor = Color.Green
    '    Else
    '        slblIfCount.ForeColor = Color.Red
    '    End If
    'End If

    '    '（以下已转移至frmInterface）
    '    'For Each r As DataGridViewRow In dgvIf.Rows
    '    '    If IsDBNull(r.Cells("NetState").Value) Then Continue For
    '    '    Select Case r.Cells("NetState").Value
    '    '        Case IPStatus.Success, IPStatus.Unknown
    '    '            Select Case r.Cells("CnState").Value
    '    '                Case BusConnectionState.Connected
    '    '                    r.DefaultCellStyle.BackColor = Color.PaleGreen
    '    '                Case BusConnectionState.Broken, BusConnectionState.MediumFailure
    '    '                    r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
    '    '                Case Else
    '    '                    r.DefaultCellStyle.BackColor = Color.LightGray
    '    '            End Select
    '    '        Case Else
    '    '            r.DefaultCellStyle.BackColor = Color.LightCoral
    '    '    End Select
    '    'Next
    'End Sub

    '''' <summary>
    '''' 定时器状态变化事件
    '''' </summary>
    'Private Sub ScheduleTimerStateChanged(e As KnxScheduleTimerState)
    '    slblScdState.Text = e.ToString
    '    Select Case e
    '        Case KnxScheduleTimerState.Stoped
    '            slblScdState.ForeColor = Color.Gray
    '        Case KnxScheduleTimerState.Starting
    '            slblScdState.ForeColor = Color.Blue
    '        Case KnxScheduleTimerState.Running
    '            slblScdState.ForeColor = Color.Green
    '        Case Else
    '            slblScdState.ForeColor = SystemColors.ControlText
    '    End Select
    'End Sub

    'Private Sub slblGithub_Click(sender As Object, e As EventArgs) Handles slblGithub.Click
    'OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnxClient")
    'End Sub

    Private Sub frmMainTable_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        frmInterface.Close()
        frmScheduleSeq.Close()
    End Sub

#Region "Schedule"
    'WithEvents tmScdS As New Timer '时间表定时器（秒），用于对齐刻度
    'WithEvents tmScd As New Timer '时间表定时器（分钟）

    'Private Sub ScheduleTimerInit() '初始化时间表
    '    tmScd.Stop()
    '    tmScd.Interval = 60000
    '    tmScdS.Interval = 1000
    '    tmScdS.Start()
    'End Sub

    'Private Sub tmScdS_Tick(sender As Timer, e As EventArgs) Handles tmScdS.Tick
    '    If Now.Second = 0 Then '当前秒数为0时启动主定时器
    '        sender.Stop()
    '        Call tmScd_Tick(tmScd, Nothing) '启动主定时器时立即触发一次
    '        tmScd.Start()
    '    End If
    'End Sub

    'Private Sub tmScd_Tick(sender As Timer, e As EventArgs) Handles tmScd.Tick
    '    With KS.Schedules.Sequence
    '        Dim n As New TimeHM(Now) '获取当前时间
    '        If n < .NextTime Then Exit Sub '当前时间早于下次事件事件则跳过
    '        For i = .NextId To .Count - 1 '从下次事件ID开始搜索
    '            If n = .Table(i)("Time") Then '找到当前时间的定时事件
    '                tbMsg.AppendText($"[{Now}]{ .Table(i)("ScheduleName")} Triggered.{vbCrLf}")
    '                KS.WriteGroupAddress(.Table.Rows(i)("InterfaceCode").ToString, .Table.Rows(i)("GroupAddress"), .Table.Rows(i)("Value"))
    '            Else '此处必然是下一次定时的时间
    '                .NextId = .Table(i)("Id")
    '            End If
    '            If i = .Count - 1 Then '当前事件为最后一个的情况，重置计数
    '                .NextId = 0
    '            End If
    '        Next
    '    End With
    'End Sub

#End Region

End Class