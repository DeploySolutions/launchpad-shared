// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="DomainEntityTenancyType.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
    /// Enum DomainEntityTenancyType
    /// </summary>
    [Serializable]
    public enum DomainEntityTenancyType
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,
        /// <summary>
        /// The i must have tenant
        /// </summary>
        IMustHaveTenant = 1,
        /// <summary>
        /// The i may have tenant
        /// </summary>
        IMayHaveTenant = 2
    }
}
