
using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault> : ISingletonDependency
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
    {
        public TSecretHelper SecretHelper { get; }

        public string GetDefaultDatabaseConnectionString(IHostEnvironment hostEnvironment, IConfigurationRoot configuration, string defaultDatabaseConnectionStringName, string secretVaultIdentifier, string caller);

        public string GetDatabaseConnectionString(IHostEnvironment hostEnvironment, IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool enableLocalDeveloperSecretsValue = false);

        Task<string> GetDatabaseConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName, string caller);
        
        public Task<string> GetJsonFromSecret(string secretVaultIdentifier, string caller);
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);
        
       
        public bool ShowDetailedErrorsToClient();
    }
}