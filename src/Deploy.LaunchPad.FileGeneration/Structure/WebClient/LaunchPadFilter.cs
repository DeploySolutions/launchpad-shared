// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadFilter.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a tile, which is a block of label and icon with a link to other page. We want to support multiple type of tile later on, not just tile that open a new route. This could be data tile, image tile, chart tile etc.
    /// </summary>
    [Serializable]
    public partial class LaunchPadFilter : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Placeholder displayed in the filter input field
        /// </summary>
        /// <value>The placeholder.</value>
        [JsonProperty("placeholder")]
        [JsonConverter(typeof(LocalizedJsonConverter<string>))]
        public string Placeholder { get; set; }

        /// <summary>
        /// Data field name representing the search. We use this field to call the search API with.
        /// </summary>
        /// <value>The data field.</value>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field type validation in the client
        /// </summary>
        /// <value>The type of the data.</value>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadFilter"/> class.
        /// </summary>
        public LaunchPadFilter() : base()
        {
            Placeholder = string.Empty;
            DataField = string.Empty;
            DataType = string.Empty;
        }
    }
}
