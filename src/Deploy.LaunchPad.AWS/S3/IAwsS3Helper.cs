// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IAwsS3Helper.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.S3;

namespace Deploy.LaunchPad.AWS.S3
{
    /// <summary>
    /// Interface IAwsS3Helper
    /// Extends the <see cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.S3.AmazonS3Config}" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.AWS.IAwsHelper{Amazon.S3.AmazonS3Config}" />
    public partial interface IAwsS3Helper : IAwsHelper<AmazonS3Config>
    {

        /// <summary>
        /// Gets the s3 client.
        /// </summary>
        /// <value>The s3 client.</value>
        public AmazonS3Client S3Client { get; }

    }
}
