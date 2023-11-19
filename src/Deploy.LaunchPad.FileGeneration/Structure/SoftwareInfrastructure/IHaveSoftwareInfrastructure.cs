// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IHaveSoftwareInfrastructure.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IHaveSoftwareInfrastructure
    /// </summary>
    public partial interface IHaveSoftwareInfrastructure
    {
        /// <summary>
        /// Describes the overall coding/environment infrastructure in which this element exists
        /// (ex which version of ABP framework, which cloud provider)
        /// </summary>
        /// <value>The software infrastructure.</value>
        public ISoftwareInfrastructure SoftwareInfrastructure { get; set; }
    }
}
