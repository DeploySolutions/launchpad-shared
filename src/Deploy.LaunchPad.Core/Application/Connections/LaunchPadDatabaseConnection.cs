using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections
{
    public partial class LaunchPadDatabaseConnection : ILaunchPadDatabaseConnection
    {
        public virtual ConnectionType ConnectionType { get; init; } = ConnectionType.PostgresDatabase;

        public virtual string Name { get; set; }
        public virtual ElementDescription Description { get; set; }

        public required virtual string HostName { get; init; }
        public required virtual string DatabaseName { get; init; }

        public virtual string Version { get; } = string.Empty;
        
        public virtual int Port { get; init; } = 5432;

        [NotMapped]
        public required virtual string UserId { get; init; }

        [NotMapped]
        public required virtual string Password { get; init; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (if supported).
        /// </summary>
        public virtual TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 15);

        [SetsRequiredMembers]
        public LaunchPadDatabaseConnection(string name, string hostName, string databaseName, string userId, string password, int port = 5432, string version="", ConnectionType connectionType = ConnectionType.PostgresDatabase)
        {
            Name = name;
            Description = new ElementDescription(name);
            HostName = hostName;
            DatabaseName = databaseName;
            UserId = userId;
            Password = password;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
        }

        [SetsRequiredMembers]
        public LaunchPadDatabaseConnection(string name, ElementDescription description, string hostName, string databaseName, string userId, string password, int port = 5432, string version = "", ConnectionType connectionType = ConnectionType.PostgresDatabase)
        {
            Name = name;
            Description = description;
            HostName = hostName;
            DatabaseName = databaseName;
            UserId = userId;
            Password = password;
            Port = port;
            Version = version;
            ConnectionType = connectionType;
        }


        public virtual string GetConnectionString()
        {
            DbConnectionStringBuilder builder = null;

            if (ConnectionType == ConnectionType.PostgresDatabase)
            {
                builder = new DbConnectionStringBuilder();
                builder["User ID"] = UserId;
                builder["Password"] = Password;
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
