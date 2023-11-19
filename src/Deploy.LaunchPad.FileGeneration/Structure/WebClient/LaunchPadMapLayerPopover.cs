// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadMapLayerPopover.cs" company="Deploy Software Solutions, inc.">
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
    /// Define the data that will show up on the popupover box.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "MapLayerPopover")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapPopover : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// The data field representing the popover data
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Data type of the popover field to help frontend know how to format the field
        /// </summary>
        /// <value>The type of the data.</value>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMapPopover"/> class.
        /// </summary>
        public LaunchPadMapPopover() : base()
        {
        }
    }
}
