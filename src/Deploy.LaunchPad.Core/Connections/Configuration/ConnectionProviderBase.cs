// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="SecretProviderBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Util.Dependency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    /// <summary>
    /// Class SecretProviderBase.
    /// Implements the <see cref="Core.Config.ConnectionProviderBase" />
    /// </summary>
    /// <seealso cref="Core.Config.ConnectionProviderBase" />
    public abstract partial class ConnectionProviderBase : IConnectionProvider, ITransientDependency
    {
        protected readonly IDictionary<string, ILaunchPadConnectionDefinition> _connections = new Dictionary<string, ILaunchPadConnectionDefinition>();

        /// <summary>
        /// Contains an outer dictionary of "secret vaults".
        /// Each secret vault contains an inner dictionary of string pairs representing a unique field contained within the vault, and the field's value.
        /// The outer dictionary key is the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        /// <value>The secret vaults.</value>
        [NotMapped]
        [JsonIgnore]
        public virtual Dictionary<string, ILaunchPadConnectionDefinition> Connections { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        public ConnectionProviderBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ConnectionProviderBase(ILogger logger)
        {
            if (logger != null)
            {
                Logger = logger;
            }
        }
        

    }
}
