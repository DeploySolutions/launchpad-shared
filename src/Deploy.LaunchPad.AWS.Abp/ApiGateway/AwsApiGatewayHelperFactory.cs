// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsApiGatewayHelperFactory.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Amazon;
using Amazon.APIGateway;
using Castle.Core.Logging;
using System;

namespace Deploy.LaunchPad.AWS.Abp.ApiGateway
{
    /// <summary>
    /// Class AwsApiGatewayHelperFactory.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    /// Implements the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.APIGateway.AmazonAPIGatewayConfig}" />
    /// <seealso cref="ISingletonDependency" />
    public partial class AwsApiGatewayHelperFactory : AwsHelperBase<AmazonAPIGatewayConfig>, ISingletonDependency
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsApiGatewayHelperFactory"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsApiGatewayHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

        /// <summary>
        /// Creates the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="apiGatewayBaseUri">The API gateway base URI.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        /// <returns>AwsApiGatewayHelper.</returns>
        public virtual AwsApiGatewayHelper Create(
            ILogger logger,
            Uri apiGatewayBaseUri,
            string awsRegionEndpointName = DefaultRegionEndpointName,
            string awsProfileName = DefaultLocalAwsProfileName,
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsApiGatewayHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                helper = new AwsApiGatewayHelper(logger, awsRegionEndpointName, apiGatewayBaseUri);
                helper.ShouldUseLocalAwsProfile = true;
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsApiGatewayHelper>();
                        logger.Debug("AwsApiGatewayHelper was registered; returning the resolved instance.");
                    }
                    catch (Exception ex)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        helper = new AwsApiGatewayHelper(logger, awsRegionEndpointName, apiGatewayBaseUri);
                        logger.Debug("AwsApiGatewayHelper was not registered; returning a new instance.");
                    }
                }
                else
                {

                }
                // after all that, load the helper
                if (helper == null)
                {
                    // create the helper using the AWS credentials resolution pattern.
                    // Since we are not using local profile here, we presumably load from EC2 role or environment
                    helper = new AwsApiGatewayHelper(logger, awsRegionEndpointName, apiGatewayBaseUri);
                    logger.Debug("AwsApiGatewayHelper was null; returning a new instance.");
                }
            }

            return helper;
        }

    }
}
