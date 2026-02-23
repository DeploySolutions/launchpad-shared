using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class XmlHelper :HelperBase
    {
        protected XmlDocument _xmlDoc = new XmlDocument();
        public virtual XmlDocument XmlDocument { get { return _xmlDoc; } set { _xmlDoc = value; } }

        protected XmlNamespaceManager _nsManager;
        public virtual XmlNamespaceManager NsManager { get { return _nsManager; } set { _nsManager = value; } }


        protected IDictionary<string, string> _xmlNamespaces;
        public virtual IDictionary<string, string> XmlNamespaces { get { return _xmlNamespaces; } set { _xmlNamespaces = value; } }


        /// <summary>
        /// Gets the cdata open.
        /// </summary>
        /// <value>The cdata open.</value>
       public const string CdataOpen = "<![CDATA["; 

        /// <summary>
        /// Gets the cdata close.
        /// </summary>
        /// <value>The cdata close.</value>
        public const string CdataClose = "]]>"; 


        public XmlHelper() :base()
        {
            _nsManager = new XmlNamespaceManager(_xmlDoc.NameTable);
            _xmlNamespaces = CreateDictionaryFromXmlNamespaceManager(_nsManager);
        }


        public XmlHelper(ILogger logger) : base(logger)
        {            
            _nsManager = new XmlNamespaceManager(_xmlDoc.NameTable);
            _xmlNamespaces = CreateDictionaryFromXmlNamespaceManager(_nsManager);
        }

        public XmlHelper(ILogger logger, XmlNamespaceManager nsManager) : base(logger)
        {
            Guard.Against<ArgumentNullException>(nsManager == null, "nsManager cannot be null.");
            _nsManager = nsManager;
            _xmlNamespaces = CreateDictionaryFromXmlNamespaceManager(nsManager);
        }

        public XmlHelper(ILogger logger, IDictionary<string, string> xmlNamespaces) : base(logger)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            _xmlNamespaces = xmlNamespaces;
            _nsManager = CreateXmlNamespaceManagerFromDictionary(xmlNamespaces);
        }

        public XmlHelper(ILogger logger, IDictionary<string,string> xmlNamespaces, XmlDocument xmlDoc) : base(logger)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            Guard.Against<ArgumentNullException>(xmlDoc == null, "xmlDoc cannot be null");
            _xmlNamespaces = xmlNamespaces; 
            _xmlDoc = xmlDoc;
            _nsManager = CreateXmlNamespaceManagerFromDictionary(xmlNamespaces, xmlDoc);
        }

        public XmlHelper(ILogger logger, XmlNamespaceManager nsManager, XmlDocument xmlDoc) : base(logger)
        {
            Guard.Against<ArgumentNullException>(nsManager == null, "nsManager cannot be null");
            Guard.Against<ArgumentNullException>(xmlDoc == null, "xmlDoc cannot be null");
            _xmlDoc = xmlDoc;
            _nsManager = nsManager;
            _xmlNamespaces = CreateDictionaryFromXmlNamespaceManager(nsManager);
        }

        public virtual IDictionary<string, string>  CreateDictionaryFromXmlNamespaceManager(XmlNamespaceManager nsManager)
        {
            Guard.Against<ArgumentNullException>(nsManager == null, "nsManager cannot be null");
            IDictionary<string, string> xmlNamespaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            xmlNamespaces = nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml).ToDictionary(kvp => kvp.Key, kvp => kvp.Value, StringComparer.OrdinalIgnoreCase);
            return xmlNamespaces;
        }


        private XmlNamespaceManager CreateXmlNamespaceManagerFromDictionary(IDictionary<string, string> xmlNamespaces, XmlDocument xmlDoc = null)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            xmlDoc ??= _xmlDoc; // Use the provided XmlDocument or fallback to the class-level _xmlDoc
            Guard.Against<ArgumentNullException>(xmlDoc == null, "xmlDoc cannot be null");

            var nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            foreach (var ns in xmlNamespaces)
            {
                nsManager.AddNamespace(ns.Key, ns.Value);
            }
            return nsManager;
        }

        /// <summary>
        /// Load the XML document from a file path and apply the provided custom XML namespaces (if any).
        /// If no namespaces are provided, it will use the default namespaces defined in the class.
        /// </summary>
        /// <param name="xmlDocumentFilePath"></param>
        /// <param name="xmlNamespaces"></param>
        /// <returns></returns>
        public virtual XmlDocument LoadXmlDocument(string folderPath, string fileName)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(folderPath), "folderPath cannot be null or empty.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(fileName), "fileName cannot be null or empty.");
            if (!folderPath.EndsWith(Path.DirectorySeparatorChar))
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
            _xmlNamespaces = GetCustomNamespacesFromXmlDocument(_xmlDoc);
            _nsManager = CreateXmlNamespaceManagerFromDictionary(_xmlNamespaces);
            return _xmlDoc; // Return the loaded XmlDocument
        }

        public virtual IDictionary<string, string> GetCustomNamespacesFromXmlDocument(XmlDocument doc)
        {
            Guard.Against<ArgumentNullException>(doc == null, "doc cannot be null");

            var namespaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (doc.DocumentElement != null && doc.DocumentElement.Attributes != null)
            {
                foreach (XmlAttribute attr in doc.DocumentElement.Attributes)
                {
                    // Look for xmlns or xmlns:prefix attributes
                    if (attr.Prefix == "xmlns" || attr.Name == "xmlns")
                    {
                        string prefix = attr.Prefix == "xmlns" ? attr.LocalName : string.Empty;
                        namespaces[prefix] = attr.Value;
                    }
                }
            }

            return namespaces;
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
                    guid = Guid.Parse(attributeValue.Replace("id_", string.Empty).Replace("_", string.Empty));
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
                    guid = Guid.Parse(elementValue.Replace("id_", string.Empty).Replace("_",string.Empty));
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
                id = Guid.Parse(idValue.Replace("id_", string.Empty).Replace("_", string.Empty));
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

        public virtual XmlElement GetXmlElementFromName(ElementName name, XmlDocument doc, string topLevelNodePrefix = "")
        {
            Guard.Against<ArgumentNullException>(doc == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            Guard.Against<ArgumentNullException>(name == null, Deploy_LaunchPad_Util_Resources.Guard_Input_IsNull);
            XmlElement nameElement = doc.CreateElement(topLevelNodePrefix + "Name");

            // Full property
            XmlElement fullChildElement = new XmlDocument().CreateElement("core:Full");
            XmlCDataSection fullCdata = doc.CreateCDataSection(name.Full);
            fullChildElement.AppendChild(fullCdata);
            nameElement.AppendChild(fullChildElement);

            // Short property
            if (!string.IsNullOrEmpty(name.Short))
            {
                XmlElement shortChildElement = new XmlDocument().CreateElement("core:Short");
                XmlCDataSection shortCdata = doc.CreateCDataSection(name.Short);
                shortChildElement.AppendChild(shortCdata);
                nameElement.AppendChild(shortChildElement);
            }

            // Suffix property
            if (!string.IsNullOrEmpty(name.Suffix))
            {
                XmlElement suffixChildElement = new XmlDocument().CreateElement("core:Suffix");
                XmlCDataSection suffixCdata = doc.CreateCDataSection(name.Suffix);
                suffixChildElement.AppendChild(suffixCdata);
                nameElement.AppendChild(suffixChildElement);
            }

            // Prefix property
            if (!string.IsNullOrEmpty(name.Prefix))
            {
                XmlElement prefixChildElement = new XmlDocument().CreateElement("core:Prefix");
                XmlCDataSection prefixChildElementCdata = doc.CreateCDataSection(name.Prefix);
                prefixChildElement.AppendChild(prefixChildElementCdata);
                nameElement.AppendChild(prefixChildElement);
            }

            return nameElement;
        }

        public virtual XmlElement GetXmlElementFromDescription( ElementName name, XmlDocument doc = null, string topLevelNodePrefix = "")
        {
            if (doc == null)
            {
                doc = new XmlDocument();
            }
            XmlElement element = doc.CreateElement(topLevelNodePrefix + "Description");

            // Full property
            XmlElement fullChildElement = new XmlDocument().CreateElement("core:Full");
            XmlCDataSection fullCdata = doc.CreateCDataSection(name.Full);
            fullChildElement.AppendChild(fullCdata);
            element.AppendChild(fullChildElement);

            // Short property
            if (!string.IsNullOrEmpty(name.Short))
            {
                XmlElement shortChildElement = new XmlDocument().CreateElement("core:Short");
                XmlCDataSection shortCdata = doc.CreateCDataSection(name.Short);
                shortChildElement.AppendChild(shortCdata);
                element.AppendChild(shortChildElement);
            }
            return element;
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
            return xhtml.Replace(CdataOpen, string.Empty).Replace(CdataClose, string.Empty).Trim();
        }

        protected virtual string CreateXmlOpeningElementString(string elementName, IDictionary<string, string> attributes = null)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<");
            sbXml.Append(elementName);
            if (attributes != null && attributes.Count > 0)
            {
                foreach (var attribute in attributes)
                {
                    sbXml.Append(" ");
                    sbXml.Append(attribute.Key);
                    sbXml.Append("=\"");
                    sbXml.Append(attribute.Value);
                    sbXml.Append("\"");
                }
            }
            sbXml.Append(">");
            return sbXml.ToString();
        }

        protected virtual string CreateXmlClosingElementString(string elementName)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("</");
            sbXml.Append(elementName);
            sbXml.Append(">");
            return sbXml.ToString();
        }

        protected virtual string CreateXmlElementString(string elementName, string elementValue, bool shouldUseCDataForvalue = true)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append(CreateXmlOpeningElementString(elementName));
            if (!string.IsNullOrEmpty(elementValue))
            {
                if (shouldUseCDataForvalue)
                {
                    sbXml.Append(CdataOpen);
                }
                sbXml.Append(elementValue);
                if (shouldUseCDataForvalue)
                {
                    sbXml.Append(CdataClose);
                }
            }
            sbXml.Append(CreateXmlClosingElementString(elementName));
            return sbXml.ToString();
        }

        public virtual IDictionary<string, string> CreateAttributeDictionaryFromXmlNamespaceManager(XmlNamespaceManager namespaceManager)
        {
            Guard.Against<ArgumentNullException>(namespaceManager == null, "namespaceManager cannot be null.");
            IDictionary<string, string> nsAttributes = new Dictionary<string, string>();
            foreach (string prefix in namespaceManager)
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    string ns = namespaceManager.LookupNamespace(prefix);
                    nsAttributes.Add($"xmlns:{prefix}", ns);
                }
                else
                {
                    // Default namespace (rarely used in your context, but for completeness)
                    string ns = namespaceManager.LookupNamespace(string.Empty);
                    if (!string.IsNullOrEmpty(ns))
                    {
                        nsAttributes.Add("xmlns", ns);
                    }
                }
            }
            return nsAttributes;
        }

        public virtual StringBuilder GetXmlOpeningRootElementSnippet( string elementName, Guid id, IDictionary<string, string> attributes = null)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(elementName), "elementName cannot be null.");
            StringBuilder sbXml = new StringBuilder();

            // ensure ID is added if not present
            if (attributes == null)
            {
                attributes = new Dictionary<string, string>();
                attributes.TryAdd("xmlns:xs", "http://www.w3.org/2001/XMLSchema");
                attributes.TryAdd("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                attributes.TryAdd("xmlns", $"http://assets.deploy.solutions/SpaceAppsRAD/{elementName}.v1.xsd");
                attributes.TryAdd("xsi:schemaLocation", $"http://assets.deploy.solutions/SpaceAppsRAD/{elementName}.v1.xsd ../../../DeploySolutionsFactory/Schemas/PUT YOUR SCHEMA HERE.v1.xsd");
            }
            if (attributes != null && !attributes.ContainsKey("id"))
            {
                attributes.TryAdd("id", id.ToString());
            }

            // add header
            sbXml.Append(CreateXmlOpeningElementString(elementName, attributes));

            return sbXml;
        }

        public virtual StringBuilder GetXmlSnippetFromName( ElementName name, string topLevelNodePrefix = "")
        {
            Guard.Against<ArgumentNullException>(name == null, "name cannot be null.");
            StringBuilder sbXml = new StringBuilder();

            // add Name
            sbXml.Append(CreateXmlOpeningElementString(topLevelNodePrefix + "Name"));
            sbXml.Append(CreateXmlElementString("core:Full", name.Full.Trim(), true));

            if (!string.IsNullOrEmpty(name.Short))
            {
                sbXml.Append(CreateXmlElementString("core:Short", name.Short.Trim(), true));
            }
            if (!string.IsNullOrEmpty(name.Suffix))
            {
                sbXml.Append(CreateXmlElementString("core:Suffix", name.Suffix.Trim(), true));
            }
            if (!string.IsNullOrEmpty(name.Prefix))
            {
                sbXml.Append(CreateXmlElementString("core:Prefix", name.Prefix.Trim(), true));
            }
            sbXml.Append(CreateXmlClosingElementString(topLevelNodePrefix + "Name"));
            return sbXml;
        }


        public virtual StringBuilder GetXmlSnippetFromDescription( ElementDescription description, string topLevelNodePrefix = "")
        {
            Guard.Against<ArgumentNullException>(description == null, "description cannot be null.");
            StringBuilder sbXml = new StringBuilder();

            // add Description
            sbXml.Append(CreateXmlOpeningElementString(topLevelNodePrefix + "Description"));
            sbXml.Append(CreateXmlElementString("core:Full", description.Full.Trim(), true));

            if (!string.IsNullOrEmpty(description.Short))
            {
                sbXml.Append(CreateXmlElementString("core:Short", description.Short.Trim(), true));
            }
            sbXml.Append(CreateXmlClosingElementString(topLevelNodePrefix + "Description"));

            return sbXml;
        }

        public virtual XmlElement GetXmlElementFromDescription( ElementDescription description, XmlDocument doc = null, string topLevelNodePrefix = "")
        {
            Guard.Against<ArgumentNullException>(description == null, "description cannot be null.");
            string coreNamespaceValue = NsManager.LookupNamespace("core");
            XNamespace coreNs = coreNamespaceValue;
            XElement root = null;
            if (doc == null)
            {

                doc = new XmlDocument();
                // Create a root element and add all namespaces from helper.NsManager
                root = new XElement("Root",
                    new XAttribute(XNamespace.Xmlns + "core", coreNs)
                );
            }

            // add Description
            var descriptionElement = new XElement(topLevelNodePrefix + "Description");
            root.Add(descriptionElement);
            var fullElement = new XElement(coreNs + "Full", "Some content");
            descriptionElement.Add(fullElement);
            if (!string.IsNullOrEmpty(description.Short))
            {
                var shortElement = new XElement(coreNs + "Short", description.Short.Trim());
                descriptionElement.Add(shortElement);
            }
            // Convert to XmlDocument
            using (var reader = root.CreateReader())
            {
                doc.Load(reader);
            }
            var element = doc.DocumentElement;
            return element;
        }


        public virtual StringBuilder GetXmlSnippetFromElementType( ElementType type, string topLevelNodePrefix = "")
        {
            Guard.Against<ArgumentNullException>(type == null, "type cannot be null.");
            StringBuilder sbXml = new StringBuilder();

            // add type
            sbXml.Append(CreateXmlOpeningElementString(topLevelNodePrefix + "ElementType"));
            sbXml.Append(CreateXmlElementString("core:FullyQualifiedType", type.FullyQualifiedType.Trim(), true));

            if (type.ParentElementType != null)
            {
            }
            if (type.InheritsFrom != null)
            {
            }
            if (type.ChildrenElementTypes != null)
            {
            }
            sbXml.Append(CreateXmlClosingElementString(topLevelNodePrefix + "ElementType"));
            return sbXml;
        }


        protected XmlElement RemoveAllChildXmlnsAttributes(XmlElement element)
        {
            // Remove all xmlns attributes from the element
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.Attributes != null)
                {
                    // Collect attributes to remove to avoid modifying the collection while iterating
                    var attrsToRemove = new List<XmlAttribute>();
                    foreach (XmlAttribute attr in child.Attributes)
                    {
                        if (attr.Prefix == "xmlns" || attr.Name == "xmlns")
                        {
                            attrsToRemove.Add(attr);
                        }
                    }
                    foreach (var attr in attrsToRemove)
                    {
                        child.Attributes.Remove(attr);
                    }
                }
            }
            return element;
        }

    }
}
