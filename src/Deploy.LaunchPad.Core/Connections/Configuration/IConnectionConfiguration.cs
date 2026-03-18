using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Secrets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    public partial interface IConnectionConfiguration
    {
        /// <summary>
        /// Gets/sets the name of the default connection string used by ORM module.
        /// It must be the key of a Connection defined in the Connections dictionary of this configuration object.
        /// </summary>
        public string DefaultConnectionStringName { get; set; }

        IReadOnlyList<ILaunchPadConnectionDefinition> Connections { get; }

        /// <summary>
        /// The connection provider
        /// </summary>
        IConnectionProvider Provider { get; set; }

        ILaunchPadConnectionDefinition? GetOrNull(string name);
    }
}
