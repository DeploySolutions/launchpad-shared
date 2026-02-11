// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IMayHaveElevation.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Deploy.LaunchPad.Domain;

namespace Deploy.LaunchPad.Domain.Geospatial.ReferencePoint
{

    /// <summary>
    /// This interface defines the physical elevation of something
    /// </summary>
    public partial interface IMayHaveElevation
    {

        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        /// <value>The elevation.</value>
        public Elevation? Elevation { get; set; }

    }
}