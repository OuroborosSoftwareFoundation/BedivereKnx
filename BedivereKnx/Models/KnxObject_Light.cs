using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX灯光对象
    /// </summary>
    public class KnxLight : KnxObject
    {

        /// <summary>
        /// 可调光
        /// </summary>
        public bool Dimmable { get; private set; }

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
        /// 亮度反馈
        /// </summary>
        public GroupValue? BrightnessFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.ValueFeedback, out KnxGroup? group);
                return group?.Value;
            }
        }

        /// <summary>
        /// 新建KNX灯光对象
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="code">对象编号</param>
        /// <param name="name">对象名称</param>
        /// <param name="ifCode">接口编号</param>
        /// <param name="areaCode">区域编号</param>
        public KnxLight(int id, string code, string? name, string? ifCode, string? areaCode)
            : base(KnxObjectType.Light, id, code, name, ifCode, areaCode)
        {
            Dimmable = groups.ContainsKey(KnxObjectPart.ValueControl)
                       && groups.ContainsKey(KnxObjectPart.ValueFeedback);
        }

        /// <summary>
        /// 灯光开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        public new void SwitchControl(bool? value = null)
        {
            base.SwitchControl(value);
        }

        /// <summary>
        /// 灯光亮度控制
        /// </summary>
        /// <param name="value">亮度值（0~100）</param>
        public void BrightnessControl(int value)
        {
            base.WriteValue(KnxObjectPart.ValueControl, value * 2.55);
        }

    }

}
