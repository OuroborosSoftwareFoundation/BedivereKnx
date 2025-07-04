using Ouroboros.Hmi.Library.Mapping;
using Knx.Falcon;
using Knx.Falcon.ApplicationData.DatapointTypes;

namespace BedivereKnx.GUI
{

    internal class KnxHmiMapping : HmiMappingBase<GroupValue>
    {

        internal KnxHmiMapping(GroupValue[] values, HmiValueChangeType changeType)
            : base(values, changeType)
        { }

        /// <summary>
        /// 这种方式新建的对象只有RawValues属性被赋值，Values属性为空
        /// </summary>
        /// <param name="valsString"></param>
        internal KnxHmiMapping(string valsString)
            : base(valsString)
        { }

        internal KnxHmiMapping(string valsString, DptBase dpt)
            : base(valsString)
        {
            List<GroupValue> values = [];
            foreach (string valStr in RawValues)
            {
                values.Add(dpt.ToGroupValue(Convert.ToDecimal(valStr)));
            }
            Values = values.ToArray();
        }

    }

}
