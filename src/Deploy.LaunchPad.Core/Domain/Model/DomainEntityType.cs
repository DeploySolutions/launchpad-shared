// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="DomainEntityType.cs" company="Deploy Software Solutions, inc.">
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
    /// Enum DomainEntityType
    /// </summary>
    [Serializable]
    public enum DomainEntityType
    {
        /// <summary>
        /// The domain entity
        /// </summary>
        DomainEntity = 0,
        /// <summary>
        /// The aggregate root
        /// </summary>
        AggregateRoot = 1,
        /// <summary>
        /// The aggregate child
        /// </summary>
        AggregateChild = 2,
        /// <summary>
        /// The value object
        /// </summary>
        ValueObject = 3
    }
}
