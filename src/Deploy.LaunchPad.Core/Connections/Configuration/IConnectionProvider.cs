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
using Deploy.LaunchPad.Core.Connections.Database.Definitions;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Core.Connections.Configuration
{
    /// <summary>
    /// Interface ISecretProvider
    /// </summary>
    public partial interface IConnectionProvider :  ITransientDependency, ILaunchPadService
    {
        
        /// <summary>
        /// Contains a dictionary of "connections"
        /// </summary>
        /// <value>The secret vaults.</value>
        //[NotMapped]
        //public Dictionary<string, ILaunchPadConnectionDefinition> Connections { get; }

        public void AddConnection(ILaunchPadConnectionDefinition connectionDefinition);
        public void RemoveConnection(string connectionDefinitionName);
        public void LoadConnectionsFromSecrets();

        public string GetDatabaseConnectionString(string connectionName);
        public ILaunchPadDatabaseConnectionDefinition SetDefaultDatabaseConnection(string connectionName);

    }
}
