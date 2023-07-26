// 
// 
using H3;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Geospatial.H3
{
    public partial interface IMayHaveH3Definition
    {
        public H3Index? H3Index { get; set; }

    }
}