using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.ApiGateway.Services
{
    public interface IAwsApiGatewayService : ILaunchPadSystemIntegrationService
    {
        public IAwsApiGatewayHelper Helper { get; set; }
    }
}
