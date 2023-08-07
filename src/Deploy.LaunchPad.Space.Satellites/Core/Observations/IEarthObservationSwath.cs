using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Domain.Model;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Space.Satellites.Core.Observations
{
    public partial interface IEarthObservationSwath : ILaunchPadCommonProperties, ILaunchPadObject
    {
        public IDictionary<string, IEarthObservationScene> Scenes { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "swathId", EmitDefaultValue = false)]
        public string SwathId { get; set; }

        /// <summary>
        /// Gets or Sets datetimeUtcEstimated
        /// </summary>
        [DataMember(Name = "datetimeUtcEstimated", EmitDefaultValue = false)]
        public DateTime DateTimeUtcEstimated { get; set; }

    }
}
