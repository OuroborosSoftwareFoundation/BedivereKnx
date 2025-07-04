using System.Text.RegularExpressions;

namespace BedivereKnx
{

    public static class KnxCommon
    {

        //废弃，使用GroupAddress.TryParse检测
        /// <summary>
        /// 判断字符串是否为KNX组地址
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        //public static bool IsGroupAddress(string inputString)
        //{
        //    return Regex.IsMatch(inputString, "^([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5])$");
        //}

        /// <summary>
        /// 判断字符串是否包含KNX组地址
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool ContainsGroupAddress(string inputString)
        {
            return Regex.IsMatch(inputString, "^.*([0-9]|0*[0-2][0-9]|0*3[0-1])/(0*[0-7])/([0-9]|0*[0-9]{2}|0*1[0-9][0-9]|0*2[0-4][0-9]|0*25[0-5]).*$");
        }

        //废弃，使用IndividualAddress.TryParse检测
        /// <summary>
        /// 判断是否为有效的KNX物理地址
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        //public static bool IsIndividualAddress(string inputString)
        //{
        //    return Regex.IsMatch(inputString, "^(^((0?[0-1]?[0-5]|0?0?[0-9])\\.){2}(25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[0-9]?[0-9]))$");
        //}

    }

}
