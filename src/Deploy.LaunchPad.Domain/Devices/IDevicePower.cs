// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IDevicePower.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Domain.Devices
{
    /// <summary>
    /// Interface IDevicePower
    /// </summary>
    public partial interface IDevicePower
    {
        /// <summary>
        /// Gets or sets the power level.
        /// </summary>
        /// <value>The power level.</value>
        DevicePowerChargeLevel PowerLevel { get; set; }
        /// <summary>
        /// Gets or sets the remaining charge time.
        /// </summary>
        /// <value>The remaining charge time.</value>
        DateTime? RemainingChargeTime { get; set; }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        void GetObjectData(SerializationInfo info, StreamingContext context);
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        string ToString();
    }
}