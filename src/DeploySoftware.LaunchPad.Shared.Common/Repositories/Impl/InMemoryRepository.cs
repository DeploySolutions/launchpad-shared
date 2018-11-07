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

namespace DeploySoftware.LaunchPad.Common.Repositories
{
    using DeploySoftware.LaunchPad.Common.Domain.Entities;
    using DeploySoftware.LaunchPad.Common.Specifications;
    using DeploySoftware.LaunchPad.Common.Util;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    /// <summary>
    /// An implementation of <see cref="WriteableRepositoryBase{TLaunchPadObject}"/> that uses an inmemory
    /// collection.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The entity type for which this repository was created.</typeparam>
    /// <remarks>This class can be used in Unit tests to represent a simple in-memory repository, or to learn the repository pattern. 
    /// When you create your own domain repositor(ies), consider creating a subclassed version of InMemoryRepository{Your Entity} as well, 
    /// in order to use it as a fake to speed up your unit tests.</remarks>
    public class InMemoryRepository<TLaunchPadObject> : WriteableRepositoryBase<TLaunchPadObject>, IRefreshableRepository<TLaunchPadObject>, IAttachableRepository<TLaunchPadObject>
        where TLaunchPadObject : IDomainEntity<int>, new()
 
    {
        readonly IList<TLaunchPadObject> _internal;

        public override ReadableRepositoryMethodsBase<TLaunchPadObject> Read {get;set;}

        public override WriteableRepositoryMethodsBase<TLaunchPadObject> Write { get; set; }

        /// <summary>
        /// Default Constructor.
        /// Creats a new instance of the <see cref="InMemoryRepository{TLaunchPadObject}"/> class.
        /// </summary>
        /// <param name="list">An optional list pre-populated with entities.</param>
        public InMemoryRepository(IList<TLaunchPadObject> list)
        {
            _internal = list ?? new List<TLaunchPadObject>() ;

            // Initialize the internal Read and Write methods for this repository
            Read = new InMemoryReadMethods(this);
            Write = new InMemoryWriteMethods(this);
        }

        /// <summary>
        /// Internal class providing InMemory-specific Repository Read methods.
        /// </summary>
        public class InMemoryReadMethods : ReadableRepositoryMethodsBase<TLaunchPadObject>
        {
            private InMemoryRepository<TLaunchPadObject> _outerType;

            /// <summary>  
            /// Initializes a new instance of <see cref="InMemoryReadMethods" />,
            /// with a reference to the external type, <see cref="InMemoryRepository{TLaunchPadObject}" />.      
            /// </summary>
            /// <param name="outerType">A reference to the external class, <see cref="InMemoryRepository{TLaunchPadObject}" /></param>
            public InMemoryReadMethods(InMemoryRepository<TLaunchPadObject> outerType)
            {
                _outerType = outerType;
            }

            /// <summary>
            /// Gets the <see cref="IQueryable{TEntity}"/> used by the <see cref="WriteableRepositoryBase{TEntity}"/> 
            /// to execute Linq queries.
            /// </summary>
            /// <value>A <see cref="IQueryable{TEntity}"/> instance.</value>
            /// <remarks>
            /// Inheritors of this base class should return a valid non-null <see cref="IQueryable{TEntity}"/> instance.
            /// </remarks>
            protected override IQueryable<TLaunchPadObject> RepositoryQuery { get { return _outerType._internal.AsQueryable(); } }

            /// <summary>
            /// Checks the query method input parameters to make sure they are within appropriate
            /// limits, and throws a <see cref="ArgumentOutOfRangeException" /> if not.
            /// </summary>
            /// <param name="maxResults">The maxResults parameter sent to a query method</param>
            /// <param name="startResult">The startResult parameter sent to a query method</param>
            private static void ConstrainQueryInput(Int32 maxResults, Int32 startResult)
            {
                Guard.Against<ArgumentOutOfRangeException>(maxResults < -1, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_LessThanMinus1);
                Guard.Against<ArgumentOutOfRangeException>(maxResults == 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_Equals0);
                Guard.Against<ArgumentOutOfRangeException>(startResult < 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_StartResult_LessThan0);
            }

            /// <summary>
            /// Checks the query method input parameters to make sure they are within appropriate
            /// limits, and throws a <see cref="ArgumentOutOfRangeException" /> or <see cref="ArgumentNullException" /> if not.
            /// </summary>
            /// <param name="query">The query sent to a query method (as <see cref="IQueryable{TLaunchPadObject}" />)</param>
            /// <param name="maxResults">The maxResults parameter sent to a query method</param>
            /// <param name="startResult">The startResult parameter sent to a query method</param>
            private static void ConstrainQueryInput(IQueryable<TLaunchPadObject> query, Int32 maxResults, Int32 startResult)
            {
                Guard.Against<ArgumentNullException>(query == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_Query_IsNull);
                Guard.Against<ArgumentOutOfRangeException>(maxResults < -1, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_LessThanMinus1);
                Guard.Against<ArgumentOutOfRangeException>(maxResults == 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_Equals0);
                Guard.Against<ArgumentOutOfRangeException>(startResult < 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_StartResult_LessThan0);
            }

            /// <summary>
            /// Checks the query method input parameters to make sure they are within appropriate
            /// limits, and throws a <see cref="ArgumentOutOfRangeException" /> or <see cref="ArgumentNullException" /> if not.
            /// </summary>
            /// <param name="query">The query sent to a query method (as <see cref="ISpecification{TLaunchPadObject}" />)</param>
            /// <param name="maxResults">The maxResults parameter sent to a query method</param>
            /// <param name="startResult">The startResult parameter sent to a query method</param>
            private static void ConstrainQueryInput(ISpecification<TLaunchPadObject> query, Int32 maxResults, Int32 startResult)
            {
                Guard.Against<ArgumentNullException>(query == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_Query_IsNull);
                Guard.Against<ArgumentOutOfRangeException>(maxResults < -1, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_LessThanMinus1);
                Guard.Against<ArgumentOutOfRangeException>(maxResults == 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_MaxResult_Equals0);
                Guard.Against<ArgumentOutOfRangeException>(startResult < 0, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_ConstrainQueryInput_StartResult_LessThan0);
            }


            /// <summary>
            /// Returns a count of all objects in the repository
            /// </summary>
            /// <returns>A count of all objects in the repository</returns>
            public override long GetCount()
            {
                return ListAll().LongCount();
            }

            /// <summary>
            /// Returns a count of all objects in the repository, for the provided query
            /// </summary>
            /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
            public override long GetCount(IQueryable<TLaunchPadObject> query)
            {
                return Query(query, -1).LongCount();
            }

            /// <summary>
            /// Returns a count of all objects in the repository, that are satisfied by the provided specification
            /// </summary>
            /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
            public override long GetCount(ISpecification<TLaunchPadObject> specification)
            {
                return Query(specification, -1).LongCount();
            }

            /// <summary>
            /// Returns a count of all objects in the repository. Runs asynchronously.
            /// </summary>
            /// <returns>A count of all objects in the repository</returns>
            public override Task<long> GetCountAsync()
            {
                return Task.FromResult(GetCount());
            }

            /// <summary>
            /// Returns a count of all objects in the repository, for the provided query. Runs asynchronously.
            /// </summary>
            /// <returns>A count of all objects in the repository that are returned from the provided query</returns>
            public override Task<long> GetCountAsync(IQueryable<TLaunchPadObject> query)
            {
                return Task.FromResult(GetCount(query));
            }

            /// <summary>
            /// Returns a count of all objects in the repository, that are satisfied by the provided specification. Runs asynchronously.
            /// </summary>
            /// <returns>A count of all objects in the repository that are satisfied by the provided specification</returns>
            public override Task<long> GetCountAsync(ISpecification<TLaunchPadObject> specification)
            {
                return Task.FromResult(GetCount(specification));
            }

            /// <summary>
            /// Returns a <see cref="TLaunchPadObject"/> using a provided key
            /// </summary>
            /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
            /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
            /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
            public override TLaunchPadObject GetByKey<TUniqueId>(IKey<TUniqueId> key)
            {
                Guard.Against<ArgumentNullException>(key == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_GetByKey_Key_IsNull);
                ISpecification<TLaunchPadObject> specification
                    = new Specification<TLaunchPadObject>
                        (
                            x =>
                            x.GlobalKey.Equals(key)
                        );
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                var queryResult = Query(specification).Single<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                return queryResult;
            }

            /// <summary>
            /// Returns a <see cref="TLaunchPadObject"/> using a provided key.
            /// Runs asynchronously.
            /// </summary>
            /// <typeparam name="TUniqueId">The type of Id property of the key</typeparam>
            /// <param name="key">The <see cref="IKey"/> to use for retrieval</param>
            /// <returns>A <see cref="TLaunchPadObject"/> object, if found</returns>
            public override async Task<TLaunchPadObject> GetByKeyAsync<TUniqueId>(IKey<TUniqueId> key)
            {
                Guard.Against<ArgumentNullException>(key == null, DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_InMemoryRepository_GetByKey_Key_IsNull);
                ISpecification<TLaunchPadObject> specification
                    = new Specification<TLaunchPadObject>
                        (
                            x =>
                            x.GlobalKey.Equals(key)
                        );
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                var queryResult = await SingleAsync( Query(specification).AsQueryable());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                return queryResult;

            }

            public override List<TLaunchPadObject>ListAll()
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = Query(-1, 0).Expression });
                var queryResult = Query(-1, 0);
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = Query(-1, 0).Expression });
                return queryResult.ToList();
            }

            public override Task<List<TLaunchPadObject>> ListAllAsync()
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = Query(-1, 0).Expression });
                var queryResult = Task.FromResult(ListAll());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = Query(-1, 0).Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns exactly ONE result of a query that should return only one result, or an error if more are found.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>Returns a <see cref="TLaunchPadObject"/>; or throws an exception if more than one object is returned.</returns>
            public override TLaunchPadObject Single(IQueryable<TLaunchPadObject> query)
            {
                
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Query(query, -1).Single<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns exactly ONE result of a specification that should return only one result, or an error if more are found
            /// </summary>
            /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
            /// <returns>Exactly ONE <see cref="TLaunchPadObject"/> that should exist, or throws an exception if more than one result is found.</returns>
            public override TLaunchPadObject Single(ISpecification<TLaunchPadObject> specification)
            {

                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                var queryResult = Query(specification, -1).Single<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                return queryResult;
            }

            /// <summary>
            /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
            public override Task<TLaunchPadObject> SingleAsync(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Task.FromResult(Query(query, -1).Single<TLaunchPadObject>());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns exactly ONE result, or an error, using the given query. Runs asynchronously.
            /// </summary>
            /// <param name="specification">A <see cref="ISpecification{TLaunchPadObject}"/> instance used to filter results to only those that satisfy the specification.</param>
            /// <returns>Exactly ONE <see cref="Task{TLaunchPadObject}"/>, or throws an exception.</returns>
            public override Task<TLaunchPadObject> SingleAsync(ISpecification<TLaunchPadObject> specification)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                var queryResult = Task.FromResult(Query(specification, -1).Single<TLaunchPadObject>());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = specification.Predicate });
                return queryResult;
            }

            /// <summary>
            /// Returns exactly ONE result of a query that should return only one result, or an error if more are found.
            /// If nothing is found, return a default value.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>Returns a <see cref="TLaunchPadObject"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
            public override TLaunchPadObject SingleOrDefault(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Query(query, -1).SingleOrDefault<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            ///  Returns the only <see cref="TLaunchPadObject"/> from a query, or a default value if the sequence is empty; this method throws an exception if there is more than one <see cref="TLaunchPadObject"/> returned by the query.
            /// Runs asynchronously.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>Returns a <see cref="Task{TLaunchPadObject}"/>, or returns a default value if the query returns nothing; or throws an exception if more than one object is returned.</returns>
            public override Task<TLaunchPadObject> SingleOrDefaultAsync(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Task.FromResult(Query(query, -1).SingleOrDefault<TLaunchPadObject>());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }


            /// <summary>
            /// Returns the first result, using the given query
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>The first <see cref="TLaunchPadObject"/> found.</returns>
            public override TLaunchPadObject First(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Query(query, -1).First<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns the first result, using the given query
            /// Runs asynchronously.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found.</returns>
            public override Task<TLaunchPadObject> FirstAsync(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Task.FromResult(Query(query, -1).First<TLaunchPadObject>());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns the first result, or a default value if nothing is found, using the given query
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>The first <see cref="TLaunchPadObject"/> found, or a default value if nothing is found.</returns>
            public override TLaunchPadObject FirstOrDefault(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Query(query, -1).FirstOrDefault<TLaunchPadObject>();
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Returns the first result, or a default value if nothing is found, using the given query
            /// Runs asynchronously.
            /// </summary>
            /// <param name="query">A <see cref="IQueryable{T}"/> instance used to filter results to only those that satisfy the query.</param>
            /// <returns>The first <see cref="Task{TLaunchPadObject}"/> found, or a default value if nothing is found.</returns>
            public override Task<TLaunchPadObject> FirstOrDefaultAsync(IQueryable<TLaunchPadObject> query)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                var queryResult = Task.FromResult(Query(query, -1).FirstOrDefault<TLaunchPadObject>());
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = query.Expression });
                return queryResult;
            }

            /// <summary>
            /// Queries the repository returns a list of results, up to a maximum number. If max results is -1, 
            /// an unlimited number of results can be returned.
            /// </summary>
            /// <param name="maxResults">The maximum number of results to return from this query</param>
            /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
            /// of the query.</returns>
            public override IQueryable<TLaunchPadObject> Query(Int32 maxResults)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = Query(maxResults, 0).Expression });
                var queryResult = Query(maxResults, 0);
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = Query(maxResults, 0).Expression });
                return queryResult;
            }

            /// <summary>
            /// Queries the repository returns a list of results, starting at a specified index, 
            /// up to a maximum number. If max results is -1, an unlimited number of results can be returned.
            /// </summary>
            /// <param name="maxResults">The maximum number of results to return from this query</param>
            /// <param name="startResult">The index of the first result to start from, from the list of those returned.</param>
            /// <returns>A <see cref="IQueryable{TLaunchPadObject}"/> that can be used to further filter the results
            /// of the query.</returns>
            /// <exception>Throws ArgumentOutOfRangeException if maxResults is not -1, or a positive number, or if startResult is less than 0</exception>
            public override IQueryable<TLaunchPadObject> Query(Int32 maxResults, Int32 startResult)
            {
                ConstrainQueryInput(maxResults, startResult);
                // if maxResults is not -1 (infinite results) then we need to filter the number of results returned
                //
                // Best Practice: Whenever possible, constrain the number of results that a query can return. 
                // This prevents performance problems caused by unexpectedly fetching large numbers of results
                if (maxResults != -1)
                {
                    return _outerType._internal.AsQueryable().Take(maxResults);
                }
                return _outerType._internal.AsQueryable<TLaunchPadObject>();
            }

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
            public override IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = Query(query, maxResults, 0).Expression });
                var queryResult = Query(query, maxResults, 0);
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = Query(query, maxResults, 0).Expression });
                return queryResult;
            }

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
            /// <exception>Throws ArgumentOutOfRangeException if maxResults is not -1, or a positive number, or if startResult is less than 0</exception>
            public override IQueryable<TLaunchPadObject> Query(IQueryable<TLaunchPadObject> query, Int32 maxResults, Int32 startResult)
            {
                ConstrainQueryInput(query, maxResults, startResult);

                // if maxResults is not -1 (infinite results) then we need to filter the number of results returned
                //
                // Best Practice: Whenever possible, constrain the number of results that a query can return. 
                // This prevents performance problems caused by unexpectedly fetching large numbers of results
                if (maxResults != -1)
                {
                    return query.Skip(startResult).Take(maxResults);
                }
                return query.Skip(startResult);
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
            public override IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification, int maxResults)
            {
                OnQueryingDataObject(new RepositoryEventArgs() { QueryExpression = Query(specification, maxResults, 0).Expression });
                var query = Query(specification, maxResults, 0);
                OnQueriedDataObject(new RepositoryEventArgs() { QueryExpression = Query(specification, maxResults, 0).Expression });
                return query;
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
            /// <exception>Throws ArgumentOutOfRangeException if maxResults is not -1, or a positive number, or if startResult is less than 0</exception>
            public override IQueryable<TLaunchPadObject> Query(ISpecification<TLaunchPadObject> specification, int maxResults, int startResult)
            {
                ConstrainQueryInput(specification, maxResults, startResult);

                // if maxResults is not -1 (infinite results) then we need to filter the number of results returned
                //
                // Best Practice: Whenever possible, constrain the number of results that a query can return. 
                // This prevents performance problems caused by unexpectedly fetching large numbers of results
                if (maxResults != -1)
                {
                    return _outerType.Read.Query(specification).AsQueryable().Skip(startResult).Take(maxResults);
                }
                return _outerType.Read.Query(specification).AsQueryable().Skip(startResult);
            }

        }

        /// <summary>
        /// Internal class providing In Memory-specific Repository Write methods.
        /// </summary>
        public class InMemoryWriteMethods : WriteableRepositoryMethodsBase<TLaunchPadObject>
        {
            private InMemoryRepository<TLaunchPadObject> _outerType;

            /// <summary>  
            /// Initializes a new instance of <see cref="InMemoryWriteMethods" />,
            /// with a reference to the external type, <see cref="InMemoryRepository{TLaunchPadObject}" />.      
            /// </summary>
            /// <param name="outerType">A reference to the external class, <see cref="InMemoryRepository{TLaunchPadObject}" /></param>
            public InMemoryWriteMethods(InMemoryRepository<TLaunchPadObject> outerType)
            {
                _outerType = outerType;
            }

            /// <summary>
            /// Adds a new data object instance to the repository.
            /// </summary>
            /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be saved
            /// to the list.</param>
            /// <remarks>Implementors of this method must handle the Insert scenario.</remarks>
            public override TLaunchPadObject Add(TLaunchPadObject repositoryObject)
            {
                OnAddingDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
                _outerType._internal.Add(repositoryObject);
                OnAddedDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
                return repositoryObject;
            }

            /// <summary>
            /// Marks the changes of an existing data object to be saved to the repository.
            /// </summary>
            /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
            /// updated in the list.</param>
            public override TLaunchPadObject Save(TLaunchPadObject repositoryObject)
            {
                OnSavingDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
                var obj = _outerType._internal.FirstOrDefault(x => x.GlobalKey.Equals(repositoryObject.GlobalKey));
                if (obj != null) obj= repositoryObject;
                OnSavedDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
                return repositoryObject;
            }

            /// <summary>
            /// Marks the changes of an existing data object to be saved to the repository.
            /// </summary>
            /// <param name="repositoryObject">An instance of <typeparamref name="TLaunchPadObject"/> that should be
            /// updated in the list.</param>
            /// <remarks>Implementors of this method must handle the Delete scenario. </remarks>
            public override void Delete(TLaunchPadObject repositoryObject)
            {
                OnDeletingDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
                _outerType._internal.Remove(repositoryObject);
                OnDeletedDataObject(new RepositoryEventArgs() { DataObject = repositoryObject });
            }

            
        }

        #region "Implementation of IAttachableRepository<TLaunchPadObject>"

        /// <summary>
        /// Raised when an object is being attached to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> AttachingDataObject;

        /// <summary>
        /// Raised when an object has been attached to the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> AttachedDataObject;

        /// <summary>  
        /// Raises the <see cref="AttachingDataObject">AttachingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnAttachingDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            AttachingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="AttachedDataObject">AttachedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnAttachedDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            AttachedDataObject?.Invoke(this, e);
        }

        /// <summary>
        /// Attaches a detached data object, previously detached via the <see cref="Detach"/> method.
        /// </summary>
        /// <param name="repositoryObject">The data object instance to attach back to the repository.</param>
        public void Attach(TLaunchPadObject repositoryObject)
        {
            return;
        }

        /// <summary>
        /// Raised when an object is being detached from the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> DetachingDataObject;

        /// <summary>
        /// Raised when an object has been detached from the repository.
        /// </summary>
        public event EventHandler<RepositoryEventArgs> DetachedDataObject;

        /// <summary>  
        /// Raises the <see cref="DetachingDataObject">DetachingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnDetachingDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            DetachingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="DetachedDataObject">DetachedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnDetachedDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            DetachedDataObject?.Invoke(this, e);
        }

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="repositoryObject">The data object instance, currently being tracked via the repository, to detach.</param>
        public void Detach(TLaunchPadObject repositoryObject)
        {
            return;
        }

        #endregion

        #region "Implementation of IRefreshableRepository<TLaunchPadObject>"

        /// <summary>
        /// Raised when the object is being refreshed from the repository
        /// </summary>
        public event EventHandler<RepositoryEventArgs> RefreshingDataObject;

        /// <summary>
        /// Raised when the object has been refreshed from the repository
        /// </summary>
        public event EventHandler<RepositoryEventArgs> RefreshedDataObject;

        /// <summary>  
        /// Raises the <see cref="RefreshingDataObject">RefreshingDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnRefreshingDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            RefreshingDataObject?.Invoke(this, e);
        }

        /// <summary>  
        /// Raises the <see cref="RefreshedDataObject">RefreshedDataObject</see> event.  
        /// </summary>  
        /// <param name="e">A <see cref="RepositoryEventArgs">RepositoryEventArgs</see> object that contains the event data.</param>  
        protected virtual void OnRefreshedDataObject(RepositoryEventArgs e)
        {
            // Best Practice: check for the handler being null before raising it
            // to prevent race conditions
            RefreshedDataObject?.Invoke(this, e);
        }

        /// <summary>
        /// Refreshes an object instance from the repository.
        /// </summary>
        /// <param name="repositoryObject">The object to refresh.</param>
        public void Refresh(TLaunchPadObject repositoryObject)
        {
            return;
        }

        #endregion
    }
}