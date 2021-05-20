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
        /// Form items (input fields or buttons) included in this form
        /// </summary>
        [JsonProperty("formItem")]
        public IList<LaunchPadFormItem> FormItems { get; set; }

        public LaunchPadRow() : base()
        {
            Id = null;
            Name = null;
        }
    }
}
