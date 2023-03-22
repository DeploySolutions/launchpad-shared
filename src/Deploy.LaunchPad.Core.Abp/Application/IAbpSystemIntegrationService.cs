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
using Deploy.LaunchPad.Core.Application;

namespace Deploy.LaunchPad.Core.Abp.Application
{
    public partial interface IAbpSystemIntegrationService : ILaunchPadSystemIntegrationService
    {
        // <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get;
            set;
        }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        public ILocalizationManager LocalizationManager { get; set; }


        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        public IObjectMapper ObjectMapper { get; set; }
    }
}
