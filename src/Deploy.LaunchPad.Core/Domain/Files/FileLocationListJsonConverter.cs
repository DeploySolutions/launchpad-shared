using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Domain.Files
{
    public partial class FileLocationListJsonConverter : JsonConverter<IList<IFileStorageLocation>>
    {
        public override IList<IFileStorageLocation> ReadJson(JsonReader reader, Type objectType, IList<IFileStorageLocation> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as System.Collections.IList;
            var itemType = objectType.GenericTypeArguments[0];
            foreach (var child in token.Values())
            {
                var childToken = child.Children().First();
                var newObject = Activator.CreateInstance(itemType);
                serializer.Populate(childToken.CreateReader(), newObject);
                list.Add(newObject);
            }
            return (IList<IFileStorageLocation>)list;
        }

        public override void WriteJson(JsonWriter writer, IList<IFileStorageLocation> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

