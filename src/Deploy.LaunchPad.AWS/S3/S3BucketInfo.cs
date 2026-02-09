using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public class S3BucketInfo : LaunchPadMinimalProperties, ILaunchPadObject
    {
        
        public IDictionary<string,S3FolderInfo> Folders { get; set; }

        public S3BucketInfo(string bucketName)
        {
            Name = new ElementName(bucketName);
            Description = new ElementDescription(bucketName);
            Folders = new Dictionary<string, S3FolderInfo>();
        }
    }
}
