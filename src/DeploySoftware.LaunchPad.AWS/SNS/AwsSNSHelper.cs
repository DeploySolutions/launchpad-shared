using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SNS
{
    public partial class AwsSNSHelper : HelperBase, IAwsSNSHelper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}
