using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Util.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Deploy.LaunchPad.Core.Connections.Database.Definitions
{
    [Serializable]
    public partial class LaunchPadDatabaseConnection : ILaunchPadDatabaseConnection
    {
        public virtual ConnectionType ConnectionType { get; init; } = ConnectionType.PostgresDatabase;

        public virtual ConnectionAuthMode ConnectionAuthMode { get; init; } = ConnectionAuthMode.ConnectionString;

        public required virtual string Name { get; set; }

        public virtual string Description { get; set; } = string.Empty;

        public virtual string ProviderName { get; set; }

        public virtual string DefaultSchema { get; set; } = "public";

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public virtual string ConnectionString { get => GetConnectionString(); }

        [NotMapped]
        [JsonIgnore]
        public virtual string? HostName => HostNameSecretRef.ResolvedValue;

        [NotMapped]
        [JsonIgnore]
        public required virtual ISecretFieldReference? HostNameSecretRef { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual string? Database => DatabaseSecretRef.ResolvedValue;

        [NotMapped]
        [JsonIgnore]
        public required virtual ISecretFieldReference? DatabaseSecretRef { get; set; }

        public virtual string Version { get; init; } = string.Empty;
        
        public virtual int Port { get; init; } = 5432;


        [NotMapped]
        [JsonIgnore]
        public virtual string? UserName => UsernameSecretRef.ResolvedValue;

        [NotMapped]
        [JsonIgnore]
        public required virtual ISecretFieldReference? UsernameSecretRef { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual string? Password => PasswordSecretRef.ResolvedValue;

        [NotMapped]
        [JsonIgnore]

        public required virtual ISecretFieldReference? PasswordSecretRef { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (if supported).
        /// </summary>
        public virtual TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 15);
        
        public virtual IReadOnlyDictionary<string, string?> Metadata { get; set; } = new Dictionary<string, string?>();

        public virtual bool IsActive { get; set; } = true;


        [SetsRequiredMembers]
        [JsonConstructor]
        public LaunchPadDatabaseConnection(string name,
            ISecretFieldReference hostNameSecretRef,
            ISecretFieldReference databaseSecretRef,
            ISecretFieldReference userNameSecretRef,
            ISecretFieldReference passwordSecretRef, 
            int port = 5432, 
            string version="", 
            ConnectionType connectionType = ConnectionType.PostgresDatabase,
            ConnectionAuthMode connectionAuthMode = ConnectionAuthMode.ConnectionString
        )
        {
            Name = name;
            HostNameSecretRef = hostNameSecretRef;
            Description = name;
            DatabaseSecretRef = databaseSecretRef;
            UsernameSecretRef = userNameSecretRef;
            PasswordSecretRef = passwordSecretRef;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
            ConnectionAuthMode = connectionAuthMode;
        }

        [SetsRequiredMembers]
        public LaunchPadDatabaseConnection(string name, 
            string description,
            ISecretFieldReference hostNameSecretRef,
            ISecretFieldReference databaseSecretRef,
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
            HostNameSecretRef = hostNameSecretRef;
            DatabaseSecretRef = databaseSecretRef;
            UsernameSecretRef = userNameSecretRef;
            PasswordSecretRef = passwordSecretRef;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
            ConnectionAuthMode = connectionAuthMode;
        }

        /// <summary>
        /// Returns the connection string for this database connection definition, resolving secrets as needed.
        /// </summary>
        /// <returns></returns>
        public virtual string GetConnectionString()
        {
            DbConnectionStringBuilder builder = null;
            // resolve using Secret Manager and the resolver
            if (ConnectionType == ConnectionType.PostgresDatabase)
            {
                builder = new DbConnectionStringBuilder();
                builder["User ID"] = UsernameSecretRef.ResolvedValue;
                builder["Password"] = PasswordSecretRef.ResolvedValue;
                builder["Host"] = HostNameSecretRef.ResolvedValue;
                builder["Port"] = Port;
                builder["Database"] = DatabaseSecretRef.ResolvedValue;
                builder["Timeout"] = Timeout.TotalSeconds;
                // Add other Postgres-specific options as needed
            }
            else if (ConnectionType == ConnectionType.SqliteDatabase)
            {
                builder = new DbConnectionStringBuilder();
                builder["Data Source"] = DatabaseSecretRef.ResolvedValue;
                if (!string.IsNullOrEmpty(Version))
                    builder["Version"] = Version;
                // SQLite does not use user/password/host/port
            }

            return builder?.ConnectionString ?? string.Empty;
        }
    }
}
