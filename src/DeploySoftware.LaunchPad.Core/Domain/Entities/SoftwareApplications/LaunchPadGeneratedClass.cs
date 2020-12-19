﻿using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Domain.Entities.SoftwareApplications
{
    /// <summary>
    /// Represents a C# class generated by LaunchPad Framework.
    /// </summary>
    public class LaunchPadGeneratedClass : LaunchPadGeneratedObjectBase
    {

        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public string NameSuffix { get; set; }

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
        /// The list of base LaunchPad properties that belong to this class (received through DomainEntity or other base class inheritance).
        /// </summary>
        public IList<LaunchPadGeneratedProperty> BaseProperties { get; set; }

        /// <summary>
        /// The list of custom properties that belong to this class (that were not inherited).
        /// </summary>
        public IList<LaunchPadGeneratedProperty> CustomProperties { get; set; }

        /// <summary>
        /// The list of base methods that belong to this class (received through DomainEntity or other base class inheritance).
        /// </summary>
        public IList<LaunchPadGeneratedMethod> BaseMethods { get; set; }

        /// <summary>
        /// The list of custom methods that belong to this class (that were not inherited).
        /// </summary>
        public IList<LaunchPadGeneratedMethod> CustomMethods { get; set; }

        public LaunchPadGeneratedClass()
        {
            BaseProperties = new List<LaunchPadGeneratedProperty>();
            CustomProperties = new List<LaunchPadGeneratedProperty>();
            BaseMethods = new List<LaunchPadGeneratedMethod>();
            CustomMethods = new List<LaunchPadGeneratedMethod>();
        }
    }
}
