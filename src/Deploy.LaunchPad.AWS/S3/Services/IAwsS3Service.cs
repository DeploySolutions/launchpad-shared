// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-23-2023
// ***********************************************************************
// <copyright file="IAwsS3Service.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.S3;
using Amazon.S3.Transfer;
using Deploy.LaunchPad.Code.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    /// <summary>
    /// Interface IAwsS3Service
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAwsS3Service : ILaunchPadSystemIntegrationService
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsS3Helper Helper { get; set; }

        /// <summary>
        /// Gets or sets the s3 client.
        /// </summary>
        /// <value>The s3 client.</value>
        public AmazonS3Client S3Client { get; }

        public TransferUtility S3Transfer { get; }

        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> CheckIfFileExists(string bucketName, string s3KeyName);

        /// <summary>
        /// Gets the file from bucket asynchronous.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3Key">The s3 key.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GetFileFromBucketAsync(string bucketName, string s3Key);

        /// <summary>
        /// Uploads the local file to bucketvia transfer utility asynchronous.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileTags">The file tags.</param>
        /// <param name="s3Prefix">The s3 prefix.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="transferMetadata">The transfer metadata.</param>
        /// <param name="storageClass">The storage class.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> UploadLocalFileToBucketviaTransferUtilityAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags, string s3Prefix = "", string contentType = @"image/tiff", IDictionary<string, string> transferMetadata = null, S3StorageClass storageClass = null);

        /// <summary>
        /// Downloads the file from bucket to localvia transfer utility asynchronous.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="modifiedSinceDateUtc">The modified since date UTC.</param>
        /// <param name="unmodifiedSinceDateUtc">The unmodified since date UTC.</param>
        /// <param name="transferMetadata">The transfer metadata.</param>
        /// <param name="versionId">The version identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> DownloadFileFromBucketToLocalviaTransferUtilityAsync(string bucketName, string s3KeyName, string localFilePath, DateTime? modifiedSinceDateUtc, DateTime? unmodifiedSinceDateUtc, IDictionary<string, string> transferMetadata = null, string versionId = "");

        /// <summary>
        /// Uploads the local file to bucket via put object asynchronous.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="s3Prefix">The s3 prefix.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public Task<bool> UploadLocalFileToBucketViaPutObjectAsync(string bucketName, string s3KeyName, string s3Prefix = "", string contentType = @"text\plain", IDictionary<string, string> metadata=null);

    }
}
