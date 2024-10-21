Imports System.Management

Module mdlDongle

    Public Function CheckDongle(dongle) As Boolean


        Return True

        Dim qStrCpu As String = "Select * From Win32_Processor"
        Dim qStrBios As String = "Select * From Win32_BIOS"
        Dim qStrDisk As String = "Select Case* From Win32_PhysicalMedia"
        Dim qStrNet As String = "Select * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) And (Manufacturer <> 'Microsoft'))"

        Try
            Dim mos As New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")

            Dim moc As ManagementObjectCollection = mos.Get()
            If moc.Count < 1 Then Return False
            For Each dvc As ManagementObject In moc
                Return (dongle = MD5Encryptor(dvc("SerialNumber").ToString.Reverse))
                'Messagebox.Show($"ID={dvc("DeviceID")}, Caption={dvc("Caption")}, SN={dvc("SerialNumber")}")
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    'Friend Sub CreateDongle()
    '    Dim mos As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'")
    '    Dim moc As ManagementObjectCollection = mos.Get()
    '    If moc.Count < 1 Then
    '        Messagebox.Show("not found")
    '        Exit Sub
    '    End If
    '    For Each dvc As ManagementObject In moc
    '        Messagebox.Show($"ID={dvc("DeviceID")}, Caption={dvc("Caption")}, SN={dvc("SerialNumber")}")
    '        Messagebox.Show(MD5Encryptor(dvc("SerialNumber").ToString.Reverse))
    '    Next
    'End Sub

End Module
