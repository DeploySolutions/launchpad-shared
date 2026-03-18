using Deploy.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets.Resolver
{
    /// <summary>
    /// Resolves the value of a secret setting/field by looking up the setting's secret sources 
    /// (if any) and querying the appropriate secret vaults, in order, for the value of the setting.
    /// </summary>
    public partial interface ISecretReferenceResolver
    {
        ISecretResolutionResult TryResolve(
            ISettingDefinition settingDefinition,
            Guid? tenantId = null,
            Guid? userId = null);

        Task<ISecretResolutionResult> TryResolveAsync(
            ISettingDefinition settingDefinition,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);

        ISecretResolutionResult TryResolveBySettingName(
            string settingName,
            Guid? tenantId = null,
            Guid? userId = null);

        Task<ISecretResolutionResult> TryResolveBySettingNameAsync(
        string settingName,
        Guid? tenantId = null,
        Guid? userId = null,
        CancellationToken cancellationToken = default);

    }
    
}
