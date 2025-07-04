using System.Reflection;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmReference : Form
    {

        AssemblyName[] asmNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

        public FrmReference()
        {
            InitializeComponent();
            lvRef.Items.Clear();
            foreach (AssemblyName asmName in asmNames)
            {
                Assembly asm = Assembly.Load(asmName);
                AssemblyCompanyAttribute? company = asm.GetCustomAttribute<AssemblyCompanyAttribute>();
                if ((company is null) || company.Company.Contains("Microsoft")) continue; //跳过空项和微软的程序集
                if (!lvRef.ContainsGroup(company.Company)) //未添加此公司的时候
                {
                    lvRef.Groups.Add(company.Company, company.Company);
                }
                string? name = asm.GetName().Name;
                if (string.IsNullOrWhiteSpace(name)) continue; //跳过空名称的程序集
                ListViewItem item = new(name)
                {
                    Name = name,
                };
                item.SubItems.Add(asm.GetName().Version?.ToString());
                item.Group = lvRef.Groups[company.Company];
                lvRef.Items.Add(item);
            }
            foreach (ListViewItem lvi in lvRef.Items) //遍历列表对象，去除子程序集
            {
                if (!lvi.Text.Contains('.')) continue;
                string parent = lvi.Text[0..lvi.Text.LastIndexOf('.')];
                if (lvRef.Items.ContainsKey(parent))
                {
                    lvi.Remove(); //去除子程序集
                }
            }
            lvRef.Sorting = SortOrder.Ascending;
            lvRef.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
