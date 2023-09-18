using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
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