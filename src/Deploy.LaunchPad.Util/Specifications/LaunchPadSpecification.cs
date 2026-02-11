// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadSpecification.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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

using Deploy.LaunchPad.Util;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Deploy.LaunchPad.Util.Specifications
{
    /// <summary>
    /// Provides a default implementation of the <see cref="ILaunchPadSpecification{TEntity}" /> interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>The <see cref="LaunchPadSpecification{TEntity}" /> implements Composite Specification pattern by overloading
    /// the &amp; and | (And, Or in VB.Net) operators to allow composing multiple specifications together.</remarks>
    public partial class LaunchPadSpecification<T> : ILaunchPadSpecification<T>
    {
        /// <summary>
        /// The predicate
        /// </summary>
        private readonly Expression<Func<T, bool>> _predicate;
        /// <summary>
        /// The predicate compiled
        /// </summary>
        private readonly Func<T, bool> _predicateCompiled;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="LaunchPadSpecification{TEntity}" /> instance with the
        /// provided predicate expression.
        /// </summary>
        /// <param name="predicate">A predicate that can be used to check entities that
        /// satisfy the specification.</param>
        public LaunchPadSpecification(Expression<Func<T, bool>> predicate)
        {
            Guard.Against<ArgumentNullException>(predicate == null, Deploy_LaunchPad_Util_Resources.Guard_Specification_Specification);
            _predicate = predicate;
            _predicateCompiled = predicate.Compile();
        }

        /// <summary>
        /// Gets the expression that encapsulates the criteria of the specification.
        /// </summary>
        /// <value>The predicate.</value>
        public virtual Expression<Func<T, bool>> Predicate
        {
            get { return _predicate; }
        }

        /// <summary>
        /// Evaluates the specification against an entity of <typeparamref name="T" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="T" /> instance to evaluate the specificaton
        /// against.</param>
        /// <returns>Should return true if the specification was satisfied by the entity, else false.</returns>
        public virtual bool IsSatisfiedBy(T entity)
        {
            return _predicateCompiled.Invoke(entity);
        }

        /// <summary>
        /// Overloads the &amp; operator and combines two <see cref="LaunchPadSpecification{TEntity}" /> in a Boolean And expression
        /// and returns a new see cref="Specification{TEntity}"/&gt;.
        /// </summary>
        /// <param name="leftHand">The left hand <see cref="LaunchPadSpecification{TEntity}" /> to combine.</param>
        /// <param name="rightHand">The right hand <see cref="LaunchPadSpecification{TEntity}" /> to combine.</param>
        /// <returns>The combined <see cref="LaunchPadSpecification{TEntity}" /> instance.</returns>
        public static LaunchPadSpecification<T> operator &(LaunchPadSpecification<T> leftHand, LaunchPadSpecification<T> rightHand)
        {
            InvocationExpression rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                                 leftHand.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftHand.Predicate.Body,
                                                                   rightInvoke);
            return new LaunchPadSpecification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

        /// <summary>
        /// Overloads the &amp; operator and combines two <see cref="LaunchPadSpecification{TEntity}" /> in a Boolean Or expression
        /// and returns a new see cref="Specification{TEntity}"/&gt;.
        /// </summary>
        /// <param name="leftHand">The left hand <see cref="LaunchPadSpecification{TEntity}" /> to combine.</param>
        /// <param name="rightHand">The right hand <see cref="LaunchPadSpecification{TEntity}" /> to combine.</param>
        /// <returns>The combined <see cref="LaunchPadSpecification{TEntity}" /> instance.</returns>
        public static LaunchPadSpecification<T> operator |(LaunchPadSpecification<T> leftHand, LaunchPadSpecification<T> rightHand)
        {
            InvocationExpression rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                                 leftHand.Predicate.Parameters.Cast<Expression>());
            BinaryExpression newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftHand.Predicate.Body,
                                                                   rightInvoke);
            return new LaunchPadSpecification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }
    }
}