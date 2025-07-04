Imports System.Net.NetworkInformation

Module mdlNet

    Public Sub GetAllNetInterface()
        Dim networkInterfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()

        ' 遍历每个网络接口
        For Each ni As NetworkInterface In networkInterfaces
            Console.WriteLine("Name: " & ni.Name)
            Console.WriteLine("Description: " & ni.Description)
            Console.WriteLine("Physical Address: " & String.Join(":", ni.GetPhysicalAddress().GetAddressBytes().Select(Function(b) b.ToString("X2"))))
            Console.WriteLine("Operational Status: " & ni.OperationalStatus)

            ' 获取网络接口上的IP配置
            Dim ipProps As IPInterfaceProperties = ni.GetIPProperties()

            ' 遍历每个Unicast IP地址
            For Each unicast As UnicastIPAddressInformation In ipProps.UnicastAddresses
                If unicast.Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then ' 仅获取IPv4地址
                    Console.WriteLine("IP Address: " & unicast.Address.ToString())
                End If
            Next

            Console.WriteLine(New String("-"c, 40))
        Next

        Console.ReadLine()

    End Sub

    Sub GetNetInterface()
        ' 指定要查找的网卡名称或描述的一部分
        Dim targetNetworkInterfaceName As String = "Ethernet" ' 你可以根据实际的网卡名称或描述进行修改

        ' 获取所有网络接口
        Dim networkInterfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()

        ' 遍历每个网络接口
        For Each ni As NetworkInterface In networkInterfaces
            ' 检查网卡名称或描述是否包含目标字符串
            If ni.Name.Contains(targetNetworkInterfaceName) OrElse ni.Description.Contains(targetNetworkInterfaceName) Then
                Console.WriteLine("Found Network Interface: " & ni.Name)

                ' 获取网络接口上的IP配置
                Dim ipProps As IPInterfaceProperties = ni.GetIPProperties()

                ' 遍历每个Unicast IP地址
                For Each unicast As UnicastIPAddressInformation In ipProps.UnicastAddresses
                    If unicast.Address.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then ' 仅获取IPv4地址
                        Console.WriteLine("IP Address: " & unicast.Address.ToString())
                    End If
                Next

                ' 如果只需要第一个匹配的网卡，可以在找到后退出循环
                Exit For
            End If
        Next

        Console.ReadLine()
    End Sub

End Module
