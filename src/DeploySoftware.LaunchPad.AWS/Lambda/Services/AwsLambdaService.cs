using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Lambda.Services
{
    public partial class AwsLambdaService : SystemIntegrationServiceBase, IAwsLambdaService
    {
        public IAwsLambdaHelper Helper { get; set; }
    }
}
