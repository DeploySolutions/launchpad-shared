// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadAction.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents an action buttons on the UI
    /// </summary>
    [Serializable]
    public partial class LaunchPadAction : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// We support `edit` and `delete` as a type at the moment
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public String Type { get; set; }
        /// <summary>
        /// Route action. Takes the user to this route when they clicks on the button.
        /// </summary>
        /// <value>The route.</value>
        [JsonProperty("route")]
        public string Route { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        /// <value>The icon.</value>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Custom method called when they clicks on the button.
        /// </summary>
        /// <value>The function.</value>
        [JsonProperty("function")]
        public string Function { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAction"/> class.
        /// </summary>
        public LaunchPadAction() : base()
        {
            Route = string.Empty;
            Icon = string.Empty;
        }
    }
}
