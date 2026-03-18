using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.Core.Application.Connections.Database.Definitions
{
    [Serializable]
    public partial class LaunchPadDatabaseConnectionDefinition : ILaunchPadDatabaseConnectionDefinition
    {
        public virtual ConnectionType ConnectionType { get; init; } = ConnectionType.PostgresDatabase;

        public virtual ConnectionAuthMode ConnectionAuthMode { get; init; } = ConnectionAuthMode.ConnectionString;

        public required virtual string Name { get; set; }
        public virtual ElementDescription Description { get; set; }

        public required virtual string HostName { get; init; }
        public required virtual string DatabaseName { get; init; }

        public virtual string Version { get; } = string.Empty;
        
        public virtual int Port { get; init; } = 5432;

        [NotMapped]
        [JsonIgnore]
        public virtual ISecretFieldReference? UsernameSecretRef { get; set; }

        [NotMapped]
        [JsonIgnore]

        public virtual ISecretFieldReference? PasswordSecretRef { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ISecretFieldReference? ConnectionStringSecretRef { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (if supported).
        /// </summary>
        public virtual TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 15);
        
        public virtual IReadOnlyDictionary<string, string?> Metadata { get; set; } = new Dictionary<string, string?>();

        public virtual bool IsActive { get; set; } = true;

        ISecretFieldReference? ILaunchPadDatabaseConnectionDefinition.UsernameSecret => UsernameSecretRef;
        ISecretFieldReference? ILaunchPadDatabaseConnectionDefinition.PasswordSecret => PasswordSecretRef;
        ISecretFieldReference? ILaunchPadDatabaseConnectionDefinition.ConnectionStringSecret => ConnectionStringSecretRef;

        [SetsRequiredMembers]
        public LaunchPadDatabaseConnectionDefinition(string name, 
            string hostName, 
            string databaseName, 
            ISecretFieldReference userNameSecretRef,
            ISecretFieldReference passwordSecretRef, 
            int port = 5432, 
            string version="", 
            ConnectionType connectionType = ConnectionType.PostgresDatabase,
            ConnectionAuthMode connectionAuthMode = ConnectionAuthMode.ConnectionString
        )
        {
            Name = name;
            Description = new ElementDescription(name);
            HostName = hostName;
            DatabaseName = databaseName;
            UsernameSecretRef = userNameSecretRef;
            PasswordSecretRef = passwordSecretRef;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
            ConnectionAuthMode = connectionAuthMode;
        }

        [SetsRequiredMembers]
        public LaunchPadDatabaseConnectionDefinition(string name, 
            ElementDescription description, 
            string hostName, 
            string databaseName,
            ISecretFieldReference userNameSecretRef,
            ISecretFieldReference passwordSecretRef,
            int port = 5432, 
            string version = "", 
            ConnectionType connectionType = ConnectionType.PostgresDatabase,
            ConnectionAuthMode connectionAuthMode = ConnectionAuthMode.ConnectionString
        )
        {
            Name = name;
            Description = description;
            HostName = hostName;
            DatabaseName = databaseName;
            UsernameSecretRef = userNameSecretRef;
            PasswordSecretRef = passwordSecretRef;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
            ConnectionAuthMode = connectionAuthMode;
        }


        public virtual string GetConnectionString()
        {
            DbConnectionStringBuilder builder = null;
            string userId = string.Empty; // resolve using Secret Manager and the resolver
            string password = string.Empty;
            if (ConnectionType == ConnectionType.PostgresDatabase)
            {
                builder = new DbConnectionStringBuilder();
                builder["User ID"] = userId;
                builder["Password"] = password;
                builder["Host"] = HostName;
                builder["Port"] = Port;
                builder["Database"] = DatabaseName;
                builder["Timeout"] = Timeout.TotalSeconds;
                // Add other Postgres-specific options as needed
            }
            else if (ConnectionType == ConnectionType.SqliteDatabase)
            {
                builder = new DbConnectionStringBuilder();
                builder["Data Source"] = DatabaseName;
                if (!string.IsNullOrEmpty(Version))
                    builder["Version"] = Version;
                // SQLite does not use user/password/host/port
            }

            return builder?.ConnectionString ?? string.Empty;
        }
    }
}
