using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Connections.Database.Definitions;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    public partial interface IConnectionConfiguration
    {
        /// <summary>
        /// Connection provider.
        /// </summary>
        IConnectionProvider Provider { get; init; }

        /// <summary>
        /// Gets/sets the name of the default database connection string used by ORM module.
        /// It must be the key of a Connection defined in the Connections dictionary of this configuration object.
        /// </summary>
        public ILaunchPadDatabaseConnectionDefinition DefaultDatabaseConnection { get; }

        public string DefaultConnectionStringName { get; }

        IDictionary<string, ILaunchPadConnectionDefinition> Connections { get; }

        ILaunchPadConnectionDefinition? GetOrNull(string name);
    }
}
