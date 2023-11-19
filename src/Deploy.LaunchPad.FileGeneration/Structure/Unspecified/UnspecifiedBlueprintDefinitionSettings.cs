// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="UnspecifiedBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class UnspecifiedBlueprintDefinitionSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    public partial class UnspecifiedBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The list of setting Key Value Pairs
        /// </summary>
        /// <value>The settings.</value>
        public virtual IDictionary<string, string> Settings { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedBlueprintDefinitionSettings"/> class.
        /// </summary>
        public UnspecifiedBlueprintDefinitionSettings() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Settings = new Dictionary<string, string>(comparer);
        }
    }
}
