using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager.Services
{
    public partial class AwsSecretsManagerService : SystemIntegrationServiceBase, IAwsSecretsManagerService
    {
        public IAwsSecretsManagerHelper Helper { get; set; }

        protected AwsSecretsManagerService() :base()
        {
        }
        public AwsSecretsManagerService(ILogger logger) :base(logger)
        {
            Helper = new AwsSecretsManagerHelper();
        }

        public AwsSecretsManagerService(ILogger logger, IAwsSecretsManagerHelper helper) : base(logger)
        {
            Helper = helper;
        }
    }
}
