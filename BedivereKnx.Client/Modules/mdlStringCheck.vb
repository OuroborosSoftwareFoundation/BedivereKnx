Imports System.Text.RegularExpressions

Module mdlStringCheck

    Dim num_255 As String = "(25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[0-9]?[0-9])"

    ''' <summary>
    ''' 判断是否为数字
    ''' </summary>
    ''' <param name="Str">需要判断的字符串</param>
    ''' <param name="AllowDot">允许小数点</param>
    ''' <param name="AllowHex">允许十六进制字符A-F</param>
    ''' <param name="AllowHex">允许十六进制字符A-F</param>
    ''' <returns></returns>
    Public Function isNumber(Str As String, Optional AllowNegative As Boolean = False, Optional AllowDot As Boolean = False, Optional AllowHex As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Str) Then Return False
        Dim rep As String = "[0-9]+"
        If AllowDot Then rep &= "(\.[0-9]+)?"
        If AllowNegative Then rep = "-?" & rep
        If AllowHex Then rep = rep.Replace("[0-9]", "[0-9A-Fa-f]")
        Return Regex.IsMatch(Str, $"^{rep}$")
    End Function

    ''' <summary>
    ''' 判断是否为有效的IP地址
    ''' </summary>
    ''' <param name="IP">IP地址字符串</param>
    ''' <returns></returns>
    Public Function chkIpAddr(IP As String) As Boolean
        If String.IsNullOrEmpty(IP) Then Return False
        IP = IP.Replace(" "c, "")
        Dim rep As String = $"^({num_255}\.){{3}}{num_255}$"
        Return Regex.IsMatch(IP, rep)
    End Function

    ''' <summary>
    ''' 判断是否为有效的KNX物理地址
    ''' </summary>
    ''' <param name="IA">物理地址字符串</param>
    ''' <returns></returns>
    Public Function chkIndAddr(IA As String) As Boolean
        If String.IsNullOrEmpty(IA) Then Return False
        IA = IA.Replace(" "c, "")
        Dim rep As String = $"^((0?[0-1]?[0-5]|0?0?[0-9])\.){{2}}{num_255}$"
        Return Regex.IsMatch(IA, rep)
    End Function

    ''' <summary>
    ''' 判断是否为有效的KNX组地址
    ''' </summary>
    ''' <param name="GA">组地址字符串</param>
    ''' <returns></returns>
    Public Function chkGrpAddr(GA As String) As Boolean
        If String.IsNullOrEmpty(GA) Then Return False
        GA = GA.Replace(" "c, "")
        Dim rep As String = $"^(0?[0-3][0-1]|0?[0-2]?[0-9])/(0?0?[0-7])/{num_255}$"
        Return Regex.IsMatch(GA, rep)
    End Function

End Module

'Public Function chkIndAddr(IA As String) As Boolean
'    If IA = vbNullString Then Return False
'    IA = IA.Replace(" "c, "") '清除空格
'    If IA = (Not IA.Contains("."c)) Then Return False
'    Dim IAs As String() = IA.Split("."c)
'    If IAs.Length = 3 Then
'        For i = 0 To 2
'            If Not isNumber(IAs(i)) Then Return False
'            If IAs(i).Length > 3 Then Return False
'            Dim a As Integer = Convert.ToInt32(IAs(i))
'            If i < 2 Then
'                If a > 15 Then Return False
'            Else
'                If a > 255 Then Return False
'            End If
'        Next
'        Return True
'    Else
'        Return False
'    End If
'End Function

'Public Function chkGrpAddr(GA As String) As Boolean
'    If GA = vbNullString Then Return False
'    GA = GA.Replace(" "c, "") '清除空格
'    If GA = (Not GA.Contains("/"c)) Then Return False
'    Dim GAs As String() = GA.Split("/"c)
'    If GAs.Length = 3 Then
'        For i = 0 To 2
'            If Not isNumber(GAs(i)) Then Return False
'            If GAs(i).Length > 3 Then Return False
'            Dim a As Integer = Convert.ToInt32(GAs(i))
'            Select Case i
'                Case 0
'                    If a > 31 Then Return False
'                Case 1
'                    If a > 7 Then Return False
'                Case 2
'                    If a > 255 Then Return False
'            End Select
'        Next
'        Return True
'    Else
'        Return False
'    End If
'End Function
