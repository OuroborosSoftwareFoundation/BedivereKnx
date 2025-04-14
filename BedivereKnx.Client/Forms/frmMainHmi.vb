Imports Knx.Falcon
Imports Ouroboros.Hmi
Imports BedivereKnx.Hmi
Imports BedivereKnx.Hmi.Controls

Public Class frmMainHmi

    Protected Friend HmiPath As String = vbNullString
    Dim isBusy As Boolean = True
    Dim dicCurrFdb As New Dictionary(Of GroupAddress, List(Of KnxHmiDigitalFdb))
    Dim dicPages As Dictionary(Of String, HmiPage)

    Private Sub frmMainHmi_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Not IsNothing(KS) Then
        '    MsgShow.Warn("未加载KNX配置文件！")
        '    Me.Close()
        'Else
        '    'AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
        '    Dim ofd As New OpenFileDialog With {
        '    .InitialDirectory = Application.StartupPath,
        '    .Multiselect = False,
        '    .Filter = "draw.io Diagrams(*.drawio)|*.drawio"
        '}
        '    If ofd.ShowDialog(Me) = DialogResult.OK Then
        dicPages = ReadDrawioToDic(HmiPath) '从文件读取到的KNX界面信息
        CompMappingPlug() '补充数据表绑定方式
        For Each k As String In dicPages.Keys
            tvHmi.Nodes.Add(k, k)
        Next
        ''=========测试内容============
        'Dim lst As New List(Of KnxSwitchIndicator)
        'lst.Add(si1)
        'Comps.Add(New GroupAddress("1/1/117"), lst)
        'si1.FeedbackAddress = New GroupAddress("1/1/117")
        'Dim fdb As New KnxHmiDigitalFdb()
        ''=========测试内容============


        '    Else
        '        Me.Close()
        '    End If
        'End If
    End Sub

    Private Sub frmMainHmi_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        isBusy = False
        Me.Dock = DockStyle.Fill
        tvHmi.SelectedNode = tvHmi.Nodes(0)
        'ShowKnxGpxPage(tvGpx.Nodes(0).Name) '加载第一个页面
        AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub tvGpx_AfterSelect(sender As TreeView, e As TreeViewEventArgs) Handles tvHmi.AfterSelect
        If isBusy Then Exit Sub
        ShowKnxGpxPage(e.Node.Name)
    End Sub

    ''' <summary>
    ''' 补充图像控件中的数据表绑定方式
    ''' </summary>
    Private Sub CompMappingPlug()
        For Each p As HmiPage In dicPages.Values '遍历HMI字典
            For Each comp As KnxHmiComponent In p.Elements.OfType(Of KnxHmiComponent) '遍历每一页中的控件
                If comp.MappingMode = HmiMappingMode.DataTable Then '找出数据表绑定方式的控件
                    If comp.Text.Contains("$"c) Then '绑定的是场景的情况
                        Dim mapTexts As String() = comp.Text.Split("$"c) '{文本}${绑定信息}
                        comp.Text = mapTexts(0) '去除绑定信息之后的字符串作为控件文本使用
                        Dim lstVals As New List(Of GroupValue)
                        For Each rv In comp.Mapping.RawValues
                            lstVals.Add(New GroupValue(Convert.ToByte(rv)))
                        Next
                        comp.Group = KS.Scenes(mapTexts(1).Split("="c)(0))(0).Groups(KnxObjectPart.SceneControl)
                        AddHandler comp.Group.GroupValueChanged, AddressOf GroupValueChanged
                        comp.Mapping.Values = lstVals.ToArray()
                    ElseIf comp.Text.Contains("*"c) Then
                        Dim mapTexts As String() = comp.Text.Split("*"c) '{文本}*{绑定信息}
                        comp.Text = mapTexts(0) '去除绑定信息之后的字符串作为控件文本使用
                        Dim isSwitch As Boolean = True '是-开关，否-数值
                        If mapTexts(1).Contains("#"c) Then '有#代表定义了组地址类型
                            Dim gvAry As String() = mapTexts(1).Split("#"c) '分割数值和数据类型
                            Dim dpt As String() = gvAry.Last.Split("."c) '分割DPT和DPST
                            If dpt(0) > 2 Then isSwitch = False
                            mapTexts(1) = gvAry(0)
                        End If

                        Dim objCode As String = mapTexts(1).Split({"="c, "@"c})(0)
                        Dim objPart As KnxObjectPart
                        Select Case comp.Direction
                            Case HmiComponentDirection.Control
                                objPart = IIf(isSwitch, KnxObjectPart.SwitchControl, KnxObjectPart.ValueControl)
                            Case HmiComponentDirection.Feedback
                                objPart = IIf(isSwitch, KnxObjectPart.SwitchFeedback, KnxObjectPart.ValueFeedback)
                        End Select
                        comp.Group = KS.Objects(objCode)(0).Groups(objPart)
                        AddHandler comp.Group.GroupValueChanged, AddressOf GroupValueChanged

                        Dim lstVals As New List(Of GroupValue)
                        For Each rv In comp.Mapping.RawValues
                            If isSwitch Then rv = (rv = "1")
                            lstVals.Add(comp.Group.DPT.ToGroupValue(rv))
                        Next
                        comp.Mapping.Values = lstVals.ToArray
                    End If
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' 显示页面
    ''' </summary>
    ''' <param name="pageName"></param>
    Private Sub ShowKnxGpxPage(pageName As String)
        pnlHmi.Controls.Clear() '清理Panel的控件
        pnlHmi.Refresh() '刷新Panel
        If IsNothing(dicPages(pageName)) Then Exit Sub
        DrawPic(pageName) '绘制图片
        For Each comp As KnxHmiComponent In dicPages(pageName).Elements.OfType(Of KnxHmiComponent)
            'If IsNothing(fdb.ColorConvertion) Then Continue For '只添加有变化效果的控件
            Select Case comp.Direction'根据控制-反馈新建不同的控件
                Case HmiComponentDirection.Control '控制控件
                    Dim ctl As New KnxHmiButton(comp)
                    AddHandler ctl.HmiWriteValue, AddressOf HmiWriteGroupValue '绑定组地址写入事件
                    pnlHmi.Controls.Add(ctl)'把按钮加入窗体中
                Case HmiComponentDirection.Feedback '反馈控件
                    Dim fdb As New KnxHmiDigitalFdb(comp) '新建控件
                    pnlHmi.Controls.Add(fdb) '把控件加到窗体
                    Dim gaF As GroupAddress = fdb.KnxGroup.Address 'fdb.GroupAddress
                    If Not dicCurrFdb.ContainsKey(gaF) Then '不可使用TryGetValue优化
                        dicCurrFdb.Add(gaF, New List(Of KnxHmiDigitalFdb)) '不存在组地址所属控件时添加组地址
                    End If
                    dicCurrFdb(gaF).Add(fdb) '把控件加入字典中
                    'fdb.BringToFront()
            End Select
        Next
        For Each text As HmiTextElement In dicPages(pageName).Elements.OfType(Of HmiTextElement)

        Next
        pnlHmi.Controls.Add(btnLeftHide)
        btnLeftHide.Height = pnlHmi.Height
        PollPageGroupValue() '读取当前页面的反馈值
    End Sub

    Private Sub pnlGpx_SizeChanged(sender As Panel, e As EventArgs) Handles pnlHmi.SizeChanged
        If isBusy Then Exit Sub
        ShowKnxGpxPage(tvHmi.SelectedNode.Name)
    End Sub

    ''' <summary>
    ''' 绘制图片
    ''' </summary>
    ''' <param name="pageName"></param>
    Private Sub DrawPic(pageName As String)
        If isBusy Then Exit Sub
        If IsNothing(dicPages(pageName)) Then Exit Sub
        For Each img As HmiImageElement In dicPages(pageName).BackImages '.Elements.OfType(Of HmiImageElement)
            Dim g As Graphics = pnlHmi.CreateGraphics()
            g.DrawImage(img.Image, New Rectangle(img.RawLocation, img.RawSize))
        Next
    End Sub

    ''' <summary>
    ''' 写入组地址的值
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub HmiWriteGroupValue(e As KnxWriteEventArgs)
        KS.WriteGroupAddress(e.InterfaceCode, e.GroupAddr, e.GroupVal, e.Priority)
    End Sub

    ''' <summary>
    ''' 读取当前页面的反馈值
    ''' </summary>
    Private Sub PollPageGroupValue()
        For Each ctl As KnxHmiDigitalFdb In pnlHmi.Controls.OfType(Of KnxHmiDigitalFdb)
            KS.ReadGroupAddress(vbNullString, ctl.KnxGroup.Address) 'ctl.GroupAddress)
        Next
    End Sub

    ''' <summary>
    ''' KnxGroup值变化事件
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub GroupValueChanged(value As GroupValue)

    End Sub

    ''' <summary>
    ''' KNX报文事件
    ''' </summary>
    Private Sub KnxMessageTransmission(e As KnxMsgEventArgs, log As String)
        If IsNothing(e.Value) Then Exit Sub '忽略没有值的报文
        If e.MessageType <> KnxMessageType.FromBus Then Exit Sub '只接收来自总线的报文
        'Dim ga As GroupAddress = e.DestinationAddress

        'Dim value As List(Of KnxHmiDigitalFdb) = Nothing
        'If dicCurrFdb.TryGetValue(ga, value) Then '查找带有报文组地址的控件列表
        '    For Each fdb As KnxHmiDigitalFdb In value '遍历这些控件列表
        '        fdb.SetValue(e.Value) '给控件设置值，实现反馈效果
        '    Next
        'End If
    End Sub

    Private Sub btnLeftHide_Click(sender As Object, e As EventArgs) Handles btnLeftHide.Click
        SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed
    End Sub

End Class