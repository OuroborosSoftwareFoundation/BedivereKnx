using System.Text.RegularExpressions;
using System.Xml;
using Ouroboros.Hmi.Library;
using Ouroboros.Hmi.Library.Elements;

namespace BedivereKnx.GUI
{

    internal static class HmiFile
    {

        /// <summary>
        /// 读取Drawio界面文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        internal static Dictionary<string, HmiPage> FromDrawio(string filePath)
        {
            //判断文件是否存在：
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{Resources.Strings.Ex_FileNotFound}{Environment.NewLine}{Path.GetFullPath(filePath)}", filePath);
            }
            //验证文件扩展名：
            string extension = Path.GetExtension(filePath).ToLower();
            if (extension != ".drawio")
            {
                throw new ArgumentException($"{string.Format(Resources.Strings.Ex_FileFormatInvalid, "Drawio")}{Environment.NewLine}{Path.GetFullPath(filePath)}");
            }
            try
            {
                FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //新建文件流
                XmlReader xml = XmlReader.Create(fs);
                xml.ReadStartElement("mxfile"); //根节点起始元素
                Dictionary<string, HmiPage> pages = [];
                string pageName = string.Empty; //当前页面名称
                while (xml.Read())
                {
                    XmlNodeType nodeType = xml.NodeType;
                    if (nodeType == XmlNodeType.Whitespace) continue; //跳过标记间的空白
                    if (nodeType == XmlNodeType.Element)
                    {
                        switch (xml.Name.ToLower())
                        {
                            case "diagram": //页面开始
                                pageName = xml.GetAttribute("name")!; //当前页面名称
                                pages.Add(pageName, new HmiPage()); //输出字典中新建一个元素
                                pages[pageName].Elements = new List<HmiElement>();
                                break;
                            case "mxgraphmodel": //页面基础信息
                                pages[pageName].PageSize = new Size()
                                {
                                    Width = Convert.ToInt32(xml.GetAttribute("pageWidth")),
                                    Height = Convert.ToInt32(xml.GetAttribute("pageHeight")),
                                }; //页面尺寸
                                break;
                            case "mxcell": //页面中的元素
                                string? parent = xml.GetAttribute("parent"); //元素的图层名称
                                if (string.IsNullOrWhiteSpace(parent)) continue; //跳过parent为空的项
                                string valueAttributr = xml.GetAttribute("value")!; //控件的值属性
                                string styleAttribute = xml.GetAttribute("style")!; //控件的样式属性
                                string innerXml = xml.ReadInnerXml(); //xmCell内部的mxGeometry元素
                                if (string.IsNullOrWhiteSpace(innerXml)) continue; //跳过无内部XML的元素
                                HmiGeometry geometry = HmiGeometry.FromDrawioXml(innerXml); //控件坐标和尺寸
                                Match match = Regex.Match(styleAttribute, "shape=image;.*?image=data:image/.*?,(.*?);"); //暂时忽略图片格式
                                if (string.IsNullOrWhiteSpace(match.Value)) //控件的情况
                                {
                                    pages[pageName].Elements.Add(new KnxHmiComponent(geometry, styleAttribute, valueAttributr));
                                }
                                else //图片的情况
                                {
                                    HmiImageElement image = new(geometry, match.Groups[1].Value);
                                    if (parent == "1") //背景图层
                                    {
                                        pages[pageName].BackImages.Add(image); //图片加入背景列表
                                    }
                                    else
                                    {
                                        pages[pageName].Elements.Add(image); //图片加入列表
                                    }
                                }
                                break;
                        }
                    }
                    else if (nodeType == XmlNodeType.EndElement) //结束标签
                    {
                        if (xml.Name.ToLower() == "diagram") //页面结束
                        {
                            //无操作
                        }
                    }
                }
                return pages;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
