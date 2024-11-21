// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="LaunchPadServiceBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System;

namespace Deploy.LaunchPad.Core.Config
{
    /// <summary>
    /// Base service for LaunchPad Services, ensuring they can have a name, description, and logger.
    /// </summary>
    public abstract partial class LaunchPadServiceBase : ILaunchPadService
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        protected LaunchPadServiceBase()
        {
            string id = Guid.NewGuid().ToString();
            string name = GetType().Name;
            Name = new ElementName(string.Format("{0} {1} ", name, id));
            Description = new ElementDescription(string.Format("{0} {1} ", name, id));
        }

        protected LaunchPadServiceBase(ILogger logger)
        {
            string id = Guid.NewGuid().ToString();
            string name = GetType().Name;
            Name = new ElementName(string.Format("{0} {1} ", name, id));
            Description = new ElementDescription(string.Format("{0} {1} ", name, id));
            Logger = logger;
        }


        protected LaunchPadServiceBase(ILogger logger, string name)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            Logger = logger;
        }


        protected LaunchPadServiceBase(ILogger logger, string name, string description)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(description);
            Logger = logger;
        }
    }
}
