﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadSpecification.cs" company="Deploy Software Solutions, inc.">
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

using System;
using System.Linq.Expressions;

namespace Deploy.LaunchPad.Core.Specifications
{
    /// <summary>
    /// The <see cref="ILaunchPadSpecification{TEntity}" /> interface defines a basic contract to express specifications declaratively.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface ILaunchPadSpecification<T>
    {
        /// <summary>
        /// Gets the expression that encapsulates the criteria of the specification.
        /// </summary>
        /// <value>The predicate.</value>
        Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Evaluates the specification against an entity of <typeparamref name="T" />.
        /// </summary>
        /// <param name="entity">The <typeparamref name="T" /> instance to evaluate the specificaton
        /// against.</param>
        /// <returns>Should return true if the specification was satisfied by the entity, else false.</returns>
        bool IsSatisfiedBy(T entity);
    }
}
