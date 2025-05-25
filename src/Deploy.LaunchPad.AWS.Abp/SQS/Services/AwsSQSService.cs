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
    /// Implements the <see cref="IAwsSQSService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsSQSService" />
    public partial class AwsSQSService : LaunchPadAbpSystemIntegrationServiceBase, IAwsSQSService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsSQSHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSQSService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSQSService(ILogger logger,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsSQSHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSQSService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsSQSService(ILogger logger, IAwsSQSHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
