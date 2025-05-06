// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMayHaveH3Definition.cs" company="Deploy Software Solutions, inc.">
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
    /// Interface IMayHaveH3Definition
    /// </summary>
    public partial interface IMayHaveH3Definition: ILaunchPadObject
    {
        /// <summary>
        /// Gets or sets the index of the h3.
        /// </summary>
        /// <value>The index of the h3.</value>
        public H3Index? H3Index { get; set; }

    }
}