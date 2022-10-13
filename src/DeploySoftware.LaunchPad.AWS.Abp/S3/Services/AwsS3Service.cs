using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.S3;
using DeploySoftware.LaunchPad.AWS.S3.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using DeploySoftware.LaunchPad.Core.Application;
using DeploySoftware.LaunchPad.Core.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Abp.S3.Services
{
    public partial class AwsS3Service : SystemIntegrationServiceBase, IAwsS3Service
    {
        public IAwsS3Helper Helper { get; set; }


        public AwsS3Service(ILogger logger, string awsRegionEndpointName) : base(logger)
        {
            Logger = logger;
            Helper = new AwsS3Helper(logger, awsRegionEndpointName);
        }

        public AwsS3Service(ILogger logger, IAwsS3Helper helper) : base(logger)
        {
            Helper = helper;
        }

        public async Task<string> GetTextFromBucketAsync(string bucketName, string s3KeyName, string s3Prefix = "")
        {

            string responseBody = "";
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3KeyName
                };
                using (GetObjectResponse response = await Helper.S3Client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
                    string contentType = response.Headers["Content-Type"];
                    Console.WriteLine("Object metadata, Title: {0}", title);
                    Console.WriteLine("Content type: {0}", contentType);

                    responseBody = reader.ReadToEnd(); // Now you process the response body.
                }
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                Console.WriteLine("Error encountered ***. Message:'{0}' when reading object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading object", e.Message);
            }
            return responseBody;
        }

        public async Task<bool> UploadFileToBucketAsync(string bucketName, string s3KeyName, string filePath, IDictionary<string, string> fileTags,  IDictionary<string, string> transferMetadata, string s3Prefix = "",  string contentType = @"image/tiff")
        {
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
                    if(!s3Prefix.EndsWith('/'))
                    {
                        s3Prefix += '/';
                    }
                }
                if(filePath.StartsWith("file:///"))
                {
                    filePath = filePath.Remove(0,8);
                }
                string key = s3Prefix + s3KeyName;
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
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
                }
                else
                {
                    Logger.Error("Error occurred: " + amazonS3Exception.Message);
                }
            }
            return didUploadSucceed;
        }


        public async Task<bool> SaveTextFileToBucketAsync(string bucketName, string s3KeyName, string contentBody, string s3Prefix = "", string contentType =@"text\plain")
        {

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3KeyName,
                    FilePath = s3Prefix,
                    ContentType = contentType,
                    ContentBody = contentBody
                };
                PutObjectResponse response = await Helper.S3Client.PutObjectAsync(putRequest);

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }
            return true;
        }

        public async Task<bool> SaveFileToBucketWithMetadataAsync(string bucketName, string s3KeyName, IDictionary<string,string> metadata, string s3Prefix = "", string contentType = @"text\plain")
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
                foreach(var item in metadata)
                {
                    putRequest.Metadata.Add(item.Key,item.Value);
                }
                PutObjectResponse response = await Helper.S3Client.PutObjectAsync(putRequest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }
            return true;
        }

    }
}
