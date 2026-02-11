using Abp.AutoMapper;
using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.ValueConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Code.Services.Dto.NameDescription
{
    [Serializable]
    [AutoMap(typeof(ElementName))]
    public partial class ElementNameDto: ElementNameLightDto, IElementNameDto
    {
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Prefix { get; set; } = string.Empty;

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Suffix { get; set; } = string.Empty;

        protected string _short = string.Empty;        
        /// <summary>
        /// The short name of this element (if different from the FullName field). If not set, it will default to the first 50 characters of the full name.
        /// </summary>
        /// <value>The fully qualified name of the element.</value>
        [MaxLength(50, ErrorMessageResourceName = "Validation_Name_Short_50CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty("short", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [JsonConverter(typeof(JsonEmptyStringToNullConverter))]
        public virtual string Short
        {
            get
            {
                if (string.IsNullOrEmpty(_short))
                {
                    return Full;
                }
                else
                {
                    return _short;
                }
            }
            set
            {
                _short = value;
            }
        }

    }
}
