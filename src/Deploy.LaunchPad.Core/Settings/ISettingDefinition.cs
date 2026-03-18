using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets.Reference;
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

        /// <summary>
        /// If this setting has sensitive fields, first attempt to load them from the secret source(s)
        /// before attempting to load from the default value or any other value source. 
        /// This allows for secrets to be stored in a secure vault, and only resolved at runtime when needed.
        /// </summary>
        IReadOnlyList<ISecretFieldReference> SecretSources { get; }
    }
}