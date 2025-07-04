Public NotInheritable Class frmAbout

    Dim PicIndex As Integer = 0

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"About {Application.ProductName}"
        lblProdName.Text = Application.ProductName
        lblVersion.Text = $"Ver {My.Application.Info.Version}"
        lblCopyright.Text = $"{My.Application.Info.Copyright}.{vbCrLf}All rights reserved."
        'PicIndex = New Random().Next(1, 4)
        ShowTitlePic(3)
    End Sub

    Private Sub picTitle_Click(sender As PictureBox, e As EventArgs) Handles picTitle.Click
        PicIndex += 1
        ShowTitlePic(PicIndex)
    End Sub

    Private Sub ShowTitlePic(idx As Integer)
        Select Case PicIndex
            Case 1
                picTitle.Image = My.Resources.Bedivere1
                lblPicInfo.Text = $"How Sir Bedivere Cast the Sword Excalibur into the Water{vbCrLf}(Aubrey Beardsley)"
            Case 2
                picTitle.Image = My.Resources.Bedivere2
                lblPicInfo.Text = $"Sir Bedivere Throwing Excalibur into the Lake{vbCrLf}(Walter Crane)"
            Case Else
                picTitle.Image = My.Resources.Bedivere3
                lblPicInfo.Text = $"Last Chapter of the Noble Knights{vbCrLf}(Yu-Gi-Oh!)"
                PicIndex = 0
        End Select
        'sender.Height = sender.Image.Height * (sender.Width / sender.Image.Width)
    End Sub

    Private Sub lblGithub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblGithub.LinkClicked
        OpenUrl("https://www.github.com/OuroborosSoftwareFoundation/BedivereKnx")
    End Sub

    Private Sub btnLicense_Click(sender As Object, e As EventArgs) Handles btnLicense.Click
        frmAboutLicense.Show()
    End Sub

    Private Sub btnLibrary_Click(sender As Object, e As EventArgs) Handles btnLibrary.Click
        frmReference.ShowDialog()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class
