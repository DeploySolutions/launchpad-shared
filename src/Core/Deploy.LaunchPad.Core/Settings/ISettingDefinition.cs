using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Util.Metadata;
using Deploy.LaunchPad.Core.Secrets.Reference;
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
        /// If this setting is a sensitive field, first attempt to load it from the secret source
        /// before attempting to load from the default value or any other value source. 
        /// This allows for secrets to be stored in a secure vault, and only resolved at runtime when needed.
        /// </summary>
        ISecretFieldReference SecretReference { get; }
    }
}