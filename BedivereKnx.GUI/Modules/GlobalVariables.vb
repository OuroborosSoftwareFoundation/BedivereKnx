Imports Ouroboros.Authorization.Iris
Imports System.Net

Module GlobalVariables

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

End Module
