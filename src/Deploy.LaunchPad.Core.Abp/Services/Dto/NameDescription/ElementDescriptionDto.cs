using Abp.AutoMapper;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Code.Services.Dto.NameDescription
{
    [Serializable]
    [AutoMap(typeof(ElementDescription))]
    public partial class ElementDescriptionDto : ElementDescriptionLightDto, IElementDescriptionLightDto, IElementDescriptionDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Short { get; set; } = string.Empty;

    }
}
