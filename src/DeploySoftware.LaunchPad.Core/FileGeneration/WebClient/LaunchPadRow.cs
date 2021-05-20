using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a row in a form or page.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadRow : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Input fields included in this form
        /// </summary>
        [JsonProperty("inputFields")]
        public IList<LaunchPadInputField> InputFields { get; set; }

        /// <summary>
        /// Buttons included in this form
        /// </summary>
        [JsonProperty("buttons")]
        public IList<LaunchPadButton> Buttons { get; set; }

        public LaunchPadRow() : base()
        {
            InputFields = new List<LaunchPadInputField>();
            Buttons = new List<LaunchPadButton>();
        }
    }
}
