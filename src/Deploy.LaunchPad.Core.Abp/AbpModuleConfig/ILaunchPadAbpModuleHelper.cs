
using Abp.Dependency;
using Deploy.LaunchPad.Core.Config;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper : ISingletonDependency
    {
        public IDictionary<string, string> GetModuleSecrets();

        public IDictionary<string, string> GetModuleSecretsForVaultIdentifier(string vaultIdentifier);

        public IList<ISecretVault> GetModuleVaults();

        public IDictionary<string, ISecretVault> AddModuleSecretVaultsToProvider<TSecretVault>(ISecretProvider provider, string secretProviderVaultsJsonPath, string caller)
            where TSecretVault : ISecretVault, new();

        public string GetDatabaseConnectionString<TSecretVault>(TSecretVault vault, IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
            where TSecretVault : ISecretVault;


        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);

    }
}