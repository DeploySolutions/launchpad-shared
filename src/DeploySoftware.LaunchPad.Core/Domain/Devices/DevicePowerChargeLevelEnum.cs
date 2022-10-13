using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain.Devices
{
    public enum DevicePowerChargeLevel
    {
        Unknown = 0,
        Charged = 1,
        Charging = 2,
        Draining = 3,
        Drained = 4
    }
}
