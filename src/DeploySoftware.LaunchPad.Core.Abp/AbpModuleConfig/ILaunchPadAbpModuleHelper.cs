
using Abp.Dependency;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Configuration;
using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper : ISingletonDependency
    {
        public IDictionary<string, string> GetModuleSecrets();

        public IDictionary<string, string> GetModuleSecretsForVaultIdentifier(string vaultIdentifier);

        public IList<ISecretVault> GetModuleVaults();

        public IDictionary<string, TSecretVault> AddModuleSecretVaultsToProvider<TSecretVault>(ISecretProvider<TSecretVault> provider, string secretProviderVaultsJsonPath, string caller)
            where TSecretVault : ISecretVault, new();

        public string GetDatabaseConnectionString<TSecretProvider, TSecretVault>(TSecretProvider provider, TSecretVault vault, IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
            where TSecretProvider : ISecretProvider<TSecretVault>
            where TSecretVault : ISecretVault, new();

        
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);

    }
}