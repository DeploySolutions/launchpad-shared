using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.Lambda;
using Deploy.LaunchPad.AWS.Lambda.Services;
using Deploy.LaunchPad.Core.Abp.Application;

namespace Deploy.LaunchPad.AWS.Abp.Lambda.Services
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
