using Ouroboros.AuthManager.Eos;

namespace BedivereKnx.GUI
{

    internal static class Globals
    {

        /// <summary>
        /// 程序集信息
        /// </summary>
        internal static AssemblyInfo AssemblyInfo = new();

        /// <summary>
        /// 程序配置
        /// </summary>
        internal static AppConfigManager AppConfig = new();

        /// <summary>
        /// 授权信息
        /// </summary>
        internal static AuthInfo? AuthInfo;

        /// <summary>
        /// KNX系统对象
        /// </summary>
        internal static KnxSystem? KnxSys;

    }

}
