// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-15-2023
// ***********************************************************************
// <copyright file="XHtmlHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using HtmlAgilityPack;
using System;
using System.IO;

namespace Deploy.LaunchPad.Core.Util
{
    /// <summary>
    /// Class XHtmlHelper.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Util.HelperBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Util.HelperBase" />
    public partial class XHtmlHelper : HelperBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="XHtmlHelper"/> class.
        /// </summary>
        public XHtmlHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XHtmlHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public XHtmlHelper(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        /// Loads the XHTML document.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>HtmlDocument.</returns>
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
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>HtmlNode.</returns>
        public HtmlNode GetNodeFromParent(HtmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            return parentNode.SelectSingleNode(xPath.ToLower());
        }

        /// <summary>
        /// Returns a HtmlNodeCollection from a parent node, given an xpath.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>HtmlNodeCollection.</returns>
        public HtmlNodeCollection GetNodeCollectionFromParent(HtmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            return parentNode.SelectNodes(xPath.ToLower());
        }

        /// <summary>
        /// Gets the text from element.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <param name="shouldReplaceNullWithEmptyString">if set to <c>true</c> [should replace null with empty string].</param>
        /// <param name="shouldUnescapeAngleBrackets">if set to <c>true</c> [should unescape angle brackets].</param>
        /// <returns>System.String.</returns>
        public string GetTextFromElement(HtmlNode parentNode, string xPath, bool shouldReplaceNullWithEmptyString = true, bool shouldUnescapeAngleBrackets = true)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            string elementNodeString = GetElementNodeString(parentNode, xPath, shouldReplaceNullWithEmptyString, shouldUnescapeAngleBrackets);
            return elementNodeString;
        }

        /// <summary>
        /// Gets the element node string.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <param name="shouldReplaceNullWithEmptyString">if set to <c>true</c> [should replace null with empty string].</param>
        /// <param name="shouldUnescapeAngleBrackets">if set to <c>true</c> [should unescape angle brackets].</param>
        /// <returns>System.String.</returns>
        protected string GetElementNodeString(HtmlNode parentNode, string xPath, bool shouldReplaceNullWithEmptyString = true, bool shouldUnescapeAngleBrackets = true)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            string elementNodeString = string.Empty;
            var elementNode = parentNode.SelectSingleNode(xPath.ToLower());
            if (elementNode != null)
            {
                if (elementNode.InnerHtml == "&nbsp;") elementNode.Remove();
                string innerHtml = elementNode.InnerHtml.Replace("<![CDATA[", "").Replace("]]>", String.Empty).Trim();
                if (shouldReplaceNullWithEmptyString)
                {
                    elementNodeString = innerHtml.Replace("null", string.Empty).Trim();
                }
                else
                {
                    elementNodeString = innerHtml;
                }
                if(shouldUnescapeAngleBrackets)
                {
                    elementNodeString = elementNodeString.Replace("&lt;", "<").Replace("&gt;", ">");
                }
            }
            return elementNodeString;
        }

        /// <summary>
        /// Gets the text from attribute.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>System.String.</returns>
        public string GetTextFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
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

        /// <summary>
        /// Gets the int from attribute.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>System.Int32.</returns>
        public int GetIntFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
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

        /// <summary>
        /// Gets the bool from attribute.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetBoolFromAttribute(HtmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_AttributeName_Is_Null);
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

        /// <summary>
        /// Gets the bool from element.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetBoolFromElement(HtmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentException>(parentNode == null, Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_ParentNode_Is_Null);
            Guard.Against<ArgumentException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Core_Resources.Guard_XhtmlHelper_XPath_Is_Null);
            bool value = false; // Default to false
            if (parentNode.HasAttributes)
            {
                try
                {
                    string elementNodeString = GetElementNodeString(parentNode, xPath, true).ToLower();
                    bool.TryParse(elementNodeString, out value);
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


        /// <summary>
        /// Ensures the valid enum from string.
        /// </summary>
        /// <typeparam name="TEnum">The type of the t enum.</typeparam>
        /// <param name="inputValue">The input value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>TEnum.</returns>
        public TEnum EnsureValidEnumFromString<TEnum>(string inputValue, TEnum defaultValue) where TEnum : struct, Enum
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(inputValue), Deploy_LaunchPad_Core_Resources.Guard_Input_IsNull);
            TEnum validEnum = defaultValue;
            if (!String.IsNullOrEmpty(inputValue))
            {
                if (!Enum.TryParse<TEnum>(inputValue, true, out validEnum))
                {
                    // log and throw
                    Logger.Warn("Could not parse enum for user provided value " + inputValue + ". Returning the provided default.");
                }
            }
            return validEnum;
        }
    }
}
