﻿// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// 

namespace Deploy.LaunchPad.Core.STAC
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Deploy.LaunchPad.Core.Domain.Geography.STAC;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// This object represents Catalogs in a SpatioTemporal Asset Catalog.
    /// </summary>
    public partial class StacCatalog
    {
        [JsonProperty("description")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Description { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Id { get; set; }

        [JsonProperty("links")]
        public virtual List<StacLink> Links { get; set; }

        [JsonProperty("stac_extensions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> StacExtensions { get; set; }

        [JsonProperty("stac_version")]
        public virtual string StacVersion { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        [JsonProperty("type")]
        public virtual dynamic Type { get; set; }
    }

    public partial class StacCatalog
    {
        public static StacCatalog FromJson(string json) => JsonConvert.DeserializeObject<StacCatalog>(json, Deploy.LaunchPad.Core.STAC.StacCatalogConverter.Settings);
    }

    public static class SerializeStacCatalog
    {
        public static string ToJson(this StacCatalog self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacCatalogConverter.Settings);
    }

    internal static class StacCatalogConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
