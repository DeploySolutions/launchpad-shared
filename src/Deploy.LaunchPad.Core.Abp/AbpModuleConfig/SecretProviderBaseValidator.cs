// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-18-2023
// ***********************************************************************
// <copyright file="SecretProviderBaseValidator.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Code.Config;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Validates the SecretProviderBase and its properties
    /// </summary>
    public partial class SecretProviderBaseValidator :
        AbstractValidator<SecretProviderBase>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBaseValidator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SecretProviderBaseValidator(ILogger logger)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();
            RuleFor(x => x.Type)
                .NotNull().NotEmpty();
            RuleForEach(x => x.SecretVaults).ChildRules(order =>
            {
                order.RuleFor(x => x.Value as ISecretVault).SetValidator(new SecretVaultValidator(logger));
            });
        }


    }
}