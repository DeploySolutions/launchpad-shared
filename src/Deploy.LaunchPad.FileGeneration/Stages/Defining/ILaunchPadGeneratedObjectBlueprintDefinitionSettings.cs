// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-05-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedObjectBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Structure;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Interface ILaunchPadGeneratedObjectBlueprintDefinitionSettings
    /// </summary>
    public partial interface ILaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The starting folder in which this item is located, relative to its parent object's folder.
        /// The folder structure can be nested deeper than this if required by the assembly code.
        /// For instance, the folder hierarchy of an "angular front-end" component belonging to a parent "collaboration portal" module in a space app
        /// might look like:
        /// [space app folder name]
        /// =&gt; [collaboration portal folder name]
        /// =&gt; angular
        /// If this value is empty, it uses the same starting folder as its parent object.
        /// </summary>
        /// <value>The relative starting path from parent.</value>
        public string RelativeStartingPathFromParent { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// The comma-delimited list of cultures this item can support
        /// </summary>
        /// <value>The supported cultures.</value>
        public string SupportedCultures { get; set; }


        /// <summary>
        /// Contains a dictionary of file or folder exclusions paths that will be applied when assembling
        /// </summary>
        /// <value>The exclusion paths.</value>
        public IDictionary<string, string> ExclusionPaths { get; set; }
    }
}
