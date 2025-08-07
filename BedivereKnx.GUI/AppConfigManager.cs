using BedivereKnx.GUI.Utilities;
using System.Configuration;
using System.Net;

namespace BedivereKnx.GUI
{

    internal class AppConfigManager
    {

        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly Dictionary<string, object?> ChangeBuff = [];

        /// <summary>
        /// KNX路由接口本地IP
        /// </summary>
        internal IPAddress? LocalIP
        {
            get => localIP;
            set
            {
                if (value != localIP)
                {
                    localIP = value;
                    ChangeBuff.TryAddOrCover("LocalIP", value); //加入缓存字典
                }
            }
        }
        private IPAddress? localIP;

        /// <summary>
        /// 是否初始化读取
        /// </summary>
        internal bool InitPolling
        {
            get => initPolling;
            set
            {
                if (value != initPolling)
                {
                    initPolling = value;
                    ChangeBuff.TryAddOrCover("InitPolling", value); //加入缓存字典
                }
            }
        }
        private bool initPolling = false;

        /// <summary>
        /// 默认数据文件路径
        /// </summary>
        internal string? DefaultDataFile
        {
            get => defaultDataFile;
            set
            {
                if (value != defaultDataFile)
                {
                    defaultDataFile = value;
                    ChangeBuff.TryAddOrCover("DataFile", value); //加入缓存字典
                }
            }
        }
        private string? defaultDataFile;

        /// <summary>
        /// 默认图形文件路径
        /// </summary>
        internal string? DefaultHmiFile
        {
            get => defaultHmiFile;
            set
            {
                if (value != defaultHmiFile)
                {
                    defaultHmiFile = value;
                    ChangeBuff.TryAddOrCover("HmiFile", value); //加入缓存字典
                }
            }
        }
        private string? defaultHmiFile;

        /// <summary>
        /// 登录密码（密文）
        /// </summary>
        internal string? LoginPwd
        {
            get => loginPwd;
            set
            {
                if (value != loginPwd)
                {
                    loginPwd = value;
                    ChangeBuff.TryAddOrCover("LoginPwd", value); //加入缓存字典
                }
            }
        }
        private string? loginPwd;

        /// <summary>
        /// 是否有登录密码
        /// </summary>
        internal bool HasLoginPwd => LoginPwd?.TrimEnd('=') != PasswordUtilitity.EmptyEncryptedPassword;

        /// <summary>
        /// 通过读取App.config文件新建程序配置对象
        /// </summary>
        internal AppConfigManager()
        {

            //本地IP：
            string? lip = ConfigurationManager.AppSettings["LocalIp"];
            if (!IPAddress.TryParse(lip, out localIP))//尝试解析IP地址
            {
                localIP = IPAddress.Loopback; //解析失败设置为环回地址（回环地址将无法通讯）
            }

            //初始化读取：
            string? initp = ConfigurationManager.AppSettings["InitPolling"];
            if (!bool.TryParse(initp, out initPolling)) //尝试解析字符串为bool
            {
                initPolling = false; //默认为false
            }

            //默认数据文件：
            string? dfp = ConfigurationManager.AppSettings["DataFile"];
            if (!string.IsNullOrWhiteSpace(dfp) && File.Exists(dfp))
            {
                defaultDataFile = dfp;
            }

            //默认图形文件：
            string? dhp = ConfigurationManager.AppSettings["HmiFile"];
            if (!string.IsNullOrWhiteSpace(dfp) && File.Exists(dhp))
            {
                defaultHmiFile = dhp;
            }

            //登录密码(密文)：
            string? lpwd = ConfigurationManager.AppSettings["LoginPwd"];
            loginPwd = lpwd;

        }

        /// <summary>
        /// 将缓存中的修改项写入App.config文件
        /// </summary>
        internal void Save()
        {
            Configuration cfgMng = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (KeyValuePair<string, object?> kv in ChangeBuff)
            {
                string? valString = kv.Value?.ToString();
                if (ConfigurationManager.AppSettings[kv.Key] != valString)
                {
                    cfgMng.AppSettings.Settings[kv.Key].Value = valString;
                }
            }
            cfgMng.Save(ConfigurationSaveMode.Modified); //保存设置
        }

        /// <summary>
        /// 保存单项设置
        /// </summary>
        /// <param name="cfgKey"></param>
        internal void SaveOne(string cfgKey, string? cfgValue)
        {
            Configuration cfgMng = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfgMng.AppSettings.Settings[cfgKey].Value = cfgValue;
            cfgMng.Save(ConfigurationSaveMode.Modified); //保存设置
        }

    }

}
