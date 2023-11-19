// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 05-17-2023
// ***********************************************************************
// <copyright file="LaunchPadUtilityMenu.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the utility menu of the application
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "UtilityMenu")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadUtilityMenu : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Configuration of where the menu should be: "top" or "side".
        /// </summary>
        /// <value>The location.</value>
        [JsonProperty("location")]
        public string Location { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadUtilityMenu"/> class.
        /// </summary>
        public LaunchPadUtilityMenu() : base()
        {
            Location = string.Empty;
        }
    }
}
