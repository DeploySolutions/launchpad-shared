// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedDtoPropertySet.cs" company="Deploy Software Solutions, inc.">
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
    /// Represents a set of properties that can be generated for a Dto
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedDtoPropertySet
    {

        /// <summary>
        /// The unique identifier of this property set, which can be referred to by generated DTOs
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id { get; set; }

        /// <summary>
        /// A dictionary containing the names of the base properties that will be required
        /// </summary>
        /// <value>The required base property names.</value>
        public virtual IDictionary<string, string> RequiredBasePropertyNames { get; set; }

        /// <summary>
        /// A dictionary containing the names of the base properties that will be optional
        /// </summary>
        /// <value>The optional base property names.</value>
        public virtual IDictionary<string, string> OptionalBasePropertyNames { get; set; }

        /// <summary>
        /// Gets the required properties.
        /// </summary>
        /// <value>The required properties.</value>
        public virtual IDictionary<string, string> RequiredProperties => new DictionaryHelper().MergeDictionaries(RequiredBasePropertyNames, RequiredCustomPropertyNames);

        /// <summary>
        /// A dictionary containing the names of the custom properties that will be required
        /// </summary>
        /// <value>The required custom property names.</value>
        public virtual IDictionary<string, string> RequiredCustomPropertyNames { get; set; }

        /// <summary>
        /// A dictionary containing the names of the custom properties that will be optional
        /// </summary>
        /// <value>The optional custom property names.</value>
        public virtual IDictionary<string, string> OptionalCustomPropertyNames { get; set; }

        /// <summary>
        /// Gets the optional properties.
        /// </summary>
        /// <value>The optional properties.</value>
        public virtual IDictionary<string, string> OptionalProperties => new DictionaryHelper().MergeDictionaries(OptionalBasePropertyNames, OptionalCustomPropertyNames);

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedDtoPropertySet"/> class.
        /// </summary>
        public LaunchPadGeneratedDtoPropertySet()
        {
            Id = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            RequiredBasePropertyNames = new Dictionary<string, string>(comparer);
            OptionalBasePropertyNames = new Dictionary<string, string>(comparer);
            RequiredCustomPropertyNames = new Dictionary<string, string>(comparer);
            OptionalCustomPropertyNames = new Dictionary<string, string>(comparer);
        }
    }
}
