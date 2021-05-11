using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LocalizedJsonConverter<T> : JsonConverter
    {
        class Value
        {
            public T en { get; set; }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.MoveToContent().TokenType)
            {
                case JsonToken.Null:
                    return null;

                default:
                    return serializer.Deserialize<Value>(reader).en;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, new Value { en = (T)value });
        }
    }

    public static partial class JsonExtensions
    {
        public static JsonReader MoveToContent(this JsonReader reader)
        {
            if (reader.TokenType == JsonToken.None)
                reader.Read();
            while (reader.TokenType == JsonToken.Comment && reader.Read())
                ;
            return reader;
        }
    }
}
