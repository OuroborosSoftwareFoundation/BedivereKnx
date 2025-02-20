Imports System.ComponentModel
Imports BedivereKnx.Hmi
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiButton : Inherits Button

    Public Event WriteValue As GroupWriteHandler

    Protected Friend Tip As New ToolTip

    <Category("Mapping")>
    Public ReadOnly Property GroupAddress As GroupAddress

    <Category("Mapping")>
    Public ReadOnly Property DPT As DptBase

    <Category("KNX")>
    Public Property Mapping As KnxHmiMapping

    ''' <summary>
    ''' 当前值
    ''' </summary>
    ''' <returns></returns>
    <Category("KNX")>
    Private Property CurrentValueId As Integer = -1

    Private Property InterfaceCode As String

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
            _GroupAddress = .Group.Address
            _DPT = .Group.DPT
            _Mapping = .Mapping
        End With
    End Sub

    Private Sub KnxHmiButton_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter
        Me.Tip.Show(Me.Text, sender)
    End Sub

    Private Sub KnxHmiButton_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        Me.Tip.Hide(sender)
    End Sub

    Private Sub KnxHmiButton_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        Dim gv As GroupValue
        Select Case Mapping.ChangeType
            Case Ouroboros.Hmi.HmiValueChangeType.Fixed
                gv = Mapping.Values(0)
            Case Ouroboros.Hmi.HmiValueChangeType.Toggle
                CurrentValueId += 1
                gv = Mapping.Values(CurrentValueId)
            Case Else
                gv = Mapping.Values(0)
        End Select
        Dim KWE As New KnxWriteEventArgs(vbNullString, GroupAddress, gv, MessagePriority.Low)
        RaiseEvent WriteValue(KWE)
    End Sub

End Class
