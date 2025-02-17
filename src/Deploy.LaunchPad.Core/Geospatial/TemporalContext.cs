using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial
{
    public enum TemporalContext
    {
        IsUnknownOrUnspecified = 0,
        Past = 1,
        Present = 2,
        FutureImmediate = 3,
        FutureShortTerm = 4,
        FutureMediumTerm = 5,
        FutureLongTerm = 6
    }

}
