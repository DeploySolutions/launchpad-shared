// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedSolutionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadGeneratedSolutionSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedSolutionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedSolutionSettings" />
    [Serializable]
    public partial class LaunchPadGeneratedSolutionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, ILaunchPadGeneratedSolutionSettings
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedSolutionSettings"/> class.
        /// </summary>
        public LaunchPadGeneratedSolutionSettings()
        {
        }

    }
}
