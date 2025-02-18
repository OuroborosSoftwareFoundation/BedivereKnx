Imports BedivereKnx.Graphics
Imports Knx.Falcon
Imports Ouroboros.Hmi

Public Class frmMainGraphics

    Dim isBusy As Boolean = True
    Dim dicCurrFdb As New Dictionary(Of GroupAddress, List(Of KnxHmiDigitalFdb))
    Dim dicPages As Dictionary(Of String, HmiPage)

    Private Sub frmMainGraphics_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsNothing(KS) Then
            MsgShow.Warn("未加载KNX配置文件！")
            Me.Close()
        Else
            'AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission
            Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath,
            .Multiselect = False,
            .Filter = "draw.io Diagrams(*.drawio)|*.drawio"
        }
            If ofd.ShowDialog(Me) = DialogResult.OK Then
                dicPages = ReadDrawioToDic(ofd.FileName) '从文件读取到的KNX界面信息
                For Each k As String In dicPages.Keys
                    tvGpx.Nodes.Add(k, k)
                Next

                ''=========测试内容============
                'Dim lst As New List(Of KnxSwitchIndicator)
                'lst.Add(si1)
                'Comps.Add(New GroupAddress("1/1/117"), lst)
                'si1.FeedbackAddress = New GroupAddress("1/1/117")
                Dim fdb As New KnxHmiDigitalFdb()
                ''=========测试内容============


            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub frmMainGraphics_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        isBusy = False
        ShowKnxGpxPage(tvGpx.Nodes(0).Name) '加载第一个页面
    End Sub

    Private Sub tvGpx_AfterSelect(sender As TreeView, e As TreeViewEventArgs) Handles tvGpx.AfterSelect
        If isBusy Then Exit Sub
        ShowKnxGpxPage(e.Node.Name)
    End Sub

    Private Sub ShowKnxGpxPage(pageName As String)
        pnlGpx.Controls.Clear() '清理Panel的控件
        pnlGpx.Refresh() '刷新Panel
        If IsNothing(dicPages(pageName)) Then Exit Sub
        For Each comp As KnxHmiComponent In dicPages(pageName).Elements.OfType(Of KnxHmiComponent)
            'If IsNothing(comp.ColorConvertion) Then Continue For '只添加有变化效果的控件
            Select Case comp.Direction'根据控制-反馈新建不同的控件
                Case HmiComponentDirection.Control '控制控件
                    Dim ctl As New KnxHmiButton(comp)
                    pnlGpx.Controls.Add(ctl)'把按钮加入窗体中
                Case HmiComponentDirection.Feedback '反馈控件
                    Dim fdb As New KnxHmiDigitalFdb(comp) '新建控件
                    pnlGpx.Controls.Add(fdb) '把控件加到窗体
                    '=============================
                    Dim gaF As GroupAddress = fdb.GroupAddress
                    '=============================
                    If Not dicCurrFdb.ContainsKey(gaF) Then '不可使用TryGetValue优化
                        dicCurrFdb.Add(gaF, New List(Of KnxHmiDigitalFdb)) '不存在组地址所属控件时添加组地址
                    End If
                    dicCurrFdb(gaF).Add(fdb) '把控件加入字典中
                    'fdb.BringToFront()
            End Select
        Next
        For Each text As HmiTextElement In dicPages(pageName).Elements.OfType(Of HmiTextElement)

        Next
        DrawPic(pageName)
        pnlGpx.Controls.Add(btnLeftHide)
        btnLeftHide.Height = pnlGpx.Height
        ReadPageGroupValue() '读取当前页面的反馈值
    End Sub

    Private Sub pnlGpx_SizeChanged(sender As Panel, e As EventArgs) Handles pnlGpx.SizeChanged
        If isBusy Then Exit Sub
        ShowKnxGpxPage(tvGpx.SelectedNode.Name)
    End Sub

    Private Sub DrawPic(pageName As String)
        If isBusy Then Exit Sub
        If IsNothing(dicPages(pageName)) Then Exit Sub
        For Each img As HmiImageElement In dicPages(pageName).Elements.OfType(Of HmiImageElement)
            Dim g As System.Drawing.Graphics = pnlGpx.CreateGraphics()
            g.DrawImage(img.Image, New Rectangle(img.RawLocation, img.RawSize))
        Next
    End Sub

    ''' <summary>
    ''' 读取当前页面的反馈值
    ''' </summary>
    Private Sub ReadPageGroupValue()

    End Sub

    ''' <summary>
    ''' KNX报文事件
    ''' </summary>
    Private Sub KnxMessageTransmission(e As KnxMsgEventArgs, log As String)
        If IsNothing(e.Value) Then Exit Sub '忽略没有值的报文
        If e.MessageType <> KnxMessageType.FromBus Then Exit Sub '只接收来自总线的报文
        Dim ga As GroupAddress = e.DestinationAddress

        If dicCurrFdb.ContainsKey(ga) Then

        End If
        'For Each c As Control In Me.Controls
        '    If c.GetType = GetType(KnxSwitchIndicator) Then
        '        Dim ksi As KnxSwitchIndicator = c
        '        ksi.FeedbackValue = e.Value
        '    End If
        'Next

        'Dim lstKsi As List(Of KnxSwitchIndicator) = Nothing
        'If dicCurrFdb.TryGetValue(ga, lstKsi) Then '查找包含报文中组地址的控件
        '    For Each Ksi As KnxSwitchIndicator In lstKsi
        '        Ksi.FeedbackValue = e.Value '写入反馈值
        '    Next
        'End If
    End Sub

    Private Sub btnLeftHide_Click(sender As Object, e As EventArgs) Handles btnLeftHide.Click
        SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed
    End Sub

End Class