// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacAsset.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.STAC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.STAC
{
    /// <summary>
    /// Class StacAsset.
    /// </summary>
    public partial class StacAsset
    {
        /// <summary>
        /// Detailed multi-line description to fully explain the Item.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>The href.</value>
        [JsonProperty("href")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Href { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Roles { get; set; }

        /// <summary>
        /// A human-readable title describing the Item.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Type { get; set; }

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
    }

}
