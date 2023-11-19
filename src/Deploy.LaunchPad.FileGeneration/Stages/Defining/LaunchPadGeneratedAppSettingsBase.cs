// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedAppSettingsBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Deploy.LaunchPad.FileGeneration.Stages.Defining
{
    /// <summary>
    /// Class LaunchPadGeneratedAppSettingsBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.Defining.ILaunchPadGeneratedAppSettings" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.Defining.ILaunchPadGeneratedAppSettings" />
    [Serializable]
    public abstract partial class LaunchPadGeneratedAppSettingsBase : ILaunchPadGeneratedAppSettings
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedAppSettingsBase"/> class.
        /// </summary>
        public LaunchPadGeneratedAppSettingsBase()
        {
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="doc">The document.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="xmlns">The XMLNS.</param>
        /// <returns>A populated instance of this object, from the given XML.</returns>
        public T LoadAppSettingsFromXml<T>(XmlDocument doc, ILogger logger = null, string xmlns = "https://assets.deploy.solutions/SpaceAppsRAD/Schemas/AppSettings")
            where T : ILaunchPadGeneratedAppSettings, new()
        {
            T appSettings = new T();
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            if (doc != null)
            {
                logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Try to load AppSettings node...");
                // create ns manager and get the AppSettings node
                XmlNode root = doc.DocumentElement;
                XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);
                xmlnsManager.AddNamespace("sar", xmlns);
                XmlNode xnl = null;
                try
                {

                    xnl = root.SelectSingleNode("//sar:AppSettings", xmlnsManager);
                }
                catch (Exception ex)
                {
                    logger.Error(string.Format("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Error while selecting root AppSettings node, message was '{0}'",
                        ex.Message
                    ));
                }
                if (xnl != null)
                {
                    logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Found AppSettings node. Parsing doc to remove CDATA...");

                    // remove CDATA from all elements in the node
                    var parsedDoc = XElement.Parse(xnl.InnerXml);
                    if (parsedDoc != null)
                    {
                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => parsedDoc element is not null. Removing CDATA...");
                        var node_cdata = parsedDoc.DescendantNodes().OfType<XCData>().ToList();
                        foreach (var node in node_cdata)
                        {
                            node.Parent.Add(node.Value);
                            node.Remove();
                        }
                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Parsed XML document:");
                        logger.Debug(parsedDoc.ToString());
                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Removed CDATA. Attempting to serialize object to JSON...");

                        // Using Newtonsoft JSON conversions: get the JSON text from XML and then serialize it into an object instance
                        string jsonText = JsonConvert.SerializeXNode(parsedDoc, Newtonsoft.Json.Formatting.Indented, true);

                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Serialized object to JSON. Json text is: ");
                        logger.Debug(jsonText);
                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Attempting to deserialize JSON to c# class...");

                        appSettings = JsonConvert.DeserializeObject<T>(jsonText);
                        logger.Debug("LaunchPadGeneratedAppSettingsBase.LoadAppSettingsFromXml() => Successfully deserialized JSON to c# class.");

                    }

                }

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
