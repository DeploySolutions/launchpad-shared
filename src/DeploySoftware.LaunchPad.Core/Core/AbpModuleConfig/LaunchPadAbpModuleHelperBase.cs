﻿using Abp.Dependency;
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

        protected TSecretHelper _secretHelper;
        [JsonIgnore]
        public TSecretHelper SecretHelper
        {
            get
            {
                return _secretHelper;
            }
        }


        public LaunchPadAbpModuleHelperBase(ILogger logger, THostEnvironment hostEnvironment, IConfigurationRoot configurationRoot) :base(logger, configurationRoot)
        {
            Logger = logger;
            _hostEnvironment = hostEnvironment; 
            _secretHelper = new TSecretHelper();
        }

        public LaunchPadAbpModuleHelperBase(ILogger logger, THostEnvironment hostEnvironment, IConfigurationRoot configurationRoot, TSecretHelper secretHelper) : base(logger, configurationRoot)
        {
            Logger = logger;
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
        public virtual string GetDatabaseConnectionString(string connectionStringFieldName, string secretVaultIdentifier, string caller, bool enableLocalDeveloperSecretsValue = false)
        {
            Guard.Against<InvalidOperationException>(_secretHelper == null, "_secretHelper is null.");
            Guard.Against<InvalidOperationException>(_hostEnvironment == null, "_hostingEnvironment is null.");
            Guard.Against<InvalidOperationException>(_configurationRoot == null, "_appConfiguration is null.");
            Logger.Debug("LaunchPadAbpModuleHelper.GetDatabaseConnectionString().");
            Console.WriteLine("LaunchPadAbpModuleHelper.GetDatabaseConnectionString().");

            // check whether to use local db or AWS RDS
            string databaseConnectionString = string.Empty;
            string enableLocalDeveloperSecretsValueMessage = string.Format("LaunchPadAbpModuleHelper.GetDatabaseConnectionString() => enableLocalDeveloperSecretsValue? {0}.", enableLocalDeveloperSecretsValue);
            Console.WriteLine(enableLocalDeveloperSecretsValueMessage);
            Logger.Debug(enableLocalDeveloperSecretsValueMessage);
            if (_hostEnvironment.IsDevelopment() && enableLocalDeveloperSecretsValue)
            {
                // Use local Postgres and AWS profile for development
                string isDevelopmentAndLocalSecretsMessage = string.Format("Getting connection string from local developer's User Secrets, connection string field name: {0}", connectionStringFieldName);
                Logger.Debug(isDevelopmentAndLocalSecretsMessage);
                Console.WriteLine(isDevelopmentAndLocalSecretsMessage);

                databaseConnectionString = _configurationRoot.GetSection("ConnectionStrings").GetSection(connectionStringFieldName).Value;
                Logger.Debug("Connection string for development is " + databaseConnectionString);
                Console.WriteLine("Connection string for development is " + databaseConnectionString);
            }
            else
            {
                string isNotDevelopmentAndEnableLocalDeveloperSecretsMessage = string.Format("Is Development? {0} .enableLocalDeveloperSecretsValue is '{1}'. Getting connection string from remote Secret Vault.", _hostEnvironment.IsDevelopment(), enableLocalDeveloperSecretsValue);
                Logger.Debug(isNotDevelopmentAndEnableLocalDeveloperSecretsMessage);
                Console.WriteLine(isNotDevelopmentAndEnableLocalDeveloperSecretsMessage);
                if (string.IsNullOrEmpty(secretVaultIdentifier))
                {
                    throw new InvalidOperationException(String.Format("Expected to get connection string '{0}' from remote Secret Vault but secret identifier was null or empty.", connectionStringFieldName));
                }
                else
                {
                    string message = string.Format("Trying to get connection string field {0} from remote Secret with secret identifier {1}.",
                        connectionStringFieldName,
                        secretVaultIdentifier);
                    Logger.Debug(message);
                    Console.WriteLine(message);
                    databaseConnectionString = _secretHelper.GetDbConnectionStringFromSecret(secretVaultIdentifier, connectionStringFieldName, caller);
                    Logger.Debug("Got connection string.");
                    Console.WriteLine(databaseConnectionString);
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
                if(_hostEnvironment.IsDevelopment())
                {
                    Console.WriteLine("DB Connection string: " + databaseConnectionString);
                    Logger.Debug("DB Connection string: " + databaseConnectionString);
                }
            }
            return databaseConnectionString;

        }

        /// <summary>
        /// Returns a db connection string from the secret
        /// </summary>
        /// <param name="secretVaultIdentifier">The unique ID that represents the secret</param>
        /// <param name="logger">The logging instance</param>
        /// <returns>A database connection string</returns>
        public async Task<String> GetDatabaseConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName, string caller)
        {
            string dbConnectionString = await _secretHelper.GetDbConnectionStringFromSecretAsync(secretVaultIdentifier, connectionStringName, caller);
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_Core_Resources.Debug_GetDbConnectionStringFromSecret, dbConnectionString));
            return dbConnectionString;
        }

        /// <summary>
        /// Gets the default connection string, using either the local settings or settings taken from a cloud-hosted secret
        /// </summary>
        public string GetDefaultDatabaseConnectionString(string defaultDatabaseConnectionStringName, string secretVaultIdentifier, string caller)
        {
            string defaultDatabaseConnectionString = GetDatabaseConnectionString(
                defaultDatabaseConnectionStringName,
                secretVaultIdentifier,
                caller
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

        public async Task<String> GetJsonFromSecret(string secretVaultIdentifier, string caller)
        {
            return await _secretHelper.GetJsonFromSecretAsync(secretVaultIdentifier, caller);
        }


        public abstract bool ShowDetailedErrorsToClient();
    }
}
