// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="DetailLevel.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Qualifiers
{
    /// <summary>
    /// Useful for specifying the rating level
    /// </summary>
    public enum RatingLevel
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Description("unknown")]
        Unknown = 0,
        /// <summary>
        /// The lowest level of rating
        /// </summary>
        [Description("verylow")]
        VeryLow = 1,
        /// <summary>
        /// A low rating
        /// </summary>
        [Description("low")]
        Low = 2,
        /// <summary>
        /// A moderate rating
        /// </summary>
        [Description("moderate")]
        Moderate = 3,
        /// <summary>
        /// A high rating
        /// </summary>
        [Description("high")]
        High = 4,
        /// <summary>
        /// A high rating
        /// </summary>
        [Description("veryhigh")]
        VeryHigh = 5
    }
}
