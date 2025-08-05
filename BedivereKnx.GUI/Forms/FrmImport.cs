using BedivereKnx.DataFile;
using Knx.Falcon;
using Knx.Falcon.ApplicationData.DatapointTypes;
using System.Data;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmImport : Form
    {

        public FrmImport()
        {
            InitializeComponent();
        }

        private void FrmImport_Load(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Title = Resources.Strings.Dlg_OpenDataFile,
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                Filter = "ETS5 Groupaddress File(*.csv)|*.csv",
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                EtsFileReader reader = new(ofd.FileName, EtsGaFileType.CSV);
                DataTable dt = reader.ToMiddleTable();
                dgvMain.DataSource = TransfromGaTable(dt);
            }
            else
            {
                this.Close();
            }
        }

        private DataTable TransfromGaTable(DataTable dt0)
        {
            //初始化输出DataTable：
            DataTable dt = new();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("SwitchDpt", typeof(DptBase));
            dt.Columns.Add("SwitchCtlAddr", typeof(GroupAddress));
            dt.Columns.Add("SwitchFdbAddr", typeof(GroupAddress));
            dt.Columns.Add("ValueDpt", typeof(DptBase));
            dt.Columns.Add("ValueCtlAddr", typeof(GroupAddress));
            dt.Columns.Add("ValueFdbAddr", typeof(GroupAddress));
            dt.Columns.Add("SceneAddr", typeof(GroupAddress));

            //按对象名称分组：
            var groups = dt0.AsEnumerable()
                            .GroupBy(row => string.Join('.', row.Field<string>("Name")!.Split('.').SkipLast(1)));

            foreach (var group in groups)
            {
                if (string.IsNullOrWhiteSpace(group.Key)) continue; //跳过空的对象名
                DataRow dr = dt.NewRow();
                dr["Code"] = group.Key; //对象编号
                foreach (DataRow r in group)
                {
                    string? desc = r.Field<string>("Desc"); //对象名称
                    string part = r.Field<string>("Name")!.Split('.').Last(); //去除对象名的组地址名，即部件
                    GroupAddress ga = r.Field<GroupAddress>("Address"); //组地址
                    DptBase? dpt = r.Field<DptBase>("DPT"); //数据类型
                    switch (part)
                    {
                        case "Ctl":
                        case "SwCtl":
                        case "Lock":
                        case "En":
                            //开关控制：
                            dr["SwitchCtlAddr"] = ga;
                            if (dr["SwitchDpt"] is DBNull) dr["SwitchDpt"] = dpt;
                            dr["Name"] = desc;
                            break;
                        case "Fdb":
                        case "SwFdb":
                            //开关反馈：
                            dr["SwitchFdbAddr"] = ga;
                            dr["SwitchDpt"] = dpt;
                            if (dr["Name"] is DBNull) dr["Name"] = desc;
                            break;
                        case "ValCtl":
                            //数值控制：
                            dr["ValueCtlAddr"] = ga;
                            if (dr["ValueDpt"] is DBNull) dr["ValueDpt"] = dpt;
                            dr["Name"] = desc;
                            break;
                        case "ValFdb":
                            //数值反馈：
                            dr["ValueFdbAddr"] = ga;
                            dr["ValueDpt"] = dpt;
                            if (dr["Name"] is DBNull) dr["Name"] = desc;
                            break;
                        case "Scn":
                        case "ScnCtl":
                            //场景控制
                            dr["SceneAddr"] = ga;
                            dr["Name"] = desc;
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }

}
