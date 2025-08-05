using System.Text;

namespace BedivereKnx.GUI.Utilities
{
    internal static class PasswordUtilitity
    {

        private static readonly string emptyPwd = @"null;";

        /// <summary>
        /// 空密码的加密字符串
        /// </summary>
        internal static string EmptyEncryptedPassword => Base64npEncode(emptyPwd);

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool PasswordCheck(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return (Base64npEncode(input) == Globals.AppConfig.LoginPwd);
        }

        /// <summary>
        /// 密码解密
        /// </summary>
        /// <param name="encrypedPwd"></param>
        /// <returns></returns>
        internal static string? ToPlainPassword(string encrypedPwd)
        {
            string plainPwd = Base64npDecode(encrypedPwd);
            if (plainPwd == emptyPwd) return null;
            return plainPwd;
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="plainPwd"></param>
        /// <returns></returns>
        internal static string ToEncryptedPassword(string? plainPwd)
        {
            if (string.IsNullOrWhiteSpace(plainPwd)) plainPwd = emptyPwd;
            return Base64npEncode(plainPwd);
        }

        /// <summary>
        /// Base64np编码
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns></returns>
        internal static string Base64npEncode(string plainText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return Base64npEncode(bytes);
        }

        /// <summary>
        /// Base64np编码
        /// </summary>
        /// <param name="data">明文字节数组</param>
        /// <returns></returns>
        public static string Base64npEncode(byte[] data)
        {
            return Convert.ToBase64String(data).TrimEnd('=');
        }

        /// <summary>
        /// Basenp解码
        /// </summary>
        /// <param name="base64Text">Base64字符串</param>
        /// <returns></returns>
        internal static string Base64npDecode(string base64Text)
        {
            byte[] bytes = Base64npDecodeToBytes(base64Text);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Base64np解码
        /// </summary>
        /// <param name="base64Text">Base64字符串</param>
        /// <returns></returns>
        public static byte[] Base64npDecodeToBytes(string base64Text)
        {
            int padding = base64Text.Length % 4; //需要将加密字符串填充至4的倍数
            if (padding > 0)
            {
                base64Text += new string('=', 4 - padding); //在后方填充'='
            }
            return Convert.FromBase64String(base64Text);
        }

    }

}
