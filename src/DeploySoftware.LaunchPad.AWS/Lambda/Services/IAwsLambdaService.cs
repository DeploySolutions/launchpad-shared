using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Application;

namespace DeploySoftware.LaunchPad.AWS.Lambda.Services
{
    public interface IAwsLambdaService : ISystemIntegrationService
    {
        public IAwsLambdaHelper Helper { get; set; }
    }
}
