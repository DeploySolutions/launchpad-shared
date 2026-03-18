using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections.Configuration
{
    [Serializable]
    public partial class ConnectionConfiguration : IConnectionConfiguration
    {
        public virtual List<ILaunchPadConnectionDefinition> Connections { get; set; } = new();

        IReadOnlyList<ILaunchPadConnectionDefinition> IConnectionConfiguration.Connections => Connections;

        public virtual ILaunchPadConnectionDefinition? GetOrNull(string name)
        {
            return Connections.FirstOrDefault(x =>
                string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
