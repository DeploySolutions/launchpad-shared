using System;
using System.Collections.Generic;
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


        public XmlHelper()
        {
            XmlNamespaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            NsManager = new XmlNamespaceManager(XmlDocument.NameTable);
        }

        public XmlHelper(IDictionary<string, string> xmlNamespaces)
        {
            Guard.Against<ArgumentNullException>(xmlNamespaces == null, "xmlNamespaces cannot be null");
            _nsManager = new XmlNamespaceManager(XmlDocument.NameTable);
            foreach (var xmlNamespace in xmlNamespaces)
            {
                _nsManager.AddNamespace(xmlNamespace.Key, xmlNamespace.Value);
            }
            _xmlNamespaces = _nsManager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
        }

        public XmlHelper(IDictionary<string,string> xmlNamespaces, XmlDocument xmlDoc)
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
        public virtual XmlDocument LoadFromFilePath(string xmlDocumentFilePath)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(xmlDocumentFilePath), "xmlDocumentFilePath cannot be null or empty");
            _xmlDoc = LoadFromFilePath(xmlDocumentFilePath, null); // Call the overloaded method with null namespaces
            return _xmlDoc; // Return the loaded XmlDocument
        }

        /// <summary>
        /// Load the XML document from a file path and apply the provided custom XML namespaces (if any).
        /// If no namespaces are provided, it will use the default namespaces defined in the class.
        /// </summary>
        /// <param name="xmlDocumentFilePath"></param>
        /// <param name="xmlNamespaces"></param>
        /// <returns></returns>
        public virtual XmlDocument LoadFromFilePath(string xmlDocumentFilePath, IDictionary<string, string> xmlNamespaces = null)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(xmlDocumentFilePath), "xmlDocumentFilePath cannot be null or empty");
            _xmlDoc.Load(xmlDocumentFilePath);
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
        /// Select the XML node using the provided XPath and automatically using the appropriate namespace manager.
        /// An optional starting node can be provided, alternatively the root document will be used for assessing the XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="startingNode"></param>
        /// <returns></returns>
        public virtual XmlNode FindNode(string xpath, XmlNode startingNode = null, bool shouldIgnoreCase = false)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(xpath), "xpath cannot be null or empty.");

            xpath = PreProcessXpath(xpath);

            var contextNode = startingNode ?? _xmlDoc;            
            var node = contextNode.SelectSingleNode(xpath, NsManager);
            if (node != null || !shouldIgnoreCase)
            {
                return node;
            }

            // If the node is not found, try a case-insensitive search
            string insensitiveXpath = ToCaseInsensitiveXPath(xpath);
            return contextNode.SelectSingleNode(insensitiveXpath, NsManager);
        }

        /// <summary>
        /// Select the XML nodes using the provided XPath and automatically using the appropriate namespace manager.
        /// An optional starting node can be provided, alternatively the root document will be used for assessing the XPath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="startingNode"></param>
        /// <returns></returns>
        public virtual XmlNodeList FindNodes(string xpath, XmlNode startingNode = null, bool shouldIgnoreCase = false)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(xpath), "xpath cannot be null or empty.");

            xpath = PreProcessXpath(xpath);

            var contextNode = startingNode ?? _xmlDoc;
            var nodes = contextNode.SelectNodes(xpath, NsManager);
            if ((nodes != null && nodes.Count > 0) || !shouldIgnoreCase)
            {
                return nodes;
            }

            // If not found, try case-insensitive search
            string insensitiveXpath = ToCaseInsensitiveXPath(xpath);
            var insensitiveNodes = contextNode.SelectNodes(insensitiveXpath, NsManager);
            return insensitiveNodes;
        }

        protected virtual string PreProcessXpath(string xpath)
        {
            // Only preprocess if a default prefix exists
            if (_xmlNamespaces != null && _xmlNamespaces.Count > 0)
            {
                // Use the first prefix as the default (e.g., "def")
                string defaultPrefix = _xmlNamespaces.Keys.First();
                // Regex: match element names not already prefixed (skip ., @, :, (), [], and //)
                xpath = System.Text.RegularExpressions.Regex.Replace(
                    xpath,
                    @"(?<=/|^)(?!@|\.|//|[\w-]+:)([\w-]+)",
                    $"{defaultPrefix}:$1"
                );
            }
            return xpath;
        }

        protected virtual string ToCaseInsensitiveXPath(string xpath)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(xpath), "xpath cannot be null or empty.");
            const string upperAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerAlphabet = "abcdefghijklmnopqrstuvwxyz";

            bool isDescendant = xpath.StartsWith("//");
            bool isAbsolute = xpath.StartsWith("/");
            var segments = xpath.TrimStart('/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
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
            Guard.Against<ArgumentNullException>(currentNode == null, "currentNode cannot be null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(attributeName), "attributeName cannot be null or empty.");
            XmlAttribute attribute = currentNode.Attributes[attributeName];
            return attribute;
        }

        public virtual string GetCleanTextFromXhtml(string xhtml)
        {
            return xhtml.Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty).Trim();
        }
    }
}
