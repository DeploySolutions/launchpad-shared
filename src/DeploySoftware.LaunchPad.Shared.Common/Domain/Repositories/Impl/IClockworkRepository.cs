//Clockwork Web Integration Platform
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

namespace DeploySolutions.Clockwork.Common.Domain.Repositories
{
    using DeploySolutions.Clockwork.Common.Domain.Entities;
    using DeploySolutions.Clockwork.Common.Util;
    using System.Linq;
    using Abp.Domain.Repositories;

    /// <summary>
    /// The <see cref="IRepository{TClockworkObject}"/> interface defines a base contract that all Clockwork repositories
    /// should implement.
    /// </summary>
    /// <typeparam name="TClockworkObject">The type of Clockwork object that the repository manages.</typeparam>
    /// <typeparam name="TKey">The type of Key that the entity uses.</typeparam>
    public interface IClockworkRepository<TClockworkObject, TKey> : IService, Abp.Domain.Repositories.IRepository<TClockworkObject,TIKey>
            where TClockworkObject : IClockworkObject, new()
            where TKey : IKey<object>
    {

        /// <summary>
        /// Defines the service context under which the repository will execute.
        /// </summary>
        /// <typeparam name="TService">The service type that defines the context of the repository.</typeparam>
        /// <returns>The same <see cref="IRepository{TClockworkObject}"/> instance.</returns>
        /// <remarks>
        /// Implementors should perform context specific actions within this method call and return
        /// the exact same instance.
        /// </remarks>
        IQueryable<TClockworkObject> For<TService>() where TService : IReadableRepository<TClockworkObject>;
    }
}