using BedivereKnx.GUI.Extensions;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmMainTable_old : Form
    {

        private readonly KnxSystem knx = Globals.KS!;
        private DataGridView? curColFilterDgv; //当前进行列筛选的dgv

        public FrmMainTable_old()
        {
            InitializeComponent();
            knx.MessageTransmission += KnxMessageTransmission;
            foreach (TabPage tp in tabMain.TabPages)
            {
                tabMain.SelectedTab = tp;
                //if (tp.Tag == "Interface") tabMain.TabPages.Remove(tp);
            }
            tabMain.SelectedIndex = 0;
            //初始化各个DataGridView：
            dgvObject.DataSource = knx.Objects.ToList();
            //dgvObject.BindDataTable(knx.Objects.Table,
            //    ["Id", "AreaCode", "InterfaceCode", "GA_Dim"]);
            //dgvScene.DataSource=knx.Objects.Scenes.Values.ToList();
            ////dgvScene.BindDataTable(knx.Scenes.Table,
            ////    ["Id", "AreaCode", "InterfaceCode", "SceneValues"]);
            //dgvDevice.BindDataTable(knx.Devices.Table,
            //    ["Id", "AreaCode", "InterfaceCode"]);
            //dgvSchedule.BindDataTable(knx.Schedule.Table,
            //    ["Id"]);
            //DgvObjectsOptimize(); //优化对象显示
            AreaTreeInit(); //初始化Area树形结构
            //右键列筛选菜单：
        }

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

        ///// <summary>
        ///// 初始化DataGridView
        ///// </summary>
        ///// <param name="dgv">DataGridView对象</param>
        ///// <param name="dt">DataTable对象</param>
        ///// <param name="hiddenCols">需要隐藏的列</param>
        //private void DgvBindingInit(DataGridView dgv, DataTable dt, string[] hiddenCols)
        //{
        //    if ((dt is null) || (dt.Rows.Count == 0)) return;
        //    dgv.DataSource = dt;
        //    dgv.ClearSelection(); //取消选定
        //    foreach (DataGridViewColumn col in dgv.Columns)
        //    {
        //        string colName = col.Name;
        //        if (dt.Columns.Contains(colName))
        //        {
        //            col.HeaderText = dt.Columns[colName]!.Caption; //设置列标名
        //            if (hiddenCols.Contains(colName)) col.Visible = false; //隐藏不需要显示的列
        //        }
        //    }
        //}

        /// <summary>
        /// Object表显示优化
        /// </summary>
        private void DgvObjectsOptimize()
        {
            if (dgvObject.RowCount == 0) return;
            bool onlySw = true;
            foreach (DataGridViewRow row in dgvObject.Rows)
            {
                if ((row.Cells["ValueCtlAddr"].Value is not DBNull) | (row.Cells["ValueFdbAddr"].Value is not DBNull))
                {
                    onlySw = false;
                    break;
                }
            }
            if (onlySw)
            {
                dgvObject.Columns["ValueDpt"].Visible = false;
                dgvObject.Columns["ValueCtlAddr"].Visible = false;
                dgvObject.Columns["ValueFdbAddr"].Visible = false;
                dgvObject.Columns["ValueFdbValue"].Visible = false;
            }
        }

        /// <summary>
        /// 区域选择
        /// </summary>
        private void tvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null) return;
            DgvRowFilter(dgvObject, e.Node.Name);
            DgvRowFilter(dgvScene, e.Node.Name);
            DgvRowFilter(dgvDevice, e.Node.Name);
        }

        private void dgv_Sorted(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dgv.ClearSelection();
            DgvRowFilter(dgv, tvArea.SelectedNode.Name);
        }

        /// <summary>
        /// DataGridView行筛选
        /// </summary>
        /// <param name="dgv">DataGridView对象</param>
        /// <param name="area">筛选区域，全部显示则留空</param>
        private void DgvRowFilter(DataGridView dgv, string? area = null)
        {
            if (dgv.Rows.Count == 0) return; //防止有表格行数为0报错
            if (!dgv.Columns.Contains("AreaCode")) return; //不包含区域编号列直接跳出
            CurrencyManager currency = (CurrencyManager)BindingContext![dgv.DataSource];
            currency.SuspendBinding(); //挂起数据绑定
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; //禁止自动调整列宽加速显示
            if (string.IsNullOrWhiteSpace(area))
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    string? areaCode = row.Cells["AreaCode"].Value.ToString(); //区域编号
                    row.Visible = areaCode is null ? true : areaCode.Contains(area); //根据是否包含区域调整行的可见度
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

        #region 对象

        /// <summary>
        /// 选择对象后各控制控件的处理
        /// </summary>
        private void dgvObject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvObject.CurrentRow is null) || dgvObject.SelectedRows.Count == 0) return;
            btnObjToggle.Enabled = (dgvObject.SelectedRows.Count == 1); //不允许同时反转控制多个对象开关
            foreach (DataGridViewRow row in dgvObject.SelectedRows)
            {
                if (row.Cells["ValueCtlAddr"].Value is DBNull) //无数值控制组的情况
                {
                    numObjVal.Enabled = false;
                    numObjVal.Value = 0;
                }
                else
                {
                    numObjVal.Enabled = true;
                    //此处应显示数值
                }
            }
        }

        /// <summary>
        /// 组地址读取
        /// </summary>
        private void dgvObject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvObject.CurrentRow is null) || dgvObject.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvObject.SelectedRows)
            {
                int objId = (int)row.Cells["Id"].Value;
                knx.ReadObjectFeedback(objId);
            }
        }

        /// <summary>
        /// KNX对象开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        private void ObjSwitch(bool? value = null)
        {
            if ((dgvObject.CurrentRow is null) || dgvObject.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvObject.SelectedRows)
            {
                int objId = (int)row.Cells["Id"].Value;
                //knx.Objects[objId].Switch(value);
            }
        }

        /// <summary>
        /// KNX对象-开
        /// </summary>
        private void btnObjOn_Click(object sender, EventArgs e)
        {
            ObjSwitch(true);
        }

        /// <summary>
        /// KNX对象-关
        /// </summary>
        private void btnObjOff_Click(object sender, EventArgs e)
        {
            ObjSwitch(false);
        }

        /// <summary>
        /// KNX对象-开关切换
        /// </summary>
        private void btnObjToggle_Click(object sender, EventArgs e)
        {
            ObjSwitch();
        }

        /// <summary>
        /// 显示对象的数值
        /// </summary>
        private void numObjVal_Scroll(object sender, EventArgs e)
        {
            lblObjVal.Text = numObjVal.Value.ToString();
        }

        /// <summary>
        /// 对象数值设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numObjVal_MouseUp(object sender, MouseEventArgs e)
        {
            if ((dgvObject.CurrentRow is null) || dgvObject.SelectedRows.Count == 0) return;
            int objId = (int)dgvObject.SelectedRows[0].Cells["Id"].Value;
            //knx.Objects[objId].BrightnessControl(numObjVal.Value * 2.55);
        }

        #endregion 对象

        #region 场景

        /// <summary>
        /// 场景控制按钮
        /// </summary>
        private void btnCtlScn_Click(object sender, EventArgs e)
        {
            if ((dgvScene.CurrentRow is null) || dgvScene.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvScene.SelectedRows)
            {
                int scnId = (int)row.Cells["Id"].Value; //场景ID
                KnxScene scene = knx.Objects.Scenes[scnId];
                FrmSceneCtl frmSceneCtl = new(scene);
                if (frmSceneCtl.ShowDialog() == DialogResult.OK)
                {
                    scene.WriteScene(frmSceneCtl.SelectedAddress);
                }
            }
        }

        /// <summary>
        /// 场景控制（dgv双击）
        /// </summary>
        private void dgvScene_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCtlScn.PerformClick(); //调用场景控制按钮事件
        }

        #endregion 场景

        #region 设备

        /// <summary>
        /// 设备状态检测
        /// </summary>
        private void dgvDevice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dgvDevice.CurrentRow is null) || dgvDevice.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dgvDevice.SelectedRows)
            {
                int devId = (int)row.Cells["Id"].Value; //设备ID
                knx.DeviceCheck(devId);
            }
        }

        /// <summary>
        /// 检测全部设备状态
        /// </summary>
        private void btnDevicePoll_Click(object sender, EventArgs e)
        {
            knx.DevicePoll(string.Empty);
        }

        #endregion 设备

        #region 日志

        /// <summary>
        /// 导出日志
        /// </summary>
        private void btnTelLogExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new()
                {
                    Title = Resources.Strings.Dlg_SaveMsgLog,
                    InitialDirectory = Application.StartupPath,
                    Filter = "CSV File(*.csv)|*.csv",
                    FileName = $"KnxMessageLog_{DateTime.Now:yyyyMMddHHmmss}.csv",
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    LogUtility.WriteCsvLog(knx.MessageLog, sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        private void btnTelLogClear_Click(object sender, EventArgs e)
        {
            lstTelLog.Items.Clear();
        }

        #endregion 日志

        /// <summary>
        /// 展示定时队列
        /// </summary>
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            new FrmScheduleSeq().Show();
        }

        /// <summary>
        /// 打开接口窗体
        /// </summary>
        private void btnInterface_Click(object sender, EventArgs e)
        {
            new FrmInterface().Show();
        }

        /// <summary>
        /// 打开链接窗体
        /// </summary>
        private void btnLink_Click(object sender, EventArgs e)
        {
            new FrmLink().Show();
        }

    }

}
