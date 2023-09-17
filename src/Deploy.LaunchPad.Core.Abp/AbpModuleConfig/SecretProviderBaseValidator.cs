using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using FluentValidation;
using System;

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
            RuleFor(x => x.SecretVaults)
                .NotNull();

        }


    }
}