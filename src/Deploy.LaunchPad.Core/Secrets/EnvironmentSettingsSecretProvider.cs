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

        public override ISettingDefinition CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, ISettingDefinition value, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller)
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

        public override HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }


        public override async Task<string?> GetValueOrNullForSettingSecretProviderDescriptorAsync(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(GetValueOrNullForSettingSecretProviderDescriptor(source, definition));
        }

        public override string? GetValueOrNullForSettingSecretProviderDescriptor(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition)
        {
            return Environment.GetEnvironmentVariable(source.Key);
        }

        public override ISettingDefinition GetValueFromSecretVault(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<ISettingDefinition> GetValueFromSecretVaultAsync(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(ISecretVault secretVault, IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<IDictionary<string, ISettingDefinition>> GetAllValuesFromSecretVaultAsync(ISecretVault secretVault, string caller)
        {
            throw new NotImplementedException();
        }
    }
}
