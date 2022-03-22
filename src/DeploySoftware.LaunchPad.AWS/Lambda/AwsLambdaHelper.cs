using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Lambda
{
    public partial class AwsLambdaHelper : HelperBase, IAwsLambdaHelper
    {
        public AwsCommonHelper AwsCommonHelper { get; set; }
    }
}
