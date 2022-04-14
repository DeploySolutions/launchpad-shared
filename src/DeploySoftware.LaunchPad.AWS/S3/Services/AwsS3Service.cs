using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3.Services
{
    public partial class AwsS3Service : SystemIntegrationServiceBase, IAwsS3Service
    {
        public IAwsS3Helper Helper { get; set; }

        public static AmazonS3Client S3Client { get; set; }

        public static TransferUtility Transfer { get; set; }

        public AwsS3Service(ILogger logger)
        {
            Logger = logger;
            Helper = new AwsS3Helper(logger);
            var credentials = Helper.GetAwsCredentials("EGS");
            S3Client = new AmazonS3Client(
               credentials,
               Helper.GetRegionEndpoint("us-east-1")
            );
        }

        public AwsS3Service(AmazonS3Client _client)
        {
            S3Client = _client;
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
                using (GetObjectResponse response = await S3Client.GetObjectAsync(request))
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

        public async Task<bool> SaveFileToBucketAsync(string bucketName, string s3KeyName, string s3Prefix = "", string contentType =@"text\plain")
        {

            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3KeyName,
                    FilePath = s3Prefix,
                    ContentBody = contentType
                };

                PutObjectResponse response = await S3Client.PutObjectAsync(putRequest);

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
                PutObjectResponse response = await S3Client.PutObjectAsync(putRequest);
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
