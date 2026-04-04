using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Geospatial.Position
{
    public partial class NaturalGeographicFeatureCollection<T> : GeographicFeatureCollection<T>
        where T : IAmNaturalGeographicFeature
    {

        public NaturalGeographicFeatureCollection() : base()
        { }
    }
}
