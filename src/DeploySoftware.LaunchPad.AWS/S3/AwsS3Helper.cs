using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public partial class AwsSNSHelper : HelperBase, IAwsS3Helper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}
