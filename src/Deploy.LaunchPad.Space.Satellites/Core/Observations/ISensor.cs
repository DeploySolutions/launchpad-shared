﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="ISensor.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    /// <summary>
    /// Interface ISensor
    /// </summary>
    public partial interface ISensor
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>The abbreviation.</value>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the supported imagery types.
        /// </summary>
        /// <value>The supported imagery types.</value>
        [Required]
        public IDictionary<string, EarthObservationImageryType> SupportedImageryTypes { get; set; }
    }
}
