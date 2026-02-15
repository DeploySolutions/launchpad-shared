// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-18-2023
// ***********************************************************************
// <copyright file="SecretVaultValidator.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using FluentValidation;
using System;

namespace Deploy.LaunchPad.Code.Config
{
    /// <summary>
    /// Validates the SecretVaultBase and its properties
    /// </summary>
    public partial class SecretVaultValidator :
        AbstractValidator<ISecretVault>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultValidator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
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