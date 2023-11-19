// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="CreateInputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

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
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public abstract partial class CreateInputDtoBase<TIdType> : EntityDtoBase<TIdType>, ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [XmlAttribute]
        public virtual string Name { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description full.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionFull { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInputDtoBase{TIdType}"/> class.
        /// </summary>
        public CreateInputDtoBase() : base()
        {

        }
    }
}
