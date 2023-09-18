using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Validates a dictionary of secret vaults and their properties
    /// </summary>
    public partial class SecretVaultsDictionaryValidator :
        AbstractValidator<Dictionary<string, ISecretVault>>
    {

        public SecretVaultsDictionaryValidator(ILogger logger)
        {
            
        }


    }
}