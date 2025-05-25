// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DeleteInputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************



using Deploy.LaunchPad.Core.Services;

namespace Deploy.LaunchPad.Core.Services.Dto
{
    /// <summary>
    /// Class DeleteInputDtoBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Services.Dto.EntityDtoBase{TIdType}" />
    /// Implements the <see cref="ICanBeAppServiceMethodInput" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Services.Dto.EntityDtoBase{TIdType}" />
    /// <seealso cref="ICanBeAppServiceMethodInput" />
    public abstract partial class DeleteInputDtoBase<TIdType> : EntityDtoBase<TIdType>, ICanBeAppServiceMethodInput
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected DeleteInputDtoBase() : base()
        {

        }

        #endregion
    }
}
