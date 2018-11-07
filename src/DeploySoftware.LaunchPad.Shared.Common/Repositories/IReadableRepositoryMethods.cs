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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    /// <summary>
    /// Defines the functionality for any repository whose data can be read or queried.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public interface IReadableRepositoryMethods<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {

        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        T UnitOfWork<T>() where T : IUnitOfWork;
        
            /// <summary>
        /// Raised when the repository is being queried for data.
        /// </summary>
        event EventHandler<RepositoryEventArgs> QueryingDataObject;

        /// <summary>
        /// Raised when the repository has been queried for data.
        /// </summary>
        event EventHandler<RepositoryEventArgs> QueriedDataObject;
        
        /// <summary>
        /// Returns a count of all objects in the repository
        /// </summary>
        /// <returns>A count of all objects in the repository</returns>
        long GetCount();

        /// <summary>
        /// Returns a count of all objects in the repository, for the provided query
        /// </summary>
        /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
        long GetCount(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns a count of all objects in the repository, that are satisfied by the provided specification
        /// </summary>
        /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
        long GetCount(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns a count of all objects in the repository. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository</returns>
        Task<long> GetCountAsync();

        /// <summary>
        /// Returns a count of all objects in the repository, for the provided query. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
        Task<long> GetCountAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns a count of all objects in the repository, that are satisfied by the provided specification. Runs asynchronously.
        /// </summary>
        /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
        Task<long> GetCountAsync(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns a <see cref="TLaunchPadObject"/> using a provided key
        /// </summary>
        /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
        /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
        /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
        TLaunchPadObject GetByKey<TUniqueId>(IKey<TUniqueId> key);

        /// <summary>
        /// Returns a <see cref="TLaunchPadObject"/> using a provided key.
        /// Runs asynchronously.
        /// </summary>
        /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
        /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
        /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
        Task<TLaunchPadObject> GetByKeyAsync<TUniqueId>(IKey<TUniqueId> key);

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        List<TLaunchPadObject> ListAll();

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        Task<List<TLaunchPadObject>> ListAllAsync();

        /// <summary>
        /// Returns exactly ONE result, or an error, using the given query
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Exactly ONE <see cref="TLaunchPadObject"/>, or throws an exception.</returns>
        TLaunchPadObject Single(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns exactly ONE result of a specification that should return only one result, or an error if more are found
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
        /// <returns>Exactly ONE <see cref="TLaunchPadObject"/> that should exist, or throws an exception if more than one result is found.</returns>
        TLaunchPadObject Single(ISpecification<TLaunchPadObject> specification);

        /// <summary>
        /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
        Task<TLaunchPadObject> SingleAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
        /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
        Task<TLaunchPadObject> SingleAsync(ISpecification<TLaunchPadObject> specification);


        /// <summary>
        ///  Returns the only <see cref="TLaunchPadObject"/> from a query, or a default value if the sequence is empty; this method throws an exception if there is more than one <see cref="TLaunchPadObject"/> returned by the query.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Returns a <see cref="TLaunchPadObject"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
        TLaunchPadObject SingleOrDefault(IQueryable<TLaunchPadObject> query);

        /// <summary>
        ///  Returns the only <see cref="TLaunchPadObject"/> from a query, or a default value if the sequence is empty; this method throws an exception if there is more than one <see cref="TLaunchPadObject"/> returned by the query.
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>Returns a <see cref="Task{TLaunchPadObject}"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
        Task<TLaunchPadObject> SingleOrDefaultAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, using the given query
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="TLaunchPadObject"/> found.</returns>
        TLaunchPadObject First(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, using the given query
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found.</returns>
        Task<TLaunchPadObject> FirstAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, or a default value if nothing is found, using the given query
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="TLaunchPadObject"/> found, or a default value if nothing is found.</returns>
        TLaunchPadObject FirstOrDefault(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Returns the first result, or a default value if nothing is found, using the given query
        /// Runs asynchronously.
        /// </summary>
        /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
        /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found, or a default value if nothing is found.</returns>
        Task<TLaunchPadObject> FirstOrDefaultAsync(IQueryable<TLaunchPadObject> query);

        /// <summary>
        /// Queries the repository returns a list of results, up to a maximum number. If max results is -1, 
        /// an unlimited number of results can be returned.
        /// </summary>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        IQueryable<TLaunchPadObject> Query(Int32 maxResults);

        /// <summary>
        /// Queries the repository returns a list of results, starting at a specified index, 
        /// up to a maximum number. If max results is -1, an unlimited number of results can be returned.
        /// </summary>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <param name="startResult">The index of the first result to start from, from the list of those returned.</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        IQueryable<TLaunchPadObject> Query(Int32 maxResults, Int32 startResult);

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
        IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults);

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
        IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults, Int32 startResult);

        /// <summary>
        /// Queries the repository based on the provided specification and returns all results that
        /// are satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{T}"/> instance used to filter results
        /// to only those that satisfy the specification.</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>0
        IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification);


        /// <summary>
        /// Queries the repository based on the provided specification and returns all results that
        /// are satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{T}"/> instance used to filter results
        /// to only those that satisfy the specification.</param>
        /// <param name="maxResults">The maximum number of results to return from this query</param>
        /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
        /// of the query.</returns>
        IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification, int maxResults);

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
        IQueryable<TLaunchPadObject> Query(Specifications.ISpecification<TLaunchPadObject> specification, int maxResults,
                                  int startResult);

    }
}
