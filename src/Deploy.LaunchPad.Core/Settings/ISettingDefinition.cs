using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Deploy.LaunchPad.Core.Configuration
{

    public partial interface ISettingDefinition : IMustHaveDisplayName, IMustHaveFullName
    {
        
        ISettingClientVisibilityProvider ClientVisibilityProvider { get; }
        object CustomData { get; }
        string DefaultValue { get; }
        ILocalizableString Description { get;  }
        ISettingDefinitionGroup Group { get;  }
        bool IsEncrypted { get; }
        bool IsInherited { get;  }
        SettingScopes Scopes { get;  }

        IReadOnlyList<SettingSecretProviderDescriptor> SecretSources { get; }
    }
}