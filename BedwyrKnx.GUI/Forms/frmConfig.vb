Imports System.Net

Public Class frmConfig

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        tbDataFile.Text = AppConfig.DefaultDataFile
        tbHmiFile.Text = AppConfig.DefaultHmiFile
        tbLocalIp.Text = AppConfig.KnxLocalIP.ToString()
        chkInitRead.Checked = AppConfig.InitPolling
    End Sub

    Private Sub btnOpenDataFile_Click(sender As Object, e As EventArgs) Handles btnOpenDataFile.Click
        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath, 'My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Multiselect = False,
            .Filter = "Excel文件(*.xlsx)|*.xlsx"
        }
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            Dim fn As String = ofd.FileName
            If ofd.FileName.Contains(Application.StartupPath) Then
                fn = fn.Substring(Application.StartupPath.Length)
            Else
            End If
            tbDataFile.Text = fn
        End If
    End Sub

    Private Sub btnOpenHmiFIle_Click(sender As Object, e As EventArgs) Handles btnOpenHmiFIle.Click
        Dim ofd As New OpenFileDialog With {
            .InitialDirectory = Application.StartupPath, 'My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Multiselect = False,
            .Filter = "Draw.io文件(*.drawio)|*.drawio"
        }
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            Dim fn As String = ofd.FileName
            If ofd.FileName.Contains(Application.StartupPath) Then
                fn = fn.Substring(Application.StartupPath.Length)
            Else
            End If
            tbHmiFile.Text = fn
        End If
    End Sub

    Private Sub btnLocalIpSel_Click(sender As Object, e As EventArgs) Handles btnLocalIpSel.Click
        If frmNetworkInfo.ShowDialog() = DialogResult.OK Then
            tbLocalIp.Text = frmNetworkInfo.SelectedIp.ToString()
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        AppConfig.DefaultDataFile = tbDataFile.Text
        AppConfig.DefaultHmiFile = tbHmiFile.Text
        AppConfig.KnxLocalIP = IPAddress.Parse(tbLocalIp.Text)
        AppConfig.InitPolling = chkInitRead.Checked
        AppConfig.Save()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
