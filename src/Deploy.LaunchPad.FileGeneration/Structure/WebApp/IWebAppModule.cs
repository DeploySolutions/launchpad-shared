// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IWebAppModule.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IWebAppModule
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    public partial interface IWebAppModule : IHaveSoftwareInfrastructure
    {
        /// <summary>
        /// Gets or sets the web API.
        /// </summary>
        /// <value>The web API.</value>
        public VisualStudioComponent WebApi { get; set; }
        /// <summary>
        /// Gets or sets the web clients.
        /// </summary>
        /// <value>The web clients.</value>
        public IDictionary<string, WebClientComponent> WebClients { get; set; }
    }
}