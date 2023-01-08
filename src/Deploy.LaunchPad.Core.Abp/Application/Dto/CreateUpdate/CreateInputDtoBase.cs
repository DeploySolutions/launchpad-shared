
using Deploy.LaunchPad.Core.Application.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{

    /// <summary>
    /// Base class to inherit a DTO object that contains Space Apps RAD basic DTO properties for input to a Create method in an app service
    /// Extends CreateUpdateInputDtoBase and overrides properties to make them required.
    /// </summary>
    public abstract partial class CreateInputDtoBase<TIdType> : EntityDtoBase<TIdType>, ICanBeAppServiceMethodInput
    {
        [DataObjectField(false)]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [XmlAttribute]
        public virtual string Name { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; }

        public CreateInputDtoBase() : base()
        {

        }
    }
}
