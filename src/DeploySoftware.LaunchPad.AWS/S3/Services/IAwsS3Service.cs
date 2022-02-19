using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Core.Configuration;

namespace DeploySoftware.LaunchPad.AWS.S3.Services
{
    public interface IAwsS3Service : ISystemIntegrationService
    {
        public IAwsS3Helper Helper { get; set; }
    }
}
