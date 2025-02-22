﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="SystemIntegrationServiceBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using Microsoft.Extensions.Configuration;
using System;

namespace Deploy.LaunchPad.Core.Application
{
    /// <summary>
    /// Class SystemIntegrationServiceBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Application.ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Application.ILaunchPadSystemIntegrationService" />
    [Serializable()]
    public abstract class SystemIntegrationServiceBase : ILaunchPadSystemIntegrationService
    {
        /// <summary>
        /// The configuration root
        /// </summary>
        protected readonly IConfigurationRoot _configurationRoot;
        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <value>The configuration root.</value>
        public IConfigurationRoot ConfigurationRoot { get { return _configurationRoot; } }


        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemIntegrationServiceBase"/> class.
        /// </summary>
        protected SystemIntegrationServiceBase()
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("SystemIntegrationServiceBase {0} ", id));
            Description = new ElementDescription(string.Format("SystemIntegrationServiceBase {0} ", id));
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemIntegrationServiceBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected SystemIntegrationServiceBase(ILogger logger)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("TSystemIntegrationServiceBase {0} ", id));
            Description = new ElementDescription(string.Format("SystemIntegrationServiceBase {0} ", id));
            Logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemIntegrationServiceBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        protected SystemIntegrationServiceBase(ILogger logger, IConfigurationRoot configurationRoot)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("SystemIntegrationServiceBase {0} ", id));
            Description = new ElementDescription(string.Format("SystemIntegrationServiceBase {0} ", id));
            Logger = logger;
            _configurationRoot = configurationRoot;
        }

    }
}
