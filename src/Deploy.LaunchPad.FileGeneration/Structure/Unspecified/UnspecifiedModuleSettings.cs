// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="UnspecifiedModuleSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class UnspecifiedModuleSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    public partial class UnspecifiedModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="UnspecifiedModuleSettings"/> class.
        /// </summary>
        public UnspecifiedModuleSettings() : base()
        {
        }
    }
}
