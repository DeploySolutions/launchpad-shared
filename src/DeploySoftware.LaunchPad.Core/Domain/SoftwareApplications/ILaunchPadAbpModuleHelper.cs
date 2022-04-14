using DeploySoftware.LaunchPad.Core.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain.SoftwareApplications
{
    public partial interface ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
    {
        public TSecretHelper SecretHelper { get; }

        public string GetDefaultDatabaseConnectionString(string defaultDatabaseConnectionStringName, string secretVaultIdentifier = "");

        public string GetDatabaseConnectionString(string connectionStringFieldName, string secretVaultIdentifier = "", bool enableLocalDeveloperSecretsValue = false);

        Task<string> GetDatabaseConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName);
        
        public Task<string> GetJsonFromSecret(string secretVaultIdentifier);
        string GetSecretVaultIdentifierFromSetting(string settingName);
        public IDictionary<string, TSecretVault> GetSecretVaults<TModule, TSecretProvider>()
            where TModule : ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider>
            where TSecretProvider : SecretProviderBase<TSecretVault>, new();
        
        void Initialize();
        void PostInitialize();
        void PreInitialize();

        public bool ShowDetailedErrorsToClient();
    }
}