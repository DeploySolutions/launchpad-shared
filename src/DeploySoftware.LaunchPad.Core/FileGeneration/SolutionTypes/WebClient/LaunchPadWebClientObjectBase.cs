using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The base class containing properties for all LaunchPad web client file generation processes.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        [XmlAttribute("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// The singular name of the object. Mostly used to display labels on UI element.
        /// </summary>
        [XmlAttribute("name")]
        public virtual string Name { get; set; }

        public LaunchPadWebClientObjectBase() : base()
        {
            Id = string.Empty;
            Name = string.Empty;
        }

    }
}
