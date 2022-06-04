using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public partial class AwsS3Helper : AwsHelperBase, IAwsS3Helper
    {
        protected AmazonS3Client _s3Client;

        [JsonIgnore]
        public AmazonS3Client S3Client { get { return _s3Client; } }


        public AwsS3Helper() : base()
        {
        }

        public AwsS3Helper(ILogger logger, IConfigurationRoot configurationRoot) :base(logger, configurationRoot)
        {

        }

        public AwsS3Helper(ILogger logger, IConfigurationRoot configurationRoot, string awsRegionEndpointName, AmazonS3Client s3Client) : base(logger, configurationRoot)
        {
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            _s3Client = s3Client;
        }

        public AwsS3Helper(ILogger logger, IConfigurationRoot configurationRoot, string awsRegionEndpointName, AmazonS3Client s3Client, string localAwsProfileName) : base(logger, configurationRoot)
        {
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = localAwsProfileName;
            _s3Client = s3Client; 
        }
    }
}
