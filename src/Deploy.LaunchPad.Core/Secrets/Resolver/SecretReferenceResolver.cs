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
            ISettingDefinition definition,
            Guid? tenantId = null,
            Guid? userId = null)
        {
            return TryResolveAsync(definition, tenantId, userId).Result;
        }

        public virtual async Task<ISecretResolutionResult> TryResolveAsync(
            ISettingDefinition definition,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            if (definition.SecretReference == null ||
                !_secretConfiguration.Vaults.ContainsKey(definition.SecretReference.VaultId)
            )
            {
                return new SecretResolutionResult(definition.Name);
            }

            ISecretVault vault = _secretConfiguration.Provider.GetSecretVaultById(definition.SecretReference.VaultId, $"SecretValueResolver.TryResolveAsync for setting {definition.Name} for tenant {tenantId} and user {userId}");
            var value = await vault.GetValueOrNullFromSecretReferenceAsync(
                definition.SecretReference,
                definition,
                cancellationToken);

            if (!string.IsNullOrEmpty(value))
            {
                return new SecretResolutionResult(
                    definition.SecretReference.FieldName,
                    value,
                    true,
                    definition.SecretReference
                );
            }
            return new SecretResolutionResult(definition.Name);
        }

        ISecretResolutionResult ISecretReferenceResolver.TryResolveBySecretName(string secretName, Guid? tenantId, Guid? userId)
        {
            ISettingDefinition definition = new SettingDefinition(secretName,null);
            return TryResolve(definition, tenantId, userId);

        }

        Task<ISecretResolutionResult> ISecretReferenceResolver.TryResolveBySecretNameAsync(string secretName, Guid? tenantId, Guid? userId, CancellationToken cancellationToken)
        {
            ISettingDefinition definition = new SettingDefinition(secretName, null);
            return TryResolveAsync(definition, tenantId, userId, cancellationToken);
        }
    }
}
