// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAllOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Abp.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    /// <summary>
    /// Class GetAllOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    /// Implements the <see cref="IMayHaveTenant" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetOutputDtoBase{TIdType}" />
    /// <seealso cref="IMayHaveTenant" />
    public abstract partial class GetAllOutputDtoBase<TIdType> : GetOutputDtoBase<TIdType>, IMayHaveTenant
    {
        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// Gets or sets the name of the tenant.
        /// </summary>
        /// <value>The name of the tenant.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual int? TenantId { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAllOutputDtoBase() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

    }
}
