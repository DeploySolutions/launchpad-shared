// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsRedshiftService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Abp.Application;

namespace Deploy.LaunchPad.AWS.Redshift.Services
{
    /// <summary>
    /// Class AwsRedshiftService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.Redshift.Services.IAwsRedshiftService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="Deploy.LaunchPad.AWS.Redshift.Services.IAwsRedshiftService" />
    public partial class AwsRedshiftService : LaunchPadAbpSystemIntegrationServiceBase, IAwsRedshiftService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsRedshiftHelper Helper { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftService"/> class.
        /// </summary>
        protected AwsRedshiftService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="regionEndpointName">Name of the region endpoint.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsRedshiftService(ILogger logger,
            string regionEndpointName,
            string localAwsProfileName,
            bool shouldUseLocalAwsProfile) : base(logger)
        {
            var secretHelperFactory = new AwsRedshiftHelperFactory(logger, regionEndpointName);
            Helper = secretHelperFactory.Create(logger, regionEndpointName, localAwsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsRedshiftService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsRedshiftService(ILogger logger, IAwsRedshiftHelper helper) : base(logger)
        {
            Helper = helper;
        }

    }
}
