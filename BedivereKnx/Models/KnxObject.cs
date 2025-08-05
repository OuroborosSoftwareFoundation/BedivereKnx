using System.Collections;
using System.Data;
using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX对象的基类
    /// </summary>
    public abstract class KnxObject : IEnumerable<KnxGroup>
    {

        /// <summary>
        /// 值读取请求
        /// </summary>
        protected internal event GroupReadHandler? ReadRequest;

        /// <summary>
        /// 值写入请求
        /// </summary>
        protected internal event GroupWriteHandler? WriteRequest;

        /// <summary>
        /// 索引器内部字典
        /// </summary>
        protected readonly Dictionary<KnxObjectPart, KnxGroup> groups = [];

        /// <summary>
        /// 对象ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// KNX组类型
        /// </summary>
        public KnxObjectType Type { get; set; }

        /// <summary>
        /// 对象编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 接口编号，留空使用IpRouting
        /// </summary>
        public string? InterfaceCode { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// 索引器（按KnxObjectPart对象）
        /// </summary>
        /// <param name="part">KnxObjectPart对象</param>
        /// <returns></returns>
        public KnxGroup this[KnxObjectPart part]
        {
            get
            {
                if (groups.TryGetValue(part, out var group))
                {
                    return group;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KnxObjectPartNotFound, part, Code));
                }
            }
            set
            {
                if (!groups.TryAdd(part, value))
                {
                    groups[part] = value;
                }
            }
        }

        /// <summary>
        /// 索引器（按KnxObjectPart字符串）
        /// </summary>
        /// <param name="partString"></param>
        /// <returns></returns>
        public KnxGroup this[string partString]
        {
            get
            {
                KnxObjectPart part = KnxGroupFactory.GetKnxObjectPart(partString);
                return this[part];
            }
            set
            {
                KnxObjectPart part = KnxGroupFactory.GetKnxObjectPart(partString);
                this[part] = value;
            }
        }

        //public KnxGroupBundle(KnxObjectType type, int id, string? ifCode)
        //{
        //    Type = type;
        //    Id = id;
        //    InterfaceCode = ifCode;
        //}

        public KnxObject(KnxObjectType type, int id, string code, string? name, string? ifCode, string? areaCode)
        {
            if (code is null)
            {
                throw new NoNullAllowedException(string.Format(ResString.ExMsg_NoNullAllowed, "ObjectCode", $"ID={id}"));
            }
            else
            {
                Type = type;
                Id = id;
                Code = code;
                Name = name;
                InterfaceCode = ifCode;
                AreaCode = areaCode;
            }
        }

        public static KnxObject NewObject(KnxObjectType type, int id, string code, string? name, string? ifCode, string? areaCode)
        {
            return type switch
            {
                KnxObjectType.Light => new KnxLight(id, code, name, ifCode, areaCode),
                KnxObjectType.Scene => new KnxScene(id, code, name, ifCode, areaCode),
                KnxObjectType.Enablement => new KnxEnablement(id, code, name, ifCode, areaCode),
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// 检查对象中是否包含部件
        /// </summary>
        /// <param name="part">部件枚举</param>
        /// <returns></returns>
        public bool ContainsPart(KnxObjectPart part)
        {
            return groups.ContainsKey(part);
        }

        /// <summary>
        /// 尝试根据部件获取KNX组
        /// </summary>
        /// <param name="part"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool TryGetGroup(KnxObjectPart part, out KnxGroup? group)
        {
            return groups.TryGetValue(part, out group);
        }

        /// <summary>
        /// 尝试添加新部件
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(KnxObjectPart part, KnxGroup value)
        {
            return groups.TryAdd(part, value);
        }

        /// <summary>
        /// 读KNX组
        /// </summary>
        /// <param name="part">部件</param>
        /// <param name="priority">优先级</param>
        public void ReadValue(KnxObjectPart part, MessagePriority priority = MessagePriority.Low)
        {
            KnxGroupEventArgs gra = new(InterfaceCode, groups[part].Address, priority);
            ReadRequest?.Invoke(gra);
        }

        /// <summary>
        /// 写入KNX组
        /// </summary>
        /// <param name="part">部件</param>
        /// <param name="value">值</param>
        /// <param name="priority">优先级</param>
        protected void WriteValue(KnxObjectPart part, object value, MessagePriority priority = MessagePriority.Low)
        {
            if (groups.TryGetValue(part, out KnxGroup? group))
            {
                GroupValue? val = group.ToGroupValue(value);
                if (val != null)
                {
                    groups[part].Value= val;
                    KnxGroupEventArgs gwa = new(InterfaceCode, groups[part].Address, priority);
                    WriteRequest?.Invoke(gwa, val);
                }
            }
        }

        /// <summary>
        /// 开关控制
        /// </summary>
        /// <param name="value"></param>
        protected void SwitchControl(bool? value = null)
        {
            if (!this.ContainsPart(KnxObjectPart.SwitchControl)) return; //不包含开关控制部分，跳过
            if (this[KnxObjectPart.SwitchControl].DPT.MainNumber != 1) return;
            bool ctlValue; //实际控制值
            if (value is null) //控制值为空，切换开关状态
            {
                GroupValue? fdbValue;
                if (this.ContainsPart(KnxObjectPart.SwitchFeedback)) //存在开关反馈的情况
                {
                    if (this[KnxObjectPart.SwitchFeedback].DPT.MainNumber != 1) return;
                    fdbValue = this[KnxObjectPart.SwitchFeedback].Value;
                }
                else //不存在开关反馈的情况，用开关控制做反馈
                {
                    fdbValue = this[KnxObjectPart.SwitchControl].Value;
                }
                if (fdbValue is null) //反馈为空，说明开关状态不确定
                {
                    ctlValue = true; //默认为开
                }
                else
                {
                    ctlValue = fdbValue.Equals(new GroupValue(false)); //翻转反馈值
                }
            }
            else
            {
                ctlValue = (bool)value;
            }
            WriteValue(KnxObjectPart.SwitchControl, ctlValue);
        }

        /// <summary>
        /// 数值控制
        /// </summary>
        /// <param name="value"></param>
        protected void ValueControl(object value)
        {
            WriteValue(KnxObjectPart.ValueControl, value);
        }

        public IEnumerator<KnxGroup> GetEnumerator()
        {
            return groups.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
