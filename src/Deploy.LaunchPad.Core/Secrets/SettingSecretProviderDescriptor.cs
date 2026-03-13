using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets
{
    public sealed class SettingSecretProviderDescriptor
    {

        public string VaultId { get; set; }

        public string Key { get; init; } = default;

        public bool Optional { get; init; } = true;
    }
}
