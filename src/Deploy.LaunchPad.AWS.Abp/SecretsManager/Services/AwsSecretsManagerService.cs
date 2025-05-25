// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsSecretsManagerService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.AWS.SecretsManager.Services;
using Deploy.LaunchPad.Core.Abp.Services;

namespace Deploy.LaunchPad.AWS.Abp.SecretsManager.Services
{
    /// <summary>
    /// Class AwsSecretsManagerService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsSecretsManagerService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsSecretsManagerService" />
    public partial class AwsSecretsManagerService : LaunchPadAbpSystemIntegrationServiceBase, IAwsSecretsManagerService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerService"/> class.
        /// </summary>
        public AwsSecretsManagerService() : base()
        {
        }


    }
}
