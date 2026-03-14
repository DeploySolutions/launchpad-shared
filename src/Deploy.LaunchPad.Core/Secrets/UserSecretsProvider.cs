using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.Core.Secrets
{
    public partial class UserSecretsProvider : SecretProviderBase
    {
        private readonly IConfiguration _configuration;

        public UserSecretsProvider(ILogger logger, IConfiguration configuration) : base(logger)
        {
            _configuration = configuration;
        }

        public override Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller)
        {
            throw new NotImplementedException();
        }

        public override void PopulateSecretVaults(ISecretProviderContext context)
        {
            UserSecretsVault vault = new UserSecretsVault(Logger); 
            string connectionString = _configuration.GetValue<string>("ConnectionStrings:Default");
            if (!string.IsNullOrEmpty(connectionString))
                {
                    vault.Fields["ConnectionStrings:Default"] = 
                    new SettingDefinition("ConnectionStrings:Default", connectionString);
                }
            SecretVaults.TryAdd(vault.VaultId,vault);
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
    }
}
