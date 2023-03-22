using Amazon.S3;
using Deploy.LaunchPad.Core.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    public interface IAwsS3Service : ILaunchPadSystemIntegrationService
    {
        public IAwsS3Helper Helper { get; set; }

        public static AmazonS3Client S3Client { get; set; }

        public Task<bool> CheckIfFileExists(string bucketName, string s3KeyName);

        public Task<string> GetFileFromBucketAsync(string bucketName, string s3Key, string s3Prefix = "");

        public Task<bool> UploadLocalFileToBucketAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags, IDictionary<string, string> transferMetadata, string s3Prefix = "", string contentType = @"image/tiff");
       
        public Task<bool> SaveFileToBucketAsync(string bucketName, string s3KeyName, string s3Prefix = "", string contentType = @"text\plain", IDictionary<string, string> metadata=null);

    }
}
