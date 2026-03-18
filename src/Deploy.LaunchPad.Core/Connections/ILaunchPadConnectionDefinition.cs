using Deploy.LaunchPad.Core.Domain.ValueObjects;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections
{
    /// <summary>
    /// Represents a connection to an external system such as a database, REST service, or message broker
    /// </summary>
    public partial interface ILaunchPadConnectionDefinition : ILaunchPadObject, 
        ILaunchPadMinimalProperties,
        IMustHaveElementDescription,
        IHavePassivable
    {
        public ConnectionType ConnectionType { get; }
        public ConnectionAuthMode ConnectionAuthMode { get; }

        IReadOnlyDictionary<string, string?> Metadata { get; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (implementors may or may not support).
        /// </summary>
        TimeSpan Timeout { get; set; }
    }
}
