// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="OrganizationRoleEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.STAC
{

    /// <summary>
    /// Enum OrganizationRole
    /// </summary>
    public enum OrganizationRole
    {
        /// <summary>
        /// The host
        /// </summary>
        Host = 0,
        /// <summary>
        /// The licensor
        /// </summary>
        Licensor = 1,
        /// <summary>
        /// The processor
        /// </summary>
        Processor = 2,
        /// <summary>
        /// The producer
        /// </summary>
        Producer = 3
    };
}
