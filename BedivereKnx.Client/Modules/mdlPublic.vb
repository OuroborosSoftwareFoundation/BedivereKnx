Imports System.Configuration
Imports Ouroboros.Authorization.Iris
Imports BedivereKnx
Imports System.Net

Module mdlPublic

    Public _AuthInfo As AuthorizationInfoCollection

    ''' <summary>
    ''' 默认数据文件路径
    ''' </summary>
    Public _DataFile As String = vbNullString

    ''' <summary>
    ''' 默认HMI文件路径
    ''' </summary>
    Public _HmiFile As String = vbNullString

    ''' <summary>
    ''' 是否初始化读取
    ''' </summary>
    Public _InitRead As Boolean = False

    ''' <summary>
    ''' 本地IP
    ''' </summary>
    Public _LocalIp As IPAddress

    ''' <summary>
    ''' KNX系统对象
    ''' </summary>
    Public KS As KnxSystem

    'Public dicDataColHead As New Dictionary(Of String, String)

    Public Sub AppSettingSave(Key As String, Value As String)
        If ConfigurationManager.AppSettings.Item(Key) = Value Then Exit Sub '设置值相同时跳过
        Dim cfg As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        cfg.AppSettings.Settings(Key).Value = Value
        cfg.Save(ConfigurationSaveMode.Modified)
    End Sub

    Public Sub OpenUrl(url As String)
        Dim psi As New ProcessStartInfo(url)
        psi.UseShellExecute = True
        Process.Start(psi)
    End Sub

    'Public Sub GetEncodingNames()
    '    Console.WriteLine("Encode")
    '    For Each e In System.Text.Encoding.GetEncodings
    '        Debug.WriteLine(e.Name)
    '    Next
    'End Sub

End Module
