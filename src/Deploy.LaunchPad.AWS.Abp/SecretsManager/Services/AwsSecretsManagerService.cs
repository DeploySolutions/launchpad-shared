using Deploy.LaunchPad.AWS.SecretsManager.Services;
using Deploy.LaunchPad.Core.Abp.Application;

namespace Deploy.LaunchPad.AWS.Abp.SecretsManager.Services
{
    public partial class AwsSecretsManagerService : LaunchPadAbpSystemIntegrationServiceBase, IAwsSecretsManagerService
    {

        public AwsSecretsManagerService() : base()
        {
        }


    }
}
