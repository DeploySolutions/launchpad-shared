// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMustHaveH3Location.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain.Model;
using H3;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Geospatial.H3
{
    /// <summary>
    /// Interface IMustHaveH3Location
    /// </summary>
    public partial interface IMustHaveH3Location: ILaunchPadObject
    {
        /// <summary>
        /// Gets or sets the Uber H3 cell information.
        /// <seealso href="https://www.uber.com/en-CA/blog/h3/">H3: Uber’s Hexagonal Hierarchical Spatial Index</seealso>
        /// </summary>
        /// <value>The H3 location information.</value>
        public H3Index H3Location { get; set; }

    }
}