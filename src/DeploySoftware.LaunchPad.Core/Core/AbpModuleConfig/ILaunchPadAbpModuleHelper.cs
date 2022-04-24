
using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault, THostEnvironment> : ISingletonDependency
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where THostEnvironment : IHostEnvironment
    {
        public TSecretHelper SecretHelper { get; }

        public THostEnvironment HostEnvironment { get; }
        
        public IConfigurationRoot ConfigurationRoot { get; }


        public string GetDefaultDatabaseConnectionString(string defaultDatabaseConnectionStringName, string secretVaultIdentifier = "");

        public string GetDatabaseConnectionString(string connectionStringFieldName, string secretVaultIdentifier = "", bool enableLocalDeveloperSecretsValue = false);

        Task<string> GetDatabaseConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName);
        
        public Task<string> GetJsonFromSecret(string secretVaultIdentifier);
        string GetSecretVaultIdentifierFromSetting(string settingName);
        public IDictionary<string, TSecretVault> GetSecretVaults<TModule, TSecretProvider, TAbpModuleHelper>()
            where TModule : ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider, TAbpModuleHelper, THostEnvironment>
            where TSecretProvider : SecretProviderBase<TSecretVault>, new()
            where TAbpModuleHelper : ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault, THostEnvironment>;


        void PreInitialize();

        void Initialize();

        void PostInitialize();
        

        public bool ShowDetailedErrorsToClient();
    }
}