using Deploy.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    public partial class EnvironmentSettingsSecretProvider : SecretProviderBase
    {
        public override SecretProviderType ProviderType => SecretProviderType.EnvironmentVariable;

        public override string CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, string value, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetJsonFromSecretVaultAsync(ISecretVault secretVault, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<ISecretVault> GetSecretVaultByVaultIdAsync(string vaultId, string caller)
        {
            throw new NotImplementedException();
        }

        public override ISecretVault RefreshSecretVault(string vaultId, string caller)
        {
            throw new NotImplementedException();
        }

        public override ISecretVault RefreshSecretVault(ISecretVault secretVault, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<ISecretVault> RefreshSecretVaultAsync(string vaultId, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault secretVault, string caller)
        {
            throw new NotImplementedException();
        }

        public override HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }


        public override async Task<string?> GetValueOrNullAsync(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(GetValueOrNull(source, definition));
        }

        public override string? GetValueOrNull(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition)
        {
            return Environment.GetEnvironmentVariable(source.Key);
        }

    }
}
