// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-04-2023
// ***********************************************************************
// <copyright file="StacBasics.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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
    /// Class StacBasics.
    /// </summary>
    public partial class StacBasics
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
    }

    public partial class StacBasics
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacBasics.</returns>
        public static StacBasics FromJson(string json) => JsonConvert.DeserializeObject<StacBasics>(json, Deploy.LaunchPad.Core.STAC.StacBasicsConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacBasics.
    /// </summary>
    public static class SerializeStacBasics
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacBasics self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacBasicsConverter.Settings);
    }

    /// <summary>
    /// Class StacBasicsConverter.
    /// </summary>
    internal static class StacBasicsConverter
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
