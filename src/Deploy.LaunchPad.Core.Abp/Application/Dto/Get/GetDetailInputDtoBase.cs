// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="GetDetailInputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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
    /// Class GetDetailInputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetInputDtoBase{TIdType}" />
    /// Implements the <see cref="ICanBeAppServiceMethodInput" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Application.Dto.GetInputDtoBase{TIdType}" />
    /// <seealso cref="ICanBeAppServiceMethodInput" />
    public abstract partial class GetDetailInputDtoBase<TIdType> : GetInputDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        /// <summary>
        /// The id of this object
        /// </summary>
        /// <value>The identifier.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public override TIdType Id { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetDetailInputDtoBase() : base()
        {

        }

        #endregion
    }
}
