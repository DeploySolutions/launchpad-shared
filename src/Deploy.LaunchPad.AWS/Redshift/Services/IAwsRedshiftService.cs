using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.Redshift.Services
{
    public interface IAwsRedshiftService : ILaunchPadSystemIntegrationService
    {
        public IAwsRedshiftHelper Helper { get; set; }
    }
}
