// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AssembleComponentOutputBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Class AssembleComponentOutputBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationOutputBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationOutputBase" />
    public abstract partial class AssembleComponentOutputBase : LaunchPadGenerationOutputBase
    {

        /// <summary>
        /// Gets or sets the assembly started.
        /// </summary>
        /// <value>The assembly started.</value>
        public DateTime AssemblyStarted { get; set; }

        /// <summary>
        /// Gets or sets the assembly ended.
        /// </summary>
        /// <value>The assembly ended.</value>
        public DateTime AssemblyEnded { get; set; }

        /// <summary>
        /// Gets or sets the duration of the assembly.
        /// </summary>
        /// <value>The duration of the assembly.</value>
        public TimeSpan AssemblyDuration { get; set; }

        /// <summary>
        /// Gets or sets the assembly output message.
        /// </summary>
        /// <value>The assembly output message.</value>
        public string AssemblyOutputMessage { get; set; }

        /// <summary>
        /// Gets or sets the file count.
        /// </summary>
        /// <value>The file count.</value>
        public int FileCount { get; set; }


    }
}
