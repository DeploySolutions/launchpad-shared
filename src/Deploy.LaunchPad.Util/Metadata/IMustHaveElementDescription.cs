using Deploy.LaunchPad.Util.Elements;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Metadata
{
    
    public partial interface IMustHaveElementDescription
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
