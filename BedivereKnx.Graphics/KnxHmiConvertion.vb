Imports Knx.Falcon
Imports System.Drawing

''' <summary>
''' KNXHmiConvertion
''' </summary>
Public Class KnxHmiConvertion : Inherits HmiConvertionBase

    Implements IHmiConvertionAddress(Of GroupAddress, GroupValue)

    ''' <summary>
    ''' 关闭时颜色
    ''' </summary>
    ''' <returns></returns>
    Public Property OffColor As Color

    ''' <summary>
    ''' 开启时颜色
    ''' </summary>
    ''' <returns></returns>
    Public Property OnColor As Color

    Public Property GroupAddress As GroupAddress

    Public Property GroupControlType As HmiValueConvertionType

    Public Property GroupValues As GroupValue()

    Private Property IHmiConvertionAddress_AddressType As GroupAddress Implements IHmiConvertionAddress(Of GroupAddress, GroupValue).AddressType
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As GroupAddress)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Property IHmiConvertionAddress_Address As GroupValue Implements IHmiConvertionAddress(Of GroupAddress, GroupValue).Address
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As GroupValue)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Overrides Property AddressType As Object
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Overrides Property Address As Object
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub New()
        Me.ConvertionPart = HmiConvertionPart.None
    End Sub

    Public Sub New(offColor As Color, onColor As Color)
        Me.ConvertionPart = HmiConvertionPart.None
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

    Public Sub New(part As HmiConvertionPart, offColor As Color, onColor As Color)
        Me.ConvertionPart = part
        Me.OffColor = offColor
        Me.OnColor = onColor
    End Sub

End Class
