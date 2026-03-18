using Deploy.LaunchPad.Core.Application.Connections.Database.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Application.Connections.Database.Resolutions
{
    /// <summary>
    /// Interface boundary between the Database Connection Definition and 
    /// the actual resolved settings at runtime, including secrets.
    /// </summary>
    public partial interface IDatabaseConnectionResolver
    {
        Task<ResolvedDatabaseConnection> ResolveAsync(
            ILaunchPadDatabaseConnectionDefinition definition,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);

        Task<ResolvedDatabaseConnection> ResolveAsync(
            string connectionName, 
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);
    }
}
