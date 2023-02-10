using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core.Domain.Files
{
    /// <summary>
    /// Converts a list of StorageLocationJson objects to or from json.
    /// </summary>
    public partial class FileLocationListJsonConverter : JsonConverter<List<StorageLocationJson>>
    {
        public override List<StorageLocationJson> ReadJson(JsonReader reader, Type objectType, List<StorageLocationJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as List<StorageLocationJson>;
            var itemType = objectType.GenericTypeArguments[0];
            foreach (var child in token.Values())
            {
                var locationTokenArray = child.Children();
                foreach (var locationToken in locationTokenArray)
                {
                    StorageLocationJson newObject = locationToken.ToObject<StorageLocationJson>();
                    list.Add(newObject);
                }

            }
            return list;
        }

        public override void WriteJson(JsonWriter writer, List<StorageLocationJson> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    public partial class StorageLocationJson
    {

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Provider { get; set; }
        public virtual string ProviderType { get; set; } = "Deploy.LaunchPad.Core.Domain.WindowsFileStorageLocation";
        public virtual string DescriptionShort { get; set; }
        public virtual string DescriptionFull { get; set; }

        public virtual Uri RootUri { get; set; }

        public virtual bool IsReadOnly { get; set; } = false;

        public virtual string DefaultPrefix { get; set; } = "";


        public StorageLocationJson()
        {
            Id = Guid.NewGuid().ToString();
            Provider = "Unknown";
            Name = string.Format("{0} (ID {1})'.", ProviderType, Id);
            string description = string.Format("Storage location for '{0}' of type '{1}'.", Name, ProviderType);
            DescriptionShort = description;
            DescriptionFull = description;
            DefaultPrefix = string.Empty;
        }

        public StorageLocationJson(string id, string name, string provider, string providerType, Uri rootUri, string description = "", bool isReadOnly = false, string defaultPrefix = "")
        {
            Id = id;
            Name = name;
            Provider = provider;
            ProviderType = providerType;
            RootUri = rootUri;
            IsReadOnly = isReadOnly;
            DescriptionShort = description;
            DescriptionFull = description;
            DefaultPrefix = defaultPrefix;
        }

    }

}

