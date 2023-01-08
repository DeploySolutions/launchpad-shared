using Deploy.LaunchPad.Core.Application.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetDetailInputDtoBase<TIdType> : GetInputDtoBase<TIdType>,
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
        public GetDetailInputDtoBase() : base()
        {

        }

        #endregion
    }
}
