using System.Text.RegularExpressions;
using Ouroboros.Hmi.Library;
using Ouroboros.Hmi.Library.Elements;
using Ouroboros.Hmi.Library.Mapping;
using Knx.Falcon;
using BedivereKnx;

namespace BedivereKnx.GUI
{

    internal class KnxHmiComponent : HmiComponentElement
    {

        private Color DEFAULTCOLOR_OFF = Color.Gray;
        private Color DEFAULTCOLOR_ON = Color.Green;
        private string DEFAULTTEXT_OFF = "OFF";
        private string DEFAULTTEXT_ON = "ON";

        /// <summary>
        /// 空白颜色字典
        /// </summary>
        private static readonly Dictionary<string, Color> dicColorEmpty = new()
        {
            ["fillColor"] = Color.Empty,
            ["strokeColor"] = Color.Empty,
            ["fontColor"] = Color.Black,
        };

        /// <summary>
        /// 默认颜色字典
        /// </summary>
        private static readonly Dictionary<string, Color> dicColorDefault = new()
        {
            ["fillColor"] = Color.White,
            ["strokeColor"] = Color.Black,
            ["fontColor"] = Color.Black,
        };


        ///// <summary>
        ///// 动态控件
        ///// </summary>
        //private bool Dynamic {  get; set; }

        /// <summary>
        /// 映射方式
        /// </summary>
        internal HmiMappingMode MappingMode { get; set; } = HmiMappingMode.None;

        /// <summary>
        /// KNX值映射对象
        /// </summary>
        internal KnxHmiMapping Mapping { get; set; }

        /// <summary>
        /// KNX组对象
        /// </summary>
        internal KnxGroup Group = new(1);

        /// <summary>
        /// 绑定的对象ID
        /// </summary>
        internal int ObjectId { get; set; } = -1;

        internal KnxHmiComponent()
            : base()
        { }

        internal KnxHmiComponent(HmiGeometry geometry, string styleAttribute, string valueAttribute)
            : base(geometry)
        {
            ReadValueAttribute(valueAttribute);
            ReadStyleAttribute(styleAttribute);
        }

        /// <summary>
        /// 读取value属性字符串
        /// 控制格式：*[ObjectCode]=[数值]，$[SceneCode]=[数值]，[组地址]=[数值]，[组地址]=[数值]#[DPT].[DPST]
        ///     开关量控制：$Channel1=0|1，0/0/0=0|1，0/0/0=0
        ///     数字量控制：$Channel1=100，0/0/0=0~255，0/0/0=000
        /// 反馈格式：*[ObjectCode]@[数值]，$[SceneCode]@[数值]，[组地址]@[数值]，[组地址]@[数值]#[DPT].[DPST]
        ///     开关量反馈：0/0/0@0|1，0/0/0
        ///     数字量反馈：0/0/0@0~255
        /// </summary>
        private void ReadValueAttribute(string valueAttribute)
        {
            valueAttribute = Regex.Replace(valueAttribute, "<span>.*?</span>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline); //去除格式信息
            //绑定模式：
            if (valueAttribute.StartsWith('*') || valueAttribute.StartsWith('$')) //*和$开头的为数据表映射模式
            {
                MappingMode = HmiMappingMode.DataTable;
            }
            else if (KnxCommon.ContainsGroupAddress(valueAttribute)) //包含合法组地址为地址映射模式
            {
                MappingMode = HmiMappingMode.Address;
            }
            else //其他情况为静态对象
            {
                MappingMode = HmiMappingMode.None;
            }
            //静态和动态的预处理：
            if (MappingMode == HmiMappingMode.None) //静态对象的情况
            {
                Text = valueAttribute.Replace("</div>", null).Replace("<div>", Environment.NewLine);
            }
            else //动态对象的情况
            {
                //将value字符串拆分为“绑定信息”和“文本内容”
                string mappingString; //绑定信息的字符串
                string textString; //文本内容的字符串
                if (valueAttribute.Contains("<div>")) //多行，即含有描述文字的情况
                {
                    valueAttribute = valueAttribute.Replace("</div>", null); //删除结束标签，<div>相当于回车符
                    string[] valueArry = valueAttribute.Split("<div>"); //根据<div>（此时相当于回车符）拆分
                    mappingString = valueArry[0]; //取第一行内容为组地址部分
                    textString = string.Join(Environment.NewLine, valueArry.Skip(1)); //其他的用回车分割为文本内容
                }
                else
                {
                    mappingString = valueAttribute; //value属性只有一行的情况认为全是组地址部分
                    textString = string.Empty;
                }

                //暂不实现文本变换
                //bool hasTextChange = Regex.IsMatch(Text, "[%|~]");
                //if (hasTextChange)
                //{
                //    switch (Mapping.ChangeType)
                //    {
                //        case 
                //    }
                //    string[] texts=
                //}
                //暂不实现文本变换

                //绑定信息1-判断方向：
                if (mappingString.Contains('=')) //带有=的方向为控制
                {
                    Direction = HmiComponentDirection.Control;
                }
                else if (mappingString.Contains('@')) //带有@的方向为反馈
                {
                    Direction = HmiComponentDirection.Feedback;
                }
                else //绑定信息不包含方向字符，认为是开关量反馈
                {
                    mappingString += "@0|1"; //补齐绑定信息
                    Direction = HmiComponentDirection.Feedback;
                }
                //绑定信息3-对象/组地址部分
                string[] mappingArry = mappingString.Split(['=', '@']); //用符号分割为数组
                switch (MappingMode)
                {
                    case HmiMappingMode.DataTable:
                        Mapping = new(mappingArry[1]);
                        Text += $"{textString}{mappingArry[0]}"; //把ObjectCode加在文本后面备用
                        Text = $"{textString}{mappingString}";
                        break;
                    case HmiMappingMode.Address:
                        if (GroupAddress.TryParse(mappingArry[0], out GroupAddress ga)) //组地址有效的情况
                        {
                            string vals; //数值
                            if (mappingArry[1].Contains('#')) //有#代表定义了组地址类型
                            {
                                string[] gvArry = mappingArry[1].Split('#'); //分割数值和数据类型
                                vals = gvArry.First(); //第一个元素为数值
                                //string[] dpt = gvArry.Last().Split('.'); //分割DPT和DPST
                                Group = new(ga, gvArry.Last()); //新建KNX组对象
                            }
                            else //不带#定义组地址类型的，认为DPT是1.001
                            {
                                vals = mappingArry[1]; //方向符号后全为数值
                                Group = new(ga, 1, 1);
                            }
                            Mapping = new(vals, Group.DPT); //新建绑定对象
                        }
                        else //组地址无效的情况，按静态对象处理
                        {
                            MappingMode = HmiMappingMode.None;
                            Text = valueAttribute.Replace("</div>", null).Replace("<div>", Environment.NewLine);
                        }
                        if (string.IsNullOrWhiteSpace(textString))
                        {
                            Text = ga.ToString();
                        }
                        else
                        {
                            Text = textString;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 读取style属性字符串
        /// </summary>
        /// <param name="styleAttribute"></param>
        private void ReadStyleAttribute(string styleAttribute)
        {
            Dictionary<string, string> dicStyle = StyleStringToDic(styleAttribute); //转为style字典
            Shape = dicStyle.First().Key switch //字典第一项为形状
            {
                "ellipse" => HmiShapeType.Ellipse, //椭圆
                "rounded" => HmiShapeType.Rectangle, //矩形（矩形会带圆角属性）
                "endArrow" => HmiShapeType.Line, //线条
                "text" => HmiShapeType.Text, //文本
                _ => HmiShapeType.None, //其他情况
            };
            Color[] fillColors = ReadColorStyle("fillColor", ref dicStyle); //填充颜色
            Color[] strokeColors = ReadColorStyle("strokeColor", ref dicStyle); //线条颜色
            Color[] fontColors = ReadColorStyle("fontColor", ref dicStyle); //字体颜色
            StrokeWidth = ReadNumStyle("strokeWidth", ref dicStyle); //线条宽度
            int fontSize = ReadNumStyle("fontSize", ref dicStyle, 12); //原始字体大小
            FontSize = (int)Math.Round(fontSize / 1.33); //drawio中字体单位为像素，Winform中字体单位为点，1点=1.33像素
            Alignment = ReadAlign(ref dicStyle); //文本对齐方式
            //静态和动态控件的颜色设置：
            if (MappingMode == HmiMappingMode.None) //静态控件颜色设置（设为开启颜色）
            {
                FillColor = fillColors.Last();
                StrokeColor = strokeColors.Last();
                FontColor = fontColors.Last();
            }
            else //动态控件颜色设置（设为关闭颜色）
            {
                Mapping.FillColors = fillColors; //填充颜色
                Mapping.StrokeColors = strokeColors; //线条颜色
                Mapping.FontColors = fontColors; //字体颜色
                FillColor = fillColors.First();
                StrokeColor = strokeColors.First();
                FontColor = fontColors.First();
            }
        }

        /// <summary>
        /// 样式字符串转字典
        /// </summary>
        /// <param name="styleString">样式字符串</param>
        /// <param name="keyToLower">是否把键转为小写</param>
        /// <returns></returns>
        private static Dictionary<string, string> StyleStringToDic(string styleString, bool keyToLower = false)
        {
            if (string.IsNullOrWhiteSpace(styleString)) return [];
            string[] styles = styleString.Trim().Split(';');
            Dictionary<string, string> dic = [];
            foreach (string style in styles)
            {
                if (string.IsNullOrWhiteSpace(style)) continue; //通过空项
                string[] kv = style.Split('=');
                string k = kv[0].Trim();
                if (keyToLower) k = k.ToLower();
                if (kv.Length == 2)
                {
                    dic.Add(k, kv[1].Trim());
                }
                else
                {
                    dic.Add(k, string.Empty);
                }
            }
            return dic;
        }

        /// <summary>
        /// 读取数字样式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <param name="defaultValue">默认数值</param>
        /// <returns></returns>
        private static int ReadNumStyle(string key, ref Dictionary<string, string> dic, int defaultValue = 0)
        {
            if (dic.TryGetValue(key, out string? value))
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 读取颜色样式
        /// none: 无颜色
        /// default: 默认颜色
        /// #000000: RGB颜色
        /// light-dark(#000000,#000000): 浅色-深色模式，浅色为开启时颜色
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        private Color[] ReadColorStyle(string key, ref Dictionary<string, string> dic)
        {
            if (string.IsNullOrWhiteSpace(key)) return [Color.Empty];
            if (dic.TryGetValue(key, out string? colorString))
            {
                if (!string.IsNullOrWhiteSpace(colorString))
                {
                    colorString = colorString.Trim().ToLower();
                    if (colorString == "none") //无颜色
                    {
                        return [Color.Empty];
                    }
                    else if (colorString == "default") //默认颜色
                    {
                        return [DEFAULTCOLOR_OFF, dicColorDefault[key]];
                    }
                    else if (colorString.Contains('#')) //其他颜色
                    {
                        MatchCollection matches = Regex.Matches(colorString, "(#[0-9a-fA-F]{6})"); //匹配RGB值
                        switch (matches.Count)
                        {
                            case 0: //没有颜色的情况，使用默认值（一般不会出现这种情况）
                                return [DEFAULTCOLOR_OFF, dicColorDefault[key]];
                            case 1: //一种颜色的情况
                                return [ColorTranslator.FromHtml(matches[0].Value)];
                            case 2: //浅色-深色模式，浅色为开启时颜色
                            default:
                                return [ColorTranslator.FromHtml(matches[1].Value), ColorTranslator.FromHtml(matches[0].Value)];
                        }
                    }
                    else //颜色字符串为非法格式
                    {
                        return [Color.Empty]; //返回空白颜色字典中的颜色
                    }
                }
                else //不存在给定值的情况
                {
                    return [dicColorEmpty[key]]; //返回空白颜色字典中的颜色
                }
            }
            else //字典内找不到的情况
            {
                return [Color.Empty]; //返回空白颜色字典中的颜色
            }
        }

        /// <summary>
        /// 读取对齐方式
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static ContentAlignment ReadAlign(ref Dictionary<string, string> dic)
        {
            if (!dic.TryGetValue("align", out string? alignH))
            {
                alignH = "center"; //横向对称默认为居中
            }
            if (!dic.TryGetValue("verticalAlign", out string? alignV))
            {
                alignV = "middle"; //纵向对齐默认为居中
            }
            switch ($"{alignV}{alignH}")
            {
                case "topleft":
                    return ContentAlignment.TopLeft;
                case "topcenter":
                    return ContentAlignment.TopCenter;
                case "topright":
                    return ContentAlignment.TopRight;
                case "middleleft":
                    return ContentAlignment.MiddleLeft;
                case "middlecenter":
                    return ContentAlignment.MiddleCenter;
                case "middleright":
                    return ContentAlignment.MiddleRight;
                case "bottomleft":
                    return ContentAlignment.BottomLeft;
                case "bottomcenter":
                    return ContentAlignment.BottomCenter;
                case "bottomright":
                    return ContentAlignment.BottomRight;
                default:
                    return ContentAlignment.MiddleCenter;
            }
        }

    }

}
