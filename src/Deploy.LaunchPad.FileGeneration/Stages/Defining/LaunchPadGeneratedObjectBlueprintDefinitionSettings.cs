using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Structure;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public enum Authentication { Tenant, User, None }

    [Serializable]
    public partial class LaunchPadGeneratedObjectBlueprintDefinitionSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        public virtual string RelativeStartingPathFromParent { get; set; }

        /// <summary>
        /// The comma-delimited list of cultures this item can support
        /// </summary>
        public virtual string SupportedCultures { get; set; }

        /// <summary>
        /// The version of this blueprint
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Whether this element supports multi-tenancy
        /// </summary>
        public virtual bool MultiTenancyIsEnabled { get; set; }

        /// <summary>
        /// Authentication type:
        /// - tenant (default) = During the login, the user have an option to select which tenant they want to login to
        /// - user = Login without tenant selection
        /// - none = Does not require login
        /// </summary>
        public virtual Authentication Authentication { get; set; }

        public virtual SourceControlRepository Repository { get; set; }

        /// <summary>
        /// Contains a dictionary of Templates belonging to this object, keyed by the template name
        /// </summary>
        public virtual IDictionary<string, TemplateBase> AvailableTemplates { get; set; }

        /// <summary>
        /// Contains a dictionary of Tokens belonging to this object, keyed by the token name
        /// </summary>
        public virtual IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

        // <summary>
        /// Contains a dictionary of file or folder exclusions paths that will be applied when assembling
        /// </summary>
        public virtual IDictionary<string, string> ExclusionPaths { get; set; }

        public LaunchPadGeneratedObjectBlueprintDefinitionSettings()
        {
            Repository = new SourceControlRepository();
            RelativeStartingPathFromParent = string.Empty;
            SupportedCultures = string.Empty;
            Version = string.Empty;
            Authentication = Authentication.Tenant;
            var comparer = StringComparer.OrdinalIgnoreCase;
            AvailableTemplates = new Dictionary<string, TemplateBase>(comparer);
            AvailableTokens = new Dictionary<string, LaunchPadToken>(comparer);
            ExclusionPaths = new Dictionary<string, string>(comparer);
        }

        public LaunchPadGeneratedObjectBlueprintDefinitionSettings(SourceControlRepository repo)
        {
            Repository = repo;
            RelativeStartingPathFromParent = string.Empty;
            SupportedCultures = string.Empty;
            Version = string.Empty;
            Authentication = Authentication.Tenant;
            var comparer = StringComparer.OrdinalIgnoreCase;
            AvailableTemplates = new Dictionary<string, TemplateBase>(comparer);
            AvailableTokens = new Dictionary<string, LaunchPadToken>(comparer);
            ExclusionPaths = new Dictionary<string, string>(comparer);
        }

        public LaunchPadGeneratedObjectBlueprintDefinitionSettings(SourceControlRepository repo, string relativeStartPath)
        {
            Repository = repo;
            RelativeStartingPathFromParent = relativeStartPath;
            SupportedCultures = string.Empty;
            Version = string.Empty;
            Authentication = Authentication.Tenant;
            var comparer = StringComparer.OrdinalIgnoreCase;
            AvailableTemplates = new Dictionary<string, TemplateBase>(comparer);
            AvailableTokens = new Dictionary<string, LaunchPadToken>(comparer);
            ExclusionPaths = new Dictionary<string, string>(comparer);
        }

    }
}
