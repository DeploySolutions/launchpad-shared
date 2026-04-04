// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-04-2023
// ***********************************************************************
// <copyright file="StacLicensing.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Deploy.LaunchPad.Domain.STAC
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Class StacLicensing.
    /// </summary>
    public partial class StacLicensing
    {
        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>The license.</value>
        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string License { get; set; }
    }

    public partial class StacLicensing
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>StacLicensing.</returns>
        public static StacLicensing FromJson(string json) => JsonConvert.DeserializeObject<StacLicensing>(json, Deploy.LaunchPad.Domain.STAC.StacLicensingConverter.Settings);
    }

    /// <summary>
    /// Class SerializeStacLicensing.
    /// </summary>
    public static class SerializeStacLicensing
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this StacLicensing self) => JsonConvert.SerializeObject(self, Deploy.LaunchPad.Domain.STAC.StacLicensingConverter.Settings);
    }

    /// <summary>
    /// Class StacLicensingConverter.
    /// </summary>
    internal static class StacLicensingConverter
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
