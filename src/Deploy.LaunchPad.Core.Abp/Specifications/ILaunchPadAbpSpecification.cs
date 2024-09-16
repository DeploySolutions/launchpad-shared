// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadAbpSpecification.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Specifications;
using Deploy.LaunchPad.Core.Specifications;

namespace Deploy.LaunchPad.Core.Abp.Specifications
{
    /// <summary>
    /// Interface ILaunchPadAbpSpecification
    /// Extends the <see cref="ILaunchPadSpecification{T}" />
    /// Extends the <see cref="ISpecification{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ILaunchPadSpecification{T}" />
    /// <seealso cref="ISpecification{T}" />
    public partial interface ILaunchPadAbpSpecification<T> :
        ILaunchPadSpecification<T>,
        ISpecification<T>
    {

    }
}
