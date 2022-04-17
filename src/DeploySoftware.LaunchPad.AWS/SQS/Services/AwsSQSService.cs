using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SQS.Services
{
    public partial class AwsSQSService : SystemIntegrationServiceBase, IAwsSQSService
    {
        public IAwsSQSHelper Helper { get; set; }

        public AwsSQSService() : base()
        {
        }

        public AwsSQSService(ILogger logger) : base(logger)
        {

        }
    }
}
