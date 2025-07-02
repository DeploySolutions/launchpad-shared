using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Deploy.LaunchPad.Util
{
    public partial class XmlHelper :HelperBase
    {
        protected XmlDocument _xmlDoc = new XmlDocument();
        public virtual XmlDocument XmlDocument { get { return _xmlDoc; } set { _xmlDoc = value; } }

        protected XmlNamespaceManager _nsManager;
        public virtual XmlNamespaceManager NsManager { get { return _nsManager; } set { _nsManager = value; } }


        protected IDictionary<string, string> _xmlNamespaces;
        public virtual IDictionary<string, string> XmlNamespaces { get { return _xmlNamespaces; } set { _xmlNamespaces = value; } }


        public XmlHelper() :base()
        {
            XmlNamespaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            NsManager = new XmlNamespaceManager(XmlDocument.NameTable);
        }

        public XmlHelper(ILogger logger, IDictionary<string, string> xmlNamespaces) : base(logger)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            _nsManager = new XmlNamespaceManager(XmlDocument.NameTable);
            foreach (var xmlNamespace in xmlNamespaces)
            {
                _nsManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            _xmlNamespaces = _nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
        }

        public XmlHelper(ILogger logger, IDictionary<string,string> xmlNamespaces, XmlDocument xmlDoc) : base(logger)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            Guard.Against<ArgumentNullException>(xmlDoc == null, "XmlDocument cannot be null");
            XmlDocument = xmlDoc;
            _nsManager = new XmlNamespaceManager(XmlDocument.NameTable);
            foreach (var xmlNamespace in xmlNamespaces)
            {
                _nsManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            _xmlNamespaces = _nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
        }

        /// <summary>
        /// Load the XML document from a file path without applying any custom XML namespaces.
        /// </summary>
        /// <param name="xmlDocumentFilePath"></param>
        /// <returns></returns>
        public virtual XmlDocument LoadXmlDocument(string folderPath, string fileName)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(folderPath), "folderPath cannot be null or empty");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(fileName), "fileName cannot be null or empty");
            _xmlDoc = LoadXmlDocument(folderPath, fileName, null); // Call the overloaded method with null namespaces
            return _xmlDoc; // Return the loaded XmlDocument
        }

        /// <summary>
        /// Load the XML document from a file path and apply the provided custom XML namespaces (if any).
        /// If no namespaces are provided, it will use the default namespaces defined in the class.
        /// </summary>
        /// <param name="xmlDocumentFilePath"></param>
        /// <param name="xmlNamespaces"></param>
        /// <returns></returns>
        public virtual XmlDocument LoadXmlDocument(string folderPath, string fileName, IDictionary<string, string> xmlNamespaces = null)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(folderPath), "folderPath cannot be null or empty");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(fileName), "fileName cannot be null or empty");
            if(!folderPath.EndsWith(Path.DirectorySeparatorChar))
            {
                folderPath += Path.DirectorySeparatorChar;
            }
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo d = new DirectoryInfo(folderPath);
                FileInfo[] Files = d.GetFiles()
                    .Where(f => f.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || 
                    f.Name.Contains(".rad.", StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                foreach (FileInfo file in Files)
                {

                    if (file.Name.Contains(fileName))
                    {
                        _xmlDoc.Load(file.FullName);
                    }
                }

            }
            _nsManager = new XmlNamespaceManager(_xmlDoc.NameTable);
            if(xmlNamespaces == null)
            {
                xmlNamespaces = _xmlNamespaces;
            }
            foreach (var xmlNamespace in xmlNamespaces)
            {
                _nsManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            _xmlNamespaces = _nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            return _xmlDoc; // Return the loaded XmlDocument
        }

        /// <summary>
        /// Gets the text from element.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <param name="shouldReplaceNullWithEmptyString">if set to <c>true</c> [should replace null with empty string].</param>
        /// <param name="shouldUnescapeAngleBrackets">if set to <c>true</c> [should unescape angle brackets].</param>
        /// <returns>System.String.</returns>
        public virtual string GetTextFromElement(XmlNode parentNode, string xPath, bool shouldReplaceNullWithEmptyString = true, bool shouldUnescapeAngleBrackets = true)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);
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
        protected virtual string GetElementNodeString(XmlNode parentNode, string xPath, bool shouldReplaceNullWithEmptyString = true, bool shouldUnescapeAngleBrackets = true, bool shouldIgnoreCase = false)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);
            string elementNodeString = string.Empty;
            var elementNode = GetNode(xPath, parentNode, shouldIgnoreCase);
            if (elementNode != null)
            {
                if (elementNode.InnerXml == "&nbsp;") elementNode.ParentNode?.RemoveChild(elementNode);
                string innerHtml = elementNode.InnerXml.Replace("<![CDATA[", "").Replace("]]>", String.Empty).Trim();
                if (shouldReplaceNullWithEmptyString)
                {
                    elementNodeString = innerHtml.Replace("null", string.Empty).Trim();
                }
                else
                {
                    elementNodeString = innerHtml;
                }
                if (shouldUnescapeAngleBrackets)
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
        public virtual string GetTextFromAttribute(XmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Util_Resources.Guard_Xml_AttributeName_Is_Null);
            string value = string.Empty;
            if (parentNode.Attributes.Count > 0)
            {
                try
                {
                    XmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
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
        public virtual int GetIntFromAttribute(XmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Util_Resources.Guard_Xml_AttributeName_Is_Null);
            int value = 0;
            if (parentNode.Attributes.Count > 0)
            {
                try
                {
                    XmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
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
        public virtual bool GetBoolFromAttribute(XmlNode parentNode, string attributeName)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Util_Resources.Guard_Xml_AttributeName_Is_Null);
            bool value = false; // Default to false
            if (parentNode.Attributes.Count > 0)
            {
                try
                {
                    XmlAttribute attr = parentNode.Attributes[attributeName.ToLower()];
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
        public virtual bool GetBoolFromElement(XmlNode parentNode, string xPath)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);
            bool value = false; // Default to false
            if (parentNode.Attributes.Count > 0)
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
        public virtual TEnum EnsureValidEnumFromString<TEnum>(string inputValue, TEnum defaultValue) where TEnum : struct, Enum
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(inputValue), Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
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

        /// <summary>
        /// Select the XML node using the provided XPath and automatically using the appropriate namespace manager.
        /// An optional starting node can be provided, alternatively the root document will be used for assessing the XPath.
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="startingNode"></param>
        /// <returns></returns>
        public virtual XmlNode GetNode(string xPath, XmlNode startingNode = null, bool shouldPreProcessXpath = false)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);

            if(shouldPreProcessXpath)
            {
                xPath = PreProcessXpath(xPath);
            }

            var contextNode = startingNode ?? _xmlDoc.DocumentElement;            
            var node = contextNode.SelectSingleNode(xPath, NsManager);
            if (node != null)
            {
                return node;
            }

            // If the node is not found, try a case-insensitive search
            string insensitiveXpath = ToCaseInsensitiveXPath(xPath);
            return contextNode.SelectSingleNode(insensitiveXpath, NsManager);
        }

        /// <summary>
        /// Select the XML nodes using the provided XPath and automatically using the appropriate namespace manager.
        /// An optional starting node can be provided, alternatively the root document will be used for assessing the XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="startingNode"></param>
        /// <returns></returns>
        public virtual XmlNodeList GetNodes(string xPath, XmlNode startingNode = null, bool shouldPreProcessXpath = false)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);

            if (shouldPreProcessXpath)
            {
                xPath = PreProcessXpath(xPath);
            }

            var contextNode = startingNode ?? _xmlDoc.DocumentElement;
            var nodes = contextNode.SelectNodes(xPath, NsManager);
            if (nodes != null && nodes.Count > 0)
            {
                return nodes;
            }

            // If not found, try case-insensitive search
            string insensitiveXpath = ToCaseInsensitiveXPath(xPath);
            var insensitiveNodes = contextNode.SelectNodes(insensitiveXpath, NsManager);
            return insensitiveNodes;
        }

        protected virtual string PreProcessXpath(string xPath)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);

            // Only preprocess if a default prefix exists
            if (_xmlNamespaces != null && _xmlNamespaces.Count > 0)
            {
                // Use the first prefix as the default (e.g., "def")
                string defaultPrefix = _xmlNamespaces.Keys.First();
                // Regex: match element names not already prefixed (skip ., @, :, (), [], and //)
                xPath = System.Text.RegularExpressions.Regex.Replace(
                    xPath,
                    @"(?<=/|^)(?!@|\.|//|[\w-]+:)([\w-]+)",
                    $"{defaultPrefix}:$1"
                );
            }
            return xPath;
        }

        protected virtual string ToCaseInsensitiveXPath(string xPath)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);
            const string upperAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerAlphabet = "abcdefghijklmnopqrstuvwxyz";

            bool isDescendant = xPath.StartsWith("//");
            bool isAbsolute = xPath.StartsWith("/");
            var segments = xPath.TrimStart('/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var insensitiveSegments = segments
                .Select(s =>
                {
                    var local = s.Contains(":") ? s.Substring(s.IndexOf(':') + 1) : s;
                    return $"*[translate(local-name(), '{upperAlphabet}', '{lowerAlphabet}')='{local.ToLowerInvariant()}']";
                });
            var insensitivePath = string.Join("/", insensitiveSegments);
            if (isDescendant)
                return "//" + insensitivePath;
            if (isAbsolute)
                return "/" + insensitivePath;
            return insensitivePath;
        }

        /// <summary>
        /// Select the XML attribute of the current node using the provided attribute name and automatically using the appropriate namespace manager
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public virtual XmlAttribute FindAttribute(XmlNode currentNode, string attributeName, bool shouldIgnoreCase = false)
        {
            Guard.Against<ArgumentNullException>(currentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Util_Resources.Guard_Xml_AttributeName_Is_Null);
            XmlAttribute attribute = currentNode.Attributes[attributeName];
            return attribute;
        }

        public virtual string GetCleanTextFromXhtml(string xhtml)
        {
            return xhtml.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty).Trim();
        }
    }
}
