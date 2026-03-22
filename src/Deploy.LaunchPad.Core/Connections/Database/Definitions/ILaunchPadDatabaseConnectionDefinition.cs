using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Core.Secrets.Reference;
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
    public partial interface ILaunchPadDatabaseConnectionDefinition : ILaunchPadConnectionDefinition
    {
        public ISecretFieldReference? HostNameSecret { get; }
        public string DefaultSchema { get; }

        public int Port { get; }

        public string Version { get; }

        public ISecretFieldReference? DatabaseSecret { get; }

        public ISecretFieldReference? UsernameSecret { get; }

        public ISecretFieldReference? PasswordSecret { get; }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        [JsonIgnore]
        [NotMapped]
        public string ConnectionString { get; }

        public string GetConnectionString();

    }
}
