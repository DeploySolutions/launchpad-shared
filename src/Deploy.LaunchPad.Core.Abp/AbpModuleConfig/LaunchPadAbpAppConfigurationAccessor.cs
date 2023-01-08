using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public class LaunchPadAbpAppConfigurationAccessor : ILaunchPadAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public LaunchPadAbpAppConfigurationAccessor(IWebHostEnvironment env)
        {
            Configuration = env.GetAppConfiguration();
        }
    }
}
