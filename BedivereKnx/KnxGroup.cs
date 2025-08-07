using System.ComponentModel;
using Knx.Falcon;
using Knx.Falcon.ApplicationData;
using Knx.Falcon.ApplicationData.DatapointTypes;

namespace BedivereKnx
{

    public class KnxGroup// : INotifyPropertyChanged
    {

        public event ValueChangeHandler<GroupValue>? GroupValueChanged;
        //public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 数据类型
        /// </summary>
        public DptBase DPT { get; set; }

        /// <summary>
        /// 组地址
        /// </summary>
        public GroupAddress Address { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public GroupValue? Value
        {
            get => _value;
            set
            {
                //if ((_value == null) || (!_value.Equals(value)))
                if (!EqualityComparer<GroupValue>.Default.Equals(_value, value)) //写入值变化的情况
                {
                    _value = value;
                    GroupValueChanged?.Invoke(value); //触发值变化事件
                    //PropertyChanged?.Invoke(this, new(nameof(Value)));
                    //PropertyChanged?.Invoke(this, new("ToString"));
                }
            }
        }
        private GroupValue? _value;

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="dpt">DPT类型数字的数组</param>
        /// <exception cref="ArgumentException"></exception>
        public KnxGroup(int[] dpt)
        {
            switch (dpt.Length)
            {
                case 0:
                    dpt = [1, -1]; //输入空白数组的情况，设置为1.xxx
                    break;
                case 1:
                    Array.Resize(ref dpt, 2);
                    dpt[1] = -1;
                    break;
            }
            DPT = DptFactory.Default.Get(dpt[0], dpt[1]);
            if (DPT is null)
            {
                throw new ArgumentException(string.Format(ResString.ExMsg_KnxDptInvalid, @$"{dpt[0]}.{dpt[1].ToString().PadLeft(3, '0')}"));
            }
        }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="dptMain">DPT主类型</param>
        /// <param name="dptSub">DPT子类型</param>
        public KnxGroup(int dptMain = 1, int dptSub = -1)
            : this([dptMain, dptSub])
        {
            //DPT = DptFactory.Default.Get(dptMain, dptSub);
            //if (DPT is null)
            //{
            //    throw new ArgumentException(string.Format(Localization.GetString("ExMsg.KnxDptInvalid"), @$"{dptMain}.{dptSub.ToString().PadLeft(3, '0')}"));
            //}
        }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="address">组地址</param>
        /// <param name="dptMain">DPT主类型</param>
        /// <param name="dptSub">DPT子类型</param>
        public KnxGroup(GroupAddress address, int dptMain = 1, int dptSub = -1)
            : this(dptMain, dptSub)
        {
            Address = address;
        }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="address">组地址</param>
        /// <param name="dpt">{DPT主类型，DPT子类型}，子类型可以省略</param>
        public KnxGroup(GroupAddress address, int[] dpt)
            : this(dpt)
        {
            Address = address;
        }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="addressString">组地址字符串</param>
        /// <param name="dpt">{DPT主类型，DPT子类型}，子类型可以省略</param>
        public KnxGroup(string addressString, int[] dpt)
            : this(dpt)
        {
            if (GroupAddress.TryParse(addressString, out GroupAddress ga))
            {
                Address = ga;
            }
        }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="address">组地址</param>
        /// <param name="dptString">DPT类型字符串</param>
        public KnxGroup(GroupAddress address, string? dptString)
            : this(address, DptIdFromString(dptString))
        { }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="addressString">组地址字符串</param>
        /// <param name="dptMain">DPT主类型</param>
        /// <param name="dptSub">DPT子类型</param>

        public KnxGroup(string addressString, int dptMain = 1, int dptSub = -1)
            : this(addressString, [dptMain, dptSub])
        { }

        /// <summary>
        /// 新建KNX组
        /// </summary>
        /// <param name="addressString">组地址字符串</param>
        /// <param name="dptString">DPT类型字符串</param>
        public KnxGroup(string addressString, string? dptString)
            : this(addressString, DptIdFromString(dptString))
        { }

        /// <summary>
        /// 获取变量值的GroupValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GroupValue? ToGroupValue(object value)
        {
            if (value is null) return null;
            //【优化】提前判断数值为什么类型
            switch (DPT.MainNumber)
            {
                case 1: //开关量
                    value = (Convert.ToInt32(value) != 0);
                    break;
                case 2: //优先级控制（bit0-值，bit1-优先级）
                    if (byte.TryParse(value.ToString(), out byte b2))
                    {
                        value = new Knx1BitControlled((b2 & 0b10) != 0, (b2 & 0b1) != 0);
                    }
                    else
                    {
                        value = new Knx1BitControlled();//(false, false);
                    }
                    break;
                case 3: //调光（bit0~2-步，bit3-方向）
                    if (byte.TryParse(value.ToString(), out byte b3))
                    {
                        value = new Knx3BitControlled((b3 >> 3) == 1, (byte)(b3 & 0b111));
                    }
                    else
                    {
                        value = new Knx3BitControlled();
                    }
                    break;
                case 18: //场景控制（bit0~5-场景值，bit6-保留值[忽略]，bit7-是否学习）
                    if (byte.TryParse(value.ToString(), out byte b18))
                    {
                        value = new KnxSceneControl((b18 >> 7) == 1, (byte)(b18 & 0b111111));
                    }
                    else
                    {
                        value = new KnxSceneControl();
                    }
                    break;
                case 26: //场景信息（bit0~6-场景值，bit7-是否激活）
                    if (byte.TryParse(value.ToString(), out byte b26))
                    {
                        value = new KnxSceneInfo((b26 & 0b1000000) == 1, (byte)(b26 & 0b111111));
                    }
                    break;
            }
            return DPT.ToGroupValue(value);
        }

        /// <summary>
        /// DPT字符串转数字数组
        /// </summary>
        /// <param name="dptString">DPT字符串</param>
        /// <returns></returns>
        private static int[] DptIdFromString(string? dptString)
        {
            if (string.IsNullOrWhiteSpace(dptString)) return [1, -1]; //空白字符串返回1.xxx
            string[] dptArry = dptString.Split(':')[0].Split('.'); //DPT数字
            if (dptArry.Length == 0) return [1, -1]; //空白字符串返回1.xxx
            int dptMain = 1;
            int dptSub = -1;
            if (int.TryParse(dptArry[0], out dptMain))
            {
                dptMain = Math.Abs(dptMain); //去除负号
            }
            if (dptArry.Length > 1 && int.TryParse(dptArry[1], out dptSub))
            {
                dptSub = Math.Abs(dptSub); //去除负号
            }
            return [dptMain, dptSub];
        }

        public override string ToString()
        {
            return Value is null ? string.Empty : Value.ToString();
        }

    }

}
