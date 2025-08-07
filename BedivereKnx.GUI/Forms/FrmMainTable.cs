using BedivereKnx.Models;
using Knx.Falcon;
using System.Diagnostics;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmMainTable : Form
    {

        private readonly KnxSystem knx = Globals.KnxSys!;
        private DataGridView? curColFilterDgv; //当前进行列筛选的dgv

        public FrmMainTable()
        {
            InitializeComponent();
            knx.MessageTransmission += KnxMessageTransmission;
            DgvInit(); //初始化各个DataGridView：
            AreaTreeInit(); //初始化树形菜单
        }

        private void FrmMainTable_Load(object sender, EventArgs e)
        {
            //把全部标签页开启一遍，防止第一次筛选无效：
            foreach (TabPage tp in tabMain.TabPages)
            {
                tabMain.SelectedTab = tp;
            }
            tabMain.SelectedIndex = 0;
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

        /// <summary>
        /// 区域选择
        /// </summary>
        private void tvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null) return;
            DgvRowFilter(dgvLight, e.Node.Name);
            DgvRowFilter(dgvEnable, e.Node.Name);
            DgvRowFilter(dgvScene, e.Node.Name);
            DgvRowFilter(dgvDevice, e.Node.Name);
        }

        #region DGV

        private void DgvInit()
        {
            //对象：
            dgvLight.DataSource = knx.Objects[KnxObjectType.Light];
            //使能：
            dgvEnable.DataSource = knx.Objects[KnxObjectType.Enablement];
            //场景：
            dgvScene.DataSource = knx.Scenes.Table;
            //设备：
            dgvDevice.DataSource = knx.Devices.Table;
        }

        /// <summary>
        /// dgv排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Sorted(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
            if (tvArea.SelectedNode is null) return;
            DgvRowFilter(dgv, tvArea.SelectedNode.Name);
        }

        /// <summary>
        /// DataGridView行筛选
        /// </summary>
        /// <param name="dgv">DataGridView对象</param>
        /// <param name="area">筛选区域，全部显示则留空</param>
        private void DgvRowFilter(DataGridView dgv, string? areaFilter = null)
        {
            if (dgv.Rows.Count == 0) return; //防止有表格行数为0报错
            if (!dgv.Columns.Contains("AreaCode")) return; //不包含区域编号列直接跳出
            CurrencyManager currency = (CurrencyManager)BindingContext![dgv.DataSource];
            currency.SuspendBinding(); //挂起数据绑定
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //禁止自动调整列宽加速显示
            if (string.IsNullOrWhiteSpace(areaFilter)) //筛选的区域为空的情况，全部显示
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Visible = true;
                }
            }
            else //筛选区域不为空的情况
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string? areaCode = row.Cells["AreaCode"].Value.ToString(); //区域编号
                    if (string.IsNullOrWhiteSpace(areaCode)) //dgv区域编号为空的情况
                    {
                        row.Visible = false;
                    }
                    else //dgv区域编号不为空的情况
                    {
                        row.Visible = areaCode.StartsWith(areaFilter);
                    }
                }
            }
            //dgv.ClearSelection(); //全部取消选定
            int selShowCount = 0; //已经选中的行在筛选后依旧显示的情况
            foreach (DataGridViewRow sr in dgv.SelectedRows)
            {
                if (sr.Visible) //选中的行筛选后依旧显示的情况
                {
                    selShowCount++;
                }
                else //选中的行筛选后隐藏的情况
                {
                    sr.Selected = false;
                }
            }
            if (selShowCount == 0) //已经选中的行在筛选后全部隐藏，滚动条置顶
            {
                if (dgv.Rows.GetFirstRow(DataGridViewElementStates.Visible) >= 0)
                {
                    dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.GetFirstRow(DataGridViewElementStates.Visible); //滚动条置顶
                }
            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; //恢复自动调整列宽
            currency.ResumeBinding(); //恢复数据绑定
            dgv.Refresh(); //刷新DataGridView
        }

        #endregion DGV

        #region 列筛选菜单

        /// <summary>
        /// 列筛选菜单打开事件
        /// </summary>
        private void menuColFilter_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            menuColFilter.Items.Clear();
            if (menuColFilter.SourceControl is DataGridView dgv) //判断源是否为dgv
            {
                curColFilterDgv = dgv;
                menuColFilter.Items.Add(new ToolStripSeparator());
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    ToolStripMenuItem item = new()
                    {
                        ImageIndex = col.Index,
                        Text = col.HeaderText,
                        CheckOnClick = true,
                        Checked = col.Visible, //勾选当前显示的列
                    };
                    menuColFilter.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 列筛选菜单关闭事件
        /// </summary>
        private void menuColFilter_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked) return;
            foreach (ToolStripMenuItem item in menuColFilter.Items.OfType<ToolStripMenuItem>())
            {
                if (item.ImageIndex >= 0)
                {
                    curColFilterDgv!.Columns[item.ImageIndex].Visible = item.Checked; //调整dgv列的可见度
                }
            }
            curColFilterDgv = null;
        }

        /// <summary>
        /// 列筛选菜单选择后不自动消失
        /// </summary>
        private void menuColFilter_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 灯光

        private void dgvLight_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvLight.Columns["Id"].Visible = false;
            dgvLight.Columns["Type"].Visible = false;
            dgvLight.Columns["InterfaceCode"].Visible = false;
            dgvLight.Columns["AreaCode"].Visible = false;
            //dgvLight.Columns["BrightnessFeedback"].DisplayIndex = dgvLight.ColumnCount - 1;
            //dgvLight.Columns["SwitchFeedback"].DisplayIndex = dgvLight.ColumnCount - 2;
            //dgvLight.Columns["Dimmable"].DisplayIndex = dgvLight.ColumnCount - 3;
            dgvLight.GetLocalizableHeader();
            //dgvLight.Columns["SwFdb"].DefaultCellStyle.Format = "Value={0}";
            dgvLight.CellFormatting += DgvLight_CellFormatting;
        }

        private void DgvLight_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is KnxGroup obj)
            {
                //Debug.Print(obj.Value?.ToString());
                e.Value = obj.ToString();
                //e.FormattingApplied = true;
            }
        }

        private void dgvLight_SelectionChanged(object sender, EventArgs e)
        {
            if ((dgvLight.CurrentRow is null) || dgvLight.SelectedRows.Count == 0) return;
            btnObjToggle.Enabled = (dgvLight.SelectedRows.Count == 1); //不允许同时反转控制多个对象开关
            foreach (DataGridViewRow row in dgvLight.SelectedRows)
            {
                int id = (int)dgvLight.SelectedRows[0].Cells["Id"].Value;
                KnxLight light = knx.Objects.Get<KnxLight>(id);
                if (light.Dimmable) //可调光的情况
                {
                    numObjVal.Enabled = true;
                    GroupValue? val = light[KnxObjectPart.ValueFeedback].Value;
                    if (val is not null)
                    {
                        numObjVal.Value = (int)((double)val.TypedValue / 2.55); //显示亮度值
                    }
                }
                else //不可调光的情况
                {
                    numObjVal.Enabled = false;
                    numObjVal.Value = 0;
                }
            }
        }

        /// <summary>
        /// 灯光组地址读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvLight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvLight.CurrentRow is null) || dgvLight.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvLight.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value;
                knx.ReadObjectFeedback(id);
            }
        }

        /// <summary>
        /// 灯光开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        private void LightSwitch(bool? value = null)
        {
            if ((dgvLight.CurrentRow is null) || dgvLight.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvLight.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value;
                KnxLight light = knx.Objects.Get<KnxLight>(id);
                light.SwitchControl(value);
            }
        }

        private void btnObjOn_Click(object sender, EventArgs e)
        {
            LightSwitch(true);
        }

        private void btnObjOff_Click(object sender, EventArgs e)
        {
            LightSwitch(false);
        }

        private void btnObjToggle_Click(object sender, EventArgs e)
        {
            LightSwitch();
            //以下为测试用：
            //if ((dgvLight.CurrentRow is null) || dgvLight.SelectedRows.Count == 0) return;
            //int id = 326;// (int)dgvLight.SelectedRows[0].Cells["Id"].Value;
            //knx.Objects.Get<KnxLight>(id)[KnxObjectPart.SwitchFeedback].Value = new(1);
            //dgvLight.Refresh();
        }

        /// <summary>
        /// 亮度条滑动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numObjVal_Scroll(object sender, EventArgs e)
        {
            lblObjVal.Text = numObjVal.Value.ToString();
        }

        /// <summary>
        /// 亮度值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numObjVal_ValueChanged(object sender, EventArgs e)
        {
            if ((dgvLight.CurrentRow is null) || dgvLight.SelectedRows.Count == 0) return;
            int id = (int)dgvLight.SelectedRows[0].Cells["Id"].Value;
            KnxLight light = knx.Objects.Get<KnxLight>(id);
            light.BrightnessControl(numObjVal.Value);
        }

        #endregion

        #region 使能

        private void dgvEnable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvEnable.Columns["Id"].Visible = false;
            dgvEnable.Columns["Type"].Visible = false;
            dgvEnable.Columns["InterfaceCode"].Visible = false;
            dgvEnable.Columns["AreaCode"].Visible = false;
            //dgvEnable.Columns["EnablementType"].DisplayIndex = dgvEnable.ColumnCount - 1;
            dgvEnable.GetLocalizableHeader();
        }

        private void dgvEnable_SelectionChanged(object sender, EventArgs e)
        {
            //if ((dgvEnable.CurrentRow is null) || dgvEnable.SelectedRows.Count == 0) return;
            //if (dgvEnable.SelectedRows.Count == 1)
            //{
            //    int id = (int)dgvEnable.SelectedRows[0].Cells["Id"].Value;
            //    KnxEnablement enable = knx.Objects.Get<KnxEnablement>(id);
            //}
            //else
            //{

            //}
        }
        private void dgvEnable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvEnable.CurrentRow is null) || dgvEnable.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvEnable.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value;
                knx.ReadObjectFeedback(id);
            }
        }

        /// <summary>
        /// 使能控制
        /// </summary>
        /// <param name="value"></param>
        private void EnableControl(bool value)
        {
            if ((dgvEnable.CurrentRow is null) || dgvEnable.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvEnable.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value;
                KnxEnablement en = knx.Objects.Get<KnxEnablement>(id);
                en.EnableControl(value);
            }
        }

        private void btnEnTrue_Click(object sender, EventArgs e)
        {
            EnableControl(true);
        }

        private void btnEnFalse_Click(object sender, EventArgs e)
        {
            EnableControl(false);
        }

        #endregion

        #region 场景

        private void dgvScene_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvScene.Columns["Id"].Visible = false;
            dgvScene.Columns["Type"].Visible = false;
            dgvScene.Columns["InterfaceCode"].Visible = false;
            dgvScene.Columns["AreaCode"].Visible = false;
            dgvScene.GetLocalizableHeader();
        }

        /// <summary>
        /// 场景控制
        /// </summary>
        private void SceneControl()
        {
            if ((dgvScene.CurrentRow is null) || dgvScene.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvScene.SelectedRows[0];
            int id = (int)row.Cells["Id"].Value; //场景ID
            KnxScene scene = knx.Scenes[id];
            FrmSceneCtl frmSceneCtl = new(scene);
            if (frmSceneCtl.ShowDialog() == DialogResult.OK)
            {
                scene.SceneControl(frmSceneCtl.SelectedAddress);
            }
        }

        private void btnCtlScn_Click(object sender, EventArgs e)
        {
            SceneControl();
        }

        private void dgvScene_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SceneControl();
        }

        #endregion

        #region 设备

        private void dgvDevice_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDevice.Columns["Id"].Visible = false;
            dgvDevice.Columns["InterfaceCode"].Visible = false;
            dgvDevice.Columns["AreaCode"].Visible = false;
            dgvDevice.GetLocalizableHeader();
        }

        private void dgvDevice_SelectionChanged(object sender, EventArgs e)
        {
            if ((dgvDevice.CurrentRow is null) || dgvDevice.SelectedRows.Count == 0) return;
            if (dgvDevice.SelectedRows.Count == 1)
            {
                int id = (int)dgvDevice.SelectedRows[0].Cells["Id"].Value;
                KnxDeviceInfo dev = knx.Devices[id];
            }
            else
            {

            }
        }

        private void dgvDevice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvDevice.CurrentRow is null) || dgvDevice.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvDevice.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value; //设备ID
                knx.DeviceCheck(id);
            }
        }

        private void btnDevicePoll_Click(object sender, EventArgs e)
        {
            knx.DevicePoll(string.Empty);
        }

        #endregion

        #region 接口

        private void btnInterface_Click(object sender, EventArgs e)
        {
            new FrmInterface().ShowOrFront();
        }

        #endregion

        private void btnLink_Click(object sender, EventArgs e)
        {
            new FrmLink().ShowOrFront();
        }

        #region 日志

        /// <summary>
        /// KNX报文传输事件
        /// </summary>
        private void KnxMessageTransmission(KnxMsgEventArgs e, string? log)
        {
            string? value = e.Value?.ToString();
            string valueString = value is null ? string.Empty : $" = {value}";
            this.Invoke((MethodInvoker)delegate
            { //在UI线程上进行操作
                lstTelLog.Items.Insert(0, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}]{e.MessageType}|{e.EventType}: {e.SourceAddress} -> {e.DestinationAddress}{valueString} ({e.MessagePriority}, Hop={e.HopCount})");
            });
        }

        #endregion

        private void dgvLight_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //    DataGridViewColumn column = dgvLight.Columns[e.ColumnIndex];
            //    ListSortDirection direction;

            //    if (column.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            //    {
            //        direction = ListSortDirection.Descending;
            //    }
            //    else
            //    {
            //        direction = ListSortDirection.Ascending;
            //    }

            //    // 清除之前的排序
            //    dgvLight.Sort(dgvLight.Columns[e.ColumnIndex], direction);
            //    column.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ?
            //        SortOrder.Ascending : SortOrder.Descending;
        }

    }

}
