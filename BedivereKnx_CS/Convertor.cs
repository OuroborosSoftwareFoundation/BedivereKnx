namespace BedivereKnx
{

    internal static class Convertor
    {

        /// <summary>
        /// 字符串转数组（自动去除空项）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        internal static string[] ToArray(string? str, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(str)) return [];
            string[] arry0 = str.Split(separator);
            List<string> lst = new();
            foreach (string s in arry0)
            {
                if (string.IsNullOrWhiteSpace(s)) continue; //跳过空项
                lst.Add(s.Trim());
            }
            return lst.ToArray();
        }

        /// <summary>
        /// 字符串转任意类型数组
        /// </summary>
        /// <typeparam name="T">数组中元素类型</typeparam>
        /// <param name="inputString"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        internal static T[] ToArray<T>(string? inputString, char separator = ',')
        {
            if (string.IsNullOrWhiteSpace(inputString)) return [];
            string[] arry0 = inputString.Split(separator);
            List<T> lst = new();
            foreach (string s in arry0)
            {
                if (string.IsNullOrWhiteSpace(s)) continue; //跳过空项
                lst.Add((T)Convert.ChangeType(s.Trim(), typeof(T)));
            }
            return lst.ToArray();
        }

    }

}
