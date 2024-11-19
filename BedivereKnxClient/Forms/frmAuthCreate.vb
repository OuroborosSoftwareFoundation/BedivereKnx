Imports System.Configuration

Public Class frmAuthCreate

    Private Sub frmAuth_Load(sender As Object, e As EventArgs) Handles Me.Load
        tbText.Text = _AuthInfo.UserName
        numVer1.Value = My.Application.Info.Version.Major
        numVer2.Value = My.Application.Info.Version.Minor
        numVer3.Value = My.Application.Info.Version.Build
        dtpDOE.Value = Now
        tbKey.Text = My.Application.Info.ProductName
        tbIv.Text = My.Application.Info.CompanyName.Split(" "c).First
#If DEBUG Then
        tbText.Enabled = True
        tbText.ReadOnly = False
#End If
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Try
            Dim dt As Date = IIf(dtpDOE.Checked, dtpDOE.Value.Date, DateTime.MaxValue)
            Dim str As String = $"{My.Application.Info.ProductName}|{New Version(numVer1.Value, numVer2.Value, numVer3.Value).ToString}|{tbText.Text.Trim}|{dt.ToString}"
            Dim auth As String = AesEncryptor(str, My.Application.Info.ProductName, My.Application.Info.CompanyName.Split(" "c).First)
            tbAuth.Text = auth
            If MessageBox.Show("Write to AppSettings?", "Prompt", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = DialogResult.OK Then
                AppSettingSave("Authn", tbAuth.Text)
                'Dim cfg As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                'cfg.AppSettings.Settings("Authn").Value = tbAuth.Text
                'cfg.Save(ConfigurationSaveMode.Modified)
                _AuthInfo = New AuthorizationInfo(auth)
                Me.DialogResult = DialogResult.OK
                frmMain.lblAuth.Text = _AuthInfo.UserName
                Me.Close()
            End If
        Catch ex As Exception
            MsgShow.Err(ex.Message)
        End Try
    End Sub

    Private Sub tbAuth_Click(sender As Object, e As EventArgs) Handles tbAuth.Click
        Clipboard.SetText(tbAuth.Text)
    End Sub

End Class