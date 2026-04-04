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

namespace Deploy.LaunchPad.Code.Methods
{
    /// <summary>
    /// Useful for specifying the level of detail required for a method's return
    /// </summary>
    public enum MethodDetailLevel
    {
        /// <summary>
        /// The basic
        /// </summary>
        [Description("basic")]
        Basic = 0,
        /// <summary>
        /// The detailed
        /// </summary>
        [Description("detailed")]
        Detailed = 1,
        /// <summary>
        /// The full
        /// </summary>
        [Description("full")]
        Full = 2,
        /// <summary>
        /// The admin
        /// </summary>
        [Description("admin")]
        Admin = 3
    }
}
