using Deploy.LaunchPad.Core.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    [Serializable]
    public partial class ConnectionConfiguration : IConnectionConfiguration
    {
        public virtual List<ILaunchPadConnectionDefinition> Connections { get; set; } = new();

        IReadOnlyList<ILaunchPadConnectionDefinition> IConnectionConfiguration.Connections => Connections;

        string IConnectionConfiguration.DefaultConnectionStringName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IConnectionProvider IConnectionConfiguration.Provider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual ILaunchPadConnectionDefinition? GetOrNull(string name)
        {
            return Connections.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
