
Module mdlAuthorization

    Public Enum AuthState
        Valid = 1
        Missing = -1
        Broken = -2
        Expired = -4
        Earlier = -8
        Incomplete = -16
        Disparity = -32
    End Enum

    Public Class AuthorizationInfo000

        Public ReadOnly Property Product As String = vbNullString

        Public ReadOnly Property Version As Version = New Version(0, 0, 0, 0)

        Public ReadOnly Property UserName As String = vbNullString

        Public ReadOnly Property DOE As Date = Now.AddDays(-1)

        Public ReadOnly Property Status As AuthState = AuthState.Missing

        Public Sub New(Auth As String)
            Try
                Dim InfoStr As String = AesDecryptor(Auth, My.Application.Info.ProductName, My.Application.Info.CompanyName.Split(" "c).First)
                If String.IsNullOrEmpty(InfoStr) Then
                    _Status = AuthState.Missing
                    Exit Sub
                Else
                    Dim InfoAry As String() = InfoStr.Split("|"c)
                    If InfoAry.Length <> 4 Then
                        _Status = AuthState.Incomplete
                        Exit Sub
                    Else
                        _Product = InfoAry(0).Trim
                        _Version = New Version(InfoAry(1).Trim)
                        _UserName = InfoAry(2).Trim
                        _DOE = InfoAry(3).Trim
                        AuthCheck()
                    End If
                End If
            Catch ex As Exception
                _Status = AuthState.Broken
                Exit Sub
            End Try
        End Sub

        Private Sub AuthCheck()
            Try
                If Now > _DOE Then
                    _DOE = Now.AddDays(-1).Date
                    _Status = AuthState.Expired
                    Exit Sub
                End If
                If My.Application.Info.ProductName <> _Product Then
                    _Status = AuthState.Disparity
                    Exit Sub
                End If
                If My.Application.Info.Version > _Version Then
                    _Status = AuthState.Earlier
                    Exit Sub
                End If
                _Status = AuthState.Valid
            Catch ex As Exception
                _Status = AuthState.Broken
            End Try
        End Sub

    End Class

    'If String.IsNullOrEmpty(Auth) Then
    '    _Status = AuthState.Missing
    'Else
    '    Dim Info As String = AesDecryptor(Auth, My.Application.Info.ProductName, My.Application.Info.CompanyName.Split(" "c).First)
    '    If String.IsNullOrEmpty(Info) Then
    '        _Status = AuthState.Broken
    '    Else
    '        Dim InfoAry As String() = Info.Split("|"c)
    '        _UserName = InfoAry(0)
    '        If InfoAry.Length >= 3 Then
    '            Try
    '                _Version = New Version(InfoAry(1))
    '                If _Version >= My.Application.Info.Version Then
    '                    _Status = AuthState.Valid
    '                Else
    '                    _Status = AuthState.Earlier
    '                End If
    '            Catch ex As Exception
    '                _Status = AuthState.Broken
    '            End Try
    '            Try
    '                _DOE = InfoAry(2)
    '                If _DOE >= Now Then
    '                    _Status = AuthState.Valid
    '                Else
    '                    _DOE = Now.AddDays(-1)
    '                    _Status = AuthState.Expired
    '                End If
    '            Catch ex As Exception
    '                _Status = AuthState.Broken
    '            End Try
    '        Else
    '            _Status = AuthState.Incomplete
    '        End If
    '    End If
    'End If

End Module
