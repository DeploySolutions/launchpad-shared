using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Secrets;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    /// <summary>
    /// Represents a "null" or missing setting definition.
    /// </summary>
    public sealed class NullableSettingDefinition : SettingDefinitionBase
    {
        public override ISettingClientVisibilityProvider ClientVisibilityProvider { get; set; } = null;
        public override object CustomData { get; set; } = null;
        public override string DefaultValue { get; set; } = null;
        public override ILocalizableString Description { get; set; } = null;
        public override ISettingDefinitionGroup Group { get; set; } = null;
        public override bool IsEncrypted { get; set; } = false;
        public override bool IsInherited { get; set; } = false;
        public override SettingScopes Scopes { get; set; } = SettingScopes.None;
        public override IReadOnlyList<SettingSecretProviderDescriptor> SecretSources { get; set; } = Array.Empty<SettingSecretProviderDescriptor>();

        // IMustHaveDisplayName
        public override ILocalizableString DisplayName { get; set; }

        // IMustHaveFullName
        public override string Name { get; set; } = string.Empty;

        // Optionally, add a static property for convenience
        public static NullableSettingDefinition Instance { get; set; } = new NullableSettingDefinition();
    }
}