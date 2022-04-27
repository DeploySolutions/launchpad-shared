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

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public abstract class LaunchPadAbpModuleHelperBase<TSecretHelper, TSecretVault, THostEnvironment> : HelperBase, 
        ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault, THostEnvironment> 
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where THostEnvironment : IHostEnvironment
    {

        protected readonly THostEnvironment _hostEnvironment;
        public THostEnvironment HostEnvironment
        {
            get
            {
                return _hostEnvironment;
            }
        }

        protected readonly IConfigurationRoot _configurationRoot;
        public IConfigurationRoot ConfigurationRoot
        {
            get
            {
                return _configurationRoot;
            }
        }


        protected TSecretHelper _secretHelper;
        [JsonIgnore]
        public TSecretHelper SecretHelper
        {
            get
            {
                return _secretHelper;
            }
        }


        public LaunchPadAbpModuleHelperBase(ILogger logger, THostEnvironment hostEnvironment, IConfigurationRoot configurationRoot)
        {
            Logger = logger;
            _configurationRoot = configurationRoot;
            _hostEnvironment = hostEnvironment; 
            _secretHelper = new TSecretHelper();
        }

        public LaunchPadAbpModuleHelperBase(ILogger logger, THostEnvironment hostEnvironment, IConfigurationRoot configurationRoot, TSecretHelper secretHelper)
        {
            Logger = logger;
            _configurationRoot = configurationRoot;
            _hostEnvironment = hostEnvironment;
            _secretHelper = secretHelper;
        }


        public virtual void PreInitialize()
        {
        }

        public virtual void PostInitialize()
        {

        }

        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Returns a database connection string, with the value set either locally or in a cloud-hosted secret.
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="connectionStringFieldName">The name of the field which contains the connection string.</param>
        /// <param name="secretVaultIdentifier">The unique identifier, if the database connection string is contained in a cloud-hosted secret.</param>
        /// <returns></returns>
        public virtual string GetDatabaseConnectionString(string connectionStringFieldName, string secretVaultIdentifier = "", bool enableLocalDeveloperSecretsValue = false)
        {
            Guard.Against<InvalidOperationException>(_secretHelper == null, "_secretHelper is null.");
            Guard.Against<InvalidOperationException>(_hostEnvironment == null, "_hostingEnvironment is null.");
            Guard.Against<InvalidOperationException>(_configurationRoot == null, "_appConfiguration is null.");
            Console.WriteLine("LaunchPadAbpModuleHelper.GetDatabaseConnectionString().");

            // check whether to use local db or AWS RDS
            string databaseConnectionString = string.Empty;
            Console.WriteLine("LaunchPadAbpModuleHelper.GetDatabaseConnectionString() enableLocalDeveloperSecretsValue? " + enableLocalDeveloperSecretsValue);
            if (_hostEnvironment.IsDevelopment() && enableLocalDeveloperSecretsValue)
            {
                // Use local Postgres and AWS profile for development
                Console.WriteLine("Is Development = true and enableLocalDeveloperSecretsValue = true.");
                Console.WriteLine("Is Development: Getting connection string from local developer's User Secrets: " + connectionStringFieldName);

                databaseConnectionString = _configurationRoot.GetSection("ConnectionStrings").GetSection(connectionStringFieldName).Value;
                Console.WriteLine("Connection string for development is " + databaseConnectionString);
            }
            else
            {
                Console.WriteLine("Is Development = " + _hostEnvironment.IsDevelopment() + " & enableLocalDeveloperSecretsValue=" + enableLocalDeveloperSecretsValue);
                Console.WriteLine("Getting connection string from remote Secret Vault.");
                if (string.IsNullOrEmpty(secretVaultIdentifier))
                {
                    throw new InvalidOperationException(String.Format("Expected to get connection string '{0}' from remote Secret Vault but secret identifier was null or empty.", connectionStringFieldName));
                }
                else
                {
                    Console.WriteLine(string.Format("Trying to get connection string field {0} from remote Secret with secret identifier {1}.",
                        connectionStringFieldName,
                        secretVaultIdentifier)
                    );
                    databaseConnectionString = _secretHelper.GetDbConnectionStringFromSecret(secretVaultIdentifier, connectionStringFieldName);
                }
            }
            // throw an exception if connectiong string is null or empty
            if (String.IsNullOrEmpty(databaseConnectionString))
            {
                throw new InvalidOperationException(string.Format(
                    "Database connection string was returned null or empty for field {0} in secret vault identifier {1}",
                    connectionStringFieldName,
                    secretVaultIdentifier));
            }
            else
            {
                Console.WriteLine("DB Connection string: " + databaseConnectionString);
                Logger.Debug("DB Connection string: " + databaseConnectionString);
            }
            return databaseConnectionString;

        }

        /// <summary>
        /// Returns a db connection string from the secret
        /// </summary>
        /// <param name="secretVaultIdentifier">The unique ID that represents the secret</param>
        /// <param name="logger">The logging instance</param>
        /// <returns>A database connection string</returns>
        public async Task<String> GetDatabaseConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName)
        {
            string dbConnectionString = await _secretHelper.GetDbConnectionStringFromSecretAsync(secretVaultIdentifier, connectionStringName);
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Debug_GetDbConnectionStringFromSecret, dbConnectionString));
            return dbConnectionString;
        }

        /// <summary>
        /// Gets the default connection string, using either the local settings or settings taken from a cloud-hosted secret
        /// </summary>
        public string GetDefaultDatabaseConnectionString(string defaultDatabaseConnectionStringName, string secretVaultIdentifier = "")
        {
            string defaultDatabaseConnectionString = GetDatabaseConnectionString(
                defaultDatabaseConnectionStringName,
                secretVaultIdentifier
            );
            return defaultDatabaseConnectionString;
        }

        public abstract string GetDefaultConnectionString();

        /// <summary>
        /// Returns the unique identifier value of a secret vault (an AWS ARN, an Azure secrets URL, etc)
        /// from the given app settings section
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public virtual string GetSecretVaultIdentifierFromSetting(string settingName)
        {
            string secretVaultIdentifier = _configurationRoot.GetSection(settingName).Value;
            return secretVaultIdentifier;
        }

        public IDictionary<string, TSecretVault> GetSecretVaults<TModule, TSecretProvider, TAbpModuleHelper>()
            where TModule : ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider, TAbpModuleHelper, THostEnvironment>
            where TSecretProvider : SecretProviderBase<TSecretVault>, new()
            where TAbpModuleHelper : ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault, THostEnvironment>
        {
            Dictionary<string, TSecretVault> secretVaults = null;
            try
            {
                // attempt to get the vaults from the Web Portal Module
                TModule webPortalModule = IocManager.Instance.Resolve<TModule>();
                if (webPortalModule != null)
                {
                    secretVaults = webPortalModule.SecretProvider.SecretVaults;
                }
            }
            catch (Castle.MicroKernel.ComponentNotFoundException cNFEx)
            {
                Logger.Error("Attemping to resolve WebPortalModule component to get Secret Vaults. Exact error was: " + cNFEx.Message);
            }

            return secretVaults;
        }

        public async Task<String> GetJsonFromSecret(string secretVaultIdentifier)
        {
            return await _secretHelper.GetJsonFromSecretAsync(secretVaultIdentifier);
        }


        public abstract bool ShowDetailedErrorsToClient();
    }
}
