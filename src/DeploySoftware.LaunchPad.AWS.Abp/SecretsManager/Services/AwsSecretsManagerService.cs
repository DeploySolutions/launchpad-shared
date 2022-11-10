using Amazon.SecretsManager;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.AWS.SecretsManager;
using DeploySoftware.LaunchPad.AWS.SecretsManager.Services;
using DeploySoftware.LaunchPad.Core.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace DeploySoftware.LaunchPad.AWS.Abp.SecretsManager.Services
{
    public partial class AwsSecretsManagerService : SystemIntegrationServiceBase, IAwsSecretsManagerService
    {

        public AwsSecretsManagerService() :base()
        {
        }

        
    }
}
