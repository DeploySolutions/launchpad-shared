using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Util.Elements;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.Core.Application.Connections.Database.Resolutions
{
    /// <summary>
    /// This is the runtime model of the Database Connection Definition. 
    /// It contains materialized secret values and helper properties for actual connection usage.
    /// </summary>
    [Serializable]
    public partial class ResolvedDatabaseConnection
    {
        public virtual ConnectionType ConnectionType { get; init; } = ConnectionType.PostgresDatabase;

        public virtual ConnectionAuthMode ConnectionAuthMode { get; init; } = ConnectionAuthMode.ConnectionString;

        public required virtual string Name { get; set; }
        public virtual ElementDescription Description { get; set; }

        public required virtual string HostName { get; init; }
        public required virtual string DatabaseName { get; init; }

        public virtual string Version { get; } = string.Empty;
        
        public virtual int Port { get; init; } = 5432;

        /// <summary>
        /// Gets/sets a timeout value for the connection (if supported).
        /// </summary>
        public virtual TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 15);
        
        public virtual IReadOnlyDictionary<string, string?> Metadata { get; set; } = new Dictionary<string, string?>();

        public virtual bool IsActive { get; set; } = true;

        // Resolved secret values
        [NotMapped]
        [JsonIgnore]
        public virtual string? Username { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual string? Password { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual string? ConnectionString { get; set; }
        public virtual bool HasCredentials =>
            !string.IsNullOrWhiteSpace(Username) ||
            !string.IsNullOrWhiteSpace(Password) ||
            !string.IsNullOrWhiteSpace(ConnectionString);


        [SetsRequiredMembers]
        public ResolvedDatabaseConnection(string name, 
            string hostName, 
            string databaseName, 
            string userName,
            string password, 
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
            Username = userName;
            Password = password;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
            ConnectionAuthMode = connectionAuthMode;
        }

        [SetsRequiredMembers]
        public ResolvedDatabaseConnection(string name, 
            ElementDescription description, 
            string hostName, 
            string databaseName,
            string userName,
            string password,
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
            Username = userName;
            Password = password;
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
