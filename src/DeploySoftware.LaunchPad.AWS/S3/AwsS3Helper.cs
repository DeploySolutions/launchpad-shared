using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public partial class AwsS3Helper : AwsHelperBase, IAwsS3Helper
    {

        public AwsS3Helper() : base()
        {
        }

        public AwsS3Helper(ILogger logger) :base(logger)
        {

        }
    }
}
