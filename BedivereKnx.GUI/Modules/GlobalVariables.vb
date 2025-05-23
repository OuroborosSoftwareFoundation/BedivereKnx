'Imports Ouroboros.Authorization.Iris
Imports Ouroboros.AuthManager.Eos

Module GlobalVariables

    ''' <summary>
    ''' 授权信息
    ''' </summary>
    'Public _AuthInfo As AuthorizationInfoCollection
    Public _AuthInfo As AuthInfo

    ''' <summary>
    ''' 程序配置
    ''' </summary>
    Public AppConfig As New AppConfigManager

    ''' <summary>
    ''' KNX系统对象
    ''' </summary>
    Public KS As KnxSystem

End Module
