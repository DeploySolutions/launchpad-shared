using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial class S3DataLakeInfo
    {
        public virtual IList<S3BucketInfo> Buckets { get; set; } = new List<S3BucketInfo>();

        public virtual string DefaultBucketName { get; set; }

        public virtual string DevOpsBucketName { get; set; }
        public virtual string LoggingBucketName { get; set; }

        

        public S3DataLakeInfo()
        {
            DefaultBucketName = string.Empty;
            DevOpsBucketName = string.Empty;
        }
        public S3DataLakeInfo(string bucketName)
        {
            DefaultBucketName = bucketName;
            DevOpsBucketName = bucketName;
        }

        public S3DataLakeInfo(string defaultBucketName, string devopsBucketName)
        {
            DefaultBucketName = defaultBucketName;
            DevOpsBucketName = devopsBucketName;
        }

        public S3DataLakeInfo(string defaultBucketName, string devopsBucketName, string loggingBucketName)
        {
            DefaultBucketName = defaultBucketName;
            DevOpsBucketName = devopsBucketName;
            LoggingBucketName = loggingBucketName;
        }
    }
}
