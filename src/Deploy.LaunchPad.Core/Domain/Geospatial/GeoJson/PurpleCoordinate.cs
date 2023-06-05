﻿// Initially generated by quicktype https://github.com/quicktype/quicktype under the Apache 2 license.
// 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Geospatial.GeoJson
{

    public partial struct PurpleCoordinate
    {
        public double? Double;
        public List<double> DoubleArray;

        public static implicit operator PurpleCoordinate(double Double) => new PurpleCoordinate { Double = Double };
        public static implicit operator PurpleCoordinate(List<double> DoubleArray) => new PurpleCoordinate { DoubleArray = DoubleArray };
    }


    internal class PurpleCoordinateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurpleCoordinate) || t == typeof(PurpleCoordinate?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new PurpleCoordinate { Double = doubleValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<double>>(reader);
                    return new PurpleCoordinate { DoubleArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type PurpleCoordinate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (PurpleCoordinate)untypedValue;
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }
            if (value.DoubleArray != null)
            {
                serializer.Serialize(writer, value.DoubleArray);
                return;
            }
            throw new Exception("Cannot marshal type PurpleCoordinate");
        }

        public static readonly PurpleCoordinateConverter Singleton = new PurpleCoordinateConverter();
    }



}
