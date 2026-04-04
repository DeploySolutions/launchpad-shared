// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGenerationOutputBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Class LaunchPadGenerationOutputBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationInputBase" />
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.ILaunchPadGenerationOutput" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationInputBase" />
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.ILaunchPadGenerationOutput" />
    public abstract partial class LaunchPadGenerationOutputBase : LaunchPadGenerationInputBase, ILaunchPadGenerationOutput
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LaunchPadGenerationOutputBase"/> is succeeded.
        /// </summary>
        /// <value><c>true</c> if succeeded; otherwise, <c>false</c>.</value>
        public virtual bool Succeeded { get; set; }
    }
}
