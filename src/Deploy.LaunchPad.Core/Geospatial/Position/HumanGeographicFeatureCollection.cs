using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Geospatial.Position
{
    public partial class HumanGeographicFeatureCollection<T> : GeographicFeatureCollection<T>
        where T : IAmHumanGeographicFeature
    {

        public HumanGeographicFeatureCollection() : base()
        { }
    }
}
