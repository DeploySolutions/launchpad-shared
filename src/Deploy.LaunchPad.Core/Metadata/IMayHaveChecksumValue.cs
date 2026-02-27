using Deploy.LaunchPad.Util.Elements;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveChecksumValue
    {
        /// <summary>
        /// Checksum value for this entity. Implementers decide
        /// </summary>
        [MaxLength(40, ErrorMessageResourceName = "Validation_Checksum_40CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public string? Checksum { get; set; }

    }
}
