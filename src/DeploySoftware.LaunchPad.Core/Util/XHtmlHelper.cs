using Abp.UI;
using Castle.Core.Logging;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public partial class XHtmlHelper
    {
        public ILogger Logger { get; set; }

        public XHtmlHelper()
        {
            Logger = NullLogger.Instance;
        }

        public XHtmlHelper(ILogger logger)
        {
            Logger = logger;
        }

        public HtmlDocument LoadXhtmlDocument(string folderPath, string fileName)
        {
            HtmlDocument xhtmlFile = new HtmlDocument();
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo d = new DirectoryInfo(folderPath);
                FileInfo[] Files = d.GetFiles("*.xml");
                foreach (FileInfo file in Files)
                {

                    if (file.Name.Contains(fileName))
                    {
                        xhtmlFile.Load(file.FullName);
                    }
                }

            }
            return xhtmlFile;
        }


        /// <summary>
        /// Returns an HtmlNode from a parent, given an xpath.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public HtmlNode GetNodeFromParent(HtmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            return parentNode.SelectSingleNode(xPath.ToLower());
        }

        /// <summary>
        /// Returns a HtmlNodeCollection from a parent node, given an xpath.
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public HtmlNodeCollection GetNodeCollectionFromParent(HtmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            return parentNode.SelectNodes(xPath.ToLower());
        }

        public string GetTextFromElement(HtmlNode parentNode, string xPath, bool replaceNullWithEmptyString = true)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            string elementNodeString = string.Empty;
            var elementNode = parentNode.SelectSingleNode(xPath.ToLower());
            if (elementNode != null)
            {
                if (elementNode.InnerHtml == "&nbsp;") elementNode.Remove();
                string innerHtml = elementNode.InnerHtml.Replace("<![CDATA[", "").Replace("]]>", String.Empty).Trim();
                if (replaceNullWithEmptyString)
                {
                    elementNodeString = innerHtml.Replace("null", string.Empty).Trim();
                }
                else
                {
                    elementNodeString = innerHtml;
                }
            }
            return elementNodeString;
        }

        public string GetTextFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
            string value = string.Empty;
            if (parentNode.HasAttributes)
            {
                try
                {
                    HtmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
                    if (attr != null)
                    {
                        value = attr.Value.Trim();
                    }
                }
                catch (NullReferenceException ex)
                {
                    // log an error
                    value = ex.Message;
                    throw;
                }
            }
            return value;
        }

        public int GetIntFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
            int value = 0;
            if (parentNode.HasAttributes)
            {
                try
                {
                    HtmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
                    if (attr != null)
                    {
                        value = int.Parse(attr.Value);
                    }
                }
                catch (NullReferenceException ex)
                {
                    // log an error
                    Logger.Error(ex.Message);
                    throw;
                }
            }
            return value;
        }

        public bool GetBoolFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), DeploySoftware_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
            bool value = false; // Default to false
            if (parentNode.HasAttributes)
            {
                try
                {
                    HtmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
                    if (attr != null)
                    {
                        value = bool.Parse(attr.Value);
                    }
                }
                catch (NullReferenceException ex)
                {
                    // log an error
                    Logger.Error(ex.Message);
                    throw;
                }
            }
            return value;
        }

    }
}
