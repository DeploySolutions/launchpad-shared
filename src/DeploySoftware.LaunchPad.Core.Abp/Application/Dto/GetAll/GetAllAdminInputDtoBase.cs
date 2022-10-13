
using DeploySoftware.LaunchPad.Core.Domain;
using DeploySoftware.LaunchPad.Core.Abp.Domain.SoftwareApplications;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetAllAdminInputDtoBase<TIdType> : GetAllDetailInputDtoBase<TIdType>
    {
        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual TIdType TranslatedFromId { get; set; }

        /// <summary>
        /// The date and time that this object was deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// The id of the user which deleted this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeleterUserId))]
        public virtual long? DeleterUserId { get; set; }


        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsDeleted { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAllAdminInputDtoBase() : base()
        {
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// </summary>
        /// <param name="id"></param>
        public GetAllAdminInputDtoBase(int tenantId) : base()
        {
            TenantId = tenantId;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        public GetAllAdminInputDtoBase(int tenantId, String culture) : base()
        {
            TenantId = tenantId;
            Culture = culture;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllAdminInputDtoBase(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            
        }

        #endregion

    }
}
