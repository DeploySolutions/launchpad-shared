// 
// 
using H3;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Domain.Geospatial.H3
{
    public partial interface ICanBeDescribedInH3
    {
        public H3Index H3Index { get; set; }

    }
}