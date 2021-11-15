using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a group row in a form or page.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadGroupRow : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Groups included in this group row. Each group represent a group column.
        /// </summary>
        [JsonProperty("groups")]
        public IList<LaunchPadGroup> Groups { get; set; }

        public LaunchPadGroupRow() : base()
        {
            Id = null;
            Name = null;
        }
    }
}
