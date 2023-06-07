﻿// "GeoJSON is a format for encoding a variety of geographic data structures using JavaScript Object Notation (JSON) [RFC7159].
// A GeoJSON object may represent a region of space (a Geometry), a spatially bounded entity (a Feature), or a list of Features (a FeatureCollection)."
// Read more: https://datatracker.ietf.org/doc/html/rfc7946 and https://stevage.github.io/geojson-spec/#section-3
// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// using the json schema found here: https://geojson.org/schema/GeoJSON.json
// 
namespace Deploy.LaunchPad.Core.GeoJson
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson;
    using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Types;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GeoJsonDefinition<TGeoJsonType> : ICanBeDescribedInGeoJson<TGeoJsonType>
        where TGeoJsonType : IAmAGeoJsonType
    {

        [JsonProperty("geojson", Required = Required.Always)]
        public virtual TGeoJsonType Definition { get; set; }
        

        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public virtual GeoJsonId? GeoJsonId { get; set; }

        public GeoJsonDefinition()
        {
        }

    }

}