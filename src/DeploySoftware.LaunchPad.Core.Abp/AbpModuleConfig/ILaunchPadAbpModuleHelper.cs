
using Abp.Dependency;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Configuration;
using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper> : ISingletonDependency
        where TSecretHelper : ISecretHelper
    {
        public TSecretHelper SecretHelper { get; }

        public IDictionary<string, TVault> GetSecretVaults<TVault>(ISettingManager appSettings, string secretProviderVaultsJsonPath, string caller)
            where TVault : ISecretVault, new();

        public string GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false);
        
        public string GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false);

        public string GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false);
        
        public Task<string> GetJsonFromSecret(string secretVaultIdentifier, string caller);
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);

    }
}