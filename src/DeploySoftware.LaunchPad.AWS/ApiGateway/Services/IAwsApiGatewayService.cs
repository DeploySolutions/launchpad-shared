using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Application;

namespace DeploySoftware.LaunchPad.AWS.ApiGateway.Services
{
    public interface IAwsApiGatewayService : ISystemIntegrationService
    {
        public IAwsApiGatewayHelper Helper { get; set; }
    }
}
