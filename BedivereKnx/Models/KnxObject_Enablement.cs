using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX使能对象
    /// </summary>
    public class KnxEnablement : KnxObject
    {

        public KnxEnablementType EnablementType { get; private set; } = KnxEnablementType.Enable;
        
        /// <summary>
        /// 使能反馈
        /// </summary>
        public GroupValue? EnableFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.SwitchControl, out KnxGroup? group);
                return group?.Value;
            }
        }

        /// <summary>
        /// 新建KNX使能对象
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="code">对象编号</param>
        /// <param name="name">对象名称</param>
        /// <param name="ifCode">接口编号</param>
        /// <param name="areaCode">区域编号</param>
        public KnxEnablement(int id, string code, string? name, string? ifCode, string? areaCode)
            : base(KnxObjectType.Enablement, id, code, name, ifCode, areaCode)
        {
            if (code.EndsWith("En") || code.EndsWith("Enable"))
            {
                EnablementType = KnxEnablementType.Enable;
            }
            else if (code.EndsWith("Lock"))
            {
                EnablementType = KnxEnablementType.Lock;
            }
            else
            {
                EnablementType = KnxEnablementType.General;
            }
        }

        /// <summary>
        /// 使能开关控制
        /// </summary>
        /// <param name="value">控制参数，null-切换，true-开，false-关</param>
        public void EnableControl(bool? value = null)
        {
            base.SwitchControl(value);
        }

    }

}
