using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public static class LaunchPadAbpAppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static LaunchPadAbpAppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "", IList<string> jsonFiles = null)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets, userSecretId, jsonFiles)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "", IList<string> jsonFiles = null)
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

            if (jsonFiles != null)
            { 
                foreach( var jsonFile in jsonFiles)
                {
                    builder.AddJsonFile(jsonFile,
                        optional: false,
                        reloadOnChange: true
                    );
                }
            }

            return builder.Build();
        }

    }
}
