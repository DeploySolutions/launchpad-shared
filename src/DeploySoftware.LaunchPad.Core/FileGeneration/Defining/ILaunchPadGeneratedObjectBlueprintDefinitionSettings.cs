using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
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

        public SourceControlRepository Repository { get; set; }

        /// <summary>
        /// Contains a dictionary of Templates belonging to this object, keyed by the template name
        /// </summary>
        public IDictionary<string, TemplateBase> AvailableTemplates { get; set; }

        /// <summary>
        /// Contains a dictionary of Tokens belonging to this object, keyed by the token name
        /// </summary>
        public IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

        /// <summary>
        /// Contains a dictionary of file or folder exclusions paths that will be applied when assembling
        /// </summary>
        public IDictionary<string, string> ExclusionPaths { get; set; }
    }
}
