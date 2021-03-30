﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a set of related static web pages or resources (.htm or CSS etc) generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedStaticWebModule : 
        LaunchPadGeneratedModule<LaunchPadGeneratedStaticWebModuleConfiguration, 
            LaunchPadGeneratedComponent<LaunchPadGeneratedConfiguration>,
            LaunchPadGeneratedConfiguration>
    {
        /// <summary>
        /// The dictionary of static web pages that belong to this module.
        /// </summary>
        public virtual IDictionary<string,LaunchPadGeneratedStaticWebPage> StaticWebPages { get; set; }

        /// <summary>
        /// The dictionary of dynamicaly-generated stylesheets (LESS or SASS compiled into CSS) that belong to this module.
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedStylesheet> Stylesheets { get; set; }


        public LaunchPadGeneratedStaticWebModule() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            StaticWebPages = new Dictionary<string,LaunchPadGeneratedStaticWebPage>(comparer);
            Stylesheets = new Dictionary<string,LaunchPadGeneratedStylesheet>(comparer);
        }

    }
}
