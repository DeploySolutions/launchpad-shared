using Deploy.LaunchPad.Util.Elements;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Metadata
{
    
    public partial interface IMustHaveElementName
    {
        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementName Name { get; set; }

    }
}
