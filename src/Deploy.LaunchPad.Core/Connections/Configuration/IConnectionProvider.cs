// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="ISecretProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Connections;
using Deploy.LaunchPad.Util.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    /// <summary>
    /// Interface ISecretProvider
    /// </summary>
    public partial interface IConnectionProvider :  ITransientDependency
    {

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Contains a dictionary of "connections"
        /// </summary>
        /// <value>The secret vaults.</value>
        [NotMapped]
        public Dictionary<string, ILaunchPadConnectionDefinition> Connections { get; }



    }
}
