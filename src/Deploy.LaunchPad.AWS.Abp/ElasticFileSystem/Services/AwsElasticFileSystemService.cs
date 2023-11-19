// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsElasticFileSystemService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.ElasticFileSystem;
using Deploy.LaunchPad.AWS.ElasticFileSystem.Services;
using Deploy.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.AWS.Abp.ElasticFileSystem.Services
{
    /// <summary>
    /// Class AwsElasticFileSystemService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsElasticFileSystemService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsElasticFileSystemService" />
    public partial class AwsElasticFileSystemService : LaunchPadAbpSystemIntegrationServiceBase, IAwsElasticFileSystemService
    {

        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsElasticFileSystemHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemService"/> class.
        /// </summary>
        protected AwsElasticFileSystemService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsElasticFileSystemService(ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsElasticFileSystemHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsElasticFileSystemService(ILogger logger, IAwsElasticFileSystemHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
