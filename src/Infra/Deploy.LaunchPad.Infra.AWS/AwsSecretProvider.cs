using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SimpleSystemsManagement;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Infra.Aws;
using Deploy.LaunchPad.Infra.AWS.Application.Configuration;
using Deploy.LaunchPad.Infra.AWS.SecretsManager;
using Deploy.LaunchPad.Infra.AWS.SecretsManager.Services;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Infra.AWS
{
    public partial class AwsSecretProvider : SecretProviderBase, ITransientDependency
    {
        public override void LoadAllSecretVaultsFromConfigurationRoot(IConfigurationRoot configurationRoot, ISecretProviderContext context = null)
        {
            Guard.AgainstNull(configurationRoot, nameof(configurationRoot));
            string infraSectionName = Constants_LaunchPadCore.LaunchPadConfigurationRootSectionName + ":comp_hub:infra:aws";
            IConfigurationSection infraSection = configurationRoot.GetSection(infraSectionName);
            AwsCloudConfiguration awsConfig = infraSection.Get<AwsCloudConfiguration>();
            string vaultsSectionName = Constants_LaunchPadCore.LaunchPadConfigurationRootSectionName + ":" + Constants_LaunchPadCore.VaultsConfigurationSectionName;
            IConfigurationSection vaultsRootSection = configurationRoot.GetSection(vaultsSectionName);
            IList<IConfigurationSection> secretVaultSections = vaultsRootSection.GetChildren().ToList();
            foreach (var secretVaultSection in secretVaultSections)
            {
                LoadValuesFromSecretVault<SecretVault>(secretVaultSection, awsConfig, context);
            }
        }

        public virtual AwsSecretVault GetAwsSecretVault(ILogger logger, string arn, AwsCloudConfiguration cloudConfiguration)            
        {
            Guard.AgainstNullOrEmpty(arn, nameof(arn));
            Guard.AgainstNull(cloudConfiguration, nameof(cloudConfiguration));
            // try to load the secret fields from the vault configuration
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            string awsProfileName = cloudConfiguration.Credentials.LocalProfileName ?? Constants_LaunchPadInfraAWS.DefaultLocalProfileName;
            bool didGetCredentials = chain.TryGetAWSCredentials(awsProfileName, out creds);
            AwsSecretVault vault = new AwsSecretVault(logger, arn);
            var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(cloudConfiguration.RegionEndpointName);
            AmazonSecretsManagerClient secretClient = new AmazonSecretsManagerClient(creds, regionEndpoint);
            AwsSecretsManagerService service = new AwsSecretsManagerService(secretClient);
            string result = service.GetPlaintextFromFromSecretVaultAsync(arn).Result;
            var dictionary = service.GetDictionaryFromSecretAsync(arn).Result;
            foreach (var item in dictionary)
            {
                ISettingDefinition fieldSetting = new SettingDefinition(item.Key, item.Value);
                vault.Fields.TryAdd(item.Key, fieldSetting);
                Secrets.TryAdd(item.Key, fieldSetting);
            }
            return vault;
        }

        public virtual void LoadValuesFromSecretVault<TVault>(IConfigurationSection vaultSection, AwsCloudConfiguration cloudConfiguration)
            where TVault : ISecretVault, new()
        {
            Guard.AgainstNull(vaultSection, nameof(vaultSection));
            Guard.AgainstNull(cloudConfiguration, nameof(cloudConfiguration));
            var vault = vaultSection.Get<TVault>() ?? new TVault();
            // try to load the secret fields from the vault configuration
            if (vault.Source == SecretVaultType.AwsSecretsManager)
            {
                var chain = new CredentialProfileStoreChain();
                AWSCredentials creds;
                string awsProfileName = cloudConfiguration.Credentials.LocalProfileName ?? Constants_LaunchPadInfraAWS.DefaultLocalProfileName;
                bool didGetCredentials = chain.TryGetAWSCredentials(awsProfileName, out creds);
                AwsSecretVault awsSecretVault = new AwsSecretVault();
                var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(cloudConfiguration.RegionEndpointName);
                AmazonSecretsManagerClient secretClient = new AmazonSecretsManagerClient(creds, regionEndpoint);
                AwsSecretsManagerService service = new AwsSecretsManagerService(secretClient);
                string arn = vault.VaultId;
                string result = service.GetPlaintextFromFromSecretVaultAsync(arn).Result;
                var dictionary = service.GetDictionaryFromSecretAsync(arn).Result;
                foreach(var item in dictionary)
                {
                    ISettingDefinition fieldSetting = new SettingDefinition(item.Key, item.Value);
                    vault.Fields.TryAdd(item.Key, fieldSetting);
                    Secrets.TryAdd(item.Key, fieldSetting);
                }

            }
            Vaults.TryAdd(vault.VaultId, vault);
        }

        public virtual void LoadValuesFromSecretVault<TVault>(IConfigurationSection configurationSection, AwsCloudConfiguration cloudConfiguration, ISecretProviderContext context)
            where TVault : ISecretVault, new()
        {
            LoadValuesFromSecretVault<TVault>(configurationSection, cloudConfiguration);
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
