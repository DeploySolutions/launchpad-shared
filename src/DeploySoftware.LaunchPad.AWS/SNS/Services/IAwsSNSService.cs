using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Application;

namespace DeploySoftware.LaunchPad.AWS.SNS.Services
{
    public interface IAwsSNSService : ISystemIntegrationService
    {
        public IAwsSNSHelper Helper { get; set; }

    }
}
