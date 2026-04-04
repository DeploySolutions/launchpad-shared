// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetAdminInputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Code.Services;
using Deploy.LaunchPad.Core.Application.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Application.Services.Dto.Get
{
    /// <summary>
    /// Class GetAdminInputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Services.Dto.GetFullInputDtoBase{TPrimaryKey}" />
    /// Implements the <see cref="ICanBeAppServiceMethodInput" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Code.Services.Dto.GetFullInputDtoBase{TPrimaryKey}" />
    /// <seealso cref="ICanBeAppServiceMethodInput" />
    public abstract partial class GetAdminInputDtoBase<TPrimaryKey> : GetFullInputDtoBase<TPrimaryKey>,
        ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// The id of this object
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public override TPrimaryKey Id { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAdminInputDtoBase() : base()
        {

        }

        #endregion
    }
}
