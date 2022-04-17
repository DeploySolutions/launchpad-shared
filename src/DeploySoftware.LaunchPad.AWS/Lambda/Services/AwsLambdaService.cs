using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
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

        public AwsLambdaService() : base()
        {
        }

        public AwsLambdaService(ILogger logger) : base(logger)
        {

        }
    }
}
