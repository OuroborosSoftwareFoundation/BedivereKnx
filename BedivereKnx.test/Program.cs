using System.Configuration;
using System.Globalization;
using System.Net;
namespace BedivereKnx_CS.test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");

            //try
            //{
            //ExcelDataFile.FromExcel("data.xlsx");

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    throw;
            //}


            //GroupValue v1 = new GroupValue(1);
            //GroupValue v2 = new GroupValue(2);

            //bool b = EqualityComparer<GroupValue>.Default.Equals(v1, v2);
            //MessageBox.Show(b.ToString());

            //string str = "Exception{0},file path: {1}";
            //MessageBox.Show(string.Format(str, "aaa", "bbbb"));

            //int[] ia = new int[] { 1 };
            //Array.Resize(ref ia, 2);
            //ia[1] = 2;
            //MessageBox.Show(string.Join(',',ia));

            //int[] i = [];
            //MessageBox.Show(i.Length.ToString());

            //string s = "switch";
            //Enum.Parse<KnxObjectType>(s);

            //foreach (DatapointType dpt in DptFactory.Default.AllDatapointTypes)
            //{
            //    Debug.WriteLine($"{dpt.MainTypeNumber}.xxx_{dpt.Name}__{dpt.GetTranslatedText("zh-cn")}");
            //    foreach (DatapointSubtype dpst in dpt.SubTypes)
            //    {
            //        Debug.WriteLine($"{dpt.MainTypeNumber}.{dpst.SubTypeNumber.ToString().PadLeft(3, '0')}_{dpst.Name}__{dpst.GetTranslatedText("zh-cn")}");
            //    }
            //}

            string s = null;
            IPAddress ip = IPAddress.Parse(s);

            Application.Run(new Form1());
        }
    }
}