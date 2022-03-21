using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SNS.Services
{
    public partial class AwsSNSService : SystemIntegrationServiceBase, IAwsSNSService
    {
        public IAwsSNSHelper Helper { get; set; }
    }
}
