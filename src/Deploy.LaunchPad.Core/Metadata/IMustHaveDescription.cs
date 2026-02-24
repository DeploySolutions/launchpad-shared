using Deploy.LaunchPad.Util.Elements;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMustHaveDescription
    {
        /// <summary>
        /// The description of this object
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementDescription Description { get; set; }

    }
}
