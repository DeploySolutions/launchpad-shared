using Amazon.S3;
using Deploy.LaunchPad.Core.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    public interface IAwsS3Service : ILaunchPadSystemIntegrationService
    {
        public IAwsS3Helper Helper { get; set; }

        public static AmazonS3Client S3Client { get; set; }

        public Task<bool> CheckIfFileExists(string bucketName, string s3KeyName);

        public Task<string> GetFileFromBucketAsync(string bucketName, string s3Key);

        public Task<bool> UploadLocalFileToBucketviaTransferUtilityAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags, string s3Prefix = "", string contentType = @"image/tiff", IDictionary<string, string> transferMetadata = null, S3StorageClass storageClass = null);

        public Task<bool> DownloadFileFromBucketToLocalviaTransferUtilityAsync(string bucketName, string s3KeyName, string localFilePath, DateTime? modifiedSinceDateUtc, DateTime? unmodifiedSinceDateUtc, IDictionary<string, string> transferMetadata = null, string versionId = "");
        
        public Task<bool> UploadLocalFileToBucketViaPutObjectAsync(string bucketName, string s3KeyName, string s3Prefix = "", string contentType = @"text\plain", IDictionary<string, string> metadata=null);

    }
}
