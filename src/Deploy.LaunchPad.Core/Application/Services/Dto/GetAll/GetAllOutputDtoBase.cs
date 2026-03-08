// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAllOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Deploy.LaunchPad.Core.Application.Services.Dto.Get;
using Deploy.LaunchPad.Core.Application.Services.Dto.NameDescription;
using Deploy.LaunchPad.Core.Metadata;

namespace Deploy.LaunchPad.Core.Application.Services.Dto.GetAll
{
    /// <summary>
    /// Class GetAllOutputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Services.Dto.GetOutputDtoBase{TPrimaryKey}" />
    /// Implements the <see cref="IMayHaveTenant" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Code.Services.Dto.GetOutputDtoBase{TPrimaryKey}" />
    /// <seealso cref="IMayHaveTenant" />
    public abstract partial class GetAllOutputDtoBase<TPrimaryKey> : GetOutputDtoBase<TPrimaryKey>, IMayHaveTenant
    {
        /// <summary>
        /// The name of this item.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementNameDto Name { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescriptionDto Description { get; set; }

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
        public virtual System.Guid? TenantId { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAllOutputDtoBase() : base()
        {
            Name = new ElementNameDto();
            Description = new ElementDescriptionDto();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GetAllOutputDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = new ElementNameDto();
            Description = new ElementDescriptionDto();
        }

        #endregion

    }
}
