using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public static class LaunchPadAbpAppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static LaunchPadAbpAppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "")
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets, userSecretId)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets && !string.IsNullOrEmpty(userSecretId))
            {
                builder.AddUserSecrets(userSecretId);
            }

            return builder.Build();
        }

    }
}
