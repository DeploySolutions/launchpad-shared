// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacProvider.cs" company="Deploy Software Solutions, inc.">
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
    /// Class StacProvider.
    /// </summary>
    public partial class StacProvider
    {
        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [JsonProperty("providers", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<Provider> Providers { get; set; }
    }

    /// <summary>
    /// Class Provider.
    /// </summary>
    public partial class Provider
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<OrganizationRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Url { get; set; }
    }

    public partial class StacProvider
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacProvider.</returns>
        public static StacProvider FromJson(string json) => JsonConvert.DeserializeObject<StacProvider>(json, Deploy.LaunchPad.Core.STAC.StacProviderConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacProvider.
    /// </summary>
    public static class SerializeStacProvider
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacProvider self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacProviderConverter.Settings);
    }

    /// <summary>
    /// Class StacProviderConverter.
    /// </summary>
    internal static class StacProviderConverter
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
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }


}
