using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Secrets;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    /// <summary>
    /// Base class for all setting definitions.
    /// </summary>
    public abstract partial class SettingDefinitionBase : ISettingDefinition
    {
        public virtual ISettingClientVisibilityProvider ClientVisibilityProvider { get; set; }
        public virtual object CustomData { get; set; }
        public virtual string DefaultValue { get; set; }
        public virtual ILocalizableString Description { get; set; }
        public virtual ISettingDefinitionGroup Group { get; set; }
        public virtual bool IsEncrypted { get; set; }
        public virtual bool IsInherited { get; set; }
        public virtual SettingScopes Scopes { get; set; }
        public virtual IReadOnlyList<SettingSecretProviderDescriptor> SecretSources { get; set; } = Array.Empty<SettingSecretProviderDescriptor>();

        // IMustHaveDisplayName
        public virtual ILocalizableString DisplayName { get; set; }

        // IMustHaveFullName
        public virtual string Name { get; set; }
    }
}