using Knx.Falcon;

namespace BedivereKnx.KnxSystem
{

    public class KnxDeviceInfo
    {

        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        protected internal KnxDeviceStateHandler? DeviceStateChanged;

        /// <summary>
        /// 接口ID
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string Code { get; internal set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string? Model { get; internal set; }

        /// <summary>
        /// 物理地址
        /// </summary>
        public IndividualAddress IndividualAddress { get; internal set; }

        /// <summary>
        /// 接口编号
        /// </summary>
        public string? InterfaceCode { get; internal set; }

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
        private KnxDeviceState state;

        public KnxDeviceInfo(int id, string code, string? name, string? model, IndividualAddress individualAddress, string? ifCode)
        {
            Id = id;
            Code = code;
            Name = name;
            Model = model;
            IndividualAddress = individualAddress;
            InterfaceCode = ifCode;
        }

    }

}
