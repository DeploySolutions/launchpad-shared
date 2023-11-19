// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="IDevice.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Domain.Devices;
using Deploy.LaunchPad.Core.Geospatial;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    /// <summary>
    /// Interface IDevice
    /// </summary>
    public partial interface IDevice
    {
        /// <summary>
        /// Gets or sets the current location.
        /// </summary>
        /// <value>The current location.</value>
        SpaceTimeInformation CurrentLocation { get; set; }
        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>The power.</value>
        DevicePower Power { get; set; }
        /// <summary>
        /// Gets or sets the previous locations.
        /// </summary>
        /// <value>The previous locations.</value>
        IList<SpaceTimeInformation> PreviousLocations { get; set; }
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>The tenant identifier.</value>
        int? TenantId { get; set; }

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