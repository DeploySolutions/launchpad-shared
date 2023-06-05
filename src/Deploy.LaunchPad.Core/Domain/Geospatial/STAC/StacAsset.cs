﻿// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// 

using Deploy.LaunchPad.Core.STAC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geography.STAC
{
    public partial class StacAsset
    {
        /// <summary>
        /// Detailed multi-line description to fully explain the Item.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Description { get; set; }

        [JsonProperty("href")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Href { get; set; }

        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Roles { get; set; }

        /// <summary>
        /// A human-readable title describing the Item.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Type { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Created { get; set; }

        /// <summary>
        /// The searchable date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        [JsonProperty("datetime")]
        public virtual DateTimeOffset? Datetime { get; set; }

        /// <summary>
        /// The searchable end date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        [JsonProperty("end_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? EndDatetime { get; set; }

        /// <summary>
        /// The searchable start date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        [JsonProperty("start_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? StartDatetime { get; set; }

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Updated { get; set; }

        [JsonProperty("constellation", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Constellation { get; set; }

        [JsonProperty("gsd", NullValueHandling = NullValueHandling.Ignore)]
        public virtual double? Gsd { get; set; }

        [JsonProperty("instruments", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Instruments { get; set; }

        [JsonProperty("mission", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Mission { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Platform { get; set; }

        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string License { get; set; }

        [JsonProperty("providers", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<Provider> Providers { get; set; }
    }

}
