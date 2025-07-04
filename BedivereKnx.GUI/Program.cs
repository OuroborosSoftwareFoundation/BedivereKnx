//   BedivereKnx.GUI

//   Copyright © 2024 Ouroboros Software Foundation. All rights reserved.
//   版权所有 © 2024 Ouroboros Software Foundation。保留所有权利。
//
//   This program Is free software: you can redistribute it And/Or modify
//   it under the terms Of the GNU General Public License As published by
//   the Free Software Foundation, either version 3 Of the License, Or
//   (at your option) any later version.
//   本程序为自由软件， 在自由软件联盟发布的GNU通用公共许可协议的约束下，
//   你可以对其进行再发布及修改。协议版本为第三版或（随你）更新的版本。

//   This program Is distributed In the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty Of
//   MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License For more details.
//   我们希望发布的这款程序有用，但不保证，甚至不保证它有经济价值和适合特定用途。
//   详情参见GNU通用公共许可协议。

//   You should have received a copy Of the GNU General Public License
//   along with this program.
//   If Not, see <https://www.gnu.org/licenses/>.
//   你理当已收到一份GNU通用公共许可协议的副本。
//   如果没有，请查阅 <http://www.gnu.org/licenses/> 

using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace BedivereKnx.GUI
{
    internal static class Program
    {
        // 程序退出代码：
        //  0   正常
        //  1   授权到期
        //  2   调整授权
        // -1   授权无效
        // -2   授权无效且非法进入主界面
        // -3   授权时间检测异常
        // -4   非法绕过授权调整窗体的模式选择
        // -5   非法显示授权窗体

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //==============================测试内容================================

            //Assembly assembly = Assembly.GetExecutingAssembly();
            //string[] strings = assembly.GetManifestResourceNames();
            //foreach (string s in strings)
            //{
            //    Debug.WriteLine(s);
            //}
            //var allCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
            //foreach (var culture in allCultures)
            //{
            //    Debug.WriteLine($"Name: {culture.Name}, DisplayName: {culture.DisplayName}");
            //}
            //MessageBox.Show(System.Net.IPAddress.Broadcast.ToString());

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-Hant");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-Hant");
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InstalledUICulture;
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;


            //==============================测试内容================================

            //检测授权：
            try
            {
                Globals.AuthInfo = new();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry)
                {
                    Forms.FrmAuthModify frmAuthModify = new();
                    frmAuthModify.lblExpMsg.Text = ex.Message;
                    frmAuthModify.ShowDialog();
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
#if DEBUG
            Forms.FrmMain mainForm = new();
            mainForm.Show();
#else
            //显示初始屏幕：
            Forms.FrmStartup frmStartup = new();
            frmStartup.Show();
            //frmStartup.Refresh(); //强制立即绘制窗体

            //异步延迟3秒后显示主窗体：
            Task.Delay(3000).ContinueWith(t =>
            {
                frmStartup.Invoke((Action)(() =>
                {
                    Forms.FrmMain mainForm = new();
                    frmStartup.Close();
                    mainForm.Show();
                }));
            }, TaskScheduler.FromCurrentSynchronizationContext());

#endif

            //无参数运行消息循环：
            Application.Run();
        }

    }

}
