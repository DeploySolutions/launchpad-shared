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
using Deploy.LaunchPad.Core.Connections.Database;
using Deploy.LaunchPad.Core.Connections.Database.Definitions;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Core.Secrets.Resolver;
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

        protected readonly IDictionary<string, ILaunchPadConnection> _connections = new Dictionary<string, ILaunchPadConnection>();
        public IDictionary<string, ILaunchPadConnection> Connections => _connections;

        public ILaunchPadDatabaseConnection DefaultDatabaseConnection { get; set; }

        public string DefaultConnectionStringName { get; set; }


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

        public virtual void AddConnection(ILaunchPadConnection connectionDefinition)
        {
            _connections.TryAdd(connectionDefinition.Name, connectionDefinition);
        }

        public virtual void RemoveConnection(string connectionDefinitionName)
        {
            _connections.Remove(connectionDefinitionName);
        }

        public virtual ILaunchPadDatabaseConnection SetDefaultDatabaseConnection(string defaultConnectionStringName)
        {
            Guard.AgainstNullOrEmpty(defaultConnectionStringName, nameof(defaultConnectionStringName));
            var key = _connections.Keys
                .FirstOrDefault(k => string.Equals(k, defaultConnectionStringName, StringComparison.OrdinalIgnoreCase));
            if (key != null && _connections[key] is ILaunchPadDatabaseConnection dbConnection)
            {
                DefaultDatabaseConnection = dbConnection;
                DefaultConnectionStringName = dbConnection.Name;
                return dbConnection;
            }
            throw new InvalidOperationException("SetDefaultDatabaseConnection failed, could not find a matching connectionName. Is it present in the _connections dictionary?)");
        }

        public virtual void LoadDatabaseConnectionsFromConfiguration(
            ISecretConfiguration secretConfiguration, 
            string connectionsJson,
            string defaultConnectionStringName = null
        )
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                Converters = new List<JsonConverter> { new SecretFieldReferenceConverter() }
            };

            var connectionsList = JsonConvert.DeserializeObject<List<LaunchPadDatabaseConnection>>(connectionsJson, settings);
            ISecretReferenceResolver resolver = new SecretReferenceResolver(secretConfiguration);
            foreach (var connection in connectionsList)
            {
                connection.HostNameSecretRef.ResolvedValue = resolver.TryResolve(connection.HostNameSecretRef.FieldName).FieldValue;
                connection.DatabaseSecretRef.ResolvedValue = resolver.TryResolve(connection.DatabaseSecretRef.FieldName).FieldValue;
                connection.UsernameSecretRef.ResolvedValue = resolver.TryResolve(connection.UsernameSecretRef.FieldName).FieldValue;
                connection.PasswordSecretRef.ResolvedValue = resolver.TryResolve(connection.PasswordSecretRef.FieldName).FieldValue;
                _connections.TryAdd(connection.Name, connection);
            } 
            if(string.IsNullOrEmpty(defaultConnectionStringName))
            {
                 defaultConnectionStringName = "default";
            }

            DefaultConnectionStringName = defaultConnectionStringName;
            SetDefaultDatabaseConnection(defaultConnectionStringName);
        }

        public virtual string GetDatabaseConnectionString(string connectionName)
        {
            var key = _connections.Keys
            .FirstOrDefault(k => string.Equals(k, connectionName, StringComparison.OrdinalIgnoreCase));
            if (key != null && _connections[key] is ILaunchPadDatabaseConnection databaseConnection)
            {
                return databaseConnection.ConnectionString;
            }
            throw new InvalidOperationException($"GetDatabaseConnectionString failed, could not find a matching connectionName. Is it present in the _connections dictionary?)");
        }
    }
}
