﻿using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Domain.Entities.SoftwareApplications
{
    /// <summary>
    /// Represents a C# class generated by LaunchPad Framework.
    /// </summary>
    public class LaunchPadGeneratedClass : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public string SubFolder { get; set; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        public string InheritsFrom { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The id type of the primary key.
        /// </summary>
        public string IdType { get; set; }

        /// <summary>
        /// The list of generated properties that belong to this class.
        /// </summary>
        public IList<LaunchPadGeneratedProperty> GeneratedProperties { get; set; }

        /// <summary>
        /// The list of generated methods that belong to this class.
        /// </summary>
        public IList<LaunchPadGeneratedMethod> GeneratedMethods { get; set; }


        public LaunchPadGeneratedClass()
        {
            GeneratedProperties = new List<LaunchPadGeneratedProperty>();
            GeneratedMethods = new List<LaunchPadGeneratedMethod>();
        }
    }
}
