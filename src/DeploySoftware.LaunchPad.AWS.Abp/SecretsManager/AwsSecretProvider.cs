﻿using Abp.Dependency;
using DeploySoftware.LaunchPad.AWS.SecretsManager;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Abp.SecretsManager
{
    public partial class AwsSecretProvider<TSecretVault> : SecretProviderBase<TSecretVault>, ISingletonDependency, ISecretProvider<TSecretVault>
        where TSecretVault: SecretVaultBase, new()
    {

        public AwsSecretProvider() :base()
        {       
        }

        public override bool RefreshSecretVault(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper, string caller)
        {
            return RefreshSecretVaultAsync(vaultSecretIdentifier, vaultName, vaultFullName, helper, caller).Result;
        }

        public override async Task<bool> RefreshSecretVaultAsync(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper, string caller)
        {
            AwsSecretVault vault = (AwsSecretVault)await helper.GetSecretVaultAsync(vaultSecretIdentifier, vaultName, vaultFullName, caller);

            return true;
        }
    }
}