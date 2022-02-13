using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{
    /// <summary>
    /// Aligns to the NASA Earth Observing System Data and Information System (EOSDIS) data product levels.
    /// https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels
    /// </summary>
    public enum EOSDISLevelEnum
    {
        Level0 = 0,
        Level1A = 1,
        Level1B = 2,
        Level1C = 3,
        Level2 = 4,
        Level2A = 5,
        Level2B = 6,
        Level3 = 7,
        Level3A = 8,
        Level4 = 9

    }
}
