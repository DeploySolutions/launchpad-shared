using Abp.AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto.NameDescription
{
    [Serializable]
    [AutoMap(typeof(ElementName))]
    public partial class ElementNameDto: ElementNameLightDto, IElementNameLightDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Prefix { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Suffix { get; set; } = string.Empty;

    }
}
