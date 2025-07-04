
Public Class frmAuth

    Dim input As String = vbNullString

    Private Sub frmAuth_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShowAuthInfo()
    End Sub

    Private Sub ShowAuthInfo()
        With lvAuth
            .Items.Clear()
            .Items.Add("Text", "Name", 0)
            .Items.Add("ProductName", "ProductName", 1)
            .Items.Add("Version", "Version", 2)
            .Items.Add("DOE", "Expiration Date", 3)
            '.Items("Text").SubItems.Add(_AuthInfo.Text)
            '.Items("ProductName").SubItems.Add(_AuthInfo.Current.ProductName)
            '.Items("Version").SubItems.Add(_AuthInfo.Current.Version.ToString)
            'If _AuthInfo.Current.DOE.Date < DateTime.MaxValue.Date Then
            '    .Items("DOE").SubItems.Add("(Hidden)")
            'Else
            '    .Items("DOE").SubItems.Add("Permanent")
            'End If
            .Items("Text").SubItems.Add(_AuthInfo.Title)
            .Items("ProductName").SubItems.Add(_AuthInfo.ProductName)
            .Items("Version").SubItems.Add(_AuthInfo.Version.ToString)
            If _AuthInfo.ExpiryDate.Date < DateTime.MaxValue.Date Then
                .Items("DOE").SubItems.Add("(Hidden)")
            Else
                .Items("DOE").SubItems.Add("Permanent")
            End If

            .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        End With
    End Sub

    Private Sub lvAuth_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvAuth.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            If IsNothing(sender.SelectedItems) OrElse (sender.SelectedItems.Count = 0) Then Exit Sub
            If sender.SelectedItems(0).Name = "DOE" Then
                MessageBox.Show($"Expiration Date: {_AuthInfo.ExpiryDate.ToShortDateString}", "Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

End Class
