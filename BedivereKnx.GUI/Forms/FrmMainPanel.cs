using System.Data;
using BedivereKnx.Models;
using BedivereKnx.GUI.Controls;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmMainPanel : Form
    {

        private readonly KnxSystem knx = Globals.KS!;

        private readonly List<KnxHmiSwitchBlock> listSwitch = [];

        public FrmMainPanel()
        {
            InitializeComponent();
            AreaTreeInit();

        }

        private void tvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null) return;
            ShowAreaPanel(e.Node.Name);
        }





        /// <summary>
        /// 初始化树形结构
        /// </summary>
        private void AreaTreeInit()
        {
            tvArea.Nodes.Clear();
            TreeNode root = tvArea.Nodes.Add(Resources.Strings.Ui_RootNode); //添加根节点
            root.Expand(); //展开根节点
            root.Nodes.AddRange(knx.Areas.MainAreas.ToTreeNodes()); //添加主区域
            foreach (TreeNode mainNode in root.Nodes) //遍历主区域节点
            {
                mainNode.Nodes.AddRange(knx.Areas[mainNode.Name].ChildrenAreas.ToTreeNodes()); //向主区域下添加中区域
                foreach (TreeNode midNode in mainNode.Nodes)
                {
                    midNode.Nodes.AddRange(knx.Areas[midNode.Name].ChildrenAreas.ToTreeNodes()); //向中区域下添加子区域
                }
            }
        }

        private void ShowAreaPanel(string area)
        {
            if (!area.Contains('.')) return;
            List<KnxObject> objs = knx.Objects.Where(obj => !string.IsNullOrWhiteSpace(obj.AreaCode) && obj.AreaCode.StartsWith(area)).ToList();
            listSwitch.Clear();
            foreach (KnxObject obj in objs)
            {
                switch (obj.Type)
                {
                    case KnxObjectType.Light:
                        //【区分调光】
                        //listSwitch.Add(new(obj) { Dock= DockStyle.Fill});
                        break;
                    case KnxObjectType.Curtain:
                        break;
                    case KnxObjectType.Value:
                        break;
                    case KnxObjectType.Enablement:
                        break;
                    default:
                        continue;
                }
            }
            //MessageBox.Show(listSwitch.Count.ToString());
            //pnlSwitch_Resize(pnlSwitch, EventArgs.Empty);
            PanelInit(tlpSwitch, listSwitch);
        }

        private void PanelInit<T>(TableLayoutPanel tlp, List<T> list)
            where T : Control, IDefaultSize
        {
            tlp.Controls.Clear();
            tlp.ColumnCount = (int)Math.Floor((double)tlp.Width / T.DefaultWidth); //列的数量
            tlp.ColumnStyles.Clear();
            for (int c = 0; c < tlp.ColumnCount; c++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); //设定列的样式
            }
            tlp.RowCount = (int)Math.Ceiling((double)list.Count / tlp.ColumnCount); //行的数量
            tlp.RowStyles.Clear();
            for (int r = 0; r < tlp.RowCount; r++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, T.DefaultHeight)); //设定行的样式
            }
            foreach (T control in list)
            {
                tlp.Controls.Add(control);
            }
        }

        private void tlpSwitch_Resize(object sender, EventArgs e)
        {
            PanelInit(tlpSwitch, listSwitch);
        }

    }

}
