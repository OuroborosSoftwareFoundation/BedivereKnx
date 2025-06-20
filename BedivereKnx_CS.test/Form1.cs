using System.Data;
using System.Net;
using BedivereKnx.DataFile;
using BedivereKnx.KnxSystem;

namespace BedivereKnx_CS.test
{
    public partial class Form1 : Form
    {

        public KnxSystem KS = new("data.xlsx", new IPAddress([127, 0, 0, 1]));
        DataTableCollection dtc;

        public Form1()
        {
            dtc = ExcelDataFile.FromExcel("data.xlsx", true, true);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataTable dt in dtc)
            {
                cb.Items.Add(dt.TableName);
            }

            tv.Nodes.Clear();
            foreach (AreaNode areaMain in KS.Areas.AreaAtLevel(1))
            {
                tv.Nodes.Add(areaMain.FullCode, areaMain.Name);
                if (areaMain.HasChildren)
                {
                    TreeNode nodeMain = tv.Nodes[areaMain.FullCode]!;
                    foreach (AreaNode areaMdl in areaMain.ChildrenAreas)
                    {
                        nodeMain.Nodes.Add(areaMdl.FullCode, areaMdl.Name);
                        if (areaMdl.HasChildren)
                        {
                            TreeNode nodeMdl = nodeMain.Nodes[areaMdl.FullCode]!;
                            foreach (AreaNode areaSub in areaMdl.ChildrenAreas)
                            {
                                nodeMdl.Nodes.Add(areaSub.FullCode, areaSub.Name);
                            }
                        }
                    }
                }
            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb.SelectedItem is null) return;
            if (string.IsNullOrWhiteSpace(cb.SelectedItem.ToString())) return;
            string tbn = cb.SelectedItem.ToString()!;
            DataTable dt = dtc[tbn]!;
            dgv.DataSource = dt;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderText = dt.Columns[i].Caption;
            }


        }

    }
}
