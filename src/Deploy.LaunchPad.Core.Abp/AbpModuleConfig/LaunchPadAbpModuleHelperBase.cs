// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-19-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpModuleHelperBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Configuration;
using Abp.Dependency;
using Castle.Core.Logging;
using Deploy.LaunchPad.Code.Config;
using Deploy.LaunchPad.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Class LaunchPadAbpModuleHelperBase.
    /// Implements the <see cref="HelperBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModuleHelper" />
    /// Implements the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="HelperBase" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModuleHelper" />
    /// <seealso cref="ISingletonDependency" />
    public abstract partial class LaunchPadAbpModuleHelperBase : HelperBase,
        ILaunchPadAbpModuleHelper
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpModuleHelperBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected LaunchPadAbpModuleHelperBase(ILogger logger) : base(logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the module secrets.
        /// </summary>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IDictionary<string, string> GetModuleSecrets()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the module secrets for vault identifier.
        /// </summary>
        /// <param name="vaultIdentifier">The vault identifier.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IDictionary<string, string> GetModuleSecretsForVaultIdentifier(string vaultIdentifier)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the module vaults.
        /// </summary>
        /// <returns>IList&lt;ISecretVault&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IList<ISecretVault> GetModuleVaults()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Adds the module secret vaults to provider.
        /// </summary>
        /// <typeparam name="TSecretVault">The type of the t secret vault.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="secretProviderVaultsJsonPath">The secret provider vaults json path.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, ISecretVault&gt;.</returns>
        public virtual IDictionary<string, ISecretVault> AddModuleSecretVaultsToProvider<TSecretVault>(ISecretProvider provider, string secretProviderVaultsJsonPath, string caller)
            where TSecretVault : ISecretVault, new()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            IDictionary<string, TSecretVault> vaults = new Dictionary<string, TSecretVault>(comparer);

            // add the secret vaults to the module's SecretProvider provider
            var appSettings = IocManager.Instance.Resolve<ISettingManager>();
            var secretProviderItems = appSettings.GetAllSettingValues().Where(
                    x => x.Name.StartsWith(secretProviderVaultsJsonPath, StringComparison.InvariantCultureIgnoreCase)
                ).ToList();
            foreach (var setting in secretProviderItems)
            {
                TSecretVault vault = new TSecretVault();
                string name = setting.Name.Replace(secretProviderVaultsJsonPath + ":", string.Empty);
                vault.Name = name;
                vault.ProviderId = provider.Id;
                vault.VaultId = setting.Value;

                // get the secret fields from the secret json
                IDictionary<string, string> secretValues = provider.GetAllValuesFromSecretVaultAsync(vault, caller).Result;
                DictionaryHelper dictionaryHelper = new DictionaryHelper();
                vault.Fields = dictionaryHelper.MergeDictionaries(vault.Fields, secretValues);

                // add the new vault to the secret vaults dictionary
                vaults = dictionaryHelper.AddToDictionary(vaults, name, vault);

            }
            return (IDictionary<string, ISecretVault>)vaults;
        }


        /// <summary>
        /// Returns a database connection string, with the value set either locally in userSecrets.json (if secretVaultIdentifier = "secrets.json") or in a Secret Vault (probably a cloud-hosted secret service).
        /// </summary>
        /// <typeparam name="TSecretVault">The type of the t secret vault.</typeparam>
        /// <param name="vault">The vault.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="caller">The caller.</param>
        /// <param name="shouldLogConnectionString">if set to <c>true</c> [should log connection string].</param>
        /// <returns>System.String.</returns>
        public virtual string GetDatabaseConnectionString<TSecretVault>(TSecretVault vault, IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
            where TSecretVault : ISecretVault
        {
            string invalidConfigurationMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but configuration is null.", caller);
            string invalidConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but connectionStringFieldName is null or empty.", caller);
            Guard.Against<ArgumentNullException>(configuration == null, invalidConfigurationMessage);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(connectionStringFieldName), invalidConnectionStringMessage);
            Logger.Debug("LaunchPadAbpModuleHelperBase.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Started.");
            string databaseConnectionString = string.Empty;

            if (vault.Name.ToLower().Equals("secrets.json"))
            {
                databaseConnectionString = GetDatabaseConnectionStringFromLocalUserSecrets(configuration, connectionStringFieldName, caller, shouldLogConnectionString);
            }
            else
            {
                databaseConnectionString = GetDatabaseConnectionStringFromSecretVault<TSecretVault>(vault, connectionStringFieldName, caller, shouldLogConnectionString);
            }
            Logger.Debug("LaunchPadAbpModuleHelperBase.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Ended.");

            return databaseConnectionString;
        }

        /// <summary>
        /// Returns a database connection string, with the value set in a Secret Vault (probably a cloud-hosted secret service).
        /// </summary>
        /// <typeparam name="TSecretVault">The type of the t secret vault.</typeparam>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="caller">The caller.</param>
        /// <param name="shouldLogConnectionString">if set to <c>true</c> [should log connection string].</param>
        /// <returns>System.String.</returns>
        protected virtual string GetDatabaseConnectionStringFromSecretVault<TSecretVault>(TSecretVault secretVault, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
            where TSecretVault : ISecretVault
        {
            string invalidConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but connectionStringFieldName is null or empty.", caller);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(connectionStringFieldName), invalidConnectionStringMessage);

            string databaseConnectionString = string.Empty;

            string getFromRemoteMessage = string.Format("Getting connection string value from field '{0}' in remote Secret Vault with identifier '{1}' for caller '{2}'.",
                connectionStringFieldName,
                secretVault.Id,
                caller
            );
            Logger.Debug(getFromRemoteMessage);
            Console.WriteLine(getFromRemoteMessage);
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(secretVault.Id), "Getting value from remote Secret Vault relies on the secretVaultIdentifier argument but that is null or empty.");

            databaseConnectionString = secretVault.GetValue(connectionStringFieldName, caller);

            // make sure the returned connection string is not empty
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(databaseConnectionString), "Expected a database connection string but it is null or empty.");


            // make sure the format is correct
            bool isValid = CheckIfDatabaseConnectionStringFormatIsValid(databaseConnectionString, caller);
            string incorrectFormatMessage = string.Format("Database connection string was not provided in the expected format, which is 'User ID=SOMEUSER;Password=SOMEPASSWORD;Host=SOMEHOST;Port=SOMEPORT;Database=SOMEDATABASE;', for caller '{0}'.", caller);
            Guard.Against<InvalidOperationException>(!isValid, incorrectFormatMessage);

            // now should we log it to file or console? Default is false so as to avoid security leaks but can be enabled for debugging purposes.
            if (shouldLogConnectionString)
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}'. shouldLogConnectionString parameter was set to true, Connection string is: '{1}'", caller, databaseConnectionString);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            else
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}', but shouldLogConnectionString parameter was set to false, so we will not display the value.", caller);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            return databaseConnectionString;
        }


        /// <summary>
        /// Returns a database connection string, with the value provided from a local development userSecrets.json file
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="caller">The caller.</param>
        /// <param name="shouldLogConnectionString">if set to <c>true</c> [should log connection string].</param>
        /// <returns>System.String.</returns>
        protected virtual string GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
        {
            string invalidConfigurationMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but configuration is null.", caller);
            string invalidConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but connectionStringFieldName is null or empty.", caller);
            Guard.Against<ArgumentNullException>(configuration == null, invalidConfigurationMessage);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(connectionStringFieldName), invalidConnectionStringMessage);

            string databaseConnectionString = string.Empty;

            // Use local userSecrets.json value
            string isDevelopmentAndLocalSecretsMessage = string.Format("Getting connection string value from field '{0}' in local developer's User Secrets for caller '{1}'.",
                connectionStringFieldName,
                caller
            );
            Logger.Debug(isDevelopmentAndLocalSecretsMessage);
            Console.WriteLine(isDevelopmentAndLocalSecretsMessage);
            databaseConnectionString = configuration.GetSection("ConnectionStrings").GetSection(connectionStringFieldName).Value;

            // make sure the returned connection string is not empty
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(databaseConnectionString), "Expected a database connection string but it is null or empty.");

            // make sure the format is correct
            bool isValid = CheckIfDatabaseConnectionStringFormatIsValid(databaseConnectionString, caller);
            string incorrectFormatMessage = string.Format("Database connection string was not provided in the expected format, which is 'User ID=SOMEUSER;Password=SOMEPASSWORD;Host=SOMEHOST;Port=SOMEPORT;Database=SOMEDATABASE;', for caller '{0}'.", caller);
            Guard.Against<InvalidOperationException>(!isValid, incorrectFormatMessage);

            // now should we log it to file or console? Default is false so as to avoid security leaks but can be enabled for debugging purposes.
            if (shouldLogConnectionString)
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}'. shouldLogConnectionString parameter was set to true, Connection string is: '{1}'", caller, databaseConnectionString);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            else
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}', but shouldLogConnectionString parameter was set to false, so we will not display the value.", caller);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            return databaseConnectionString;

        }

        /// <summary>
        /// Checks if database connection string format is valid.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        /// <param name="caller">The caller.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool CheckIfDatabaseConnectionStringFormatIsValid(string databaseConnectionString, string caller)
        {
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(databaseConnectionString), "Expected a database connection string but it is null or empty.");

            // make sure the correct string value is provided
            // format is: User ID=SOMEUSER;Password=SOMEPASSWORD;Host=SOMEHOST;Port=SOMEPORT;Database=SOMEDATABASE;
            // REGEX is: User ID=(.*?);Password=(.*?);Host=(.*?);Port=(.*?);Database=(.*?);
            string regexDbConnectionStringPattern = "User ID=(.*?);Password=(.*?);Host=(.*?);Port=(.*?);Database=(.*?);";
            Stopwatch sw = new Stopwatch();
            sw = Stopwatch.StartNew();
            bool isValid = Regex.IsMatch(databaseConnectionString, regexDbConnectionStringPattern, RegexOptions.IgnoreCase);
            sw.Stop();
            return isValid;
        }

        /// <summary>
        /// Returns the unique identifier value of a secret vault (an AWS ARN, an Azure secrets URL, etc)
        /// from the given app settings section
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>System.String.</returns>
        public virtual string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName)
        {
            string secretVaultIdentifier = configuration.GetSection(settingName).Value;
            return secretVaultIdentifier;
        }



    }
}
