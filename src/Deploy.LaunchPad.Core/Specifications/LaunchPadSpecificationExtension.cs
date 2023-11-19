﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadSpecificationExtension.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Deploy.LaunchPad.Core.Specifications
{
    /// <summary>
    /// Extension methods for <see cref="ILaunchPadSpecification{T}" />.
    /// </summary>
    public static class LaunchPadSpecificationExtension
    {
        /// <summary>
        /// Retuns a new specification adding this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns>ILaunchPadSpecification&lt;T&gt;.</returns>
        public static ILaunchPadSpecification<T> And<T>(this ILaunchPadSpecification<T> rightHand, ILaunchPadSpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                leftHand.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftHand.Predicate.Body,
                                                      rightInvoke);
            return new LaunchPadSpecification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

        /// <summary>
        /// Retuns a new specification or'ing this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns>ILaunchPadSpecification&lt;T&gt;.</returns>
        public static ILaunchPadSpecification<T> Or<T>(this ILaunchPadSpecification<T> rightHand, ILaunchPadSpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                leftHand.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftHand.Predicate.Body,
                                                      rightInvoke);
            return new LaunchPadSpecification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

    }
}
