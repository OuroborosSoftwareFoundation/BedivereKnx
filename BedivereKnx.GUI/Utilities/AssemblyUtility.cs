using System.Reflection;

namespace BedivereKnx.GUI
{

    /// <summary>
    /// 程序集信息
    /// </summary>
    internal class AssemblyInfo
    {

        /// <summary>
        /// 程序集名称
        /// </summary>
        internal string? Name { get; private set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        internal string? ProductName { get; private set; }

        /// <summary>
        /// 程序集版本
        /// </summary>
        internal Version? Version { get; private set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        internal string? CompanyName { get; private set; }

        /// <summary>
        /// 版权信息
        /// </summary>
        internal string? CopyRight { get; private set; }

        /// <summary>
        /// 程序集路径
        /// </summary>
        internal string? LocationPath { get; private set; }

        internal AssemblyInfo()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly(); //获取当前程序集
                AssemblyName assemblyName = assembly.GetName();
                Name = assemblyName.Name; //程序集名称
                Version = assemblyName.Version; //版本
                AssemblyProductAttribute? assemblyProd = assembly.GetCustomAttribute<AssemblyProductAttribute>();
                if (assemblyProd is not null) ProductName = assemblyProd.Product; //产品名称
                AssemblyCompanyAttribute? assemblyComp = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
                if (assemblyComp is not null) CompanyName = assemblyComp.Company; //公司名
                AssemblyCopyrightAttribute? assemblyCr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
                if (assemblyCr is not null) CopyRight = assemblyCr.Copyright; //版权信息
                LocationPath = $@"{Path.GetDirectoryName(assembly.Location)}\"; //程序集路径
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
