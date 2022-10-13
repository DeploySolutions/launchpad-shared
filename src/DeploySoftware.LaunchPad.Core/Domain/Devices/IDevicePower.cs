//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

using DeploySoftware.LaunchPad.Core.Domain.Devices;
using System;
using System.Runtime.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public interface IDevicePower
    {
        DevicePowerChargeLevel PowerLevel { get; set; }
        DateTime? RemainingChargeTime { get; set; }

        void GetObjectData(SerializationInfo info, StreamingContext context);
        string ToString();
    }
}