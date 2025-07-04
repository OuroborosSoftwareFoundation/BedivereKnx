namespace BedivereKnx.GUI
{

    internal static class DictionaryExtension
    {

        /// <summary>
        /// 尝试向字典中添加键值对，如果存在则覆盖值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        internal static void TryAddOrCover<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (!dic.TryAdd(key, value)) dic[key] = value;
        }

    }

}
