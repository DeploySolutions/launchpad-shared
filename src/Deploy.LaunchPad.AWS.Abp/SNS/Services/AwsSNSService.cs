// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsSNSService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.SNS;
using Deploy.LaunchPad.AWS.SNS.Services;
using Deploy.LaunchPad.Core.Abp.Services;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.AWS.Abp.SNS.Services
{
    /// <summary>
    /// Class AwsSNSService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsSnsService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsSnsService" />
    public partial class AwsSnsService : LaunchPadAbpSystemIntegrationServiceBase, IAwsSnsService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsSnsHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSnsService"/> class.
        /// </summary>
        protected AwsSnsService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSnsService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSnsService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsSnsHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSnsService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsSnsService(ILogger logger, IAwsSnsHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
