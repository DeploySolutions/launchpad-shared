// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IMayHaveAltitude.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Deploy.LaunchPad.Core.Domain;

namespace Deploy.LaunchPad.Core.Geospatial.ReferencePoint
{

    /// <summary>
    /// This interface defines the physical Altitude of something
    /// </summary>
    public partial interface IMayHaveAltitude
    {

        /// <summary>
        /// Gets or sets the Altitude.
        /// </summary>
        /// <value>The Altitude.</value>
        public Altitude? Altitude { get; set; }

    }
}