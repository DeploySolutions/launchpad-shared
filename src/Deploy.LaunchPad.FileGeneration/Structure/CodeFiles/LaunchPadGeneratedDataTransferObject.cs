﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedDataTransferObject.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a C# DTO generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedDataTransferObject : LaunchPadGeneratedObjectBase
    {

        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        /// <value>The visual studio configuration.</value>
        public virtual VisualStudioClassConfiguration VisualStudioConfig { get; set; }

        /// <summary>
        /// The dictionary of unique base properties that belong to this DTO (which may be identical to the domain entity, or not)
        /// </summary>
        /// <value>The base properties.</value>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> BaseProperties { get; set; }

        /// <summary>
        /// The dictionary of unique custom properties that belong to this DTO (which may be identical to the domain entity, or not)
        /// </summary>
        /// <value>The custom properties.</value>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> Properties => new DictionaryHelper().MergeDictionaries(BaseProperties, CustomProperties);

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        /// <value>The namespace.</value>
        public virtual string Namespace { get; set; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        /// <value>The inherits from.</value>
        public virtual string InheritsFrom { get; set; }

        /// <summary>
        /// The ID of the property set which applies to this DTO
        /// </summary>
        /// <value>The property set identifier.</value>
        public virtual string PropertySetId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedDataTransferObject"/> class.
        /// </summary>
        public LaunchPadGeneratedDataTransferObject() : base()
        {
            Annotations = string.Empty;
            Namespace = string.Empty;
            InheritsFrom = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            BaseProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            VisualStudioConfig = new VisualStudioClassConfiguration();
            PropertySetId = string.Empty;
        }
    }
}
