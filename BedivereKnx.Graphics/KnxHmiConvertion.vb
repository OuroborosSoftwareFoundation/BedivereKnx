Imports Knx.Falcon
Imports System.Drawing


Public Class KnxHmiGroup : Inherits KnxGroup

    Public Sub New(GroupAddress As GroupAddress, Optional DPTmain As Integer = 1, Optional DPTsub As Integer = 0)
        MyBase.New(GroupAddress, DPTmain, DPTsub)
    End Sub

    Public Sub New(AddressString As String, Optional DPTmain As Integer = 1, Optional DPTsub As Integer = 0)
        MyBase.New(AddressString, DPTmain, DPTsub)
    End Sub

End Class

Public Class KnxHmiConvertion : Inherits HmiConvertionBase

End Class

''' <summary>
''' KNX颜色变换
''' </summary>
Public Class KnxHmiColorConvertion : Inherits HmiConvertionBase

    Implements IHmiConvertion(Of GroupValue, Color)

    ''' <summary>
    ''' 关闭时颜色
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OffValue As Color
        Get
            Dim color As Color
            If Me.Values.TryGetValue(New GroupValue(0, 1), color) Then
                Return color
            Else
                Return DEFAULTCOLOR_OFF
            End If
        End Get
    End Property

    ''' <summary>
    ''' 开启时颜色
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OnValue As Color
        Get
            Dim color As Color
            If Me.Values.TryGetValue(New GroupValue(1, 1), color) Then
                Return color
            Else
                Return DEFAULTCOLOR_ON
            End If
        End Get
    End Property

    Public Overloads Property Values As New Dictionary(Of GroupValue, Color) Implements IHmiConvertion(Of GroupValue, Color).Values

    Public Sub New()
        Me.New(HmiConvertionPart.None)
    End Sub

    Public Sub New(part As HmiConvertionPart)
        Me.ConvertionPart = part
    End Sub

    Public Sub New(offColor As Color, onColor As Color)
        Me.New(HmiConvertionPart.None, offColor, onColor)
    End Sub

    Public Sub New(part As HmiConvertionPart, offColor As Color, onColor As Color)
        Me.ConvertionPart = part
        Me.Values = New Dictionary(Of GroupValue, Color) From {
            {New GroupValue(0, 1), offColor},
            {New GroupValue(1, 1), onColor}
        }
    End Sub

End Class

''' <summary>
''' KNX文本变换
''' </summary>
Public Class KnxHmiTextConvertion : Inherits KnxHmiConvertion

    Implements IHmiConvertion(Of GroupValue, String)

    ''' <summary>
    ''' 关闭时颜色
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OffValue As String
        Get
            Dim text As String = vbNullString
            If Me.Values.TryGetValue(New GroupValue(0, 1), text) Then
                Return text
            Else
                Return DEFAULTTEXT_OFF
            End If
        End Get
    End Property

    ''' <summary>
    ''' 开启时颜色
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OnValue As String
        Get
            Dim text As String = vbNullString
            If Me.Values.TryGetValue(New GroupValue(1, 1), text) Then
                Return text
            Else
                Return DEFAULTTEXT_ON
            End If
        End Get
    End Property

    Public Overloads Property Values As New Dictionary(Of GroupValue, String) Implements IHmiConvertion(Of GroupValue, String).Values

    Public Sub New()
        Me.New(HmiConvertionPart.None, DEFAULTTEXT_OFF, DEFAULTTEXT_ON)
    End Sub

    Public Sub New(part As HmiConvertionPart)
        Me.New(part, DEFAULTTEXT_OFF, DEFAULTTEXT_ON)
    End Sub

    Public Sub New(offText As String, onText As String)
        Me.New(HmiConvertionPart.None, offText, onText)
    End Sub

    Public Sub New(part As HmiConvertionPart, offText As String, onText As String)
        Me.ConvertionPart = part
        Me.Values = New Dictionary(Of GroupValue, String) From {
            {New GroupValue(0, 1), offText},
            {New GroupValue(1, 1), onText}
        }
    End Sub

End Class
