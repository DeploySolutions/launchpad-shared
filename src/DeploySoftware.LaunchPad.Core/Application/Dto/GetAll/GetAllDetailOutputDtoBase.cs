
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAllDetailOutputDtoBase<TIdType> : GetAllOutputDtoBase<TIdType>
    {

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The user name that created the entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String CreatorUserName { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The user name that last modified the entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String LastModifierUserName { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllDetailOutputDtoBase() : base()
        {
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllDetailOutputDtoBase(SerializationInfo info, StreamingContext context) :base(info,context)
        {

        }

        #endregion

    }
}
