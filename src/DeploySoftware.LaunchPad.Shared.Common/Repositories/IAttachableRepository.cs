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

    /// <summary>
    /// An attachable repository is one that supports attaching or detaching
    /// objects from its direct control, for manipulation elsewhere.
    /// </summary>
    /// <typeparam name="TLaunchPadObject">The type of object that the Repository manipulates</typeparam>
    public interface IAttachableRepository<TLaunchPadObject> : IRepository<TLaunchPadObject>
        where TLaunchPadObject : ILaunchPadObject, new()
    {
        /// <summary>
        /// Raised when an object is being attached to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> AttachingDataObject;

        /// <summary>
        /// Raised when an object has been attached to the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> AttachedDataObject;

        /// <summary>
        /// Raised when an object is being detached from the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> DetachingDataObject;

        /// <summary>
        /// Raised when an object has been detached from the repository.
        /// </summary>
        event EventHandler<RepositoryEventArgs> DetachedDataObject;

        /// <summary>
        /// Attaches a detached data object, previously detached via the <see cref="Detach"/> method.
        /// </summary>
        /// <param name="repositoryObject">The data object instance to attach back to the repository.</param>
        void Attach(TLaunchPadObject repositoryObject);

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="repositoryObject">The data object instance, currently being tracked via the repository, to detach.</param>
        void Detach(TLaunchPadObject repositoryObject);

    }
}
