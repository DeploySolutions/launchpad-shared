// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="SecretProviderBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Connections.Database.Definitions;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    /// <summary>
    /// Class SecretProviderBase.
    /// Implements the <see cref="Core.Config.ConnectionProvider" />
    /// </summary>
    /// <seealso cref="Core.Config.ConnectionProvider" />
    public partial class ConnectionProvider : LaunchPadServiceBase, IConnectionProvider, ITransientDependency
    {

        protected readonly ISecretProvider _secretProvider;

        protected readonly IDictionary<string, ILaunchPadConnectionDefinition> _connections = new Dictionary<string, ILaunchPadConnectionDefinition>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        public ConnectionProvider(ISecretProvider secretProvider)
        {
            _secretProvider = secretProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ConnectionProvider(ILogger logger, ISecretProvider secretProvider)
        {
            if (logger != null)
            {
                Logger = logger;
            }
            _secretProvider = secretProvider;
        }

        public virtual void AddConnection(ILaunchPadConnectionDefinition connectionDefinition)
        {
            _connections.TryAdd(connectionDefinition.Name, connectionDefinition);
        }

        public virtual void RemoveConnection(string connectionDefinitionName)
        {
            _connections.Remove(connectionDefinitionName);
        }

        public virtual ILaunchPadDatabaseConnectionDefinition SetDefaultDatabaseConnection()
        {
            string defaultConnectionStringName = "postgresql_default_connection";
            if (_connections.TryGetValue(defaultConnectionStringName, out var connectionDefinition) &&
                connectionDefinition is ILaunchPadDatabaseConnectionDefinition dbConnection)
            {
                return dbConnection;
            }
            throw new InvalidOperationException("SetDefaultDatabaseConnection failed, could not find a matching connectionName. Is it present in the _connections dictionary?)");
        }

        public virtual IDictionary<string, ILaunchPadConnectionDefinition> GetConnectionsFromSecrets()
        {
            // foreach secret vault, load all secrets that are tagged as connection definitions, and add them to the connections collection
            string name = "test";
            string hostName = "test";
            string databaseName = "test";
            ISecretFieldReference userNameSecretRef = new SecretFieldReference("field:database:name", "PRR Secret Vault", Secrets.SecretVaultType.AwsSecretsManager);
            ISecretFieldReference passwordSecretRef = new SecretFieldReference("field:database:name", "PRR Secret Vault", Secrets.SecretVaultType.AwsSecretsManager);
            ISecretFieldReference nameSecretRef = new SecretFieldReference("field:database:name", "PRR Secret Vault", Secrets.SecretVaultType.AwsSecretsManager);
            ILaunchPadConnectionDefinition connectionDefinition = new LaunchPadDatabaseConnectionDefinition(name, hostName, databaseName, userNameSecretRef, passwordSecretRef);
            AddConnection(connectionDefinition);
            return _connections;
        }

        public virtual string GetDatabaseConnectionString(string connectionName)
        {
            return "User ID=postgres;Password=g6E0!pzVK*tX.hDdZ8MS[mJ*$(:W;Host=dss-prr-crm-dev.c4cq6vdw0ezp.us-east-1.rds.amazonaws.com;Port=5432;Database=boilerplate_zero;";
        }
    }
}
