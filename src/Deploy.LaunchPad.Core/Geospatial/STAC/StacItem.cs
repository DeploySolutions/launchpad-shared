// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacItem.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Deploy.LaunchPad.Core.STAC
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Runtime.Serialization;
    using Deploy.LaunchPad.Core.Geospatial;
    using Deploy.LaunchPad.Core.Geospatial.GeoJson;
    using Deploy.LaunchPad.Core.Geospatial.STAC;
    using Deploy.LaunchPad.Core.Temporal;
    using Deploy.LaunchPad.Util;
    using NetTopologySuite.Geometries;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// This object represents the metadata for an item in a SpatioTemporal Asset Catalog.
    /// </summary>
    public partial class StacItem
    {
        /// <summary>
        /// Gets or sets the bbox.
        /// </summary>
        /// <value>The bbox.</value>
        [JsonProperty("bbox", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<double> Bbox { get; set; }

        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>The geometry.</value>
        [JsonProperty("geometry")]
        public virtual Geometry Geometry { get; set; }

        /// <summary>
        /// Provider item ID
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty("properties")]
        public virtual CommonMetadata Properties { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public virtual StacItemType Type { get; set; }

        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        [JsonProperty("assets")]
        public virtual Dictionary<string, StacAsset> Assets { get; set; }

        /// <summary>
        /// Links to item relations
        /// </summary>
        /// <value>The links.</value>
        [JsonProperty("links")]
        public virtual List<StacLink> Links { get; set; }

        /// <summary>
        /// Gets or sets the stac extensions.
        /// </summary>
        /// <value>The stac extensions.</value>
        [JsonProperty("stac_extensions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> StacExtensions { get; set; }

        /// <summary>
        /// Gets or sets the stac version.
        /// </summary>
        /// <value>The stac version.</value>
        [JsonProperty("stac_version")]
        public virtual string StacVersion { get; set; }
    }



    /// <summary>
    /// Class CommonMetadata.
    /// </summary>
    public partial class CommonMetadata : IMustHaveTemporalInformation
    {
        /// <summary>
        /// Detailed multi-line description to fully explain the Item.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Description { get; set; }

        /// <summary>
        /// A human-readable title describing the Item.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Created { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public virtual TemporalInformation TemporalInformation { get; set; }
        
        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Updated { get; set; }

        /// <summary>
        /// Gets or sets the constellation.
        /// </summary>
        /// <value>The constellation.</value>
        [JsonProperty("constellation", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Constellation { get; set; }

        /// <summary>
        /// Gets or sets the GSD.
        /// </summary>
        /// <value>The GSD.</value>
        [JsonProperty("gsd", NullValueHandling = NullValueHandling.Ignore)]
        public virtual double? Gsd { get; set; }

        /// <summary>
        /// Gets or sets the instruments.
        /// </summary>
        /// <value>The instruments.</value>
        [JsonProperty("instruments", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Instruments { get; set; }

        /// <summary>
        /// Gets or sets the mission.
        /// </summary>
        /// <value>The mission.</value>
        [JsonProperty("mission", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Mission { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Platform { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>The license.</value>
        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string License { get; set; }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [JsonProperty("providers", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<Provider> Providers { get; set; }

        public CommonMetadata(string title, string description)
        {
            Title = title;
            Description = description;
            Created = DateTimeOffset.UtcNow;
            Updated = DateTimeOffset.UtcNow;
            Providers = new List<Provider>();
            Instruments = new List<string>();
            TemporalInformation = new TemporalInformation();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected CommonMetadata(SerializationInfo info, StreamingContext context)
        {
            Title = info.GetString("Title");
            Description = info.GetString("Description");
            Providers = (List<Provider>)info.GetValue("Providers", typeof(List<Provider>));
            Created = (DateTimeOffset)info.GetValue("Created", typeof(DateTimeOffset));
            Updated = (DateTimeOffset)info.GetValue("Updated", typeof(DateTimeOffset));
            TemporalInformation = (TemporalInformation)info.GetValue("TemporalInformation", typeof(TemporalInformation));
            Constellation = info.GetString("Constellation");
            Gsd = info.GetDouble("Gsd");
            Instruments = (List<string>)info.GetValue("Instruments", typeof(List<string>));
            License = info.GetString("License");
            Platform = info.GetString("Platform");
            Mission = info.GetString("Mission");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Title", Title);
            info.AddValue("Description", Description);
            info.AddValue("Providers", Providers);
            info.AddValue("Created", Created);
            info.AddValue("Updated", Updated);
            info.AddValue("TemporalInformation", TemporalInformation);
            info.AddValue("Constellation", Constellation);
            info.AddValue("Gsd", Gsd);
            info.AddValue("Instruments", Instruments);
            info.AddValue("License", License);
            info.AddValue("Platform", Platform);
            info.AddValue("Mission", Mission);
        }

    }

    /// <summary>
    /// Enum OrganizationRole
    /// </summary>
    public enum OrganizationRole { Host, Licensor, Processor, Producer };

    /// <summary>
    /// Enum StacItemType
    /// </summary>
    public enum StacItemType { Feature };


    public partial class StacItem
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacItem.</returns>
        public static StacItem FromJson(string json) => JsonConvert.DeserializeObject<StacItem>(json, Deploy.LaunchPad.Core.STAC.StacItemConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacItem.
    /// </summary>
    public static class SerializeStacItem
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacItem self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacItemConverter.Settings);
    }

    /// <summary>
    /// Class StacItemConverter.
    /// </summary>
    internal static class StacItemConverter
    {
        /// <summary>
        /// The settings
        /// </summary>
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                OrganizationRoleConverter.Singleton,
                StacItemTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


    /// <summary>
    /// Class StacItemTypeConverter.
    /// Implements the <see cref="JsonConverter" />
    /// </summary>
    /// <seealso cref="JsonConverter" />
    internal class StacItemTypeConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns><c>true</c> if this instance can convert the specified t; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type t) => t == typeof(StacItemType) || t == typeof(StacItemType?);

        /// <summary>
        /// Reads the json.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="t">The t.</param>
        /// <param name="existingValue">The existing value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.Exception">Cannot unmarshal type StacItemType</exception>
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Feature")
            {
                return StacItemType.Feature;
            }
            throw new Exception("Cannot unmarshal type StacItemType");
        }

        /// <summary>
        /// Writes the json.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="untypedValue">The untyped value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <exception cref="System.Exception">Cannot marshal type StacItemType</exception>
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StacItemType)untypedValue;
            if (value == StacItemType.Feature)
            {
                serializer.Serialize(writer, "Feature");
                return;
            }
            throw new Exception("Cannot marshal type StacItemType");
        }

        /// <summary>
        /// The singleton
        /// </summary>
        public static readonly StacItemTypeConverter Singleton = new StacItemTypeConverter();
    }
}
