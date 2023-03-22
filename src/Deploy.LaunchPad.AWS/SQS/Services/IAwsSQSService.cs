using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.SQS.Services
{
    public interface IAwsSQSService : ILaunchPadSystemIntegrationService
    {
        public IAwsSQSHelper Helper { get; set; }
    }
}
