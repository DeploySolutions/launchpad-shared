using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a group in a page/form. For web client, this will draw a gray border around the group.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadGroup : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Rows inside this group
        /// </summary>
        [JsonProperty("rows")]
        public IList<LaunchPadRow> Rows { get; set; }

        public LaunchPadGroup() : base()
        {
            Id = null;
        }
    }
}
