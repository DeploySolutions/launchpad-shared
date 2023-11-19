// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IWebAppSolution.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IWebAppSolution
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.IHaveSoftwareInfrastructure" />
    public partial interface IWebAppSolution : IHaveSoftwareInfrastructure
    {
        /// <summary>
        /// Gets or sets the web application module.
        /// </summary>
        /// <value>The web application module.</value>
        public WebAppModule WebAppModule { get; set; }

        /// <summary>
        /// Checks the validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CheckValidity();
    }
}