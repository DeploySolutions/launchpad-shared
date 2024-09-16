// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpAppSettingProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Configuration;
using Deploy.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Class LaunchPadAbpAppSettingProvider.
    /// Implements the <see cref="SettingProvider" />
    /// </summary>
    /// <seealso cref="SettingProvider" />
    public partial class LaunchPadAbpAppSettingProvider : SettingProvider
    {
        /// <summary>
        /// The application configuration
        /// </summary>
        protected readonly IConfiguration _appConfiguration;

        /// <summary>
        /// The dictionary helper
        /// </summary>
        protected readonly DictionaryHelper _dictionaryHelper = new DictionaryHelper();

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpAppSettingProvider"/> class.
        /// </summary>
        public LaunchPadAbpAppSettingProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpAppSettingProvider"/> class.
        /// </summary>
        /// <param name="configurationAccessor">The configuration accessor.</param>
        public LaunchPadAbpAppSettingProvider(ILaunchPadAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>List of settings</returns>
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
        /// <param name="jsonSecretProviderVaultsPathInAppSettings">The json secret provider vaults path in application settings.</param>
        /// <returns>IDictionary&lt;System.String, SettingDefinition&gt;.</returns>
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
