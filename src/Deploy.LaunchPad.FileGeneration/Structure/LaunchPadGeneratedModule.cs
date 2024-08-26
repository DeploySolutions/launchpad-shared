// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-07-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedModule.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadGeneratedModule.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadGeneratedObjectBase" />
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedModule{TModuleSettings}" />
    /// </summary>
    /// <typeparam name="TModuleSettings">The type of the t module settings.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadGeneratedObjectBase" />
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedModule{TModuleSettings}" />
    public partial class LaunchPadGeneratedModule<TModuleSettings> : LaunchPadGeneratedObjectBase,
        ILaunchPadGeneratedModule<TModuleSettings>
        where TModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual TModuleSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the licensed third party items.
        /// </summary>
        /// <value>The licensed third party items.</value>
        public virtual IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }


        /// <summary>
        /// Returns a bool indicating if the module is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the module is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public virtual bool CheckValidity()
        {
            bool isValid = false;
            if (Name != null && !string.IsNullOrEmpty(Id.ToString()) && !string.IsNullOrEmpty(Inheritance.FullyQualifiedType)
                  && Settings != null
            )
            {
                isValid = true;
            }
            return isValid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedModule{TModuleSettings}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LaunchPadGeneratedModule(ILogger logger) : base()
        {
            Logger = logger;
            Settings = new TModuleSettings(); 
            var comparer = StringComparer.OrdinalIgnoreCase;
            LicensedThirdPartyItems = new Dictionary<string, ILicensedThirdPartySoftwareItem>(comparer);
        }
    }
}
