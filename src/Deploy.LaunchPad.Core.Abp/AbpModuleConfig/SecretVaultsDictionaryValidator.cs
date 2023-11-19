// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-18-2023
// ***********************************************************************
// <copyright file="SecretVaultsDictionaryValidator.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultsDictionaryValidator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SecretVaultsDictionaryValidator(ILogger logger)
        {
            
        }


    }
}