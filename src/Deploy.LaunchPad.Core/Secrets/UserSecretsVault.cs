using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    public partial class UserSecretsVault : SecretVault
    {
        public override SecretSource Source { get; set; } = SecretSource.UserSecrets;

        [SetsRequiredMembers]
        public UserSecretsVault(ILogger logger) : base(logger, "userSecrets.json")
        {
            
        }


        public override HttpStatusCode BatchUpdateFields(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpStatusCode> BatchUpdateFieldsAsync(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            throw new NotImplementedException();
        }

        public override ISettingDefinition CreateOrUpdateField(string originalSecretJson, string key, ISettingDefinition value, string caller)
        {
            throw new NotImplementedException();
        }

        public override Task<IDictionary<string, ISettingDefinition>> GetAllValuesAsync(string caller)
        {
            throw new NotImplementedException();
        }

        public override ISettingDefinition GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<ISettingDefinition> GetValueAsync(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        public override string GetValueOrNullForSettingSecretProviderDescriptor(SettingSecretProviderDescriptor source, ISettingDefinition definition)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetValueOrNullForSettingSecretProviderDescriptorAsync(SettingSecretProviderDescriptor source, ISettingDefinition definition, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }
    }
}
