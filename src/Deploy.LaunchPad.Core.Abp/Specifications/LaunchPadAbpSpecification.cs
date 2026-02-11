// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpSpecification.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util.Specifications;
using System;
using System.Linq.Expressions;

namespace Deploy.LaunchPad.Core.Abp.Specifications
{
    /// <summary>
    /// Class LaunchPadAbpSpecification.
    /// Implements the <see cref="LaunchPadSpecification{T}" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.Specifications.ILaunchPadAbpSpecification{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="LaunchPadSpecification{T}" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.Specifications.ILaunchPadAbpSpecification{T}" />
    public partial class LaunchPadAbpSpecification<T> : LaunchPadSpecification<T>, ILaunchPadAbpSpecification<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpSpecification{T}"/> class.
        /// </summary>
        /// <param name="predicate">A predicate that can be used to check entities that
        /// satisfy the specification.</param>
        public LaunchPadAbpSpecification(Expression<Func<T, bool>> predicate) : base(predicate)
        {
        }

        /// <summary>
        /// Abp method to help with repository filtering
        /// </summary>
        /// <returns>Expression&lt;Func&lt;T, System.Boolean&gt;&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual Expression<Func<T, bool>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
