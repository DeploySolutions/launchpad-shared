using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using FluentValidation;
using System;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Validates the SecretVaultBase and its properties
    /// </summary>
    public partial class SecretVaultValidator :
        AbstractValidator<ISecretVault>
    {

        public SecretVaultValidator(ILogger logger)
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
            RuleFor(x => x.Name)
                .NotNull().NotEmpty();
            RuleFor(x => x.VaultId)
                .NotNull().NotEmpty().Must(id => !id.StartsWith("{{p:"))
                .WithMessage("The vault id must not start with a token value i.e. {{");
            RuleFor(x => x.ProviderId)
                .NotNull().NotEmpty();
            RuleFor(x => x.Fields)
                .NotNull().NotEmpty();

        }


    }
}