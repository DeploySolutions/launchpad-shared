// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacCollection.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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
    /// Class StacCollection.
    /// </summary>
    public partial class StacCollection
    {
        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [JsonProperty("providers", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<Provider> Providers { get; set; }
    }

    public partial class StacCollection
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacCollection.</returns>
        public static StacCollection FromJson(string json) => JsonConvert.DeserializeObject<StacCollection>(json, Deploy.LaunchPad.Core.STAC.StacCollectionConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacCollection.
    /// </summary>
    public static class SerializeStacCollection
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacCollection self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacCollectionConverter.Settings);
    }

    /// <summary>
    /// Class StacCollectionConverter.
    /// </summary>
    internal static class StacCollectionConverter
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
