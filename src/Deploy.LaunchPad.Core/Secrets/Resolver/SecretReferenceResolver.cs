using Deploy.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets.Resolver
{
    /// <summary>
    /// A class that resolves the value of a secret setting/field by looking up the setting's secret sources 
    /// (if any) and querying the appropriate secret vaults, in order, for the value of the setting.
    /// </summary>
    public partial class SecretReferenceResolver : ISecretReferenceResolver
    {
        private readonly ISecretProvider _provider;

        public SecretReferenceResolver(ISecretProvider provider)
        {
            _provider = provider;
        }

        public virtual ISecretResolutionResult TryResolve(
            ISettingDefinition settingDefinition,
            Guid? tenantId = null,
            Guid? userId = null)
        {
            return TryResolveAsync(settingDefinition, tenantId, userId).Result;
        }

        public virtual async Task<ISecretResolutionResult> TryResolveAsync(
            ISettingDefinition settingDefinition,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            if (settingDefinition.SecretSources == null || settingDefinition.SecretSources.Count == 0)
            {
                return new SecretResolutionResult(settingDefinition.Name);
            }

            foreach (var source in settingDefinition.SecretSources)
            {
                if (!_provider.SecretVaults.ContainsKey(source.VaultId))
                {
                    continue;
                }
                ISecretVault vault = _provider.GetSecretVaultById(source.VaultId, $"SecretValueResolver.TryResolveAsync for setting {settingDefinition.Name} for tenant {tenantId} and user {userId}");
                var value = await vault.GetValueOrNullFromSecretReferenceAsync(
                    source,
                    settingDefinition,
                    cancellationToken);

                if (!string.IsNullOrEmpty(value))
                {
                    return new SecretResolutionResult(
                        source.FieldName,
                        value,
                        true,
                        source
                    );
                }
            }
            return new SecretResolutionResult(settingDefinition.Name);
        }

        ISecretResolutionResult ISecretReferenceResolver.TryResolveBySettingName(string settingName, Guid? tenantId, Guid? userId)
        {
            ISettingDefinition definition = new SettingDefinition(settingName,null);
            return TryResolve(definition, tenantId, userId);

        }

        Task<ISecretResolutionResult> ISecretReferenceResolver.TryResolveBySettingNameAsync(string settingName, Guid? tenantId, Guid? userId, CancellationToken cancellationToken)
        {
            ISettingDefinition definition = new SettingDefinition(settingName, null);
            return TryResolveAsync(definition, tenantId, userId, cancellationToken);
        }
    }
}
