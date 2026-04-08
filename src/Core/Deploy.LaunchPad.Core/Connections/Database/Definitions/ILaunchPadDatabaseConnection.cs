using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Secrets.Reference;
using Deploy.LaunchPad.Util.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections.Database.Definitions
{
    /// <summary>
    /// This is the configuration-facing abstraction of a Database Connection's settings. 
    /// It is intentionally entirely non-secret, with secret references only where needed.
    /// </summary>
    public partial interface ILaunchPadDatabaseConnection : ILaunchPadConnection
    {
        public ISecretFieldReference? HostNameSecretRef { get; }
        public string DefaultSchema { get; }

        public int Port { get; }

        public ISecretFieldReference? DatabaseSecretRef { get; }

        public ISecretFieldReference? UsernameSecretRef { get; }

        public ISecretFieldReference? PasswordSecretRef { get; }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public string ConnectionString { get; }

        public string GetConnectionString();

    }
}
