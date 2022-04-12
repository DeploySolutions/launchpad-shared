using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using DeploySoftware.LaunchPad.Core.Configuration;

namespace DeploySoftware.LaunchPad.AWS.S3.Services
{
    public interface IAwsS3Service : ISystemIntegrationService
    {
        public IAwsS3Helper Helper { get; set; }

        public static AmazonS3Client S3Client { get; set; }

        public static TransferUtility Transfer { get; set; }


        public Task<string> GetTextFromBucketAsync(string bucketName, string s3Key, string s3Prefix = "");
    }
}
