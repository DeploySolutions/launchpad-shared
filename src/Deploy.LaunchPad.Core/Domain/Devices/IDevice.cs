// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

using Deploy.LaunchPad.Core.Domain.Devices;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Core.Domain
{
    public partial interface IDevice
    {
        SpaceTimeInformation CurrentLocation { get; set; }
        DevicePower Power { get; set; }
        IList<SpaceTimeInformation> PreviousLocations { get; set; }
        int? TenantId { get; set; }

        void GetObjectData(SerializationInfo info, StreamingContext context);
        string ToString();
    }
}