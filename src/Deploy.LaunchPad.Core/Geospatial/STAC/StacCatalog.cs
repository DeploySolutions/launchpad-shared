// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacCatalog.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Deploy.LaunchPad.Core.STAC
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Deploy.LaunchPad.Core.Geospatial.STAC;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// This object represents Catalogs in a SpatioTemporal Asset Catalog.
    /// </summary>
    public partial class StacCatalog
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the links.
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

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public virtual dynamic Type { get; set; }
    }

    public partial class StacCatalog
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacCatalog.</returns>
        public static StacCatalog FromJson(string json) => JsonConvert.DeserializeObject<StacCatalog>(json, Deploy.LaunchPad.Core.STAC.StacCatalogConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacCatalog.
    /// </summary>
    public static class SerializeStacCatalog
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacCatalog self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacCatalogConverter.Settings);
    }

    /// <summary>
    /// Class StacCatalogConverter.
    /// </summary>
    internal static class StacCatalogConverter
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
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
