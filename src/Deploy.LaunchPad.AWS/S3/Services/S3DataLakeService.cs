using System;
using System.Collections.Generic;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using Castle.Core.Logging;
using System.Data.Common;

namespace Deploy.LaunchPad.AWS.S3.Services
{
    /// <summary>
    /// Extends AwsS3Service to facilitate Data Lake usage across multiple buckets and folders. 
    /// This service is intended to be used in scenarios where there are multiple buckets and folders that need to be accessed
    /// and managed as part of a data lake solution based in AWS S3. 
    /// It provides additional functionality for finding and managing logs and other special buckets.
    /// </summary>
    public partial class S3DataLakeService : AwsS3Service
    {

        public virtual S3DataLakeInfo DataLakeInfo { get; set; }

        public S3DataLakeService(ILogger logger, AmazonS3Client s3Client, S3DataLakeInfo props) :base(logger, s3Client)
        {
            S3Client = s3Client;
            S3Transfer = new TransferUtility(s3Client);
            DataLakeInfo = props;
        }

        public async Task<bool> UploadFileToBucket(string uploadFilePath, string bucketName, string s3Key)
        {
            bool didUploadSucceed = false;
            try
            {
                Logger.Debug("Uploading file " + uploadFilePath + " in bucket " + bucketName + " with key " + s3Key);
                await S3Transfer.UploadAsync(uploadFilePath, bucketName, s3Key);
                didUploadSucceed = true;
                Logger.Debug("Uploaded file " + uploadFilePath + " in bucket " + bucketName + " with key " + s3Key);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}'", e.Message);
                Logger.Error(e.Message);
                Logger.Error(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}'", e.Message);
                Logger.Error(e.Message);
                Logger.Error(e.StackTrace);
            }
            return didUploadSucceed;
        }

        public async Task<DeleteObjectResponse> DeleteFileFromBucket(string bucketName, string deleteFilePath)
        {
            DeleteObjectResponse response = null;
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = deleteFilePath,
                };
                response = await S3Client.DeleteObjectAsync(request);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}'", e.Message);
                Logger.Error(e.Message);
                Logger.Error(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}'", e.Message);
                Logger.Error(e.Message);
                Logger.Error(e.StackTrace);
            }
            Console.WriteLine(response);
            Logger.Debug(response.ToString());
            return response;
        }

    }
}
