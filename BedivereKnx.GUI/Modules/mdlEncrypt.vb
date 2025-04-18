Module mdlEncrypt

    ''' <summary>
    ''' 进行AES加密，返回一个Base64的字符串
    ''' </summary>
    ''' <param name="text">明文</param>
    ''' <param name="key">密钥</param>
    ''' <param name="iv">初始化向量</param>
    Public Function AesEncryptor(text As String, key As String, iv As String) As String
        Try
            '将明文，密钥，初始化向量都转为字节数组备用
            Dim bs_text As Byte() = System.Text.Encoding.UTF8.GetBytes(text)
            Dim bs_key As Byte() = System.Text.Encoding.UTF8.GetBytes(key)
            Dim bs_iv As Byte() = System.Text.Encoding.UTF8.GetBytes(iv)
            '创建AES加密对象
            Dim aes As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
            '设置密钥，密钥是一个长度为32的字节数组，所以可以用SHA256对bs_key进行加密
            aes.Key = System.Security.Cryptography.SHA256.Create().ComputeHash(bs_key)
            '设置初始化向量，初始化向量是一个长度为16的字节数组，所以可以用MD5对bs_iv进行加密
            aes.IV = System.Security.Cryptography.MD5.Create().ComputeHash(bs_iv)
            '创建对称加密器对象
            Dim encryptor As System.Security.Cryptography.ICryptoTransform = aes.CreateEncryptor()
            '对bs_text字节数组进行加密，获得加密后的字节数组bs_encryptor
            Dim bs_encryptor As Byte() = encryptor.TransformFinalBlock(bs_text, 0, bs_text.Length)
            '以Base64将加密后的字节数组返回
            Return Convert.ToBase64String(bs_encryptor)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 进行AES解密，返回明文
    ''' </summary>
    ''' <param name="text">以Base64格式表示的密文</param>
    ''' <param name="key">密钥</param>
    ''' <param name="iv">初始化向量</param>
    Public Function AesDecryptor(text As String, key As String, iv As String) As String
        Try
            If String.IsNullOrEmpty(text) Then Return Nothing
            '将基于Base64的密文，密钥，初始化向量都转为字节数组备用
            Dim bs_text As Byte() = System.Convert.FromBase64String(text)
            Dim bs_key As Byte() = System.Text.Encoding.UTF8.GetBytes(key)
            Dim bs_iv As Byte() = System.Text.Encoding.UTF8.GetBytes(iv)
            '创建AES加密对象
            Dim aes As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
            '设置密钥，密钥是一个长度为32的字节数组，所以可以用SHA256对bs_key进行加密
            aes.Key = System.Security.Cryptography.SHA256.Create().ComputeHash(bs_key)
            '设置初始化向量，初始化向量是一个长度为16的字节数组，所以可以用MD5对bs_iv进行加密
            aes.IV = System.Security.Cryptography.MD5.Create().ComputeHash(bs_iv)
            '创建对称解密器对象
            Dim decryptor As System.Security.Cryptography.ICryptoTransform = aes.CreateDecryptor()
            '对bs_text字节数组进行解密，获得解密后的字节数组bs_decryptor
            Dim bs_decryptor As Byte() = decryptor.TransformFinalBlock(bs_text, 0, bs_text.Length)
            '将解密后的字节数组重新以UTF8编码格式的字符串返回
            Return System.Text.Encoding.UTF8.GetString(bs_decryptor)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' MD5加密
    ''' </summary>
    ''' <param name="text">需要加密的字符串</param>
    Public Function MD5Encryptor(text As String) As String
        Dim data() As Byte = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(text))
        Dim sb As New System.Text.StringBuilder()
        For i = 0 To data.Length - 1
            sb.Append(data(i).ToString("X2", Globalization.CultureInfo.InvariantCulture))
        Next
        Return sb.ToString
    End Function

    ''' <summary>
    ''' Base64加密
    ''' </summary>
    ''' <param name="text">明文字符串</param>
    Public Function Base64Encryptor(text As String) As String
        Dim b() As Byte = System.Text.Encoding.UTF8.GetBytes(text)
        Return Convert.ToBase64String(b)
    End Function

    ''' <summary>
    ''' Base64解密
    ''' </summary>
    ''' <param name="text">Base64密文字符串</param>
    Public Function Base64Decryptor(text As String) As String
        Try
            Dim b() As Byte = Convert.FromBase64String(text)
            Return System.Text.Encoding.UTF8.GetString(b)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Module
