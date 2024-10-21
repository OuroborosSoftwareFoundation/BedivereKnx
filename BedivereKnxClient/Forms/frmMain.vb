Imports System.Configuration.ConfigurationManager

Public Class frmMain

    Dim WithEvents tmDoe As New Timer
    Dim rand As New Random

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _AuthInfo.Status <> AuthState.Valid Then Application.Exit()
        Me.Text = $"{My.Application.Info.ProductName} (Ver.{Application.ProductVersion})"
        lblAuth.Text = _AuthInfo.UserName
        tmDoe.Interval = 1000
        tmDoe.Start()
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '自动打开默认数据文件
        If Not String.IsNullOrEmpty(_DataFile) Then
            frmMainTable.Close()
            frmMainTable.fp = _DataFile
            ShowSubForm(frmMainTable)
        End If
    End Sub

    Private Sub Menu_Open_Click(sender As Object, e As EventArgs) Handles Menu_Open.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = Application.StartupPath
        ofd.Multiselect = False
        ofd.Filter = "Excel文件(*.xlsx)|*.xlsx"
        If ofd.ShowDialog(Me) = DialogResult.OK Then
            frmMainTable.Close()
            frmMainTable.fp = ofd.FileName
            ShowSubForm(frmMainTable)
        End If
    End Sub

    ''' <summary>
    ''' 显示子窗体
    ''' </summary>
    Private Sub ShowSubForm(f As Form)
        CloseChildenForm()
        f.MdiParent = Me
        f.Parent = pnlMain
        f.BringToFront()
        f.Dock = DockStyle.Fill
        f.Show()
    End Sub

    ''' <summary>
    ''' 关闭全部子窗体
    ''' </summary>
    Private Sub CloseChildenForm()
        If IsNothing(Application.OpenForms) Then Exit Sub
        For Each f As Form In Application.OpenForms
            If IsNothing(f.Tag) Then
                Continue For
            Else
                If f.Tag.ToString = "Children" Then f.Close()
            End If
        Next
    End Sub

    Private Sub Menu_Config_Click(sender As Object, e As EventArgs) Handles Menu_Config.Click
        frmConfig.ShowDialog()
    End Sub

    Private Sub Menu_Auth_Click(sender As Object, e As EventArgs) Handles Menu_Auth.Click
        frmAuth.ShowDialog()
    End Sub

    Private Sub Menu_About_Click(sender As Object, e As EventArgs) Handles Menu_About.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub tmDoe_Tick(sender As Timer, e As EventArgs) Handles tmDoe.Tick
        Try
            If _AuthInfo.DOE.Date = Date.MaxValue.Date Then
                sender.Stop()
                Exit Sub
            Else
                Dim span = _AuthInfo.DOE.Subtract(Now)
                'Dim cdv As String = $"{span.Days.ToString("X2")}{span.Hours.ToString("X2")}{span.Minutes.ToString("X2")}{span.Seconds.ToString("X2")}"
                Dim cdv = Convert.ToInt64(span.TotalSeconds).ToString("X")
                tbCtDn.Text = cdv
            End If
        Catch ex As Exception
            Application.Exit()
        Finally
            sender.Interval = rand.Next(1000, 10000)
        End Try
        If Now > _AuthInfo.DOE Then Application.Exit()
    End Sub

    Private Sub tmSec_Tick(sender As Object, e As EventArgs) Handles tmSec.Tick
        lblDateTime.Text = Now.ToString("F")
    End Sub

    Private Sub Menu_Exit_Click(sender As Object, e As EventArgs) Handles Menu_Exit.Click
        Application.Exit()
    End Sub

    Private Sub frmMain_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
    End Sub

End Class