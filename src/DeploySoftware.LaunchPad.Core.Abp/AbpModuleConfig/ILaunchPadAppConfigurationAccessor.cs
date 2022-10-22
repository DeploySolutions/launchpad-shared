using Abp.Dependency;
using Microsoft.Extensions.Configuration;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public interface ILaunchPadAppConfigurationAccessor : ISingletonDependency
    {
        IConfigurationRoot Configuration { get; }
    }
}