using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public interface IAwsS3Helper : IAwsHelper
    {

        public AmazonS3Client S3Client { get;}

    }
}
