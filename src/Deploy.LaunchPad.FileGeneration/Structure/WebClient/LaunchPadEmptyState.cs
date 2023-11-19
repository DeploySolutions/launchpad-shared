// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadEmptyState.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a state when a table is empty.
    /// </summary>
    [Serializable]
    public partial class LaunchPadEmptyState : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// SVG icon displayed in the empty state UI design
        /// </summary>
        /// <value>The icon.</value>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// CSV Import action for non-readonly domain entiry
        /// </summary>
        /// <value>The import.</value>
        [JsonProperty("import")]
        public LaunchPadAction Import { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadEmptyState"/> class.
        /// </summary>
        public LaunchPadEmptyState() : base()
        {
        }
    }
}
