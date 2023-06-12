using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.STAC
{

    public partial class StacLink
    {
        [JsonProperty("href")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Href { get; set; }

        [JsonProperty("rel")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Rel { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Type { get; set; }
    }

}
