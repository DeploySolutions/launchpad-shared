//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 
//This file is a derivative work from the original created in NCommon and copyright 2010 by Ritesh Rao 

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

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    using DeploySoftware.LaunchPad.Common.Domain.Entities;
    using DeploySoftware.LaunchPad.Common.Specifications;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Fetching strategy wrapper for a IRepository implementation.
    /// </summary>
    /// <typeparam name="TRepository">The type of repository to wrap.</typeparam>
    /// <typeparam name="TEntity">The entity type of the repository.</typeparam>
    public abstract class RepositoryWrapperBase<TRepository, TLaunchPadObject> : IReadableRepository<TLaunchPadObject>, IEnumerable where 
        TRepository : IReadableRepository<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {
        readonly TRepository _rootRootRepository;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="RepositoryWrapperBase{TRepository,TEntity}"/> class.
        /// </summary>
        /// <param name="rootRootRepository">The <see cref="IRepository{TEntity}"/> instance to wrap.</param>
        protected RepositoryWrapperBase(TRepository rootRootRepository)
        {
            _rootRootRepository = rootRootRepository;
        }

        ///<summary>
        /// Gets the <see cref="IRepository{TEntity}"/> instnace that this RepositoryWrapperBase wraps.
        ///</summary>
        /// <value>The wrapped <see cref="IRepository{TEntity}"/> instance</value>
        public virtual TRepository RootRepository
        {
            get { return _rootRootRepository; }
        }

        /// <summary>
        /// A <see cref="ReadableRepositoryMethodsBase{TLaunchPadObject}">ReadableRepositoryMethodsBase{TLaunchPadObject}</see> containing of
        /// methods used to read or query a repository's collection of data
        /// </summary>
        public ReadableRepositoryMethodsBase<TLaunchPadObject> Read
        { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public virtual IEnumerator<TLaunchPadObject> GetEnumerator()
        {
            return _rootRootRepository.Read.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rootRootRepository.Read.GetEnumerator();
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public virtual Expression Expression
        {
            get { return _rootRootRepository.Read.Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public virtual Type ElementType
        {
            get { return _rootRootRepository.Read.ElementType; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public virtual IQueryProvider Provider
        {
            get { return _rootRootRepository.Read.Provider; }
        }

        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        public virtual T UnitOfWork<T>() where T : IUnitOfWork
        {
            return _rootRootRepository.Read.UnitOfWork<T>();
        }       

        /// <summary>
        /// Querries the repository based on the provided specification and returns results that
        /// are only satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instnace used to filter results
        /// that only satisfy the specification.</param>
        /// <returns>A <see cref="IEnumerable{TLaunchPadObject}"/> that can be used to enumerate over the results
        /// of the query.</returns>
        public virtual IEnumerable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification)
        {
            return _rootRootRepository.Read.Query(specification);
        }

        /// <summary>
        /// Defines the service context under which the repository will execute.
        /// </summary>
        /// <typeparam name="TService">The service type that defines the context of the repository.</typeparam>
        /// <returns>The same <see cref="IRepository{TLaunchPadObject}"/> instance.</returns>
        /// <remarks>
        /// Implementors should perform context specific actions within this method call and return
        /// the exact same instance.
        /// </remarks>
        public IQueryable<TLaunchPadObject> For<TService>() where TService : IReadableRepository<TLaunchPadObject>
        {
            return _rootRootRepository.For<TService>();
        }
    }
}