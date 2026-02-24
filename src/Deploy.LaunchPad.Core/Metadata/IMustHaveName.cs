using Deploy.LaunchPad.Util.Elements;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMustHaveName
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
