using Abp.Configuration;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
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
    }
}
