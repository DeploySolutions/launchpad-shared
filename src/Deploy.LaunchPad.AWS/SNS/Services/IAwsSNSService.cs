using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.SNS.Services
{
    public interface IAwsSNSService : ISystemIntegrationService
    {
        public IAwsSNSHelper Helper { get; set; }

    }
}
