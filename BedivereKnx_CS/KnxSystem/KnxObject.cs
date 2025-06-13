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
        /// 切换开关状态
        /// </summary>
        public void SwitchToggle()
        {
            if (this[KnxObjectPart.SwitchControl].DPT.MainNumber != 1) return;
            if (this[KnxObjectPart.SwitchFeedback].DPT.MainNumber != 1) return;
            GroupValue? fdb = this[KnxObjectPart.SwitchFeedback].Value;
            if (fdb is null) //反馈为空，说明开关状态不确定
            {
                WriteValue(KnxObjectPart.SwitchControl, true); //默认为开
            }
            else
            {
                bool setVal = fdb.TypedValue.Equals(false); //翻转反馈值
                WriteValue(KnxObjectPart.SwitchControl, setVal);
            }
        }

    }

}
