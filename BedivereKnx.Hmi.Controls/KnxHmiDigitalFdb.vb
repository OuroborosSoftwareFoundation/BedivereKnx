﻿Imports System.ComponentModel
Imports BedivereKnx.Hmi
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiDigitalFdb : Inherits KnxHmiShape

    Public Sub New(comp As KnxHmiComponent)
        MyBase.New(comp)
    End Sub

    ''' <summary>
    ''' 设置值
    ''' </summary>
    ''' <param name="value"></param>
    Public Sub SetValue(value As GroupValue)
        Me.KnxGroup.Value = value
    End Sub

    Protected Friend Overrides Sub _GroupValueChanged(value As GroupValue)
        Dim isOn As Boolean = False
        Select Case value.TypedValue
            Case True, > 0
                isOn = True
            Case False, 0
                isOn = False
            Case Else
                isOn = False
        End Select
        If Mapping.HasFillColorConvertion Then
            FillColor = IIf(isOn, Mapping.FillColors.Last, Mapping.FillColors.First)
        End If
        If Mapping.HasStrokeColorConvertion Then
            StrokeColor = IIf(isOn, Mapping.StrokeColors.Last, Mapping.StrokeColors.First)
        End If
    End Sub

End Class
