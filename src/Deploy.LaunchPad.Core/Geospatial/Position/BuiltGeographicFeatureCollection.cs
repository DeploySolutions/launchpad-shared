using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Geospatial.Position
{
    public partial class BuiltGeographicFeatureCollection<T> : HumanGeographicFeatureCollection<T>
        where T : IAmBuiltGeographicFeature
    {

        public BuiltGeographicFeatureCollection() : base()
        { }
    }
}
