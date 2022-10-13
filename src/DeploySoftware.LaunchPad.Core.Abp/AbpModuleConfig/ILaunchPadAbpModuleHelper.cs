
using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;


namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault> : ISingletonDependency
        where TSecretHelper : ISecretHelper
        where TSecretVault : SecretVaultBase, new()
    {
        public TSecretHelper SecretHelper { get; }

        public string GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldUseLocalDeveloperSecretsValue = false, bool shouldOutputConnectionString = false);

        public Task<string> GetJsonFromSecret(string secretVaultIdentifier, string caller);
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);
        
       
        public bool ShowDetailedErrorsToClient();
    }
}