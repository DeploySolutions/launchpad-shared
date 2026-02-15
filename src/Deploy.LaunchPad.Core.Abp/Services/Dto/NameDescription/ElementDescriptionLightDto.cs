using Abp.AutoMapper;
using Deploy.LaunchPad.Core.Elements;
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
    public partial class ElementDescriptionLightDto : IElementDescriptionLightDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Full { get; set; } = string.Empty;
    }
}
