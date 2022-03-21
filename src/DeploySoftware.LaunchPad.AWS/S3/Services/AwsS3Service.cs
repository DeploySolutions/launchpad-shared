using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.S3.Services
{
    public partial class AwsS3Service : SystemIntegrationServiceBase, IAwsS3Service
    {
        public IAwsS3Helper Helper { get; set; }
    }
}
