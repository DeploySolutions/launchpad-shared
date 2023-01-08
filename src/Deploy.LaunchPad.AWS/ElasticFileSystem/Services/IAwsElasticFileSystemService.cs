using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.AWS.ElasticFileSystem.Services
{
    public interface IAwsElasticFileSystemService : ISystemIntegrationService
    {
        public IAwsElasticFileSystemHelper Helper { get; set; }
    }
}
