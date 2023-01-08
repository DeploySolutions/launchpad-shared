using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.Lambda.Services
{
    public interface IAwsLambdaService : ISystemIntegrationService
    {
        public IAwsLambdaHelper Helper { get; set; }
    }
}
