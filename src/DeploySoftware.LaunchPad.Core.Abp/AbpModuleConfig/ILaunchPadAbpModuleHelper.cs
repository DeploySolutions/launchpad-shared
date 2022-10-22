
using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using Abp.Configuration;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault> : ISingletonDependency
        where TSecretHelper : ISecretHelper
        where TSecretVault : SecretVaultBase, new()
    {
        public TSecretHelper SecretHelper { get; }

        public IDictionary<string, TVault> GetSecretVaults<TVault>(ISettingManager appSettings, string secretProviderVaultsJsonPath, string caller)
            where TVault : ISecretVault, new();

        public string GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false);
        
        public string GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false);

        public string GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false);
        
        public Task<string> GetJsonFromSecret(string secretVaultIdentifier, string caller);
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);

        public bool ShowDetailedErrorsToClient();
    }
}