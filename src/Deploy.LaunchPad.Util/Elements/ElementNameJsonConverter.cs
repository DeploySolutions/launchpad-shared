using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.Util
{
    public partial class ElementNameJsonConverter : JsonConverter<ElementName>
    {
        public override ElementName ReadJson(JsonReader reader, Type objectType, ElementName existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
                return null;
            return new ElementName(value);
        }

        public override void WriteJson(JsonWriter writer, ElementName value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }

}
