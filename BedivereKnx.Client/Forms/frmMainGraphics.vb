Imports Knx.Falcon
Imports BedivereKnx
Imports BedivereKnx.Graphics

Public Class frmMainGraphics

    Dim isBusy As Boolean = True
    Dim dicKSI As New Dictionary(Of GroupAddress, List(Of KnxSwitchIndicator))
    Dim dicGpx As Dictionary(Of String, KnxGpxElement())

    Private Sub frmMainGraphics_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath,
            .Multiselect = False,
            .Filter = "draw.io Diagrams(*.drawio)|*.drawio"
        }
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            dicGpx = ReadDrawioToDic(ofd.FileName) '从文件读取到的KNX界面信息
            For Each k As String In dicGpx.Keys
                tvGpx.Nodes.Add(k, k)
            Next

            ''=========测试内容============
            'Dim lst As New List(Of KnxSwitchIndicator)
            'lst.Add(si1)
            'Comps.Add(New GroupAddress("1/1/117"), lst)
            'si1.FeedbackAddress = New GroupAddress("1/1/117")
            ''=========测试内容============

            'AddHandler KS.MessageTransmission, AddressOf KnxMessageTransmission

        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmMainGraphics_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'For Each img As KnxGpxPicture In dicGpx("F01").OfType(Of KnxGpxPicture)
        '    'Dim picbox As New PictureBox With {
        '    '    .Location = img.Location,
        '    '    .Size = img.Size,
        '    '    .Image = img.Image,
        '    '    .SizeMode = PictureBoxSizeMode.StretchImage,
        '    '    .BackColor = Color.Transparent
        '    '}
        '    'pnlGpx.Controls.Add(picbox)
        '    Dim g As Graphics = pnlGpx.CreateGraphics()
        '    g.DrawImage(img.Image, New Rectangle(img.Location, img.Size))
        'Next
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
        If IsNothing(dicGpx(pageName)) Then Exit Sub
        For Each comp As KnxGpxComponent In dicGpx(pageName).OfType(Of KnxGpxComponent)
            If comp.Convertion.Length = 0 Then Continue For '只添加有变化效果的控件
            Select Case comp.Direction
                Case KnxGpxComponentType.Control '控制控件
                    Dim gaC As GroupAddress = comp.Convertion(0).GroupAddress
                    Dim btn As New Button With {
                            .Location = comp.Location,
                            .Size = comp.Size,
                            .Tag = gaC.ToString}
                    pnlGpx.Controls.Add(btn)'把按钮加入窗体中
                Case KnxGpxComponentType.Feedback '反馈控件
                    '=============================
                    Dim gaF As GroupAddress
                    '=============================
                    If Not dicKSI.ContainsKey(gaF) Then '不可使用TryGetValue优化
                        dicKSI.Add(gaF, New List(Of KnxSwitchIndicator)) '不存在组地址所属控件时添加组地址
                    End If
                    Dim ksi As New KnxSwitchIndicator(comp) '新建控件
                    pnlGpx.Controls.Add(ksi) '把控件加到窗体
                    dicKSI(gaF).Add(ksi) '把控件加入字典中
                    'ksi.BringToFront()
            End Select
        Next
        DrawPic(pageName)
        pnlGpx.Controls.Add(btnLeftHide)
        btnLeftHide.Height = pnlGpx.Height
    End Sub

    Private Sub pnlGpx_SizeChanged(sender As Panel, e As EventArgs) Handles pnlGpx.SizeChanged
        If isBusy Then Exit Sub
        ShowKnxGpxPage(tvGpx.SelectedNode.Name)
    End Sub

    Private Sub DrawPic(pageName As String)
        If isBusy Then Exit Sub
        If IsNothing(dicGpx(pageName)) Then Exit Sub
        For Each img As KnxGpxPicture In dicGpx(pageName).OfType(Of KnxGpxPicture)
            Dim g As System.Drawing.Graphics = pnlGpx.CreateGraphics()
            g.DrawImage(img.Image, New Rectangle(img.Location, img.Size))
        Next
    End Sub

    ''' <summary>
    ''' KNX报文事件
    ''' </summary>
    Private Sub KnxMessageTransmission(e As KnxMsgEventArgs, log As String)
        If IsNothing(e.Value) Then Exit Sub '忽略没有值的报文
        If e.MessageType <> KnxMessageType.FromBus Then Exit Sub '只接收报文
        Dim ga As GroupAddress = e.DestinationAddress

        'For Each c As Control In Me.Controls
        '    If c.GetType = GetType(KnxSwitchIndicator) Then
        '        Dim ksi As KnxSwitchIndicator = c
        '        ksi.FeedbackValue = e.Value
        '    End If
        'Next

        Dim lstKsi As List(Of KnxSwitchIndicator) = Nothing
        If dicKSI.TryGetValue(ga, lstKsi) Then '查找包含报文中组地址的控件
            For Each Ksi As KnxSwitchIndicator In lstKsi
                Ksi.FeedbackValue = e.Value '写入反馈值
            Next
        End If
    End Sub

    Private Sub btnLeftHide_Click(sender As Object, e As EventArgs) Handles btnLeftHide.Click
        SplitContainer1.Panel1Collapsed = Not SplitContainer1.Panel1Collapsed
    End Sub

End Class