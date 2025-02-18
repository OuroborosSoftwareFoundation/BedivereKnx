Imports System.ComponentModel
Imports BedivereKnx.Graphics
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiDigitalFdb : Inherits KnxHmiShape

    Private _Groupvalue As GroupValue
    <Category("Mapping")>
    Public Property GroupAddress As GroupAddress

    Public Property DPT As DptBase

    Public Property Groupvalue As GroupValue
        Get
            Return _Groupvalue
        End Get
        Set
            _Groupvalue = Value
            Select Case Value.TypedValue
                Case False, 0
                    Me.FillColor = OffColor
                Case True, 1
                    Me.FillColor = OnColor
                Case Else
                    Me.FillColor = OffColor
            End Select
        End Set
    End Property

    <Category("Mapping")>
    Public Property OffColor As Color = Color.Gray

    <Category("Mapping")>
    Public Property OnColor As Color = Color.Green

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(comp As KnxHmiComponent)
        MyBase.New(comp)
        Me.GroupAddress = comp.Group.Address
        Me.DPT = comp.Group.DPT
    End Sub

End Class
