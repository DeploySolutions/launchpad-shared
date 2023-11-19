// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsElasticFileSystemHelperFactory.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Amazon;
using Amazon.ElasticFileSystem;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Deploy.LaunchPad.AWS.ElasticFileSystem;

namespace Deploy.LaunchPad.AWS.Abp.ElasticFileSystem
{
    /// <summary>
    /// Class AwsElasticFileSystemHelperFactory.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.ElasticFileSystem.AmazonElasticFileSystemConfig}" />
    /// Implements the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.ElasticFileSystem.AmazonElasticFileSystemConfig}" />
    /// <seealso cref="ISingletonDependency" />
    public partial class AwsElasticFileSystemHelperFactory : AwsHelperBase<AmazonElasticFileSystemConfig>, ISingletonDependency
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemHelperFactory"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsElasticFileSystemHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

        /// <summary>
        /// Creates the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        /// <returns>AwsElasticFileSystemHelper.</returns>
        public virtual AwsElasticFileSystemHelper Create(
            ILogger logger,
            string awsRegionEndpointName = DefaultRegionEndpointName,
            string awsProfileName = DefaultLocalAwsProfileName,
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsElasticFileSystemHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                helper = new AwsElasticFileSystemHelper(logger, awsRegionEndpointName);
                helper.ShouldUseLocalAwsProfile = true;
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsElasticFileSystemHelper>();
                        logger.Debug("AwsElasticFileSystemHelper was registered; returning the resolved instance.");
                    }
                    catch (ComponentNotFoundException)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        helper = new AwsElasticFileSystemHelper(logger, awsRegionEndpointName);
                        logger.Debug("AwsElasticFileSystemHelper was not registered; returning a new instance.");
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
                    helper = new AwsElasticFileSystemHelper(logger, awsRegionEndpointName);
                    logger.Debug("AwsElasticFileSystemHelper was null; returning a new instance.");
                }
            }

            return helper;
        }

    }
}
