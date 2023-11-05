﻿using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Structure;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public interface ILaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The starting folder in which this item is located, relative to its parent object's folder.
        /// The folder structure can be nested deeper than this if required by the assembly code.
        /// For instance, the folder hierarchy of an "angular front-end" component belonging to a parent "collaboration portal" module in a space app 
        /// might look like:
        /// [space app folder name]
        ///     => [collaboration portal folder name]
        ///         => angular
        /// If this value is empty, it uses the same starting folder as its parent object.
        /// </summary>
        public string RelativeStartingPathFromParent { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The comma-delimited list of cultures this item can support
        /// </summary>
        public string SupportedCultures { get; set; }


        /// <summary>
        /// Contains a dictionary of file or folder exclusions paths that will be applied when assembling
        /// </summary>
        public IDictionary<string, string> ExclusionPaths { get; set; }
    }
}
