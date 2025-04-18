﻿Imports System.Text.RegularExpressions
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Knx.Falcon.ApplicationData.MasterData

Module mdlPublic

    Public Function IsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$")
    End Function

    Public Function ContainsGroupAddress(inputString As String) As Boolean
        Return Regex.IsMatch(inputString, "^.*([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5]).*$")
    End Function

    Private Sub test()

        For Each dpt As DatapointType In DptFactory.Default.AllDatapointTypes
            Debug.WriteLine($"{dpt.MainTypeNumber}.xxx_{dpt.Name}__{dpt.Text}")
            For Each s As DatapointSubtype In dpt.SubTypes
                Debug.WriteLine($"{dpt.MainTypeNumber}.{s.SubTypeNumber.ToString.PadLeft(3, "0"c)}_{s.Name}__{s.Text}")
            Next
        Next

    End Sub

End Module
