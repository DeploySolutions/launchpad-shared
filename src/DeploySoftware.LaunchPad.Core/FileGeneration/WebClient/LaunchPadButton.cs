using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a UI button
    /// </summary>  
    [Serializable]
    public partial class LaunchPadButton : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Type of this button. Types can be "reset" or "submit".
        /// </summary>
        public string Type { get; set; }

        public LaunchPadButton() : base()
        {
        }
    }
}
