// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-26-2023
// ***********************************************************************
// <copyright file="AwsS3Service.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SQS;
using AwsSignatureVersion4.Private;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.AWS.S3.Services;
using Deploy.LaunchPad.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    /// <summary>
    /// Class AwsS3Service.
    /// Implements the <see cref="SystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsS3Service" />
    /// </summary>
    /// <seealso cref="SystemIntegrationServiceBase" />
    /// <seealso cref="IAwsS3Service" />
    public partial class AwsS3Service : SystemIntegrationServiceBase, IAwsS3Service
    {
        /// <summary>
        /// Gets or sets the helper.
        /// </summary>
        /// <value>The helper.</value>
        public IAwsS3Helper Helper { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Service"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="awsRegionEndpointName">Name of the aws region endpoint.</param>
        public AwsS3Service(ILogger logger, string awsRegionEndpointName) : base(logger)
        {
            Logger = logger;
            Helper = new AwsS3Helper(logger, awsRegionEndpointName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsS3Service"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="helper">The helper.</param>
        public AwsS3Service(ILogger logger, IAwsS3Helper helper) : base(logger)
        {
            Helper = helper;
        }

        /// <summary>
        /// Get file from bucket as an asynchronous operation.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3Key">The s3 key.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<string> GetFileFromBucketAsync(string bucketName, string s3Key)
        {

            string responseBody = "";
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3Key
                };
                using (GetObjectResponse response = await Helper.S3Client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                    string contentType = response.Headers["Content-Type"];
                    Console.WriteLine("Object metadata, Title: {0}", title);
                    Console.WriteLine("Content type: {0}", contentType);

                    responseBody = await reader.ReadToEndAsync(); // Now you process the response body.
                }
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("Error encountered ***. Message:'{0}' when reading object", e.Message);
                Logger.Error(errorMessage, e);
                throw new InvalidOperationException(errorMessage, e);
            }
            return responseBody;
        }

        /// <summary>
        /// Download file from bucket to localvia transfer utility as an asynchronous operation.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="localFilePath">The local file path.</param>
        /// <param name="modifiedSinceDateUtc">The modified since date UTC.</param>
        /// <param name="unmodifiedSinceDateUtc">The unmodified since date UTC.</param>
        /// <param name="transferMetadata">The transfer metadata.</param>
        /// <param name="versionId">The version identifier.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException">Check the provided AWS Credentials.</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<bool> DownloadFileFromBucketToLocalviaTransferUtilityAsync(string bucketName, string s3KeyName, string localFilePath, DateTime? modifiedSinceDateUtc, DateTime? unmodifiedSinceDateUtc, IDictionary<string, string> transferMetadata = null, string versionId = "")
        {
            
            bool didDownloadSucceed = false;
            try
            {
                // ensure the local file directory structure is ready
                string localDirectory = Path.GetDirectoryName(localFilePath);
                DirectoryInfo di = Directory.CreateDirectory(localDirectory);

                TransferUtility transferUtil = new TransferUtility(Helper.S3Client);
                TransferUtilityDownloadRequest fileTransferUtilityRequest = new TransferUtilityDownloadRequest
                {
                    BucketName = bucketName,
                    FilePath = localFilePath,
                    Key = s3KeyName,
                    ChecksumMode = ChecksumMode.ENABLED,
                    VersionId = versionId
                };
                if(modifiedSinceDateUtc.HasValue)
                {
                    fileTransferUtilityRequest.ModifiedSinceDate = modifiedSinceDateUtc.Value.ToUniversalTime();
                }
                if (unmodifiedSinceDateUtc.HasValue)
                {
                    fileTransferUtilityRequest.UnmodifiedSinceDate = unmodifiedSinceDateUtc.Value.ToUniversalTime();
                }
                await transferUtil.DownloadAsync(fileTransferUtilityRequest);
                transferUtil.Dispose();
                didDownloadSucceed = true;
            }

            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Logger.Error(amazonS3Exception.Message + ".Check the provided AWS Credentials.", amazonS3Exception);
                    throw new InvalidOperationException("Check the provided AWS Credentials.", amazonS3Exception);
                }
                else
                {
                    string errorMessage = string.Format("Unknown encountered on server. Message:'{0}' when reading object", amazonS3Exception.Message);
                    Logger.Error(errorMessage, amazonS3Exception);
                    throw new InvalidOperationException(errorMessage, amazonS3Exception);
                }
            }
            return didDownloadSucceed;
        }

        /// <summary>
        /// Upload local file to bucketvia transfer utility as an asynchronous operation.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileTags">The file tags.</param>
        /// <param name="s3Prefix">The s3 prefix.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="transferMetadata">The transfer metadata.</param>
        /// <param name="storageClass">The storage class.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<bool> UploadLocalFileToBucketviaTransferUtilityAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags, string s3Prefix = "", string contentType = @"image/tiff", IDictionary<string, string> transferMetadata =null, S3StorageClass storageClass = null)
        {
            if(storageClass == null)
            {
                storageClass = S3StorageClass.Standard;
            }
            bool didUploadSucceed = false;
            try
            {
                TransferUtility transferUtil = new TransferUtility(Helper.S3Client);
                List<Tag> tagSet = new List<Tag>();
                foreach (var item in fileTags)
                {
                    tagSet.Add(new Tag()
                    {
                        Key = item.Key,
                        Value = item.Value
                    });
                }
                if (!string.IsNullOrEmpty(s3Prefix))
                {
                    if (!s3Prefix.EndsWith('/'))
                    {
                        s3Prefix += '/';
                    }
                }
                if (filePath.StartsWith("file:///"))
                {
                    filePath = filePath.Remove(0, 8);
                }
                string key = s3Prefix + s3KeyName;
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = storageClass,
                    PartSize = 6291456, // 6 MB.  
                    Key = key,
                    TagSet = tagSet
                };
                foreach (var item in transferMetadata)
                {
                    fileTransferUtilityRequest.Metadata.Add(item.Key, item.Value);
                }
                await transferUtil.UploadAsync(fileTransferUtilityRequest);
                transferUtil.Dispose();
                didUploadSucceed = true;
            }

            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Logger.Error("Check the provided AWS Credentials.");
                    throw new InvalidOperationException(amazonS3Exception.Message, amazonS3Exception);
                }
                else
                {
                    Logger.Error("Error occurred: " + amazonS3Exception.Message);
                    throw new InvalidOperationException(amazonS3Exception.Message, amazonS3Exception);
                }
            }
            return didUploadSucceed;
        }

        /// <summary>
        /// Upload local file to bucket via put object as an asynchronous operation.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <param name="s3Prefix">The s3 prefix.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public async Task<bool> UploadLocalFileToBucketViaPutObjectAsync(string bucketName, string s3KeyName, string s3Prefix = "", string contentType = @"text\plain", IDictionary<string, string> metadata = null)
        {
            try
            {

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3KeyName,
                    FilePath = s3Prefix,
                    ContentType = contentType
                };
                if (metadata != null)
                {
                    foreach (var item in metadata)
                    {
                        putRequest.Metadata.Add(item.Key, item.Value);
                    }
                }

                PutObjectResponse response = await Helper.S3Client.PutObjectAsync(putRequest);
            }
            catch (AmazonS3Exception e)
            {
                string errorMessage = string.Format("Unknown error encountered on server. Message:'{0}' when  writing an object", e.Message);
                Logger.Error(errorMessage, e);
                throw new InvalidOperationException(e.Message, e);
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("Unknown error encountered on server. Message:'{0}' when  writing an object", e.Message);
                Logger.Error(errorMessage, e);
                throw new InvalidOperationException(e.Message, e);
            }
            return true;
        }

        /// <summary>
        /// Check if the file exists in the S3 bucket, without downloading it, by calling its metadata.
        /// If that worked, the file must exist.
        /// </summary>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="s3KeyName">Name of the s3 key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>

        public async Task<bool> CheckIfFileExists(string bucketName, string s3KeyName)
        {
            bool fileExists = false;
            using (Helper.S3Client)
            {
                var getObjectMetadataRequest = new GetObjectMetadataRequest() { 
                    BucketName = bucketName, 
                    Key = s3KeyName
                };
                var meta = await Helper.S3Client.GetObjectMetadataAsync(getObjectMetadataRequest);
                if (meta.HttpStatusCode == HttpStatusCode.OK)
                {
                    //file exists
                    fileExists = true;
                }
            }
            return fileExists;
        }

        

    }
}
