﻿using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a static website component generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class WebClientComponent 
        : LaunchPadGeneratedComponent<WebClientBlueprintDefinitionSettings, WebClientBlueprintDefinitionInstructions>
    {

        /// <summary>
        /// The list of static web pages that belong to this module.
        /// </summary>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStaticWebPage> StaticWebPages { get; set; }

        /// <summary>
        /// The dictionary of dynamic stylesheets (.less or .sass) that belong to this module.
        /// </summary>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStylesheet> Stylesheets { get; set; }

        /// <summary>
        /// Backend middleware that supports this web app project could be ASPBoilerplate or ASPNetZero(Default)
        /// </summary>
        public string SupportedFramework { get; set; }


        /// <summary>
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public override bool CheckValidity()
        {
            bool isValid = false;
            if (BlueprintDefinition != null
                && StaticWebPages != null && StaticWebPages.Count > 0
                && Stylesheets != null && Stylesheets.Count > 0
            )
            {
                isValid = true;
            }
            return isValid;
        }

        public WebClientComponent() : base(NullLogger.Instance)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            StaticWebPages = new Dictionary<string, LaunchPadGeneratedStaticWebPage>(comparer);
            Stylesheets = new Dictionary<string, LaunchPadGeneratedStylesheet>(comparer);
        }

        public WebClientComponent(ILogger logger) : base(logger)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            StaticWebPages = new Dictionary<string, LaunchPadGeneratedStaticWebPage>(comparer);
            Stylesheets = new Dictionary<string, LaunchPadGeneratedStylesheet>(comparer);
            SupportedFramework = "ASPNetZero";
        }
    }
}
