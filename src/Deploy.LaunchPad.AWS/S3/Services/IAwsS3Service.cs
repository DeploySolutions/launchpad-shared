using Amazon.S3;
using Deploy.LaunchPad.Core.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    public interface IAwsS3Service : ISystemIntegrationService
    {
        public IAwsS3Helper Helper { get; set; }

        public static AmazonS3Client S3Client { get; set; }

        public Task<bool> UploadFileToBucketAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags, IDictionary<string, string> transferMetadata, string s3Prefix = "", string contentType = @"image/tiff");

        public Task<string> GetTextFromBucketAsync(string bucketName, string s3Key, string s3Prefix = "");
    }
}
