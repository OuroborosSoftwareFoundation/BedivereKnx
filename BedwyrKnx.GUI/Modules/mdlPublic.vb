﻿Imports System.Configuration
Imports BedwyrKnx
Imports System.Net

Module mdlPublic

    '''' <summary>
    '''' 打开全部KNX接口
    '''' </summary>
    'Friend Sub OpenAllKnxInterface(Optional GroupPoll As Boolean = False)
    '    Try
    '        KS.Bus.AllConnect(GroupPoll)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Public Sub OpenUrl(url As String)
        Dim psi As New ProcessStartInfo(url)
        psi.UseShellExecute = True
        Process.Start(psi)
    End Sub

    'Public Sub GetEncodingNames()
    '    Console.WriteLine("Encode")
    '    For Each e In System.Text.Encoding.GetEncodings
    '        Debug.WriteLine(e.Name)
    '    Next
    'End Sub

End Module
