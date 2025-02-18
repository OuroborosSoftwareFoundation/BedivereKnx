'BedivereKnx.Client

'   Copyright © 2024 Ouroboros Software Foundation. All rights reserved.
'   版权所有 © 2024 Ouroboros Software Foundation。保留所有权利。
'
'   This program Is free software: you can redistribute it And/Or modify
'   it under the terms Of the GNU General Public License As published by
'   the Free Software Foundation, either version 3 Of the License, Or
'   (at your option) any later version.
'   本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，
'   你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

'   This program Is distributed In the hope that it will be useful,
'   but WITHOUT ANY WARRANTY; without even the implied warranty Of
'   MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the
'   GNU General Public License For more details.
'   我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。
'   详情参见GNU通用公共许可协议。

'   You should have received a copy Of the GNU General Public License
'   along with this program.
'   If Not, see <https://www.gnu.org/licenses/>.
'   你理当已收到一份GNU通用公共许可协议的副本。
'   如果没有，请查阅 <http://www.gnu.org/licenses/> 

Imports System.Configuration
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' Example:
    ' Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
    '
    '   ' Setting the application-wide default Font:
    '   e.Font = New Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)
    '
    '   ' Setting the HighDpiMode for the Application:
    '   e.HighDpiMode = HighDpiMode.PerMonitorV2
    '
    '   ' If a splash dialog is used, this sets the minimum display time:
    '   e.MinimumSplashScreenDisplayTime = 4000
    ' End Sub

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

#If DEBUG Then

            '==============================测试内容================================

            Dim str As String = "0/0/0=0|1&lt;div&gt;测试&lt;/div&gt;&lt;div&gt;wwwwwq&lt;23/2/1/div&gt;"
            'Dim ms As MatchCollection = Regex.Matches(str, "&lt;div&gt;(.*?)&lt;/div&gt;")
            'For Each m As Match In ms
            '    MsgBox(m.Groups(1).Value)

            'Next

            'Dim c As New HmiConvertionColor
            'c.Add(0, Color.Red)
            'c.Add(1, Color.Green)
            'c.Add(2, Color.Blue)
            'c.Add(3, Color.Yellow)
            'c.Add(4, Color.FromArgb(1, 4, 24, 5))
            'For Each i As Color In c.Values
            '    MsgBox(i.Name)
            'Next


            '==============================测试内容================================

#End If
            _AuthInfo = New Ouroboros.Authorization.AuthorizationInfoCollection
        End Sub

        Public Sub InitDics()
            '    dicDataColHead.Add("InterfaceCode", "接口编号")
            '    dicDataColHead.Add("", "")
            '    dicDataColHead.Add("SceneName", "场景名称")
        End Sub

        Private Sub AppSettingInit()
            '默认数据文件
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("DataFile")) Then
                Dim fp As String = ConfigurationManager.AppSettings.Item("DataFile")
                If IO.File.Exists(fp) Then
                    _DataFile = ConfigurationManager.AppSettings.Item("DataFile") '设置默认数据文件
                Else
                    _DataFile = vbNullString
                End If
            End If
            '初始化读取
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings.Item("InitRead")) Then
                Try
                    _InitRead = Convert.ToBoolean(ConfigurationManager.AppSettings.Item("InitRead"))
                Catch ex As Exception
                    _InitRead = False
                End Try
            End If
        End Sub

        'Public Sub AppSettingsRepair()
        '    Dim cfg As String(,) = {{"DataFile", "data.xlsx"}, {"RouteMcAddr", "224.0.23.12"}}
        '    For i = 0 To cfg.GetUpperBound(0)
        '        If String.IsNullOrEmpty(AppSettings.Items(cfg(i, 0)).Trim) Then
        '            AppSettings.Add(cfg(i, 0), cfg(i, 1))
        '        End If
        '    Next
        'End Sub

    End Class

End Namespace
