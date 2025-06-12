using System.Data;
using Knx.Falcon;

namespace BedivereKnx
{

    /// <summary>
    /// KNX对象的基类
    /// </summary>
    public abstract class KnxGroupBundle
    {

        protected internal event GroupReadHandler? ReadRequest;
        protected internal event GroupWriteHandler? WriteRequest;

        /// <summary>
        /// KNX组类型
        /// </summary>
        public KnxObjectType Type { get; set; }

        /// <summary>
        /// 对象ID
        /// </summary>
        public int Id { get; private set; }

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
        /// 索引器内部字典
        /// </summary>
        private readonly Dictionary<KnxObjectPart, KnxGroup> Groups = [];

        /// <summary>
        /// 索引器（按KnxObjectPart对象）
        /// </summary>
        /// <param name="part">KnxObjectPart对象</param>
        /// <returns></returns>
        public KnxGroup this[KnxObjectPart part]
        {
            get
            {
                if (Groups.TryGetValue(part, out var group))
                {
                    return group;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(ResString.ExMsg_KnxObjectPartNotFound, part, Name));
                }
            }
            set
            {
                if (!Groups.TryAdd(part, value))
                {
                    Groups[part] = value;
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

        public KnxGroupBundle(KnxObjectType type, int id, string code, string? name, string? ifCode)
        {
            if (code is null)
            {
                throw new NoNullAllowedException();
            }
            else
            {
                Type = type;
                Id = id;
                Code = code;
                Name = name;
                InterfaceCode = ifCode;
            }
        }

        /// <summary>
        /// 检查对象中是否包含部件
        /// </summary>
        /// <param name="part">部件枚举</param>
        /// <returns></returns>
        public bool ContainsPart(KnxObjectPart part)
        {
            return Groups.ContainsKey(part);
        }

        /// <summary>
        /// 尝试根据部件获取KNX组
        /// </summary>
        /// <param name="part"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool TryGetGroup(KnxObjectPart part, out KnxGroup? group)
        {
            return Groups.TryGetValue(part, out group);
        }

        /// <summary>
        /// 尝试添加新部件
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(KnxObjectPart part, KnxGroup value)
        {
            return Groups.TryAdd(part, value);
        }

        /// <summary>
        /// 读KNX组
        /// </summary>
        /// <param name="part">部件</param>
        /// <param name="priority">优先级</param>
        public void ReadValue(KnxObjectPart part, MessagePriority priority = MessagePriority.Low)
        {
            KnxGroupEventArgs gra = new(InterfaceCode, Groups[part].Address, priority);
            ReadRequest?.Invoke(gra);
        }

        /// <summary>
        /// 写入KNX组
        /// </summary>
        /// <param name="part">部件</param>
        /// <param name="value">值</param>
        /// <param name="priority">优先级</param>
        public void WriteValue(KnxObjectPart part, object value, MessagePriority priority = MessagePriority.Low)
        {
            GroupValue? val = Groups[part].ToGroupValue(value);
            if (val != null)
            {
                KnxGroupEventArgs gwa = new(InterfaceCode, Groups[part].Address, priority);
                WriteRequest?.Invoke(gwa, val);
            }
        }

    }

}
