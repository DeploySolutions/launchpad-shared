using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    public partial interface IEarthObservationSwath<TPrimaryKey> : ILaunchPadDomainEntity<TPrimaryKey>
    {
        public IDictionary<TPrimaryKey, IEarthObservationScene> Scenes { get; set; }

        /// <summary>
        /// Gets or Sets datetimeUtcEstimated
        /// </summary>
        [DataMember(Name = "datetimeUtcEstimated", EmitDefaultValue = false)]
        public DateTime DateTimeUtcEstimated { get; set; }

    }
}
