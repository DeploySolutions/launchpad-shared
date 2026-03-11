using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    public partial interface ISettingDefinition : IMustHaveDisplayName, IMustHaveFullName
    {
        ISettingClientVisibilityProvider ClientVisibilityProvider { get; set; }
        object CustomData { get; set; }
        string DefaultValue { get; set; }
        ILocalizableString Description { get; set; }
        ISettingDefinitionGroup Group { get; set; }
        bool IsEncrypted { get; set; }
        bool IsInherited { get; set; }
        SettingScopes Scopes { get; set; }

        IReadOnlyList<SettingSecretProviderDescriptor> SecretSources { get; }
    }
}