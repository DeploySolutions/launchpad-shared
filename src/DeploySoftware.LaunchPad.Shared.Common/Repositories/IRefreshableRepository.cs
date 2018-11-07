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
    using System;

    /// <summary>
    /// An refreshable repository is one that supports refreshing an
    /// object to ensure that its state is not stale from its data source.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public interface IRefreshableRepository<TLaunchPadObject> : IRepository<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {
        /// <summary>
        /// Raised when the object is being refreshed from the repository
        /// </summary>
        event EventHandler<RepositoryEventArgs> RefreshingDataObject;

        /// <summary>
        /// Raised when the object has been refreshed from the repository
        /// </summary>
        event EventHandler<RepositoryEventArgs> RefreshedDataObject;

        /// <summary>
        /// Refreshes an object instance from the repository.
        /// </summary>
        /// <param name="repositoryObject">The object to refresh.</param>
        void Refresh(TLaunchPadObject repositoryObject);

    }
}
