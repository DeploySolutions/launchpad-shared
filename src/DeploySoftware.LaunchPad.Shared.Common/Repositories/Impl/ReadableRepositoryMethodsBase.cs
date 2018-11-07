//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Shared.Repositories
{
    using DeploySoftware.LaunchPad.Shared.Domain;
    using DeploySoftware.LaunchPad.Shared.Specifications;
    using DeploySoftware.LaunchPad.Shared.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    /// <summary>
    /// Base type containing a collection of methods to read or query data from a repository.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public abstract partial class ReadableRepositoryMethodsBase<TLaunchPadObject> : RepositoryMethodsBase<TLaunchPadObject>, IReadableRepositoryMethods<TLaunchPadObject>, IEnumerable
        where TLaunchPadObject : ILaunchPadObject, new()
    {
        /// <summary>
        /// Raised when the repository is being queried for data.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> QueryingDataObject;

        /// <summary>
        /// Raised when the repository has been queried for data.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> QueriedDataObject;

        /// <summary>  
        /// Raises the <see cref="QueryingDataObject">QueryingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnQueryingDataObject(RepositoryEventArgs e)
        {
            QueryingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="QueriedDataObject">QueriedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnQueriedDataObject(RepositoryEventArgs e)
        {
            QueriedDataObject?.Invoke(this, e);
        }

        #region IReadableRepositoryMethods<TLaunchPadObject> Members

        /// <summary>
        /// Gets the <see cref="IQueryable{TLaunchPadObject}"/> used by the <see cref="RepositoryBase{TLaunchPadObject}"/> 
        /// to execute Linq queries.
        /// </summary>
        /// <value>A <see cref="IQueryable{TLaunchPadObject}"/> instance.</value>
        /// <remarks>
        /// Inheritors of this base class should return a valid non-null <see cref="IQueryable{TLaunchPadObject}"/> instance.
        /// </remarks>
        protected abstract IQueryable<TLaunchPadObject> RepositoryQuery { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{TLaunchPadObject}" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<TLaunchPadObject> GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
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
            return RepositoryQuery.GetEnumerator();
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="IQueryable" />.
        /// </summary>
        /// <returns>
        /// The <see cref="Expression" /> that is associated with this instance of <see cref="IQueryable" />.
        /// </returns>
        public virtual Expression Expression
        {
            get { return RepositoryQuery.Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="IQueryable" /> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public virtual Type ElementType
        {
            get { return RepositoryQuery.ElementType; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryProvider" /> that is associated with this data source.
        /// </returns>
        public virtual IQueryProvider Provider
        {
            get { return RepositoryQuery.Provider; }
        }

        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        public virtual T UnitOfWork<T>() where T : IUnitOfWork
        {
            var currentScope = UnitOfWorkManager.CurrentUnitOfWork;
            Guard.Against<InvalidOperationException>(currentScope == null,
                                                     DeploySoftware_LaunchPad_Shared_Resources.Guard_RepositoryBase_UnitOfWork_NoCompatibleTypeFound);

            Guard.TypeOf<T>(currentScope, DeploySoftware_LaunchPad_Shared_Resources.Guard_RepositoryBase_UnitOfWork_NotCompatibleType);
            return ((T)currentScope);
        }

        /// <summary>
        /// Returns a count of all objects in the repository
        /// </summary>
        /// <returns>A count of all objects in the repository</returns>
        public abstract long GetCount();

        /// <summary>
        /// Returns a count of all objects in the repository, for the provided query
        /// </summary>
        /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
        public abstract long GetCount(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns a count of all objects in the repository, that are satisfied by the provided specification
        /// </summary>
        /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
        public abstract long GetCount(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns a count of all objects in the repository. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository</returns>
        public abstract Task<long> GetCountAsync();

        /// <summary>
        /// Returns a count of all objects in the repository, for the provided query. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
        public abstract Task<long> GetCountAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns a count of all objects in the repository, that are satisfied by the provided specification. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
        public abstract Task<long> GetCountAsync(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns a <see cref="TLaunchPadObject"/> using a provided key
        /// </summary>
        /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
        /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
        /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
        public abstract TLaunchPadObject GetByKey<TUniqueId>(IKey<TUniqueId> key);

        /// <summary>
        /// Returns a <see cref="TLaunchPadObject"/> using a provided key.
        /// Runs asynchronously.
        /// </summary>
        /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
        /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
        /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
        public abstract Task<TLaunchPadObject> GetByKeyAsync<TUniqueId>(IKey<TUniqueId> key);

        /// <summary>
        /// Return all entities as a List.
        /// </summary>
        /// <returns>List of all entities</returns>
        public abstract List<TLaunchPadObject> ListAll();

        /// <summary>
        /// Return all entities as a List, asynchronously.
        /// </summary>
        /// <returns>List of all entities</returns>
        public abstract Task<List<TLaunchPadObject>> ListAllAsync();

        /// <summary>
        /// Returns exactly ONE result of a query that should return only one result, or an error if more are found
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Exactly ONE <see cref="TLaunchPadObject"/> that should exist, or throws an exception if more than one result is found.</returns>
        public abstract TLaunchPadObject Single(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns exactly ONE result of a specification that should return only one result, or an error if more are found
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
        /// <returns>Exactly ONE <see cref="TLaunchPadObject"/> that should exist, or throws an exception if more than one result is found.</returns>
        public abstract TLaunchPadObject Single(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
        public abstract Task<TLaunchPadObject> SingleAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
        /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
        public abstract Task<TLaunchPadObject> SingleAsync(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns exactly ONE result of a query that should return only one result, or an error if more are found.
        /// If nothing is found, return a default value.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Returns a <see cref="TLaunchPadObject"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
        public abstract TLaunchPadObject SingleOrDefault(IQueryable<TLaunchPadObject> query);

        /// <summary>
        ///  Returns the only <see cref="TLaunchPadObject"/> from a query, or a default value if the sequence is empty; this method throws an exception if there is more than one <see cref="TLaunchPadObject"/> returned by the query.
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Returns a <see cref="Task{TLaunchPadObject}"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
        public abstract Task<TLaunchPadObject> SingleOrDefaultAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, using the given query
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="TLaunchPadObject"/> found.</returns>
        public abstract TLaunchPadObject First(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, using the given query
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found.</returns>
        public abstract Task<TLaunchPadObject> FirstAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, or a default value if nothing is found, using the given query
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="TLaunchPadObject"/> found, or a default value if nothing is found.</returns>
        public abstract TLaunchPadObject FirstOrDefault(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, or a default value if nothing is found, using the given query
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found, or a default value if nothing is found.</returns>
        public abstract Task<TLaunchPadObject> FirstOrDefaultAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Queries the repository returns a list of results, up to a maximum number. If max results is -1, 
        /// an unlimited number of results can be returned.
        /// </summary>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public abstract IQueryable<TLaunchPadObject> Query(Int32 maxResults);

        /// <summary>
        /// Queries the repository returns a list of results, starting at a specified index, 
        /// up to a maximum number. If max results is -1, an unlimited number of results can be returned.
        /// </summary>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <param name="startResult">The index of the first result to start from, from the list of those returned.</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public abstract IQueryable<TLaunchPadObject> Query(Int32 maxResults, Int32 startResult);

        /// <summary>
        /// Queries the repository based on the provided LINQ query and returns all results that
        /// are satisfied by the specification, up to a maximum number. 
        /// If max results is -1, an unlimited number of results can be returned.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results
        /// to only those that satisfy the query.</param>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public abstract IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults);

        /// <summary>
        /// Queries the repository based on the provided LINQ query and returns all results that
        /// are satisfied by the specification, from a starting index, up to a maximum number. 
        /// If max results is -1, an unlimited number of results can be returned.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results
        /// to only those that satisfy the specification.</param>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <param name="startResult">The index of the first result to start from, from the list of those returned.</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public abstract IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults, Int32 startResult);

        /// <summary>
        /// Querries the repository based on the provided specification and returns results that
        /// are only satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instnace used to filter results
        /// that only satisfy the specification.</param>
        /// <returns>A <see cref="IEnumerable{TLaunchPadObject}"/> that can be used to enumerate over the results
        /// of the query.</returns>
        public virtual IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification)
        {
            return RepositoryQuery.Where(specification.Predicate).AsQueryable();
        }

        /// <summary>
        /// Queries the repository based on the provided specification and returns all results that
        /// are satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{T}"/> instance used to filter results
        /// to only those that satisfy the specification.</param>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public virtual IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification, int maxResults)
        {
            //TODO: add maxresults code
            return RepositoryQuery.Where(specification.Predicate).AsQueryable();
        }

        /// <summary>
        /// Queries the repository based on the provided specification and returns all results that
        /// are satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{T}"/> instance used to filter results
        /// to only those that satisfy the specification.</param>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <param name="startResult">The index of the first result to start from, from the list of those returned.</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        public virtual IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification, int maxResults,
                                  int startResult)
        {
            //TODO: add maxresults and startresult code
            return RepositoryQuery.Where(specification.Predicate).AsQueryable();            
        }        

        #endregion
    }
}
