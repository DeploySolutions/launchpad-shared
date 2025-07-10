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
                string innerHtml = GetCleanTextFromXhtml(elementNode.InnerXml);
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
        /// Select the XML node using the provided XPath and automatically using the appropriate namespace manager.
        /// An optional starting node can be provided, alternatively the root document will be used for assessing the XPath.
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="startingNode"></param>
        /// <returns></returns>
        public virtual XmlNode GetNode(string xPath, XmlNode startingNode = null, bool shouldPreProcessXpath = false)
        {
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(xPath), Deploy_LaunchPad_Util_Resources.Guard_Xml_XPath_Is_NullOrEmpty);

            if (shouldPreProcessXpath)
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

        /// <summary>
        /// Returns an XmlNode from a parent, given an xpath.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>XmlNode.</returns>
        public virtual XmlNode GetNodeFromParent(XmlNode parentNode, string xPath, bool shouldPreProcessXpath = false)
        {
            XmlNode node = GetNode(xPath, parentNode, shouldPreProcessXpath);
            return node;
            //return XhtmlHelper.GetNodeFromParent(parentNode, xPath);
        }

        /// <summary>
        /// Returns a XmlNodeList from a parent node, given an xpath.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>XmlNodeList.</returns>
        public virtual XmlNodeList GetNodeCollectionFromParent(XmlNode parentNode, string xPath, bool shouldPreProcessXpath = false)
        {
            return GetNodes(xPath, parentNode, shouldPreProcessXpath);
        }

        /// <summary>
        /// Select the XML attribute of the current node using the provided attribute name and automatically using the appropriate namespace manager
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public virtual XmlAttribute GetAttribute(XmlNode currentNode, string attributeName, bool shouldIgnoreCase = false)
        {
            Guard.Against<ArgumentNullException>(currentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Xml_Node_Is_Null);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(attributeName), Deploy_LaunchPad_Util_Resources.Guard_Xml_AttributeName_Is_Null);
            XmlAttribute attribute = currentNode.Attributes[attributeName];
            return attribute;
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
        /// Gets the unique identifier from attribute.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>Guid.</returns>
        public virtual Guid GetGuidFromAttribute(XmlNode parentNode, string xPath)
        {
            Guid guid = Guid.Empty;
            string attributeValue = GetTextFromAttribute(parentNode, xPath);
            if (!string.IsNullOrEmpty(attributeValue))
            {
                try
                {
                    guid = Guid.Parse(attributeValue);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error while attempting to get Guid from attribute with xpath {0}. Message was '{1}'.", xPath, ex.Message);
                    InvalidDataException iedEx = new InvalidDataException(message);
                }
            }
            return guid;
        }


        /// <summary>
        /// Gets the unique identifier from element.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="xPath">The x path.</param>
        /// <returns>Guid.</returns>
        public virtual Guid GetGuidFromElement(XmlNode parentNode, string xPath)
        {
            Guid guid = Guid.Empty;
            string elementValue = GetTextFromElement(parentNode, xPath);
            if (!string.IsNullOrEmpty(elementValue))
            {
                try
                {
                    guid = Guid.Parse(elementValue);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error while attempting to get Guid from element with xpath {0}. Message was '{1}'.", xPath, ex.Message);
                    InvalidDataException iedEx = new InvalidDataException(message);
                }
            }
            return guid;
        }

        public virtual Guid GetIdFromXmlNode(XmlNode parentNode, string xPath = "id")
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            Guid id = Guid.Empty;
            string idValue = GetTextFromAttribute(parentNode, xPath);
            if (!string.IsNullOrEmpty(idValue))
            {
                id = Guid.Parse(idValue);
            }
            return id;
        }

        public virtual ElementName GetNameFromXmlNode(XmlNode parentNode, string xPath = null)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            bool shouldPreProcessXpath = false;
            if (xPath == null)
            {
                xPath = "//Name[1]";
                shouldPreProcessXpath = true;
            }
            ElementName name = null;
            XmlNode nameNode = GetNode(xPath, parentNode, shouldPreProcessXpath);
            if (nameNode != null)
            {
                string fullValue = GetTextFromElement(nameNode, "./core:Full");
                string shortValue = GetTextFromElement(nameNode, "./core:Short") ?? fullValue;
                name = new ElementName(fullValue, shortValue);
            }
            return name;
        }

        public virtual ElementDescription GetDescriptionFromXmlNode(XmlNode parentNode, string xPath = null)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            bool shouldPreProcessXpath = false;
            if (xPath == null)
            {
                xPath = "//Description[1]";
                shouldPreProcessXpath = true;
            }
            XmlNode descriptionNode = GetNode(xPath, parentNode, shouldPreProcessXpath);
            ElementDescription description = null;
            if (descriptionNode != null)
            {
                string fullValue = GetTextFromElement(descriptionNode, "./core:Full");
                string shortValue = GetTextFromElement(descriptionNode, "./core:Short") ?? fullValue;
                description = new ElementDescription(fullValue, shortValue);
            }
            else
            {
                // If no description node is found, return a default description
                Logger.Warn("No description node found in the XML. Returning default description.");
                description = new ElementDescription("No description provided.", "No description provided.");
            }
            return description;
        }

        public virtual ElementType GetElementTypeFromXmlNode(XmlNode parentNode, string xPath = null)
        {
            Guard.Against<ArgumentNullException>(parentNode == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            bool shouldPreProcessXpath = false;
            if (xPath == null)
            {
                xPath = "//ElementType[1]";
                shouldPreProcessXpath = true;
            }
            XmlNode descriptionNode = GetNode(xPath, parentNode, shouldPreProcessXpath);
            ElementType inheritance = null;

            // populate the inheritance information
            XmlNode inheritanceNode = GetNodeFromParent(parentNode, "ElementType");
            if (inheritanceNode != null)
            {
                string fullyQualifiedValue = GetTextFromElement(inheritanceNode, "core:FullyQualifiedType");
                inheritance = new ElementType(fullyQualifiedValue);
                inheritance.FullyQualifiedType = fullyQualifiedValue;
                string parentFullyQualifiedTypeValue = GetTextFromElement(inheritanceNode, "core:ParentFullyQualifiedType");
                if (!string.IsNullOrEmpty(parentFullyQualifiedTypeValue))
                {
                    inheritance.ParentElementType = new ElementType(parentFullyQualifiedTypeValue);
                }
                XmlNodeList inheritsFromStatementsNodes = GetNodeCollectionFromParent(inheritanceNode, "core:InheritsFrom/core:InheritsFromStatement");
                if (inheritsFromStatementsNodes != null)
                {
                    foreach (XmlNode statementNode in inheritsFromStatementsNodes)
                    {
                        string inheritsFromValue = GetTextFromElement(statementNode, ".");
                        if (!string.IsNullOrEmpty(inheritsFromValue))
                        {
                            inheritance.InheritsFrom.TryAdd(inheritsFromValue, inheritsFromValue);
                        }
                    }
                }
            }
            else
            {
                Logger.Warn("Inheritance node is null.");
            }
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(inheritance.FullyQualifiedType), string.Format("Inheritance.FullyQualifiedType property is null or empty for element {0}", parentNode.Name));
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(inheritance.TypeName), string.Format("inheritance.TypeName property is null or empty for element {0}", parentNode.Name));

            return inheritance;
        }

        /// <summary>
        /// Replaces the CDATA values of an XHTML or XML element string with clean text (just the value inside the CDATA)
        /// </summary>
        /// <param name="xhtml"></param>
        /// <returns></returns>
        public virtual string GetCleanTextFromXhtml(string xhtml)
        {
            return xhtml.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty).Trim();
        }


    }
}
