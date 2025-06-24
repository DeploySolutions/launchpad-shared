// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 06-30-2025
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-30-2025
// ***********************************************************************
// <copyright file="IMayHaveHeight.cs" company="Deploy Software Solutions, inc.">
//     2018-2025 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Deploy.LaunchPad.Core.Domain;

namespace Deploy.LaunchPad.Core.Geospatial.ReferencePoint
{

    /// <summary>
    /// This interface defines the physical Height of something
    /// </summary>
    public partial interface IMayHaveHeight
    {

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        /// <value>The Height.</value>
        public Height? Height { get; set; }

    }
}