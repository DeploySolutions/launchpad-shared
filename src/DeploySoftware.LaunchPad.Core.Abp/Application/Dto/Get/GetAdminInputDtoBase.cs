
using DeploySoftware.LaunchPad.Core.Application.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetAdminInputDtoBase<TIdType> : GetFullInputDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// The id of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public override TIdType Id { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAdminInputDtoBase() : base()
        {

        }

        #endregion
    }
}
