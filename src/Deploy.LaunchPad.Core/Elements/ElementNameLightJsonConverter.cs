using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.Core
{
    public partial class ElementNameLightJsonConverter : JsonConverter<ElementNameLight>
    {
        public override ElementNameLight ReadJson(JsonReader reader, Type objectType, ElementNameLight existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
                return null;
            return new ElementName(value);
        }

        public override void WriteJson(JsonWriter writer, ElementNameLight value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }

}
