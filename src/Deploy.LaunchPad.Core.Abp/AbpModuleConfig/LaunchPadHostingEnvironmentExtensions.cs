using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public static class LaunchPadHostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env, string userSecretId = "", IList<string> jsonFiles = null)
        {
            return LaunchPadAbpAppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment(), userSecretId, jsonFiles);
        }
    }
}
