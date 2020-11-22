
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    public abstract partial class GetAllOutputDtoBase<TIdType> : GetOutputDtoBase<TIdType>
    {
        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String DescriptionShort { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllOutputDtoBase() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }

        #endregion

    }
}
