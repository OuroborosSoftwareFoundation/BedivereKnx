using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX设备信息
    /// </summary>
    public class KnxDeviceInfo
    {

        /// <summary>
        /// 设备状态变化事件
        /// </summary>
        protected internal KnxDeviceStateHandler? DeviceStateChanged;

        /// <summary>
        /// 接口ID
        /// </summary>
        public int Id { get; }

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
        /// 区域编号
        /// </summary>
        public string? AreaCode { get; internal set; }

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

        /// <summary>
        /// 新建KNX设备信息对象
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <param name="code">设备编号</param>
        /// <param name="name">设备名称</param>
        /// <param name="model">设备型号</param>
        /// <param name="indAddress">物理地址</param>
        /// <param name="ifCode">接口编号</param>
        /// <param name="areaCode">区域编号</param>
        public KnxDeviceInfo(int id, string code, string? name, string? model, IndividualAddress indAddress, string? ifCode, string? areaCode)
        {
            Id = id;
            Code = code;
            Name = name;
            Model = model;
            IndividualAddress = indAddress;
            InterfaceCode = ifCode;
            AreaCode = areaCode;
        }

    }

}
