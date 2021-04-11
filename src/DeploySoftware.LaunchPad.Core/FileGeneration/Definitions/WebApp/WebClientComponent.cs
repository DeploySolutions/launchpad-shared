﻿using System;
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
        /// Brand configuration of the app. Thsi could include name, icon, logo and/or theme color.
        /// </summary>
        [XmlElement]
        public LaunchPadBrand Brand { get; set; }

        /// <summary>
        /// The top section of the side navigation bar
        /// </summary>
        [XmlElement]
        public IDictionary<string, LaunchPadNavigationItem> TopNavigations { get; set; }

        /// <summary>
        /// The bottom section of the side navigation bar
        /// </summary>
        [XmlElement]
        public IDictionary<string, LaunchPadNavigationItem> BottomNavigations { get; set; }

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
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public override bool CheckValidity()
        {
            bool isValid = false;
            if (BlueprintDefinition != null
                && StaticWebPages != null && StaticWebPages.Count > 0
                && Stylesheets != null && Stylesheets.Count > 0
                && TopNavigations != null && TopNavigations.Count > 0
                && BottomNavigations != null && BottomNavigations.Count > 0
                && Brand != null
            )
            {
                isValid = true;
            }
            return isValid;
        }

        public WebClientComponent() : base()
        {
            Brand = new LaunchPadBrand();
            var comparer = StringComparer.OrdinalIgnoreCase;
            StaticWebPages = new Dictionary<string, LaunchPadGeneratedStaticWebPage>(comparer);
            Stylesheets = new Dictionary<string, LaunchPadGeneratedStylesheet>(comparer);
            TopNavigations = new Dictionary<string, LaunchPadNavigationItem>(comparer);
            BottomNavigations = new Dictionary<string, LaunchPadNavigationItem>(comparer);
        }
    }
}
