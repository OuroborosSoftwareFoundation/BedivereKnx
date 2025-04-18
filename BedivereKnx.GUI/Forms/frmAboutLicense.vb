Imports System.IO

Public Class frmAboutLicense

    Dim dtLic As New DataTable
    Dim isbusy As Boolean = True
    Private Sub frmAboutLicense_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not File.Exists("gpl-3.0.txt") Then
            Dim sw As New StreamWriter("gpl-3.0.txt")
            sw.Write(My.Resources.gpl_3_0) '从资源文件恢复原版gpl
        End If

        dtLic.Clear()
        dtLic.Columns.Add("Lang")
        dtLic.Columns.Add("File")
        dtLic.Rows.Add("English", "gpl-3.0.txt")
        If File.Exists("gpl-3.0_zh-CN.txt") Then
            dtLic.Rows.Add("简体中文", "gpl-3.0_zh-CN.txt")
        End If

        cbLang.ValueMember = "File"
        cbLang.DisplayMember = "Lang"
        cbLang.DataSource = dtLic

        isbusy = False
        cbLang.SelectedIndex = 0
    End Sub

    Private Sub cbLang_SelectedIndexChanged(sender As ComboBox, e As EventArgs) Handles cbLang.SelectedIndexChanged
        Dim sr As New StreamReader(sender.SelectedValue.ToString)
        tbLicense.Text = sr.ReadToEnd
    End Sub

End Class