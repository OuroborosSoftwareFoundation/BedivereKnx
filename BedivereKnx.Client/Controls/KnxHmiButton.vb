Imports System.ComponentModel
Imports BedivereKnx.Graphics
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes
Imports Ouroboros.Hmi

Public Class KnxHmiButton : Inherits Button

    Private Component As New KnxHmiComponent
    Protected Friend Tip As New ToolTip

    <Category("Hmi"), DefaultValue(HmiComponentDirection.Feedback)>
    Public ReadOnly Property Direction As HmiComponentDirection
        Get
            Return Component.Direction
        End Get
    End Property

    <Category("KNX")>
    Public ReadOnly Property GroupAddress As GroupAddress
        Get
            Return Component.Group.Address
        End Get
    End Property

    <Category("KNX")>
    Public ReadOnly Property DPT As DptBase
        Get
            Return Component.Group.DPT
        End Get
    End Property

    <Category("KNX")>
    Public ReadOnly Property Values As GroupValue()
        Get
            Return Component.Mapping.Values
        End Get
    End Property

    <Category("KNX")>
    Private Property CurrentValueId As Integer

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        '在此处添加自定义绘制代码
    End Sub

    Public Sub New(comp As KnxHmiComponent)
        InitializeComponent()
        With comp
            Me.Left = .RawLocation.X
            Me.Top = .RawLocation.Y
            Me.Width = .RawSize.Width
            Me.Height = .RawSize.Height
            Me.Text = .Text
            Me.Visible = True
        End With
    End Sub

    Private Sub KnxHmiButton_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick

    End Sub

End Class
