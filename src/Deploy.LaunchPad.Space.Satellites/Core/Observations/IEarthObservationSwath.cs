// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="IEarthObservationSwath.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Domain;
using Deploy.LaunchPad.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    /// <summary>
    /// Interface IEarthObservationSwath
    /// Extends the <see cref="ILaunchPadCoreProperties" />
    /// Extends the <see cref="ILaunchPadObject" />
    /// </summary>
    /// <seealso cref="ILaunchPadCoreProperties" />
    /// <seealso cref="ILaunchPadObject" />
    public partial interface IEarthObservationSwath : ILaunchPadCoreProperties, ILaunchPadObject
    {
        /// <summary>
        /// Gets or sets the scenes.
        /// </summary>
        /// <value>The scenes.</value>
        public IDictionary<string, IEarthObservationScene> Scenes { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        /// <value>The swath identifier.</value>
        [DataMember(Name = "swathId", EmitDefaultValue = false)]
        public string SwathId { get; set; }

        /// <summary>
        /// Gets or Sets datetimeUtcEstimated
        /// </summary>
        /// <value>The date time UTC estimated.</value>
        [DataMember(Name = "datetimeUtcEstimated", EmitDefaultValue = false)]
        public DateTime DateTimeUtcEstimated { get; set; }

    }
}
