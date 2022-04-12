using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SQS
{
    public partial class AwsSQSHelper : AwsHelperBase, IAwsSQSHelper
    {
        public AwsSQSHelper() : base()
        {
        }

        public AwsSQSHelper(ILogger logger) : base(logger)
        {

        }
    }
}
