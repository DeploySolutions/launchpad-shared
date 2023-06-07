using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson.Types
{

    public enum GeoJsonType
    { 
        Feature = 0, 
        FeatureCollection = 1, 
        GeometryCollection =2, 
        LineString = 3, 
        MultiLineString =4, 
        MultiPoint =5, 
        MultiPolygon = 6, 
        Point = 7, 
        Polygon =8
    };

    internal class GeoJsonTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeoJsonType) || t == typeof(GeoJsonType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Feature":
                    return GeoJsonType.Feature;
                case "FeatureCollection":
                    return GeoJsonType.FeatureCollection;
                case "GeometryCollection":
                    return GeoJsonType.GeometryCollection;
                case "LineString":
                    return GeoJsonType.LineString;
                case "MultiLineString":
                    return GeoJsonType.MultiLineString;
                case "MultiPoint":
                    return GeoJsonType.MultiPoint;
                case "MultiPolygon":
                    return GeoJsonType.MultiPolygon;
                case "Point":
                    return GeoJsonType.Point;
                case "Polygon":
                    return GeoJsonType.Polygon;
            }
            throw new Exception("Cannot unmarshal type GeoJsonType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeoJsonType)untypedValue;
            switch (value)
            {
                case GeoJsonType.Feature:
                    serializer.Serialize(writer, "Feature");
                    return;
                case GeoJsonType.FeatureCollection:
                    serializer.Serialize(writer, "FeatureCollection");
                    return;
                case GeoJsonType.GeometryCollection:
                    serializer.Serialize(writer, "GeometryCollection");
                    return;
                case GeoJsonType.LineString:
                    serializer.Serialize(writer, "LineString");
                    return;
                case GeoJsonType.MultiLineString:
                    serializer.Serialize(writer, "MultiLineString");
                    return;
                case GeoJsonType.MultiPoint:
                    serializer.Serialize(writer, "MultiPoint");
                    return;
                case GeoJsonType.MultiPolygon:
                    serializer.Serialize(writer, "MultiPolygon");
                    return;
                case GeoJsonType.Point:
                    serializer.Serialize(writer, "Point");
                    return;
                case GeoJsonType.Polygon:
                    serializer.Serialize(writer, "Polygon");
                    return;
            }
            throw new Exception("Cannot marshal type GeoJsonType");
        }

        public static readonly GeoJsonTypeConverter Singleton = new GeoJsonTypeConverter();
    }
}
