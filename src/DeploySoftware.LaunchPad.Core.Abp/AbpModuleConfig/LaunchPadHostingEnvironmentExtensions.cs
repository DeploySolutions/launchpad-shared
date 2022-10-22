using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public static class LaunchPadHostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env, string userSecretId = "")
        {
            return LaunchPadAbpAppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment(), userSecretId);
        }
    }
}
