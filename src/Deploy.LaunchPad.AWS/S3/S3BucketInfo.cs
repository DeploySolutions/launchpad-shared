using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Core.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial class S3BucketInfo : LaunchPadMinimalProperties, IS3BucketInfo
    {
        
        public S3BucketInfo(string bucketName)
        {
            Name = new ElementName(bucketName);
            Description = new ElementDescription(bucketName);
        }
    }
}
