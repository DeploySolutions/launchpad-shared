// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DevicePowerChargeLevelEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.Core.Domain.Devices
{
    /// <summary>
    /// Enum DevicePowerChargeLevel
    /// </summary>
    public enum DevicePowerChargeLevel
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The charged
        /// </summary>
        Charged = 1,
        /// <summary>
        /// The charging
        /// </summary>
        Charging = 2,
        /// <summary>
        /// The draining
        /// </summary>
        Draining = 3,
        /// <summary>
        /// The drained
        /// </summary>
        Drained = 4
    }
}
