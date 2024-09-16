// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-04-2023
// ***********************************************************************
// <copyright file="StacDateTime.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Deploy.LaunchPad.Core.STAC
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Class StacDateTime.
    /// </summary>
    public partial class StacDateTime
    {
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Created { get; set; }

        /// <summary>
        /// The searchable date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        /// <value>The datetime.</value>
        [JsonProperty("datetime")]
        public virtual DateTimeOffset? Datetime { get; set; }

        /// <summary>
        /// The searchable end date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        /// <value>The end datetime.</value>
        [JsonProperty("end_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? EndDatetime { get; set; }

        /// <summary>
        /// The searchable start date/time of the assets, in UTC (Formatted in RFC 3339)
        /// </summary>
        /// <value>The start datetime.</value>
        [JsonProperty("start_datetime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? StartDatetime { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? Updated { get; set; }
    }

    public partial class StacDateTime
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacDateTime.</returns>
        public static StacDateTime FromJson(string json) => JsonConvert.DeserializeObject<StacDateTime>(json, Deploy.LaunchPad.Core.STAC.StacDateTimeConverter.Settings);
    }

    /// <summary>
    /// Class Serialize.
    /// </summary>
    public static class Serialize
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacDateTime self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacDateTimeConverter.Settings);
    }

    /// <summary>
    /// Class StacDateTimeConverter.
    /// </summary>
    internal static class StacDateTimeConverter
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
