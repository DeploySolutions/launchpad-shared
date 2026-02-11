using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Geospatial.Position
{
    public partial interface IBuiltGeographicFeatureCollection<T> : IGeographicFeatureCollection<T> where T : IAmBuiltGeographicFeature
    {
    }
}
