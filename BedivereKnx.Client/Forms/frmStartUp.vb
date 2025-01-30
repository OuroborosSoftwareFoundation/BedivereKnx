
Public NotInheritable Class frmStartUp

    Private Sub frmStartUp_Load(sender As Object, e As EventArgs) Handles Me.Load
#If DEBUG Then
        Me.Close()
#End If
        Me.Text = My.Application.Info.ProductName
        lblProdName.Text = My.Application.Info.ProductName
        lblVersion.Text = $"Ver {Application.ProductVersion}"
        lblCopyright.Text = $"{My.Application.Info.Copyright}.{vbCrLf}All rights reserved."
    End Sub

End Class
