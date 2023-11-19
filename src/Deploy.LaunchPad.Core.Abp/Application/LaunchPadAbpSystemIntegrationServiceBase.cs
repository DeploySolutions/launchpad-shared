// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpSystemIntegrationServiceBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Localization.Sources;
using Abp.Localization;
using Abp.ObjectMapping;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Abp.Application
{
    /// <summary>
    /// Class LaunchPadAbpSystemIntegrationServiceBase.
    /// Implements the <see cref="SystemIntegrationServiceBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.IAbpSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="SystemIntegrationServiceBase" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.IAbpSystemIntegrationService" />
    [Serializable()]
    public abstract class LaunchPadAbpSystemIntegrationServiceBase : SystemIntegrationServiceBase, IAbpSystemIntegrationService
    {


        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        /// <value>The setting manager.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public virtual ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager" />.
        /// </summary>
        /// <value>The unit of work manager.</value>
        /// <exception cref="Abp.AbpException">Must set UnitOfWorkManager before use it.</exception>
        public virtual IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        /// <summary>
        /// The unit of work manager
        /// </summary>
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        /// <value>The current unit of work.</value>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        /// <value>The localization manager.</value>
        public virtual ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)" /> and <see cref="L(string,CultureInfo)" /> methods.
        /// </summary>
        /// <value>The name of the localization source.</value>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName" /> is set.
        /// </summary>
        /// <value>The localization source.</value>
        /// <exception cref="Abp.AbpException">Must set LocalizationSourceName before, in order to get LocalizationSource</exception>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        /// <summary>
        /// The localization source
        /// </summary>
        private ILocalizationSource _localizationSource;


        /// <summary>
        /// Reference to the object to object mapper.
        /// </summary>
        /// <value>The object mapper.</value>
        public virtual IObjectMapper ObjectMapper { get; set; }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        protected LaunchPadAbpSystemIntegrationServiceBase() :base()
        {
            ObjectMapper = NullObjectMapper.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpSystemIntegrationServiceBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected LaunchPadAbpSystemIntegrationServiceBase(ILogger logger) : base(logger)
        {
            ObjectMapper = NullObjectMapper.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpSystemIntegrationServiceBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationRoot">The configuration root.</param>
        protected LaunchPadAbpSystemIntegrationServiceBase(ILogger logger, IConfigurationRoot configurationRoot) :base(logger, configurationRoot)
        {
            ObjectMapper = NullObjectMapper.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

    }
}
