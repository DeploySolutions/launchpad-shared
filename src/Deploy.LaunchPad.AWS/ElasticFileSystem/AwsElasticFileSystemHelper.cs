// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsElasticFileSystemHelper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.ElasticFileSystem;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.ElasticFileSystem
{
    /// <summary>
    /// Class AwsElasticFileSystemHelper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.ElasticFileSystem.AmazonElasticFileSystemConfig}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.ElasticFileSystem.IAwsElasticFileSystemHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.ElasticFileSystem.AmazonElasticFileSystemConfig}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.ElasticFileSystem.IAwsElasticFileSystemHelper" />
    public partial class AwsElasticFileSystemHelper : AwsHelperBase<AmazonElasticFileSystemConfig>, IAwsElasticFileSystemHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemHelper"/> class.
        /// </summary>
        public AwsElasticFileSystemHelper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsElasticFileSystemHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsElasticFileSystemHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

    }
}
