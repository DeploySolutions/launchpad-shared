// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="IAwsSNSService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.SimpleNotificationService;
using Amazon.SimpleSystemsManagement;
using Deploy.LaunchPad.Core.Services;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.SSM.Services
{
    /// <summary>
    /// Interface IAwsSNSService
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAwsSsmService : ILaunchPadSystemIntegrationService
    {
        public AmazonSimpleSystemsManagementClient SsmClient { get; }

        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsSsmHelper Helper { get; set; }

        public Task<string> GetParameterFromSystemsManager(string parameterName);
    }
}
