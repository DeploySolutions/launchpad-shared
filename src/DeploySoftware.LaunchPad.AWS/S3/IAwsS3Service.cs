using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public interface IAwsS3Service : ILaunchPadDomainService
    {
        public IAwsS3Helper Helper { get; set; }
    }
}
