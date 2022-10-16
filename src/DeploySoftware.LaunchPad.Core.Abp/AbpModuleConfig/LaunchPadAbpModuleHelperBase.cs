using Abp.Dependency;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public abstract class LaunchPadAbpModuleHelperBase<TSecretHelper, TSecretVault> : HelperBase, 
        ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault> 
        where TSecretHelper : ISecretHelper
        where TSecretVault : SecretVaultBase, new()
    {


        protected TSecretHelper _secretHelper;
        [JsonIgnore]
        public TSecretHelper SecretHelper
        {
            get
            {
                return _secretHelper;
            }
        }


        public LaunchPadAbpModuleHelperBase(ILogger logger) :base(logger)
        {
            Logger = logger;
        }

        public LaunchPadAbpModuleHelperBase(ILogger logger, TSecretHelper secretHelper) : base(logger)
        {
            Logger = logger;
            _secretHelper = secretHelper;
        }

        /// <summary>
        /// Returns a database connection string, with the value set either locally in userSecrets.json (if secretVaultIdentifier = "userSecrets.json") or in a Secret Vault (probably a cloud-hosted secret service).
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="secretVaultIdentifier">The unique identifier. If it's "userSecrets.json" it will attempt to load from local user secrets. Otherwise it will attempt to connect to a cloud-hosted secret service.</param>
        /// <returns></returns>
        public virtual string GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false)
        {
            string invalidConfigurationMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but configuration is null.", caller);
            string invalidConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but connectionStringFieldName is null or empty.", caller);
            Guard.Against<ArgumentNullException>(configuration == null, invalidConfigurationMessage);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(connectionStringFieldName), invalidConnectionStringMessage);
            Logger.Debug("LaunchPadAbpModuleHelperBase.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Started.");
            string databaseConnectionString = string.Empty;

            if (secretVaultIdentifier.ToLower().Equals("usersecrets.json"))
            {
                databaseConnectionString=GetDatabaseConnectionStringFromLocalUserSecrets(configuration, connectionStringFieldName, caller, shouldLogConnectionString);
            }
            else
            {
                databaseConnectionString=GetDatabaseConnectionStringFromSecretVault(configuration, connectionStringFieldName, secretVaultIdentifier, caller, shouldLogConnectionString);
            }
            Logger.Debug("LaunchPadAbpModuleHelperBase.GetDatabaseConnectionString(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Ended.");

            return databaseConnectionString;
        }



        /// <summary>
        /// Returns a database connection string, with the value set in a Secret Vault (probably a cloud-hosted secret service).
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="secretVaultIdentifier">The unique identifier, if the database connection string is contained in a cloud-hosted secret.</param>
        /// <returns></returns>
        public virtual string GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false)
        {
            string invalidConfigurationMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but configuration is null.", caller);
            string invalidConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Getting connection string for caller '{0}', but connectionStringFieldName is null or empty.", caller);
            Guard.Against<ArgumentNullException>(configuration == null, invalidConfigurationMessage);
            Guard.Against<ArgumentNullException>(String.IsNullOrEmpty(connectionStringFieldName), invalidConnectionStringMessage);

            string databaseConnectionString = string.Empty;

            string getFromRemoteMessage = string.Format("Getting connection string value from field '{0}' in remote Secret Vault with identifier '{1}' for caller '{2}'.",
                connectionStringFieldName,
                secretVaultIdentifier,
                caller
            );
            Logger.Debug(getFromRemoteMessage);
            Console.WriteLine(getFromRemoteMessage);
            Guard.Against<InvalidOperationException>(_secretHelper == null, "Getting value from remote Secret Vault relies on the _secretHelper variable but that is null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(secretVaultIdentifier), "Getting value from remote Secret Vault relies on the secretVaultIdentifier argument but that is null or empty.");

            databaseConnectionString = _secretHelper.GetDbConnectionStringFromSecret(secretVaultIdentifier, connectionStringFieldName, caller);

            // make sure the returned connection string is not empty
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(databaseConnectionString), "Expected a database connection string but it is null or empty.");


            // make sure the format is correct
            bool isValid = CheckIfDatabaseConnectionStringFormatIsValid(databaseConnectionString, caller);
            string incorrectFormatMessage = string.Format("Database connection string was not provided in the expected format, which is 'User ID=SOMEUSER;Password=SOMEPASSWORD;Host=SOMEHOST;Port=SOMEPORT;Database=SOMEDATABASE;', for caller '{0}'.", caller);
            Guard.Against<InvalidOperationException>(!isValid, incorrectFormatMessage);

            // now should we log it to file or console? Default is false so as to avoid security leaks but can be enabled for debugging purposes.
            if (shouldLogConnectionString)
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}'. Connection string is: '{1}'", caller, databaseConnectionString);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            else
            {
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromSecretVault(IConfigurationRoot configuration, string connectionStringFieldName, string secretVaultIdentifier, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}', but shouldLogConnectionString parameter was set to false, so we will not display the value.", caller);
                Logger.Debug(outputConnectionStringMessage);
                Console.WriteLine(outputConnectionStringMessage);
            }
            return databaseConnectionString;
        }


        /// <summary>
        /// Returns a database connection string, with the value provided from a local development userSecrets.json file
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <returns></returns>
        public virtual string GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
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
                string outputConnectionStringMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionStringFromLocalUserSecrets(IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false) => Successfully got database connection string for caller '{0}'. Connection string is: '{1}'", caller, databaseConnectionString);
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
        /// <param name="appConfig"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public virtual string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName)
        {
            string secretVaultIdentifier = configuration.GetSection(settingName).Value;
            return secretVaultIdentifier;
        }


        public async Task<String> GetJsonFromSecret(string secretVaultIdentifier, string caller)
        {
            return await _secretHelper.GetJsonFromSecretAsync(secretVaultIdentifier, caller);
        }


        public abstract bool ShowDetailedErrorsToClient();
    }
}
