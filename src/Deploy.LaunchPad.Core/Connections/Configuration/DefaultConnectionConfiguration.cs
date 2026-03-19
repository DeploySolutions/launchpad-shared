using Deploy.LaunchPad.Core.Connections;
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

        public string DefaultConnectionStringName { get; set; }
        public IConnectionProvider Provider { get; set; }

        public DefaultConnectionConfiguration()
        {
            Provider = new ConnectionProvider();
        }

        public virtual ILaunchPadConnectionDefinition? GetOrNull(string name)
        {
            return Connections.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
