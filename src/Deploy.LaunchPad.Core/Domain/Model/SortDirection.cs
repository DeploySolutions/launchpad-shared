// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="SortDirection.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Enum SortDirection
    /// </summary>
    [Serializable]
    public enum SortDirection
    {
        /// <summary>
        /// The ascending
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// The descending
        /// </summary>
        Descending = 1
    }
}
