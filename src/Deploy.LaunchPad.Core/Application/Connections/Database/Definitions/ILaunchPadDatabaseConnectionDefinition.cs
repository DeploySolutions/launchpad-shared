using Deploy.LaunchPad.Core.Secrets.Reference;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections.Database.Definitions
{
    /// <summary>
    /// This is the configuration-facing abstraction of a Database Connection's settings. 
    /// It is intentionally entirely non-secret, with secret references only where needed.
    /// </summary>
    public partial interface ILaunchPadDatabaseConnectionDefinition : ILaunchPadConnectionDefinition
    {
        public string HostName { get;  }

        public int Port { get; }

        public string Version { get; }

        public string DatabaseName { get; }

        public ISecretFieldReference? UsernameSecret { get; }

        public ISecretFieldReference? PasswordSecret { get; }
        public ISecretFieldReference? ConnectionStringSecret { get; }

        public string GetConnectionString();

    }
}
