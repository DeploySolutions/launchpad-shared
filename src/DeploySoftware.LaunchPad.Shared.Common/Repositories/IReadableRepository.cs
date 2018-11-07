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
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the functionality for any repository whose data can be read or queried.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public interface IReadableRepository<TLaunchPadObject> : IRepository<TLaunchPadObject>, IQueryable<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {
        /// <summary>
        /// A <see cref="ReadableRepositoryMethodsBase{TLaunchPadObject}">ReadableRepositoryMethodsBase{TLaunchPadObject}</see> containing of
        /// methods used to read or query a repository's collection of data
        /// </summary>
        ReadableRepositoryMethodsBase<TLaunchPadObject> Read { get; set; }

    }
}
