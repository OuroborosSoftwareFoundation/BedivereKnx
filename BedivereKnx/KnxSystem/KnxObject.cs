using Knx.Falcon;

namespace BedivereKnx.KnxSystem
{

    /// <summary>
    /// KNX对象
    /// </summary>
    public class KnxObject : KnxGroupBundle
    {

        /// <summary>
        /// 新建KNX对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象ID</param>
        /// <param name="code">对象编号</param>
        /// <param name="name">对象名称</param>
        /// <param name="ifCode">接口编号</param>
        public KnxObject(KnxObjectType type, int id, string code, string? name, string? ifCode)
            : base(type, id, code, name, ifCode)
        { }

        /// <summary>
        /// 对象开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        public void Switch(bool? value = null)
        {
            if (this[KnxObjectPart.SwitchControl].DPT.MainNumber != 1) return;
            bool valueCtl; //实际控制值
            if (value is null) //控制值为空，切换开关状态
            {
                if (this[KnxObjectPart.SwitchFeedback].DPT.MainNumber != 1) return;
                GroupValue? fdb = this[KnxObjectPart.SwitchFeedback].Value;
                if (fdb is null) //反馈为空，说明开关状态不确定
                {
                    valueCtl = true; //默认为开
                }
                else
                {
                    valueCtl = fdb.Equals(new GroupValue(false)); //翻转反馈值
                }
            }
            else
            {
                valueCtl = (bool)value;
            }
            WriteValue(KnxObjectPart.SwitchControl, valueCtl);
        }

        ///// <summary>
        ///// 对象开关控制
        ///// </summary>
        ///// <param name="ctl">控制参数，true-开，false-关</param>
        //public void Switch(bool ctl)
        //{
        //    if (this[KnxObjectPart.SwitchControl].DPT.MainNumber != 1) return;
        //    WriteValue(KnxObjectPart.SwitchControl, ctl);
        //}

        ///// <summary>
        ///// 切换开关状态
        ///// </summary>
        //public void SwitchToggle()
        //{
        //    if (this[KnxObjectPart.SwitchControl].DPT.MainNumber != 1) return;
        //    if (this[KnxObjectPart.SwitchFeedback].DPT.MainNumber != 1) return;
        //    GroupValue? fdb = this[KnxObjectPart.SwitchFeedback].Value;
        //    if (fdb is null) //反馈为空，说明开关状态不确定
        //    {
        //        WriteValue(KnxObjectPart.SwitchControl, true); //默认为开
        //    }
        //    else
        //    {
        //        bool setVal = fdb.TypedValue.Equals(false); //翻转反馈值
        //        WriteValue(KnxObjectPart.SwitchControl, setVal);
        //    }
        //}

        /// <summary>
        /// 设置对象数值
        /// </summary>
        /// <param name="value"></param>
        public void ValueSet(object value)
        {
            WriteValue(KnxObjectPart.ValueControl, value);
        }

    }

}
