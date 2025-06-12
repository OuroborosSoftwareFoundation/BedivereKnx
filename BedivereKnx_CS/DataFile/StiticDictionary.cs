using Knx.Falcon;
using Knx.Falcon.Configuration;

namespace BedivereKnx.DataFile
{

    internal static class StaticDict
    {

        internal static Dictionary<string, Type> NullStringCols = new()
        {
            { "Enable", typeof(bool) },
            { "Port", typeof(int) },
            //{ "IndividualAddress",typeof(IndividualAddress) },
        };

        /// <summary>
        /// 数据表中列的类型
        /// </summary>
        internal static Dictionary<string, Type> ColumnType = new()
        {
            { "Enable", typeof(bool) },
            { "Port", typeof(int) },
            { "IndividualAddress",typeof(IndividualAddress) },
            { "GroupAddress",typeof(GroupAddress) },
            { "Sw_Ctl_GrpAddr",typeof(GroupAddress) },
            { "Sw_Fdb_GrpAddr",typeof(GroupAddress) },
            { "Val_Ctl_GrpAddr",typeof(GroupAddress) },
            { "Val_Fdb_GrpAddr",typeof(GroupAddress) },

            { "InterfaceType", typeof(ConnectorType) },
            { "ObjectType", typeof(KnxObjectType) },
            //{ "TargetType", typeof(KnxObjectType) },
        };

        /// <summary>
        /// 数据表中列的枚举类型
        /// </summary>
        internal static Dictionary<string, Type> ColumnTypeEnum = new()
        {
            { "InterfaceType", typeof(ConnectorType) },
            { "ObjectType", typeof(KnxObjectType) },
            { "TargetType", typeof(KnxObjectType) },

        };


    }

}
