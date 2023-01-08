using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.Redshift.Services
{
    public interface IAwsRedshiftService : ISystemIntegrationService
    {
        public IAwsRedshiftHelper Helper { get; set; }
    }
}
