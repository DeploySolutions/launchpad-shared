
using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    public abstract partial class GetInputDtoBase<TIdType> : EntityDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        public virtual String Culture { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetInputDtoBase() : base()
        {

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetInputDtoBase(SerializationInfo info, StreamingContext context)
        {
            Culture = info.GetString("Culture");
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Culture", Culture);
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected override String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Culture={0};", Culture);
            // ABP properties

            return sb.ToString();
        }

    }
}
