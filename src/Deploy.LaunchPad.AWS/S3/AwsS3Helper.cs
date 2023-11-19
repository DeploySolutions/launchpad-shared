// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsS3Helper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.S3;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.AWS.S3
{
    /// <summary>
    /// Class AwsS3Helper.
    /// Implements the <see cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.S3.AmazonS3Config}" />
    /// Implements the <see cref="Deploy.LaunchPad.AWS.S3.IAwsS3Helper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.AwsHelperBase{Amazon.S3.AmazonS3Config}" />
    /// <seealso cref="Deploy.LaunchPad.AWS.S3.IAwsS3Helper" />
    public partial class AwsS3Helper : AwsHelperBase<AmazonS3Config>, IAwsS3Helper
    {
        /// <summary>
        /// The s3 client
        /// </summary>
        protected AmazonS3Client _s3Client;

        /// <summary>
        /// Gets the s3 client.
        /// </summary>
        /// <value>The s3 client.</value>
        [JsonIgnore]
        public AmazonS3Client S3Client { get { return _s3Client; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Helper"/> class.
        /// </summary>
        public AwsS3Helper() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Helper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsS3Helper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {
            _s3Client = new AmazonS3Client(Region);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Helper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="s3Client">The s3 client.</param>
        public AwsS3Helper(ILogger logger, string awsRegionEndpointName, AmazonS3Client s3Client) : base(logger, awsRegionEndpointName)
        {
            _s3Client = s3Client;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Helper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        /// <param name="s3Client">The s3 client.</param>
        /// <param name="localAwsProfileName">Name of the local aws profile.</param>
        public AwsS3Helper(ILogger logger, string awsRegionEndpointName, AmazonS3Client s3Client, string localAwsProfileName) : base(logger, awsRegionEndpointName)
        {
            AwsProfileName = localAwsProfileName;
            _s3Client = s3Client;
        }
    }
}
