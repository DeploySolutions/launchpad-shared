using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedObject
    {
        string Description { get; set; }
        string Id { get; set; }
        string IdType { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }

        string NamePrefix { get; set; }
        string NameSuffix { get; set; }
        string ObjectTypeName { get; set; }

        string ObjectTypeFullName { get; set; }

        string ObjectTypeAssemblyName { get; set; }

        public GitHubRepository Repository { get; set; }

        /// <summary>
        /// Contains a dictionary of Templates belonging to this object, keyed by the template name
        /// </summary>
        public IDictionary<string, TemplateBase> AvailableTemplates { get; set; }

        /// <summary>
        /// Contains a dictionary of Tokens belonging to this object, keyed by the token name
        /// </summary>
        public IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

    }
}