using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.SQS.Services
{
    public interface IAwsSQSService : ISystemIntegrationService
    {
        public IAwsSQSHelper Helper { get; set; }
    }
}
