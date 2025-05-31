using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Services.Dto.NameDescription
{
    public partial interface IElementNameDto: IElementNameLightDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public string Short { get; set; }


        [DataObjectField(false)]
        [XmlAttribute]
        public string Prefix { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string Suffix { get; set; }
    }
}