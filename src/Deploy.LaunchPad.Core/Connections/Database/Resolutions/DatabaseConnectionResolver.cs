using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Connections.Configuration;
using Deploy.LaunchPad.Core.Connections.Database.Definitions;
using Deploy.LaunchPad.Core.Secrets.Resolver;
using Deploy.LaunchPad.Util.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Connections.Database.Resolutions
{
    public partial class DatabaseConnectionResolver : IDatabaseConnectionResolver, ITransientDependency
    {
        private readonly IConnectionConfiguration _connectionConfiguration;
        private readonly ISecretReferenceResolver _secretReferenceResolver;
        private readonly ISettingDefinitionManager _settingDefinitionManager;

        public DatabaseConnectionResolver(
           IConnectionConfiguration connectionConfiguration,
           ISecretReferenceResolver secretReferenceResolver,
           ISettingDefinitionManager settingDefinitionManager)
        {
            connectionConfiguration = _connectionConfiguration ?? throw new ArgumentNullException(nameof(connectionConfiguration));
            _secretReferenceResolver = secretReferenceResolver ?? throw new ArgumentNullException(nameof(secretReferenceResolver));
            _settingDefinitionManager = settingDefinitionManager ?? throw new ArgumentNullException(nameof(settingDefinitionManager));
        }

        public virtual async Task<ResolvedDatabaseConnection> ResolveAsync(
            string connectionName,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
            {
                throw new ArgumentException("Connection name is required.", nameof(connectionName));
            }

            var definition = _connectionConfiguration.GetOrNull(connectionName);

            if (definition == null)
            {
                throw new InvalidOperationException($"Connection '{connectionName}' was not found.");
            }

            return await ResolveAsync(definition.Name, tenantId, userId, cancellationToken);
        }

        public virtual async Task<ResolvedDatabaseConnection> ResolveAsync(
            ILaunchPadDatabaseConnectionDefinition definition,
            Guid? tenantId = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default)
        {
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }

            string username = await TryResolveSettingValueAsync(
                definition.UsernameSecret.FieldName,
                tenantId,
                userId,
                cancellationToken);
            string password = await TryResolveSettingValueAsync(
                definition.PasswordSecret.FieldName,
                tenantId,
                userId,
                cancellationToken);
            string connectionString = await TryResolveSettingValueAsync(
                definition.ConnectionStringSecret.FieldName,
                tenantId,
                userId,
                cancellationToken);

            var resolved = new ResolvedDatabaseConnection
            (definition.Name, definition.HostName, definition.DatabaseName,
            username,password, definition.Port,null, definition.ConnectionType, definition.ConnectionAuthMode
                )
            {
                Timeout = definition.Timeout,
                IsActive = definition.IsActive,
                Metadata = new Dictionary<string, string?>(definition.Metadata)
            };

            ValidateResolvedConnection(definition, resolved);

            return resolved;
        }

        protected virtual async Task<string?> TryResolveSettingValueAsync(
            string? settingName,
            Guid? tenantId,
            Guid? userId,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                return null;
            }

            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(settingName);

            if (settingDefinition == null)
            {
                throw new InvalidOperationException(
                    $"Setting definition '{settingName}' was not found.");
            }

            var result = await _secretReferenceResolver.TryResolveAsync(
                settingDefinition,
                tenantId,
                userId,
                cancellationToken);

            return result.WasFound ? result.FieldValue : null;
        }

        protected virtual void ValidateResolvedConnection(
            ILaunchPadDatabaseConnectionDefinition definition,
            ResolvedDatabaseConnection resolved)
        {
            switch (definition.ConnectionAuthMode)
            {
                case ConnectionAuthMode.None:
                    return;

                case ConnectionAuthMode.UsernamePassword:
                    if (string.IsNullOrWhiteSpace(resolved.Username) ||
                        string.IsNullOrWhiteSpace(resolved.Password))
                    {
                        throw new InvalidOperationException(
                            $"Connection '{definition.Name}' requires username and password.");
                    }
                    break;

                case ConnectionAuthMode.ConnectionString:
                    if (string.IsNullOrWhiteSpace(resolved.ConnectionString))
                    {
                        throw new InvalidOperationException(
                            $"Connection '{definition.Name}' requires a connection string.");
                    }
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported auth mode '{definition.ConnectionAuthMode}' for connection '{definition.Name}'.");
            }
        }

    }
}
