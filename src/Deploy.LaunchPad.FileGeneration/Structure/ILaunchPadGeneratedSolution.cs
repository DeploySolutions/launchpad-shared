// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-09-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedSolution.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ILaunchPadGeneratedSolution
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    public partial interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {

        /// <summary>
        /// Gets or sets the software infrastructure.
        /// </summary>
        /// <value>The software infrastructure.</value>
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }

        /// <summary>
        /// Contains configuration information related to this solution
        /// </summary>
        /// <value>The settings.</value>
        public LaunchPadGeneratedSolutionSettings Settings { get; set; }


    }
}