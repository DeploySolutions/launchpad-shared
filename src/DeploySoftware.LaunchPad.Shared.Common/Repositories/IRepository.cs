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
    using DeploySoftware.LaunchPad.Common.Util;
    using System.Linq;

    /// <summary>
    /// The <see cref="IRepository{TLaunchPadObject}"/> interface defines a base contract that repository
    /// components should implement.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository encapsulates.</typeparam>
    public interface IRepository<TLaunchPadObject> : IService        
         where TLaunchPadObject : ILaunchPadObject, new()
    {

        /// <summary>
        /// Defines the service context under which the repository will execute.
        /// </summary>
        /// <typeparam name="TService">The service type that defines the context of the repository.</typeparam>
        /// <returns>The same <see cref="IRepository{TLaunchPadObject}"/> instance.</returns>
        /// <remarks>
        /// Implementors should perform context specific actions within this method call and return
        /// the exact same instance.
        /// </remarks>
        IQueryable<TLaunchPadObject> For<TService>() where TService : IReadableRepository<TLaunchPadObject>;
    }
}