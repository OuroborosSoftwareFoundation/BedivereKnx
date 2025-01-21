Public Class frmConfig

    Dim Raw_DataFile As String = _DataFile
    Dim Raw_InitRead As String = _InitRead

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        tbDataFile.Text = Raw_DataFile
        chkInitRead.Checked = Raw_InitRead
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

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        '保存配置
        If tbDataFile.Text <> Raw_DataFile Then
            _DataFile = tbDataFile.Text
            AppSettingSave("DataFile", tbDataFile.Text)
        End If
        If chkInitRead.Checked <> Raw_InitRead Then
            _InitRead = chkInitRead.Checked
            AppSettingSave("InitRead", chkInitRead.Checked.ToString)
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
