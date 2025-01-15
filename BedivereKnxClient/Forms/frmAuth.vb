
Public Class frmAuth

    Dim input As String = vbNullString

    Private Sub frmAuth_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShowAuthInfo()
        Me.KeyPreview = (_AuthInfo.Status = AuthState.Expired)
#If DEBUG Then
        Me.KeyPreview = True
#End If
    End Sub

    Private Sub ShowAuthInfo()
        With lvAuth
            .Items.Clear()
            .Items.Add("UserName", "User Name", 0)
            .Items.Add("Product", "Product", 1)
            .Items.Add("Version", "Version", 2)
            .Items.Add("DOE", "Expiration Date", 3)
            .Items.Add("FeedbackStatus", "FeedbackStatus", 4)
            .Items("UserName").SubItems.Add(_AuthInfo.UserName)
            .Items("Product").SubItems.Add(_AuthInfo.Product)
            .Items("Version").SubItems.Add(_AuthInfo.Version.ToString)
            .Items("FeedbackStatus").SubItems.Add(_AuthInfo.Status.ToString)
            If _AuthInfo.DOE.Date < DateTime.MaxValue.Date Then
                .Items("DOE").SubItems.Add("(Hidden)")
            Else
                .Items("DOE").SubItems.Add("Permanent")
            End If
            .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        End With
    End Sub

    Private Sub frmAuth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        input &= e.KeyChar
        If e.KeyChar = "s" Then
            If input = "Ouroboros" Then
                input = vbNullString
                If frmAuthCreate.ShowDialog() = DialogResult.OK Then
                    ShowAuthInfo()
                End If
            Else
                input = vbNullString
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub lvAuth_DoubleClick(sender As ListView, e As EventArgs) Handles lvAuth.DoubleClick
    End Sub

    Private Sub lvAuth_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvAuth.MouseDoubleClick
        If e.Button = MouseButtons.Right Then
            If IsNothing(sender.SelectedItems) OrElse (sender.SelectedItems.Count = 0) Then Exit Sub
            If sender.SelectedItems(0).Name = "DOE" Then
                MsgShow.Info($"Expiration Date: {_AuthInfo.DOE.ToShortDateString}")
            End If

        End If

    End Sub
End Class
