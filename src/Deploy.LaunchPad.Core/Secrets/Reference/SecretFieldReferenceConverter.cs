using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets.Reference
{
    public class SecretFieldReferenceConverter : JsonConverter<ISecretFieldReference>
    {
        public override ISecretFieldReference ReadJson(JsonReader reader, Type objectType, ISecretFieldReference existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var oldConverters = serializer.Converters.ToList();
            serializer.Converters.Remove(this);
            var result = jo.ToObject<SecretFieldReference>(serializer);
            serializer.Converters.Clear();
            foreach (var c in oldConverters) serializer.Converters.Add(c);
            return result;
        }

        public override void WriteJson(JsonWriter writer, ISecretFieldReference value, JsonSerializer serializer)
        {
            // Serialize as the concrete type, but avoid recursion by not using the converter for the concrete type
            var concrete = value as SecretFieldReference;
            if (concrete != null)
            {
                // Temporarily remove this converter to avoid recursion
                var oldConverters = serializer.Converters.ToList();
                serializer.Converters.Remove(this);
                serializer.Serialize(writer, concrete);
                serializer.Converters.Clear();
                foreach (var c in oldConverters) serializer.Converters.Add(c);
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
