// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="StacLink.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.STAC
{

    /// <summary>
    /// Class StacLink.
    /// </summary>
    public partial class StacLink
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>The href.</value>
        [JsonProperty("href")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Href { get; set; }

        /// <summary>
        /// Gets or sets the relative.
        /// </summary>
        /// <value>The relative.</value>
        [JsonProperty("rel")]
        [JsonConverter(typeof(MinMaxLengthCheckConverter))]
        public virtual string Rel { get; set; }

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
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Type { get; set; }
    }

}
