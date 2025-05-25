using Abp.AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Services.Dto.NameDescription
{
    [Serializable]
    [AutoMap(typeof(ElementName))]
    public partial class ElementNameLightDto : IElementNameLightDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Short { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Full { get; set; } = string.Empty;
    }
}
