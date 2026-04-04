// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedAppSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace Deploy.LaunchPad.FileGeneration.Stages.Defining
{
    /// <summary>
    /// Interface ILaunchPadGeneratedAppSettings
    /// </summary>
    public partial interface ILaunchPadGeneratedAppSettings
    {

        /// <summary>
        /// Loads the application settings from XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc">The document.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="xmlns">The XMLNS.</param>
        /// <returns>T.</returns>
        public T LoadAppSettingsFromXml<T>(XmlDocument doc, ILogger logger, string xmlns)
           where T : ILaunchPadGeneratedAppSettings, new();

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>JObject.</returns>
        public JObject ToJson();



    }
}
