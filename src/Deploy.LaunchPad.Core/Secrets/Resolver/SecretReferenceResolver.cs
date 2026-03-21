using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets.Configuration;
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
        private readonly ISecretConfiguration _secretConfiguration;

        public SecretReferenceResolver(ISecretConfiguration secretConfiguration)
        {
            _secretConfiguration = secretConfiguration;
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
            if (settingDefinition.SecretReference == null ||
                !_secretConfiguration.Vaults.ContainsKey(settingDefinition.SecretReference.VaultId)
            )
            {
                return new SecretResolutionResult(settingDefinition.Name);
            }

            ISecretVault vault = _secretConfiguration.Provider.GetSecretVaultById(settingDefinition.SecretReference.VaultId, $"SecretValueResolver.TryResolveAsync for setting {settingDefinition.Name} for tenant {tenantId} and user {userId}");
            var value = await vault.GetValueOrNullFromSecretReferenceAsync(
                settingDefinition.SecretReference,
                settingDefinition,
                cancellationToken);

            if (!string.IsNullOrEmpty(value))
            {
                return new SecretResolutionResult(
                    settingDefinition.SecretReference.FieldName,
                    value,
                    true,
                    settingDefinition.SecretReference
                );
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
