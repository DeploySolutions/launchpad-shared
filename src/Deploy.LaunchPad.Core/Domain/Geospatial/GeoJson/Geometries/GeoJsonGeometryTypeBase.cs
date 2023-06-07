using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Geometries
{
    [Serializable]
    public abstract partial class GeoJsonGeometryTypeBase : IAmAGeometryType
    {
        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<double> Bbox { get; set; }

        protected GeoJsonGeometryTypeBase() { 
            Bbox = new List<double> { };
        }
    }
}
