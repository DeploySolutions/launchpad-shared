using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedObject
    {
        string Description { get; set; }
        Guid Id { get; set; }
        string IdType { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }

        string NamePrefix { get; set; }
        string NameSuffix { get; set; }

        public GitHubRepository Repository { get; set; }
        public ILaunchPadGeneratedObjectInheritance Inheritance { get; set; }

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