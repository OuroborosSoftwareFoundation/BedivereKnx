Imports System.ComponentModel

Public Class frmMainGraphics

    Private Sub frmMainGraphics_Load(sender As Object, e As EventArgs) Handles Me.Load

        'MsgBox($"{il1.Location.X},{il1.Location.Y}{vbCrLf}{il1.Center.X},{il1.Center.Y}")
        'MsgBox(CategoryAttribute.Layout.Category)
    End Sub

    Private Sub IndicatorLight1_MouseClick(sender As KnxSwitchIndicator, e As MouseEventArgs)
        MsgBox(sender.Selected)
    End Sub
End Class