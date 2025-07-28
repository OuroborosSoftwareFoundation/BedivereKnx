using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX数值对象
    /// </summary>
    public class KnxValue : KnxObject
    {

        /// <summary>
        /// 开关反馈
        /// </summary>
        public GroupValue? SwitchFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.SwitchFeedback, out KnxGroup? group);
                return group?.Value;
            }
        }

        /// <summary>
        /// 数值反馈
        /// </summary>
        public GroupValue? ValueFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.ValueFeedback, out KnxGroup? group);
                return group?.Value;
            }
        }

        /// <summary>
        /// 新建KNX数值对象
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="code">对象编号</param>
        /// <param name="name">对象名称</param>
        /// <param name="ifCode">接口编号</param>
        /// <param name="areaCode">区域编号</param>
        public KnxValue(int id, string code, string? name, string? ifCode, string? areaCode)
            : base(KnxObjectType.Value, id, code, name, ifCode, areaCode)
        { }

        /// <summary>
        /// 数值开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        public new void SwitchControl(bool? value = null)
        {
            base.SwitchControl(value);
        }

        /// <summary>
        /// 数值控制
        /// </summary>
        /// <param name="value">亮度值（0~100）</param>
        public new void ValueControl(object value)
        {
            base.ValueControl(value);
        }

    }

}
