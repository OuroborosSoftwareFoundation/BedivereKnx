Imports System.ComponentModel
Imports BedivereKnx.Hmi
Imports Knx.Falcon
Imports Knx.Falcon.ApplicationData.DatapointTypes

Public Class KnxHmiButton : Inherits Button

    Public Event HmiWriteValue As GroupWriteHandler

    Private _KnxGroup As KnxGroup
    Private ReadOnly _DPT As DptBase
    Private ReadOnly _GroupAddress As GroupAddress

    Protected Friend Tip As New ToolTip

    ''' <summary>
    ''' 组地址
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping")>
    Public ReadOnly Property GroupAddress As GroupAddress
        Get
            Return _KnxGroup.Address
        End Get
    End Property

    ''' <summary>
    ''' KNX数据类型
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping")>
    Public ReadOnly Property DPT As DptBase
        Get
            Return _KnxGroup.DPT
        End Get
    End Property

    ''' <summary>
    ''' 绑定对象
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping"), Browsable(False)>
    Public Property Mapping As KnxHmiMapping

    ''' <summary>
    ''' 当前值
    ''' </summary>
    ''' <returns></returns>
    <Category("Mapping"), Browsable(False)>
    Private Property CurrentValueId As Integer = -1

    ''' <summary>
    ''' 接口编号
    ''' </summary>
    ''' <returns></returns>
    Private Property InterfaceCode As String

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
    End Sub

    Public Sub New()
        InitializeComponent()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
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
            _KnxGroup = .Group
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
        _KnxGroup.Value = gv
        Dim ctrlCode As String = vbNullString
        Select Case _KnxGroup.DPT.MainNumber
            Case 1
                ctrlCode = [Enum].GetName(GetType(KnxObjectPart), KnxObjectPart.SwitchControl)
            Case 3
                ctrlCode = [Enum].GetName(GetType(KnxObjectPart), KnxObjectPart.DimmingControl)
            Case 17, 18, 26
                ctrlCode = [Enum].GetName(GetType(KnxObjectPart), KnxObjectPart.SceneControl)
            Case Else
                ctrlCode = [Enum].GetName(GetType(KnxObjectPart), KnxObjectPart.ValueControl)
        End Select
        Dim KWE As New KnxWriteEventArgs($"${ctrlCode}", GroupAddress, gv, MessagePriority.Low)
        RaiseEvent HmiWriteValue(KWE)
    End Sub

End Class
