using Abp.Configuration;
using Deploy.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial class LaunchPadAbpAppSettingProvider : SettingProvider
    {
        protected readonly IConfiguration _appConfiguration;

        protected readonly DictionaryHelper _dictionaryHelper = new DictionaryHelper();

        public LaunchPadAbpAppSettingProvider()
        {
        }

        public LaunchPadAbpAppSettingProvider(ILaunchPadAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {

            var comparer = StringComparer.OrdinalIgnoreCase;
            IDictionary<string, SettingDefinition> settingDefinitions = new Dictionary<string, SettingDefinition>(comparer);

            var emailConfirmationRequiredSetting = new SettingDefinition("Abp.Zero.UserManagement.IsEmailConfirmationRequiredForLogin", "true", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: false);
            settingDefinitions.Add(emailConfirmationRequiredSetting.Name, emailConfirmationRequiredSetting);
            return settingDefinitions.Values.ToArray();

        }


        /// <summary>
        /// Adds a new SettingDefinition for each secret listed in the SecretProvider element.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public IDictionary<string, SettingDefinition> GetSecretProviderSettings(string jsonSecretProviderVaultsPathInAppSettings)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            IDictionary<string, SettingDefinition> settingDefinitions = new Dictionary<string, SettingDefinition>(comparer);
            IDictionary<string, string> secretProviderSection = _appConfiguration.GetSection(jsonSecretProviderVaultsPathInAppSettings).Get<Dictionary<string, string>>();
            if (secretProviderSection != null && secretProviderSection.Count > 0)
            {
                foreach (var secretVault in secretProviderSection)
                {
                    SettingDefinition secretVaultSettingDefinition = new SettingDefinition(
                        jsonSecretProviderVaultsPathInAppSettings + ":" + secretVault.Key,
                        secretVault.Value,
                        scopes: SettingScopes.Application,
                        clientVisibilityProvider: new HiddenSettingClientVisibilityProvider()
                    );
                    settingDefinitions.Add(secretVaultSettingDefinition.Name, secretVaultSettingDefinition);

                }
            }

            return settingDefinitions;
        }
    }
}
