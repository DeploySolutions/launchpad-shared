using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.AWS.S3.Services;
using Deploy.LaunchPad.Core.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.Abp.S3.Services
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
                    fileTransferUtilityRequest.ModifiedSinceDateUtc = modifiedSinceDateUtc.Value.ToUniversalTime();
                }
                if (unmodifiedSinceDateUtc.HasValue)
                {
                    fileTransferUtilityRequest.UnmodifiedSinceDateUtc = unmodifiedSinceDateUtc.Value.ToUniversalTime();
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
                    Logger.Error("Check the provided AWS Credentials.");
                }
                else
                {
                    Logger.Error("Error occurred: " + amazonS3Exception.Message);
                }
            }
            return didDownloadSucceed;
        }

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
                }
                else
                {
                    Logger.Error("Error occurred: " + amazonS3Exception.Message);
                }
            }
            return didUploadSucceed;
        }

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

        /// <summary>
        /// Check if the file exists in the S3 bucket, without downloading it, by calling its metadata. 
        /// If that worked, the file must exist.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="s3KeyName"></param>
        /// <returns></returns>

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
