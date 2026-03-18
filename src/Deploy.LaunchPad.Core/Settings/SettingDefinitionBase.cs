using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Secrets.Reference;
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

        /// <summary>
        /// If this setting has sensitive fields, first attempt to load them from the secret source(s)
        /// before attempting to load from the default value or any other value source. 
        /// This allows for secrets to be stored in a secure vault, and only resolved at runtime when needed.
        /// </summary>
        public virtual ISecretFieldReference SecretReference { get; set; }

        // IMustHaveDisplayName
        public virtual ILocalizableString DisplayName { get; set; }

        // IMustHaveFullName
        public virtual string Name { get; set; }
    }
}