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

    ///<summary>
    /// A base class for implementors of <see cref="IRepository{TLaunchPadObject}"/>.
    /// This repository type only assumes it can read, but not perform other more advanced functionality such as attaching/detaching, refreshing, or adding/deleting.    
    ///</summary>
    ///<typeparam name="TLaunchPadObject"></typeparam>
    public abstract class ReadableRepositoryBase<TLaunchPadObject> : IReadableRepository<TLaunchPadObject>
         where TLaunchPadObject : ILaunchPadObject, new()
    {

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{TLaunchPadObject}" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<TLaunchPadObject> GetEnumerator()
        {
            return Read.GetEnumerator();
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
            return Read.GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="IQueryable" />.
        /// </summary>
        /// <returns>
        /// The <see cref="Expression" /> that is associated with this instance of <see cref="IQueryable" />.
        /// </returns>
        public Expression Expression
        {
            get { return Read.Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="IQueryable" /> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { return Read.ElementType; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryProvider" /> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider
        {
            get { return Read.Provider; }
        }

        #endregion

        /// <summary>
        /// A <see cref="ReadableRepositoryMethodsBase{TLaunchPadObject}">ReadableRepositoryMethodsBase{TLaunchPadObject}</see> containing of
        /// methods used to read or query a repository's collection of data
        /// </summary>
        public abstract ReadableRepositoryMethodsBase<TLaunchPadObject> Read
        { get; set; }        

        /// <summary>
        /// Defines the service context under which the repository will execute.
        /// </summary>
        /// <typeparam name="TService">The service type that defines the context of the repository.</typeparam>
        /// <returns>The same <see cref="IRepository{TLaunchPadObject}"/> instance.</returns>
        /// <remarks>
        /// Implementors should perform context specific actions within this method call and return
        /// the exact same instance.
        /// </remarks>
        public virtual IQueryable<TLaunchPadObject> For<TService>()
            where TService : IReadableRepository<TLaunchPadObject>
        {
            //var strategy = ServiceLocator
            //    .Current
            //    .GetAllInstances<IFetchingStrategy<TLaunchPadObject, TService>>()
            //    .FirstOrDefault();
            // TODO: Figure out how to load Fetching Strategy from DI
            IFetchingStrategy<TLaunchPadObject, TService> strategy = null;
            if (strategy != null)
                return strategy.Define(this);
            return this;
        }
    }
}
