// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DeleteOutputDtoBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Services;

namespace Deploy.LaunchPad.Core.Services.Dto
{
    /// <summary>
    /// Class DeleteOutputDtoBase.
    /// Implements the <see cref="ICanBeAppServiceMethodOutput" />
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    /// <seealso cref="ICanBeAppServiceMethodOutput" />
    public abstract partial class DeleteOutputDtoBase<TIdType> : ICanBeAppServiceMethodOutput
    {
        /// <summary>
        /// Determines if the delete operation succeeded. It is up to the implementer to determine what "success" means.
        /// Defaults to false.
        /// </summary>
        /// <value><c>true</c> if succeeded; otherwise, <c>false</c>.</value>
        public bool Succeeded { get; set; } = false;

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected DeleteOutputDtoBase() : base()
        {

        }

        #endregion
    }
}
