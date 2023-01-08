using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.ApiGateway.Services
{
    public interface IAwsApiGatewayService : ISystemIntegrationService
    {
        public IAwsApiGatewayHelper Helper { get; set; }
    }
}
