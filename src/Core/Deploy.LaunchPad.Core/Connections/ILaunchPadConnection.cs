
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Connections
{
    /// <summary>
    /// Represents a connection to an external system such as a database, REST service, or message broker
    /// </summary>
    public partial interface ILaunchPadConnection : ILaunchPadObject, 
        ILaunchPadMinimalProperties,
        IMustHaveElementDescription,
        IHavePassivable,
        IMayHaveVersionInformation
    {
        string ProviderName { get; }          // ex: PostgreSql, OpenSearch, OpenAI, RestApi

        public ConnectionType ConnectionType { get; }

        public ConnectionAuthMode ConnectionAuthMode { get; }

        IReadOnlyDictionary<string, string?> Metadata { get; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (implementors may or may not support).
        /// </summary>
        TimeSpan Timeout { get; set; }
    }
}
