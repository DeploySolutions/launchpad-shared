// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="IAbpSystemIntegrationService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Localization.Sources;
using Abp.Localization;
using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Deploy.LaunchPad.Code.Services;

namespace Deploy.LaunchPad.Core.Abp.Services
{
    /// <summary>
    /// Interface IAbpSystemIntegrationService
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAbpSystemIntegrationService : ILaunchPadSystemIntegrationService
    {
        // <summary>
        /// <summary>
        /// Gets or sets the setting manager.
        /// </summary>
        /// <value>The setting manager.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager" />.
        /// </summary>
        /// <value>The unit of work manager.</value>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get;
            set;
        }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        /// <value>The localization manager.</value>
        public ILocalizationManager LocalizationManager { get; set; }


        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        /// <value>The object mapper.</value>
        public IObjectMapper ObjectMapper { get; set; }
    }
}
