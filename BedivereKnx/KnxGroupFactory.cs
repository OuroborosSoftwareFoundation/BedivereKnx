using System.ComponentModel;

namespace BedivereKnx
{

    /// <summary>
    /// KNX组的工厂类
    /// </summary>
    internal static class KnxGroupFactory
    {

        /// <summary>
        /// 获取KNX对象部件
        /// </summary>
        /// <param name="partString"></param>
        /// <returns></returns>
        internal static KnxObjectPart GetKnxObjectPart(string partString)
        {
            partString = partString.Trim().ToLower(); //去掉首位空格并转为小写
            //开关
            if (partString.Contains("sw"))
            {
                //控制
                if (partString.Contains("ctl") || partString.Contains("control"))
                {
                    return KnxObjectPart.SwitchControl;
                }
                //反馈
                else if (partString.Contains("fdb") || partString.Contains("feedback"))
                {
                    return KnxObjectPart.SwitchFeedback;
                }
                //其他情况报错
                else
                {
                    throw new InvalidEnumArgumentException(string.Format(ResString.ExMsg_KnxObjectPartInvalid, partString));
                }
            }
            //数值
            else if (partString.Contains("val"))
            {
                //控制
                if (partString.Contains("ctl") || partString.Contains("control"))
                {
                    return KnxObjectPart.ValueControl;
                }
                //反馈
                else if (partString.Contains("fdb") || partString.Contains("feedback"))
                {
                    return KnxObjectPart.ValueFeedback;
                }
                //其他情况报错
                else
                {
                    throw new InvalidEnumArgumentException(string.Format(ResString.ExMsg_KnxObjectPartInvalid, partString));
                }
            }
            //调光
            else if (partString.Contains("dim"))
            {
                return KnxObjectPart.DimmingControl;
            }
            //场景
            else if (partString.Contains("scn") || partString.Contains("scene"))
            {
                return KnxObjectPart.SceneControl;
            }
            //其他情况报错
            else
            {
                throw new InvalidEnumArgumentException(string.Format(ResString.ExMsg_KnxObjectPartInvalid, partString));
            }
        }

    }

}
