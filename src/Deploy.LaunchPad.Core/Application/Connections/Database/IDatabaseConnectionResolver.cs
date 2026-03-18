using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Application.Connections.Database
{
    /// <summary>
    /// Interface boundary between the Database Connection Definition and 
    /// the actual resolved settings at runtime, including secrets.
    /// </summary>
    public partial interface IDatabaseConnectionResolver
    {
        Task<ResolvedDatabaseConnection> ResolveAsync(
            ILaunchPadDatabaseConnectionDefinition definition,
            CancellationToken cancellationToken = default);

        Task<ResolvedDatabaseConnection> ResolveAsync(
            string connectionName,
            CancellationToken cancellationToken = default);
    }
}
