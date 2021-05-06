using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
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
        /// The version of this module
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Authentication type:
        /// - tenant (default) = During the login, the user have an option to select which tenant they want to login to
        /// - user = Login without tenant selection
        /// - none = Does not require login
        /// </summary>
        public virtual Authentication Authentication { get; set; }

        public virtual SourceControlRepository Repository { get; set; }

        /// <summary>
        /// Contains a dictionary of Templates belonging to this component, keyed by the property set id
        /// </summary>
        public virtual IDictionary<string, TemplateBase> AvailableTemplates { get; set; }

        public IDictionary<string, LaunchPadToken> AvailableTokens { get; set; }

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
        }

    }
}
