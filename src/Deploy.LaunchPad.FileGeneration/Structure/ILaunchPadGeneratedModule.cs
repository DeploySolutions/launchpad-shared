// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-26-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedModule.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ILaunchPadGeneratedModule
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    /// </summary>
    /// <typeparam name="TModuleSettings">The type of the t module settings.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    public partial interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public TModuleSettings Settings { get; set; }
        /// <summary>
        /// Gets or sets the licensed third party items.
        /// </summary>
        /// <value>The licensed third party items.</value>
        public IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }

        /// <summary>
        /// Checks the validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckValidity();
    }
}