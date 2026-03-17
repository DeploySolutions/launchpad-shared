using Deploy.LaunchPad.Core.Domain.ValueObjects;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections
{
    /// <summary>
    /// Represents a connection to an external system such as a database, REST service, or message broker
    /// </summary>
    public partial interface ILaunchPadConnection : ILaunchPadObject, 
        ILaunchPadMinimalProperties,
        IMustHaveElementDescription
    {
        public ConnectionType ConnectionType { get; }

        /// <summary>
        /// Gets/sets a timeout value for the connection (implementors may or may not support).
        /// </summary>
        TimeSpan Timeout { get; set; }
    }
}
