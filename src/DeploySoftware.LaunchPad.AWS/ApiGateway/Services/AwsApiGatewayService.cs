using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.ApiGateway.Services
{
    public partial class AwsApiGatewayService : SystemIntegrationServiceBase, IAwsApiGatewayService
    {
        public IAwsApiGatewayHelper Helper { get; set; }
    }
}
