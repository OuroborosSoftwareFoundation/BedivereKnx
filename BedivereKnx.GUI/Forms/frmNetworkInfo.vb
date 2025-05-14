Imports System.Net
Imports System.Net.NetworkInformation

Public Class frmNetworkInfo

    ''' <summary>
    ''' 选中的IP地址
    ''' </summary>
    Friend SelectedIp As IPAddress

    Private Sub frmNetworkInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        LvInit()
        ShowIPs()
    End Sub

    Private Sub LvInit()
        lvNI.Clear()
        lvNI.Groups.Clear()
        lvNI.Columns.Add("IpFamily", "IP类型")
        lvNI.Columns.Add("IpAddr", "IP地址")
        'lvNI.Items.Clear()
    End Sub

    Private Sub ShowIPs()
        lvNI.BeginUpdate() 'ListView开始更新操作
        For Each ni In NetworkInterface.GetAllNetworkInterfaces() '遍历网络接口
            Dim niType As NetworkInterfaceType = ni.NetworkInterfaceType '网络接口类型
            Select Case niType
                Case NetworkInterfaceType.Loopback, NetworkInterfaceType.Tunnel
                    Continue For '无视回送地址和隧道
                Case Else
                    If Not lvNI.ContainsGroup(ni.Id) Then
                        Dim niMac As String = String.Join(":"c, ni.GetPhysicalAddress.GetAddressBytes().Select(Function(b) b.ToString("X2")))
                        Dim status As String
                        Select Case ni.OperationalStatus
                            Case OperationalStatus.Up
                                status = "已连接"
                            Case OperationalStatus.Down
                                status = "未连接"
                            Case Else
                                status = ni.OperationalStatus.ToString()
                        End Select
                        lvNI.Groups.Add(ni.Id, $"{ni.Name}({niMac}), {status}") '组名：{网卡名}({MAC})
                    End If
                    For Each addr As UnicastIPAddressInformation In ni.GetIPProperties().UnicastAddresses
                        Dim ipFam As String = vbNullString 'IP地址类型
                        Select Case addr.Address.AddressFamily
                            Case Net.Sockets.AddressFamily.InterNetwork 'IPv4
                                ipFam = "IPv4"
                            Case Net.Sockets.AddressFamily.InterNetworkV6 'IPv6
                                ipFam = "IPv6"
                            Case Else
                                ipFam = addr.Address.AddressFamily.ToString()
                        End Select
                        Dim lvi As New ListViewItem(ipFam) '新建ListView项
                        lvi.SubItems.Add(addr.Address.ToString()) '新建子项，内容为IP地址
                        lvi.Group = lvNI.Groups(ni.Id) '设置组
                        lvNI.Items.Add(lvi)
                    Next
            End Select
        Next
        lvNI.EndUpdate() 'ListView结束更新操作
        lvNI.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize) '自动调整列宽
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If lvNI.SelectedItems.Count = 0 Then Exit Sub
        SelectedIp = IPAddress.Parse(lvNI.SelectedItems(0).SubItems(1).Text)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class

'Private dtNI As New DataTable
'Private Sub DtInit()
'    dtNI.Columns.Add("Id", GetType(String))
'    dtNI.Columns("Id").Caption = "ID"
'    dtNI.Columns.Add("Name", GetType(String))
'    dtNI.Columns("Name").Caption = "名称"
'    dtNI.Columns.Add("Type", GetType(String))
'    dtNI.Columns("Type").Caption = "类型"
'    dtNI.Columns.Add("Mac", GetType(String))
'    dtNI.Columns("Mac").Caption = "MAC地址"
'    dtNI.Columns.Add("IpFamily", GetType(String))
'    dtNI.Columns("IpFamily").Caption = "IP类型"
'    dtNI.Columns.Add("IP", GetType(String))
'    dtNI.Columns("IP").Caption = "IP地址"
'End Sub

'Private Sub DgvInit()
'    DtInit()
'    For Each ni In NetworkInterface.GetAllNetworkInterfaces() '遍历网络接口
'        Dim niType As NetworkInterfaceType = ni.NetworkInterfaceType '网络接口类型
'        Select Case niType
'            Case NetworkInterfaceType.Loopback, NetworkInterfaceType.Tunnel
'                Continue For '无视回送地址和隧道
'            Case Else
'                Dim niName As String = ni.Name
'                Dim niId As String = ni.Id
'                Dim niMac As PhysicalAddress = ni.GetPhysicalAddress()
'                Dim ipProps As IPInterfaceProperties = ni.GetIPProperties()
'                For Each addr As UnicastIPAddressInformation In ipProps.UnicastAddresses
'                    Dim dr As DataRow = dtNI.NewRow()
'                    dr("Id") = niId
'                    dr("Name") = niName
'                    Select Case niType
'                        Case NetworkInterfaceType.Ethernet
'                            dr("Type") = "Wired"
'                        Case NetworkInterfaceType.Wireless80211
'                            dr("Type") = "Wireless"
'                        Case Else
'                            dr("Type") = niType.ToString()
'                    End Select
'                    dr("Mac") = String.Join(":"c, niMac.GetAddressBytes().Select(Function(b) b.ToString("X2")))
'                    Select Case addr.Address.AddressFamily
'                        Case Net.Sockets.AddressFamily.InterNetwork
'                            dr("IpFamily") = "IPv4"
'                        Case Net.Sockets.AddressFamily.InterNetworkV6
'                            dr("IpFamily") = "IPv6"
'                        Case Else
'                            dr("IpFamily") = addr.Address.AddressFamily.ToString()
'                    End Select
'                    dr("IP") = addr.Address.ToString()
'                    dtNI.Rows.Add(dr)
'                Next
'        End Select
'    Next
'    dgvNI.DataSource = dtNI
'    'dgvNI.Columns("Id").Visible = False
'    For Each col As DataGridViewColumn In dgvNI.Columns
'        col.HeaderText = dtNI.Columns(col.Name).Caption
'    Next
'End Sub
