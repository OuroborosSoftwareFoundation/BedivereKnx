Imports System.Configuration
Imports System.Net

Public Class AppConfigManager

    ''' <summary>
    ''' 缓存字典
    ''' </summary>
    Private ReadOnly buffer As New Dictionary(Of String, String)

    Private _DefaultDataFile As String = vbNullString
    Private _DefaultHmiFile As String = vbNullString
    Private _KnxLocalIP As IPAddress = New IPAddress({127, 0, 0, 1})
    Private _InitPolling As Boolean

    ''' <summary>
    ''' 默认数据文件路径
    ''' </summary>
    ''' <returns></returns>
    Public Property DefaultDataFile As String
        Get
            Return _DefaultDataFile
        End Get
        Set
            If Value <> _DefaultDataFile Then
                _DefaultDataFile = Value
                buffer.TryAddOrCover("DataFile", Value) '加入缓存字典
            End If
        End Set
    End Property

    ''' <summary>
    ''' 默认图形文件路径
    ''' </summary>
    ''' <returns></returns>
    Public Property DefaultHmiFile As String
        Get
            Return _DefaultHmiFile
        End Get
        Set
            If Value <> _DefaultHmiFile Then
                _DefaultHmiFile = Value
                buffer.TryAddOrCover("HmiFile", Value) '加入缓存字典
            End If
        End Set
    End Property

    ''' <summary>
    ''' KNX路由接口本地IP
    ''' </summary>
    Public Property KnxLocalIP As IPAddress
        Get
            Return _KnxLocalIP
        End Get
        Set
            If Not Value.Equals(_KnxLocalIP) Then
                _KnxLocalIP = Value
                buffer.TryAddOrCover("LocalIP", Value.ToString()) '加入缓存字典
            End If
        End Set
    End Property

    ''' <summary>
    ''' 是否初始化读取
    ''' </summary>
    Public Property InitPolling As Boolean
        Get
            Return _InitPolling
        End Get
        Set
            If Value <> _InitPolling Then
                _InitPolling = Value
                buffer.TryAddOrCover("InitPolling", Value) '加入缓存字典
            End If
        End Set
    End Property

    ''' <summary>
    ''' 通过读取App.config文件新建程序配置对象
    ''' </summary>
    Public Sub New()
        '默认数据文件
        If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("DataFile")) Then
            Dim fp As String = ConfigurationManager.AppSettings.Item("DataFile")
            If IO.File.Exists(fp) Then
                _DefaultDataFile = ConfigurationManager.AppSettings.Item("DataFile") '设置默认数据文件
            Else
                _DefaultDataFile = vbNullString '文件不存在的情况下置空
            End If
        End If

        '默认图形文件
        If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("HmiFile")) Then
            Dim fp As String = ConfigurationManager.AppSettings.Item("HmiFile")
            If IO.File.Exists(fp) Then
                _DefaultHmiFile = ConfigurationManager.AppSettings.Item("HmiFile") '设置默认图形文件
            Else
                _DefaultHmiFile = vbNullString '文件不存在的情况下置空
            End If
        End If

        '本地IP
        Dim ipStr As String = ConfigurationManager.AppSettings.Item("LocalIP")
        Dim localIp As New IPAddress({127, 0, 0, 1})
        If IPAddress.TryParse(ipStr, localIp) Then '尝试解析配置中的ip地址
            _KnxLocalIP = localIp
        Else
            _KnxLocalIP = New IPAddress({127, 0, 0, 1}) '解析失败使用回送地址
        End If

        '初始化读取
        If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("InitPolling")) Then
            Dim initPoll As Boolean = False
            If Boolean.TryParse(ConfigurationManager.AppSettings.Item("InitPolling"), initPoll) Then
                _InitPolling = initPoll
            Else
                _InitPolling = False '默认不初始化读取
            End If
        End If
    End Sub

    ''' <summary>
    ''' 将缓存中的修改项写入App.config文件
    ''' </summary>
    Public Sub Save()
        Dim cfg As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        For Each kv As KeyValuePair(Of String, String) In buffer
            If ConfigurationManager.AppSettings.Item(kv.Key) <> kv.Value Then '设置值与当前值不同的情况
                cfg.AppSettings.Settings(kv.Key).Value = kv.Value
            End If
        Next
        cfg.Save(ConfigurationSaveMode.Modified) '保存设置
    End Sub

End Class

'''' <summary>
'''' 保存单项配置
'''' </summary>
'''' <param name="Key">配置项</param>
'''' <param name="Value">值</param>
'Public Sub SaveAppSetting(Key As String, Value As String)
'    If ConfigurationManager.AppSettings.Item(Key) = Value Then Exit Sub '设置值相同时跳过
'    Dim cfg As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
'    cfg.AppSettings.Settings(Key).Value = Value
'    cfg.Save(ConfigurationSaveMode.Modified)
'End Sub

'''' <summary>
'''' 保存多项配置
'''' </summary>
'''' <param name="settings">配置的字典对象</param>
'Public Sub SaveAppSettings(settings As Dictionary(Of String, String))
'    Dim cfg As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
'    For Each kv As KeyValuePair(Of String, String) In settings
'        If ConfigurationManager.AppSettings.Item(kv.Key) <> kv.Value Then '设置值与当前值不同的情况
'            cfg.AppSettings.Settings(kv.Key).Value = kv.Value
'        End If
'    Next
'    cfg.Save(ConfigurationSaveMode.Modified) '保存设置
'End Sub
