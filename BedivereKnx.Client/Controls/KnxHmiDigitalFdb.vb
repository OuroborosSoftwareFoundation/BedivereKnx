Imports System.ComponentModel
Imports BedivereKnx.Hmi
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiDigitalFdb : Inherits KnxHmiShape

    Private _Groupvalue As GroupValue

    <Category("Mapping")>
    Public ReadOnly Property GroupAddress As GroupAddress

    <Category("Mapping")>
    Public ReadOnly Property DPT As DptBase

    <Category("KNX")>
    Public Property Groupvalue As GroupValue
        Get
            Return _Groupvalue
        End Get
        Set
            _Groupvalue = Value
            Dim isOn As Boolean = False
            Select Case Value.TypedValue
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
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(comp As KnxHmiComponent)
        MyBase.New(comp)
        With comp
            GroupAddress = .Group.Address
            DPT = .Group.DPT
        End With
    End Sub

    ''' <summary>
    ''' 设置值
    ''' </summary>
    ''' <param name="value"></param>
    Public Sub SetValue(value As GroupValue)
        Groupvalue = value
    End Sub

End Class
