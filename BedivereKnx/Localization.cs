//using System.Globalization;
//using System.Reflection;
//using System.Resources;

//namespace BedivereKnx

//{

//    internal static class Localization
//    {

//        private static ResourceManager _resourceManager;
//        private static CultureInfo _currentCulture;

//        static Localization()
//        {
//            _currentCulture = CultureInfo.CurrentCulture;
//            _resourceManager = new ResourceManager("BedivereKnx.Resources", Assembly.GetExecutingAssembly());
//        }

//        /// <summary>
//        /// 设置语言环境
//        /// </summary>
//        /// <param name="culture"></param>
//        public static void SetCulture(CultureInfo culture)
//        {
//            _currentCulture = culture;
//        }

//        /// <summary>
//        /// 获取当前语言的字符串
//        /// </summary>
//        /// <param name="resName"></param>
//        /// <returns></returns>
//        public static string GetString(string resName)
//        {
//            try
//            {
//                return _resourceManager.GetString(resName, _currentCulture) ?? resName;
//            }
//            catch
//            {
//                return resName;
//            }
//        }

//    }

//}

