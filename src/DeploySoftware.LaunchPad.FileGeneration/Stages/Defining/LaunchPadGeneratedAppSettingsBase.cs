using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages.Defining
{
    [Serializable]
    public abstract partial class LaunchPadGeneratedAppSettingsBase : ILaunchPadGeneratedAppSettings
    {

        public LaunchPadGeneratedAppSettingsBase()
        {

        }

        public override string ToString()
        {
            JObject o = (JObject)JToken.FromObject(this);
            Console.WriteLine(o.ToString());
            return o.ToString();
        }


        /// <summary>
        /// Load an instance of this AppSettings object using values provided in an XML document.
        /// Uses the Newtonsoft JSON conversion rules which are here: https://www.newtonsoft.com/json/help/html/ConvertingJSONandXML.htm
        /// CDATA elements will be stripped out automatically.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>A populated instance of this object, from the given XML.</returns>
        public T LoadAppSettingsFromXml<T>(XmlDocument doc)
            where T : ILaunchPadGeneratedAppSettings, new()
        {
            T appSettings = new T();
            if (doc != null)
            {
                // create ns manager and get the AppSettings node
                XmlNode root = doc.DocumentElement;
                XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);
                xmlnsManager.AddNamespace("sar", "https://assets.deploy.solutions/SpaceAppsRAD/Schemas/Modules/SpaceAppHub.xsd");
                XmlNode xnl = root.SelectSingleNode("//sar:AppSettings", xmlnsManager);

                // remove CDATA from all elements in the node
                var parsedDoc = XElement.Parse(xnl.InnerXml);
                var node_cdata = parsedDoc.DescendantNodes().OfType<XCData>().ToList();
                foreach (var node in node_cdata)
                {
                    node.Parent.Add(node.Value);
                    node.Remove();
                }

                // Using Newtonsoft JSON conversions: get the JSON text from XML and then serialize it into an object instance
                string jsonText = JsonConvert.SerializeXNode(parsedDoc, Newtonsoft.Json.Formatting.Indented, true);
                appSettings = JsonConvert.DeserializeObject<T>(jsonText);
            }
            return appSettings;
        }

        /// <summary>
        /// Converts this object to its own unique AppSettings JSON format.
        /// </summary>
        /// <returns>A JObject representing this object properties.</returns>
        public JObject ToJson()
        {
            return (JObject)JToken.FromObject(this);
        }

    }
}
