using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    [Serializable]
    public partial class DefaultConnectionConfiguration : IConnectionConfiguration
    {
        public virtual List<ILaunchPadConnectionDefinition> Connections { get; set; } = new();

        IReadOnlyList<ILaunchPadConnectionDefinition> IConnectionConfiguration.Connections => Connections;

        public virtual string DefaultConnectionStringName { get; set; }
        public virtual IConnectionProvider Provider { get; set; }

        public DefaultConnectionConfiguration(ISecretProvider secretProvider)
        {
            Provider = new ConnectionProvider(secretProvider);
        }

        public virtual ILaunchPadConnectionDefinition? GetOrNull(string name)
        {
            return Connections.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
