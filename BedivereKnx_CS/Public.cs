using System.Text.RegularExpressions;

namespace BedivereKnx
{

    internal static class Public
    {

        /// <summary>
        /// 判断字符串是否为KNX组地址
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        internal static bool IsGroupAddress(string inputString)
        {
            return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$");
        }

        /// <summary>
        /// 判断字符串是否包含KNX组地址
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        internal static bool ContainsGroupAddress(string inputString)
        {
            return Regex.IsMatch(inputString, "^.*([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5]).*$");
        }

    }

}
