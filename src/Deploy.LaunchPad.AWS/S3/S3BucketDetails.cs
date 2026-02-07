using Deploy.LaunchPad.Core.Domain.Model;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.S3
{
    public class S3BucketDetails : LaunchPadMinimalProperties, ILaunchPadObject
    {
        public string BucketName { get; set; }
        
        public IDictionary<string,S3FolderDetails> Folders { get; set; }

        public S3BucketDetails(string bucketName)
        {
            BucketName = bucketName;
            Folders = new Dictionary<string, S3FolderDetails>();
        }
    }
}
