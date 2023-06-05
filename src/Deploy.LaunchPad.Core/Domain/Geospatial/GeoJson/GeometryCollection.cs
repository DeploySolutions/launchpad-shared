﻿// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// using the json schema found here: https://geojson.org/schema/GeometryCollection.json
// 

namespace Deploy.LaunchPad.Core.GeoJson
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GeometryCollection
    {
        [JsonProperty("bbox", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<double> Bbox { get; set; }

        [JsonProperty("geometries", Required = Required.Always)]
        public virtual List<GeoJsonDefinition> Geometries { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public virtual GeometryCollectionType Type { get; set; }
    }


    public enum GeometryCollectionType { GeometryCollection };

    public partial class GeometryCollection
    {
        public static GeometryCollection FromJson(string json) => JsonConvert.DeserializeObject<GeometryCollection>(json, Deploy.LaunchPad.Core.GeoJson.GeometryCollectionConverter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GeometryCollection self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.GeoJson.GeometryCollectionConverter.Settings);
    }

    internal static class GeometryCollectionConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GeometryCoordinateConverter.Singleton,
                FluffyCoordinateConverter.Singleton,
                PurpleCoordinateConverter.Singleton,
                GeometryTypeConverter.Singleton,
                GeometryCollectionTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GeometryCollectionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeometryCollectionType) || t == typeof(GeometryCollectionType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "GeometryCollection")
            {
                return GeometryCollectionType.GeometryCollection;
            }
            throw new Exception("Cannot unmarshal type GeometryCollectionType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeometryCollectionType)untypedValue;
            if (value == GeometryCollectionType.GeometryCollection)
            {
                serializer.Serialize(writer, "GeometryCollection");
                return;
            }
            throw new Exception("Cannot marshal type GeometryCollectionType");
        }

        public static readonly GeometryCollectionTypeConverter Singleton = new GeometryCollectionTypeConverter();
    }
}
