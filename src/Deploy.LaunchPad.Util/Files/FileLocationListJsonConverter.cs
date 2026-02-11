// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-10-2023
// ***********************************************************************
// <copyright file="FileLocationListJsonConverter.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Util.Files
{
    /// <summary>
    /// Converts a list of StorageLocationJson objects to or from json.
    /// </summary>
    public partial class FileLocationListJsonConverter : JsonConverter<List<StorageLocationJson>>
    {
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override List<StorageLocationJson> ReadJson(JsonReader reader, Type objectType, List<StorageLocationJson> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as List<StorageLocationJson>;
            if (token.HasValues)
            {
                var childValues = token.Values();
                JTokenType type = childValues.FirstOrDefault().Type;
                if (type != JTokenType.Null && childValues.First().Any())
                {
                    if (type == JTokenType.Object) // it's a single object
                    {
                        var locationToken = childValues.FirstOrDefault();
                        StorageLocationJson newObject = locationToken.ToObject<StorageLocationJson>();
                        list.Add(newObject);
                    }
                    if (type == JTokenType.Array) // it's an array of objects
                    {
                        foreach (var child in childValues)
                        {
                            var locationTokenArray = child.Children();
                            foreach (var locationToken in locationTokenArray)
                            {
                                StorageLocationJson newObject = locationToken.ToObject<StorageLocationJson>();
                                list.Add(newObject);
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, List<StorageLocationJson> value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    /// <summary>
    /// Class StorageLocationJson.
    /// </summary>
    public partial class StorageLocationJson
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public virtual string Provider { get; set; }
        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        public virtual string ProviderType { get; set; } = "Deploy.LaunchPad.Core.Domain.WindowsFileStorageLocation";
        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        public virtual string DescriptionShort { get; set; }
        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        public virtual string DescriptionFull { get; set; }

        /// <summary>
        /// Gets or sets the root URI.
        /// </summary>
        /// <value>The root URI.</value>
        public virtual Uri RootUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public virtual bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Gets or sets the default prefix.
        /// </summary>
        /// <value>The default prefix.</value>
        public virtual string DefaultPrefix { get; set; } = "";


        /// <summary>
        /// Initializes a new instance of the <see cref="StorageLocationJson"/> class.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageLocationJson"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="providerType">Type of the provider.</param>
        /// <param name="rootUri">The root URI.</param>
        /// <param name="description">The description.</param>
        /// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
        /// <param name="defaultPrefix">The default prefix.</param>
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

