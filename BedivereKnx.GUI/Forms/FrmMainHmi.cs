using BedivereKnx.GUI.Controls;
using Knx.Falcon;
using Ouroboros.Hmi.Library;
using Ouroboros.Hmi.Library.Elements;
using Ouroboros.Hmi.Library.Mapping;

namespace BedivereKnx.GUI.Forms
{

    public partial class FrmMainHmi : Form
    {

        private readonly KnxSystem knx = Globals.KnxSys!;
        private readonly Dictionary<string, HmiPage> hmi = []; //HMI文件数据字典
        private readonly Dictionary<GroupAddress, List<KnxHmiDigitalFdb>>? fdbAddresses = []; //当前页面反馈组地址对应的控件

        public FrmMainHmi(string hmiPath)
        {
            InitializeComponent();
            Text = Path.GetFileNameWithoutExtension(hmiPath); //文件名设为标题
            hmi = HmiFile.FromDrawio(hmiPath); //从文件读取到的HMI数据
            CompMappingPlug(); //补充数据表绑定方式
            foreach (string key in hmi.Keys)
            {
                tvHmi.Nodes.Add(key, key);
            }
            knx.MessageTransmission += MessageTransmission;
        }

        private void FrmMainHmi_Load(object sender, EventArgs e)
        {
            tvHmi.SelectedNode = tvHmi.Nodes[0]; //选择第一个页面
                                                 //ShowPage(tvHmi.SelectedNode.Name); //显示页面
            WindowState = FormWindowState.Maximized;
        }

        private void CompMappingPlug()
        {
            //[需重写】
            //foreach (HmiPage page in hmi.Values) //遍历HMI页面
            //{
            //    foreach (KnxHmiComponent comp in page.Elements.OfType<KnxHmiComponent>()) //遍历页面中的控件
            //    {
            //        if (comp.MappingMode != HmiMappingMode.DataTable) continue; //只处理数据表绑定的控件
            //        if (comp.Text.Contains('*')) //绑定到对象的情况
            //        {
            //            string[] texts = comp.Text.Split('*'); //{文本}*{绑定信息}
            //            bool isSwitch = true; //是-开关，否-数值
            //            comp.Text = texts[0]; //去除绑定信息之后的字符串作为控件文本使用
            //            string mapText = texts[1]; //绑定信息
            //            if (mapText.Contains('#')) //有#代表定义了组地址类型
            //            {
            //                string[] gvArry = mapText.Split('#'); //分割数值和数据类型
            //                string[] dpt = gvArry.Last().Split('.'); //分割DPT和DPST
            //                if (Convert.ToInt32(dpt[0]) > 2) isSwitch = false;
            //                mapText = gvArry[0];
            //            }

            //            string objCode = mapText.Split(['=', '@'])[0]; //对象编号
            //            KnxObjectPart objPart = KnxObjectPart.None;
            //            switch (comp.Direction)
            //            {
            //                case HmiComponentDirection.Control:
            //                    objPart = isSwitch ? KnxObjectPart.SwitchControl : KnxObjectPart.ValueControl;
            //                    break;
            //                case HmiComponentDirection.Feedback:
            //                    objPart = isSwitch ? KnxObjectPart.SwitchFeedback : KnxObjectPart.DimmingControl;
            //                    break;
            //            }
            //            comp.ObjectId = knx.Objects[objCode][0].Id;
            //            comp.Group = knx.Objects[objCode][0][objPart];
            //            comp.Group.GroupValueChanged += GroupValueChanged;

            //            List<GroupValue> vals = [];
            //            foreach (string v in comp.Mapping.RawValues)
            //            {
            //                object val = isSwitch ? (v == "1") : v;
            //                vals.Add(comp.Group.DPT.ToGroupValue(val));
            //            }
            //            comp.Mapping.Values = vals.ToArray();
            //        }
            //        else if (comp.Text.Contains('$')) //绑定到场景的情况
            //        {
            //            string[] texts = comp.Text.Split('$'); //{文本}${绑定信息}
            //            comp.Text = texts[0]; //去除绑定信息之后的字符串作为控件文本使用
            //            string mapText = texts[1]; //绑定信息
            //            string scnCode = mapText.Split('=')[0]; //场景编号
            //            comp.Group = knx.Objects.Scenes[scnCode][0][KnxObjectPart.SceneControl];
            //            comp.Group.GroupValueChanged += GroupValueChanged;

            //            List<GroupValue> vals = [];
            //            foreach (string v in comp.Mapping.RawValues)
            //            {
            //                vals.Add(new GroupValue(Convert.ToByte(v)));
            //            }
            //            comp.Mapping.Values = vals.ToArray();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 页面选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvHmi_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null) return;
            ShowPage(e.Node.Name);
        }
        private void pnlHmi_SizeChanged(object sender, EventArgs e)
        {
            ShowPage(tvHmi.SelectedNode.Name);
        }

        /// <summary>
        /// 显示页面
        /// </summary>
        /// <param name="pageName"></param>
        private void ShowPage(string pageName)
        {
            if (hmi[pageName] is null) return;
            HmiPage page = hmi[pageName]; //页面对象
            pnlHmi.Controls.Clear(); //清理Panel的控件
            pnlHmi.Refresh(); //刷新panel
            pnlHmi.BackColor = page.BackColor; //设置背景颜色
            DrawBackImage(pageName); //绘制背景图片
            foreach (KnxHmiComponent comp in page.Elements.OfType<KnxHmiComponent>())
            {
                switch (comp.Direction) //根据控制-反馈新建不同的控件
                {
                    case HmiComponentDirection.Control:
                        if (comp.MappingMode == HmiMappingMode.DataTable) //绑定到数据表的控件
                        {
                            KnxHmiDigitalGroup ctl = new(comp)
                            {
                                KnxObject = knx.Objects[comp.ObjectId],
                            };
                            pnlHmi.Controls.Add(ctl); //把控件加入到窗体
                        }
                        else if (comp.MappingMode == HmiMappingMode.Address) //绑定到组地址的控件
                        {
                            KnxHmiButton btn = new(comp);
                            btn.HmiWriteValue += HmiGroupValueWrite; //绑定组地址写入事件
                            pnlHmi.Controls.Add(btn); //把控件加入到窗体
                        }
                        break;
                    case HmiComponentDirection.Feedback:
                        KnxHmiDigitalFdb fdb = new(comp);
                        pnlHmi.Controls.Add(fdb); //把控件加到窗体
                        GroupAddress gaF = fdb.knxGroup.Address; //反馈控件的组地址
                        fdbAddresses!.TryAdd(gaF, []); //不存在组地址所属控件时添加组地址
                        fdbAddresses[gaF].Add(fdb); //把控件加入字典中
                        break;
                }
            }
            foreach (HmiTextElement comp in page.Elements.OfType<HmiTextElement>())
            {
                //暂不支持文本
            }
            pnlHmi.Controls.Add(btnLeftHide);
            btnLeftHide.Height = pnlHmi.Height;
            PollPageGroupValue(); //读取当前页面的反馈值
        }

        /// <summary>
        /// 绘制背景图片
        /// </summary>
        /// <param name="pageName"></param>
        private void DrawBackImage(string pageName)
        {
            if (hmi[pageName] is null) return;
            foreach (HmiImageElement img in hmi[pageName].BackImages)
            {
                Graphics graphics = pnlHmi.CreateGraphics();
                graphics.DrawImage(img.Image, new Rectangle(img.RawLocation, img.RawSize));
            }
        }

        /// <summary>
        /// 侧边栏按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeftHide_Click(object sender, EventArgs e)
        {
            SplitContainer1.Panel1Collapsed = !SplitContainer1.Panel1Collapsed;
        }

        /// <summary>
        /// 组地址写入事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        private void HmiGroupValueWrite(KnxGroupEventArgs e, GroupValue value)
        {
            knx.WriteGroupAddress(e.InterfaceCode, e.GroupAddress, value, e.Priority);
        }

        /// <summary>
        /// 读取当前页面的反馈值（有问题，同样组地址的控件如果有多个，会多次读取）
        /// </summary>
        private void PollPageGroupValue()
        {
            //foreach (KnxHmiDigitalFdb fdb in pnlHmi.Controls.OfType<KnxHmiDigitalFdb>())
            //{
            //    knx.ReadGroupAddress(string.Empty, fdb.knxGroup.Address);
            //}
        }

        /// <summary>
        /// KNX报文传输事件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="log"></param>
        private void MessageTransmission(KnxMsgEventArgs e, string? log)
        {
            if (e.Value is null) return;
            if (e.MessageType != KnxMessageType.FromBus) return; //只接收来自总线的报文
                                                                 //GroupAddress ga = e.DestinationAddress;

            ////List<KnxHmiDigitalFdb>? list = [];
            //if (fdbAddresses!.TryGetValue(ga, out var list)) //查找带有报文组地址的控件列表
            //{
            //    foreach (var fdb in list) //遍历这些控件列表
            //    {
            //        fdb. //给控件设置值，实现反馈效果
            //    }
            //}
        }

        /// <summary>
        /// KnxGroup值变化事件
        /// </summary>
        /// <param name="value"></param>
        private void GroupValueChanged(GroupValue? value)
        {

        }

    }

}
