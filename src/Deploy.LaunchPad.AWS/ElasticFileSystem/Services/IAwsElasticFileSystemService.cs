using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.ElasticFileSystem.Services
{
    public interface IAwsElasticFileSystemService : ILaunchPadSystemIntegrationService
    {
        public IAwsElasticFileSystemHelper Helper { get; set; }
    }
}
