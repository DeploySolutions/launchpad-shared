// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsSQSService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.SQS;
using Deploy.LaunchPad.AWS.SQS.Services;
using Deploy.LaunchPad.Core.Abp.Services;

namespace Deploy.LaunchPad.AWS.Abp.SQS.Services
{
    /// <summary>
    /// Class AwsSQSService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsSqsService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsSqsService" />
    public partial class AwsSqsService : LaunchPadAbpSystemIntegrationServiceBase, IAwsSqsService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsSqsHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSqsService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSqsService(ILogger logger,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsSqsHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSqsService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsSqsService(ILogger logger, IAwsSqsHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
