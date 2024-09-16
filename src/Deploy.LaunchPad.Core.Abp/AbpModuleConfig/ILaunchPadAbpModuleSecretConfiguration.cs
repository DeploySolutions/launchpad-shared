// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-19-2023
// ***********************************************************************
// <copyright file="ILaunchPadAbpModuleSecretConfiguration.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Interface ILaunchPadAbpModuleSecretConfiguration
    /// </summary>
    public partial interface ILaunchPadAbpModuleSecretConfiguration
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public IDictionary<string, string> Fields { get; }
    }
}
