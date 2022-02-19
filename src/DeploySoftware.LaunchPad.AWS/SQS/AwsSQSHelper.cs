using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SQS
{
    public partial class AwsSQSHelper : HelperBase, IAwsSQSHelper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}
