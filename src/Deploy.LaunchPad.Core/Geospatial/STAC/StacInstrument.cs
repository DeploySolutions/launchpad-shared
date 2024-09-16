// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-04-2023
// ***********************************************************************
// <copyright file="StacInstrument.cs" company="Deploy Software Solutions, inc.">
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
    /// Class StacInstrument.
    /// </summary>
    public partial class StacInstrument
    {
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
    }

    public partial class StacInstrument
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacInstrument.</returns>
        public static StacInstrument FromJson(string json) => JsonConvert.DeserializeObject<StacInstrument>(json, Deploy.LaunchPad.Core.STAC.StacInstrumentConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacInstrument.
    /// </summary>
    public static class SerializeStacInstrument
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacInstrument self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Core.STAC.StacInstrumentConverter.Settings);
    }
    /// <summary>
    /// Class StacInstrumentConverter.
    /// </summary>
    internal static class StacInstrumentConverter
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
