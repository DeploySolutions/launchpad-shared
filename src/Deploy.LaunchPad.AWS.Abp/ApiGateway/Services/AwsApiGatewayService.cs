// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsApiGatewayService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.ApiGateway.Services;
using Deploy.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;
using System;

namespace Deploy.LaunchPad.AWS.Abp.ApiGateway.Services
{
    /// <summary>
    /// Class AwsApiGatewayService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsApiGatewayService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsApiGatewayService" />
    public partial class AwsApiGatewayService : LaunchPadAbpSystemIntegrationServiceBase, IAwsApiGatewayService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsApiGatewayHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayService"/> class.
        /// </summary>
        protected AwsApiGatewayService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="apiGatewayBaseUri">The API gateway base URI.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsApiGatewayService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            Uri apiGatewayBaseUri,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsApiGatewayHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, apiGatewayBaseUri, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsApiGatewayService(ILogger logger, IAwsApiGatewayHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
