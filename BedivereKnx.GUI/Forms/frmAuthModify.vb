Imports Ouroboros.AuthManager.Eos

Public Class frmAuthModify

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If String.IsNullOrWhiteSpace(tbInput.Text) Then Exit Sub
        If chkModeNew.Checked Then
            Try
                Dim auth As New AuthInfo(tbInput.Text)
                If auth.CreateFile() Then
                    MessageBox.Show("授权成功，请重启程序", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        ElseIf chkModUpdate.Checked Then
            Try
                If _AuthInfo.Update(tbInput.Text) Then
                    MessageBox.Show("授权成功，请重启程序", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Else
            Environment.Exit(0)
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmAuthModify_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Environment.Exit(0)
    End Sub

End Class