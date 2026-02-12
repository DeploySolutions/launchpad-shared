// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsRedshiftHelperFactory.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Amazon;
using Amazon.Redshift;
using Castle.Core.Logging;
using System;

namespace Deploy.LaunchPad.AWS.Redshift
{
    /// <summary>
    /// Class AwsRedshiftHelperFactory.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Redshift.AmazonRedshiftConfig}" />
    /// Implements the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.Redshift.AmazonRedshiftConfig}" />
    /// <seealso cref="ISingletonDependency" />
    public partial class AwsRedshiftHelperFactory : AwsHelperBase<AmazonRedshiftConfig>, ISingletonDependency
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftHelperFactory"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsRedshiftHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

        /// <summary>
        /// Creates the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        /// <returns>AwsRedshiftHelper.</returns>
        public virtual AwsRedshiftHelper Create(
            ILogger logger,
            string awsRegionEndpointName = DefaultRegionEndpointName,
            string awsProfileName = DefaultLocalAwsProfileName,
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsRedshiftHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                helper = new AwsRedshiftHelper(logger, awsRegionEndpointName);
                helper.ShouldUseLocalAwsProfile = true;
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsRedshiftHelper>();
                        logger.Debug("AwsRedshiftHelper was registered; returning the resolved instance.");
                    }
                    catch (Exception ex)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        helper = new AwsRedshiftHelper(logger, awsRegionEndpointName);
                        logger.Debug("AwsRedshiftHelper was not registered; returning a new instance.");
                    }
                }
                // after all that, load the helper
                if (helper == null)
                {
                    // create the helper using the AWS credentials resolution pattern.
                    // Since we are not using local profile here, we presumably load from EC2 role or environment
                    helper = new AwsRedshiftHelper(logger, awsRegionEndpointName);
                    logger.Debug("AwsRedshiftHelper was null; returning a new instance.");
                }
            }

            return helper;
        }

    }
}
