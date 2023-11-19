// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAllDetailOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    /// <summary>
    /// Class GetAllDetailOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetAllOutputDtoBase{TIdType}" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetAllOutputDtoBase{TIdType}" />
    public abstract partial class GetAllDetailOutputDtoBase<TIdType> : GetAllOutputDtoBase<TIdType>
    {

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The user name that created the entity
        /// </summary>
        /// <value>The name of the creator user.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String CreatorUserName { get; set; }

        /// <summary>
        /// The id of the User Agent which created this entity
        /// </summary>
        /// <value>The creator user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LastModifierUserId))]
        public virtual Int64? LastModifierUserId { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The user name that last modified the entity
        /// </summary>
        /// <value>The last name of the modifier user.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String LastModifierUserName { get; set; }


        /// <summary>
        /// The sequence number for this entity, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Int32 SeqNum { get; set; } = 0;


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAllDetailOutputDtoBase() : base()
        {
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllDetailOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

    }
}
