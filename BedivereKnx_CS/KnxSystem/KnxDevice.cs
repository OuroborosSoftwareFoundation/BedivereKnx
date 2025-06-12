using Knx.Falcon;

namespace BedivereKnx.KnxSystem
{

    public class KnxDeviceInfo(int id, string code, string? name, IndividualAddress individualAddress, string? ifCode)
    {

        private KnxDeviceState state;

        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        protected internal KnxDeviceStateHandler? DeviceStateChanged;

        /// <summary>
        /// 接口ID
        /// </summary>
        public int Id { get; internal set; } = id;

        /// <summary>
        /// 设备编号
        /// </summary>
        public string Code { get; internal set; } = code;

        /// <summary>
        /// 接口名称
        /// </summary>
        public string? Name { get; internal set; } = name;

        /// <summary>
        /// 物理地址
        /// </summary>
        public IndividualAddress IndividualAddress { get; internal set; } = individualAddress;

        /// <summary>
        /// 接口编号
        /// </summary>
        public string? InterfaceCode { get; internal set; } = ifCode;

        /// <summary>
        /// 设备状态
        /// </summary>
        public KnxDeviceState State
        {
            get => state;
            set
            {
                if (value != state)
                {
                    state = value;
                    DeviceStateChanged?.Invoke(this, state);
                }
            }
        }

    }

}
